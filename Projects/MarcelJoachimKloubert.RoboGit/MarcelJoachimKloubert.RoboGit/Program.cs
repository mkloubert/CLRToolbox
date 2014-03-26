// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using LibGit2Sharp;
using MarcelJoachimKloubert.CLRToolbox.Configuration.Impl;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarcelJoachimKloubert.RoboGit
{
    internal static class Program
    {
        #region Fields (3)

        private const string _METHOD_PULL = "PULL";
        private const string _METHOD_PUSH = "PUSH";
        private static object _SYNC_CONSOLE = new object();

        #endregion Fields

        #region Methods (6)

        // Private Methods (6) 

        private static double CalcPercentage(int current, int total)
        {
            if (total == 0)
            {
                return 0;
            }

            return (float)current / (float)total * 100.0f;
        }

        private static IConsole InvokeForConsoleColor(this IConsole console,
                                                      Action<IConsole> action,
                                                      ConsoleColor? foreColor = null, ConsoleColor? bgColor = null)
        {
            return InvokeForConsoleColor<object>(console,
                                                 (c, state) => action(c),
                                                 actionState: null,
                                                 foreColor: foreColor, bgColor: bgColor);
        }

        private static IConsole InvokeForConsoleColor<T>(this IConsole console,
                                                         Action<IConsole, T> action,
                                                         T actionState,
                                                         ConsoleColor? foreColor = null, ConsoleColor? bgColor = null)
        {
            lock (_SYNC_CONSOLE)
            {
                var oldForeColor = console.ForegroundColor;
                var oldBgColor = console.BackgroundColor;

                try
                {
                    if (foreColor.HasValue)
                    {
                        console.ForegroundColor = foreColor.Value;
                    }

                    if (bgColor.HasValue)
                    {
                        console.BackgroundColor = bgColor.Value;
                    }

                    action(console, actionState);
                }
                finally
                {
                    console.ForegroundColor = oldForeColor;
                    console.BackgroundColor = oldBgColor;
                }
            }

            return console;
        }

        private static void Main(string[] args)
        {
            try
            {
                var config = new IniFileConfigRepository(@"./config.ini");

                foreach (var taskName in config.GetCategoryNames())
                {


                    bool isActive;
                    config.TryGetValue<bool>(category: taskName,
                                             name: "is_active",
                                             value: out isActive,
                                             defaultVal: true);

                    if (isActive == false)
                    {
                        continue;
                    }

                    string name;
                    config.TryGetValue<string>(category: taskName,
                                               name: "name",
                                               value: out name);

                    string username;
                    config.TryGetValue<string>(category: taskName,
                                               name: "username",
                                               value: out username);

                    string email;
                    config.TryGetValue<string>(category: taskName,
                                               name: "email",
                                               value: out email);

                    string pwd;
                    config.TryGetValue<string>(category: taskName,
                                               name: "password",
                                               value: out pwd);

                    bool useCredentials;
                    config.TryGetValue<bool>(category: taskName,
                                             name: "use_credentials",
                                             value: out useCredentials,
                                             defaultVal: false);

                    GlobalConsole.Current
                                 .InvokeForConsoleColor((c, s) => c.WriteLine("[TASK] '{0}'...",
                                                                              s.TaskName.Trim()),
                                                        new
                                                        {
                                                            TaskName = string.IsNullOrWhiteSpace(name) ? taskName : name,
                                                        }, foreColor: ConsoleColor.White);

                    try
                    {
                        string method;
                        config.TryGetValue<string>(category: taskName,
                                                   name: "method",
                                                   value: out method);

                        if (string.IsNullOrWhiteSpace(method))
                        {
                            method = _METHOD_PUSH;
                        }

                        method = method.ToUpper().Trim();
                        switch (method)
                        {
                            case _METHOD_PULL:
                            case _METHOD_PUSH:
                                break;

                            default:
                                // invalid
                                GlobalConsole.Current
                                             .InvokeForConsoleColor((c, s) => c.WriteLine("  [UNKNOWN] Method '{0}'!",
                                                                                          s.Method),
                                                                    new
                                                                    {
                                                                        Method = method,
                                                                    }, foreColor: ConsoleColor.Yellow);
                                continue;
                        }

                        var sourceDir = new DirectoryInfo(config.GetValue<string>(category: taskName,
                                                                                  name: "source"));

                        if (sourceDir.Exists == false)
                        {
                            GlobalConsole.Current
                                         .InvokeForConsoleColor((c, s) => c.WriteLine("  [NOT FOUND] Repository at '{0}'!",
                                                                                      s.SourceDirectory.FullName),
                                                                new
                                                                {
                                                                    SourceDirectory = sourceDir,
                                                                }, foreColor: ConsoleColor.Yellow);

                            continue;
                        }

                        var repoOpts = new RepositoryOptions();
                        repoOpts.WorkingDirectoryPath = sourceDir.FullName;

                        GlobalConsole.Current
                                     .InvokeForConsoleColor((c, s) => c.Write("  [OPEN] Repository at '{0}'... ",
                                                                              s.SourceDirectory.FullName),
                                                            new
                                                            {
                                                                SourceDirectory = sourceDir,
                                                            });

                        using (var repo = new Repository(sourceDir.FullName, repoOpts))
                        {
                            GlobalConsole.Current
                                         .InvokeForConsoleColor((c) => c.WriteLine("[OK]"),
                                                                foreColor: ConsoleColor.Green);

                            string remotes;
                            config.TryGetValue<string>(category: taskName,
                                                       name: "remotes",
                                                       value: out remotes);

                            var remoteList = new HashSet<string>();
                            remoteList.AddRange((remotes ?? string.Empty).Split('\n')
                                                                         .Select(x => x.ToUpper().Trim()));

                            foreach (var remoteName in remoteList)
                            {
                                try
                                {
                                    var filteredRepoRemotes = repo.Network.Remotes.Where(x => remoteList.Where(y => y != string.Empty).LongCount() < 1 ||
                                                                                              remoteName ==
                                                                                              (x.Name ?? string.Empty).ToUpper().Trim())
                                                                                  .ToArray();

                                    if (filteredRepoRemotes.Length > 0)
                                    {
                                        try
                                        {
                                            Action<Repository, IEnumerable<Remote>, bool, string, string, string> action = null;

                                            switch (method)
                                            {
                                                case _METHOD_PULL:
                                                    action = Pull;
                                                    break;

                                                case _METHOD_PUSH:
                                                    action = Push;
                                                    break;
                                            }

                                            if (action != null)
                                            {
                                                action(repo, filteredRepoRemotes,
                                                       useCredentials,
                                                       username, email, pwd);
                                            }

                                            GlobalConsole.Current
                                                         .InvokeForConsoleColor((c) => c.WriteLine("[OK]"),
                                                                                foreColor: ConsoleColor.Green);
                                        }
                                        catch (Exception ex)
                                        {
                                            GlobalConsole.Current
                                                         .InvokeForConsoleColor((c, s) => c.WriteLine(s.Exception),
                                                                                new
                                                                                {
                                                                                    Exception = ex.GetBaseException() ?? ex,
                                                                                }, foreColor: ConsoleColor.Red);
                                        }
                                        finally
                                        {
                                            GlobalConsole.Current.WriteLine();
                                        }
                                    }
                                    else
                                    {
                                        GlobalConsole.Current
                                                     .InvokeForConsoleColor((c) => c.WriteLine("[NOT FOUND] Remote configuration!"),
                                                                            foreColor: ConsoleColor.Yellow);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    GlobalConsole.Current
                                                 .InvokeForConsoleColor((c, s) => c.WriteLine(s.Exception),
                                                                        new
                                                                        {
                                                                            Exception = ex.GetBaseException() ?? ex,
                                                                        }, foreColor: ConsoleColor.Red);
                                }
                            }

                            GlobalConsole.Current
                                         .InvokeForConsoleColor((c, s) => c.Write("  [CLOSE] Repository at '{0}'... ",
                                                                                  s.SourceDirectory.FullName),
                                                            new
                                                            {
                                                                SourceDirectory = sourceDir,
                                                            });
                        }

                        GlobalConsole.Current
                                     .InvokeForConsoleColor((c) => c.WriteLine("[OK]"),
                                                            foreColor: ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        GlobalConsole.Current
                                     .InvokeForConsoleColor((c, s) => c.WriteLine(s.Exception),
                                                            new
                                                            {
                                                                Exception = ex.GetBaseException() ?? ex,
                                                            }, foreColor: ConsoleColor.Red);
                    }
                    finally
                    {
                        GlobalConsole.Current.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalConsole.Current
                             .InvokeForConsoleColor((c, s) => c.WriteLine(s.Exception),
                                                    new
                                                    {
                                                        Exception = ex.GetBaseException() ?? ex,
                                                    }, foreColor: ConsoleColor.Yellow
                                                     , bgColor: ConsoleColor.Red);
            }

            GlobalConsole.Current
                         .WriteLine().WriteLine()
                         .WriteLine("===== ENTER =====")
                         .ReadLine();
        }

        private static void Pull(Repository repo, IEnumerable<Remote> remotes,
                                 bool useCredentials, string username, string email,
                                 string pwd)
        {
            foreach (var rem in remotes)
            {
                GlobalConsole.Current
                             .InvokeForConsoleColor((c, s) => c.Write("    [PULL] From remote location '{0}' ({1})... ",
                                                                      s.Name,
                                                                      s.Url),
                                                    new
                                                    {
                                                        Name = rem.Name,
                                                        Url = rem.Url,
                                                    });

                var fetchOpts = new FetchOptions();

                if (useCredentials)
                {
                    fetchOpts.Credentials = new Credentials()
                    {
                        Username = username,
                        Password = pwd,
                    };
                }

                repo.Fetch(rem.Name, fetchOpts);

                GlobalConsole.Current
                             .InvokeForConsoleColor((c) => c.WriteLine("[OK]"),
                                                    foreColor: ConsoleColor.Green);

                // merge with remote branches
                foreach (var b in repo.Branches.Where(x => x.IsRemote))
                {
                    try
                    {
                        GlobalConsole.Current
                                     .InvokeForConsoleColor((c, s) => c.Write("    [MERGE] Branch '{0}'... ",
                                                                              s.Branch.Name),
                                                            new
                                                            {
                                                                Branch = b,
                                                            });

                        repo.Merge(b.Tip, new Signature(username, email,
                                                        DateTimeOffset.Now));

                        GlobalConsole.Current
                                     .InvokeForConsoleColor((c) => c.WriteLine("[OK]"),
                                                            foreColor: ConsoleColor.Green);
                    }
                    catch (Exception ex)
                    {
                        GlobalConsole.Current
                                     .InvokeForConsoleColor((c, s) => c.WriteLine(s.Exception),
                                                            new
                                                            {
                                                                Exception = ex.GetBaseException() ?? ex,
                                                            }, foreColor: ConsoleColor.Red);
                    }
                }
            }
        }

        private static void Push(Repository repo, IEnumerable<Remote> remotes,
                                 bool useCredentials, string username, string email,
                                 string pwd)
        {
            var branches = repo.Branches
                               .Where(b => b.Remote != null &&
                                           remotes.Any(r => r.Name == b.Remote.Name));

            foreach (var b in branches)
            {
                try
                {
                    GlobalConsole.Current
                                 .InvokeForConsoleColor((c, s) => c.Write("    [PUSH] Branch '{0}' to remote '{1}'... ",
                                                                          s.Branch.Name,
                                                                          s.Remote.Name),
                                                        new
                                                        {
                                                            Branch = b,
                                                            Remote = b.Remote,
                                                        });


                    var pushOpts = new PushOptions();

                    if (useCredentials)
                    {
                        pushOpts.Credentials = new Credentials()
                        {
                            Username = username,
                            Password = pwd,
                        };
                    }

                    repo.Network.Push(b.Remote, "HEAD", b.CanonicalName, pushOpts);

                    GlobalConsole.Current
                                 .InvokeForConsoleColor((c) => c.WriteLine("[OK]"),
                                                        foreColor: ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    GlobalConsole.Current
                                 .InvokeForConsoleColor((c, s) => c.WriteLine(s.Exception),
                                                        new
                                                        {
                                                            Exception = ex.GetBaseException() ?? ex,
                                                        }, foreColor: ConsoleColor.Red);
                }
            }
        }

        #endregion Methods
    }
}
