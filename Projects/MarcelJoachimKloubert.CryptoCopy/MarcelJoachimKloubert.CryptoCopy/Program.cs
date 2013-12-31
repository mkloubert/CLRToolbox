// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.IO;
using MarcelJoachimKloubert.CryptoCopy.Helpers;

namespace MarcelJoachimKloubert.CryptoCopy
{
    internal static class Program
    {
        #region Fields (3)

        private const int _EXIT_CODE_EXCEPTION = -1;
        private const int _EXIT_CODE_INVALID_ARGS = 2;
        private const int _EXIT_CODE_OK = 0;

        #endregion Fields

        #region Methods (7)

        // Private Methods (7) 

        private static void EncryptStream(Stream input,
                                          Stream output,
                                          byte[] pwd, byte[] salt)
        {
            var pdb = new PasswordDeriveBytes(pwd, salt);

            var alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            var cryptoStream = new CryptoStream(output,
                                                alg.CreateEncryptor(),
                                                CryptoStreamMode.Write);
            {
                input.CopyTo(cryptoStream);

                cryptoStream.Flush();
                cryptoStream.Close();
            }
        }

        private static void ExtractPasswordAndSalt(IEnumerable<string> args,
                                                   out byte[] pwd, out byte[] salt)
        {
            pwd = null;
            salt = Globals.DEFAULT_SEED;

            foreach (var a in args)
            {
                if (a.ToLower().StartsWith("/pwd:"))
                {
                    pwd = Encoding.UTF8.GetBytes(a.Substring(a.IndexOf(':') + 1));
                }
            }
        }

        private static int Main(string[] args)
        {
            var sync = new object();

            try
            {
                var result = _EXIT_CODE_INVALID_ARGS;
                var ctx = new CryptExecutionContext(sync)
                    {
                        Arguments = new SynchronizedCollection<string>(syncRoot: new object(),
                                                                       list: args),
                        InitialAction = ShowHelpScreenForInvalidArguments,
                        ExitCode = result,
                    };

                if (args.Length > 0)
                {
                    switch (args[0].ToLower().Trim())
                    {
                        case "/?":
                        case "/help":
                            result = _EXIT_CODE_OK;
                            break;

                        case "/e":
                        case "/encrypt":
                            // crypt
                            {
                                IEnumerable<string> nextArgs;
                                DirectoryInfo srcDir;
                                DirectoryInfo destDir;
                                if (args.Length < 3)
                                {
                                    srcDir = new DirectoryInfo(Environment.CurrentDirectory);
                                    destDir = new DirectoryInfo(args[1]);

                                    nextArgs = args.Skip(2);
                                }
                                else
                                {
                                    srcDir = new DirectoryInfo(args[1]);
                                    destDir = new DirectoryInfo(args[2]);

                                    nextArgs = args.Skip(3);
                                }

                                byte[] pwd;
                                byte[] salt;
                                ExtractPasswordAndSalt(nextArgs,
                                                       pwd: out pwd, salt: out salt);

                                if (pwd != null)
                                {
                                    ctx = new CryptExecutionContext(sync)
                                        {
                                            Arguments = new SynchronizedCollection<string>(syncRoot: sync,
                                                                                           list: nextArgs),
                                            Destination = destDir,
                                            InitialAction = StartEncryption,
                                            Mode = RunningMode.Encrypt,
                                            Password = pwd,
                                            Salt = salt,
                                            Source = srcDir,
                                        };
                                }
                            }
                            break;

                        case "/d":
                        case "/decrypt":
                            {
                                IEnumerable<string> nextArgs;
                                DirectoryInfo srcDir;
                                DirectoryInfo destDir;
                                if (args.Length < 3)
                                {
                                    srcDir = new DirectoryInfo(Environment.CurrentDirectory);
                                    destDir = new DirectoryInfo(args[1]);

                                    nextArgs = args.Skip(2);
                                }
                                else
                                {
                                    srcDir = new DirectoryInfo(args[1]);
                                    destDir = new DirectoryInfo(args[2]);

                                    nextArgs = args.Skip(3);
                                }

                                byte[] pwd;
                                byte[] salt;
                                ExtractPasswordAndSalt(nextArgs,
                                                       pwd: out pwd, salt: out salt);

                                if (pwd != null)
                                {
                                    ctx = new CryptExecutionContext(sync)
                                        {
                                            Arguments = new SynchronizedCollection<string>(syncRoot: sync,
                                                                                           list: nextArgs),
                                            Destination = destDir,
                                            InitialAction = StartDecryption,
                                            Mode = RunningMode.Decrypt,
                                            Password = pwd,
                                            Salt = salt,
                                            Source = srcDir,
                                        };
                                }
                            }
                            break;
                    }
                }

                if (ctx != null)
                {
                    ctx.InvokeInitalAction();

                    if (ctx.ExitCode.HasValue)
                    {
                        result = ctx.ExitCode.Value;
                    }
                }

                return result;
            }
            catch
            {
                return _EXIT_CODE_EXCEPTION;
            }
            finally
            {
#if DEBUG
                GlobalConsole.Current.WriteLine();
                GlobalConsole.Current.WriteLine();
                GlobalConsole.Current.WriteLine("===== ENTER ====");
                GlobalConsole.Current.ReadLine();
#endif
            }
        }

        private static void ShowHelpScreen()
        {
            var title = string.Format("CryptoCopy v{0}",
                                      Assembly.GetExecutingAssembly().GetName().Version);

            GlobalConsole.Current.WriteLine(title);
            for (var i = 0; i < title.Length + 5; i++)
            {
                GlobalConsole.Current.Write('=');
            }

            GlobalConsole.Current.WriteLine();
            GlobalConsole.Current.WriteLine();
        }

        private static void ShowHelpScreenForInvalidArguments(CryptExecutionContext ctx)
        {
            ShowHelpScreen();
            ctx.ExitCode = _EXIT_CODE_INVALID_ARGS;
        }

        private static void StartDecryption(CryptExecutionContext ctx)
        {
            var tasks = new List<Task>();
            tasks.Add(DecryptionHelper.CreateDecryptionTask(ctx));

            // ... and start them
            foreach (var t in tasks)
            {
                t.Start();
            }

            TaskHelper.WaitAll(tasks);
        }

        private static void StartEncryption(CryptExecutionContext ctx)
        {
            var tasks = new List<Task>();
            tasks.Add(EncryptionHelper.CreateEncryptionTask(ctx));

            // ... and start them
            foreach (var t in tasks)
            {
                t.Start();
            }

            TaskHelper.WaitAll(tasks);
        }

        #endregion Methods
    }
}
