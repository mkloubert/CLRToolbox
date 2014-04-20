// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MarcelJoachimKloubert.FileCompare
{
    internal static class Program
    {
        #region Methods (5)

        // Private Methods (5) 

        private static void CompareDirectories(DirectoryInfo src, DirectoryInfo dest, CompareContext ctx)
        {
            try
            {
                ctx.Logger.WriteLine();
                ctx.Logger.WriteLine();

                ctx.Logger.WriteLine("Enter directory '{0}'...", src.FullName);

                dest.Refresh();
                if (dest.Exists == false)
                {
                    ctx.Logger.WriteLine("[DESTINATION DIRECTORY NOT FOUND]");
                    return;
                }

                var subDirsSrc = src.GetDirectories()
                                    .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase);

                // compare directories
                foreach (var sd in subDirsSrc)
                {
                    try
                    {
                        ctx.Logger.Write("\t[DIR] '{0}'", sd.Name);
                        ++ctx.ProcessedDirs;

                        var differences = 0;
                        var logDifference = true;

                        var destDir = new DirectoryInfo(Path.Combine(dest.FullName,
                                                                     sd.Name));
                        if (destDir.Exists)
                        {
                            if (sd.Attributes != destDir.Attributes)
                            {
                                ++differences;

                                ctx.Logger.WriteLine();
                                ctx.Logger.Write("\t\t{{Unique attributes: '{0}'/'{1}'}}",
                                                 FileAttribsListToString(sd.Attributes),
                                                 FileAttribsListToString(destDir.Attributes));
                            }
                        }
                        else
                        {
                            ++differences;
                            logDifference = false;

                            ++ctx.MissingDirs;
                            ctx.MISSING_DIR_LIST.Add(sd.FullName);

                            ctx.Logger.WriteLine();
                            ctx.Logger.Write("\t\t{Source only}");
                        }

                        if (differences > 0)
                        {
                            ++ctx.DifferentDirs;

                            if (logDifference)
                            {
                                ctx.DIFFERENT_DIR_LIST.Add(destDir.FullName);
                            }
                        }
                        else
                        {
                            ++ctx.SameDirs;

                            ctx.Logger.WriteLine();
                            ctx.Logger.Write("\t\t{Same}");
                        }
                    }
                    catch (Exception ex)
                    {
                        ++ctx.Errors;

                        ctx.LogException(ex);
                    }
                    finally
                    {
                        ctx.Logger.WriteLine();
                    }
                }

                // extra directories
                foreach (var sd in dest.GetDirectories()
                                       .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        var srcDir = new DirectoryInfo(Path.Combine(src.FullName,
                                                                    sd.Name));

                        if (srcDir.Exists == false)
                        {
                            ++ctx.DifferentDirs;
                            ++ctx.ExtraDirs;
                            ctx.EXTRA_DIR_LIST.Add(sd.FullName);

                            ctx.Logger.WriteLine("\t[EXTRA DIR] '{0}'",
                                                 sd.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        ++ctx.Errors;

                        ctx.LogException(ex);
                    }
                }

                foreach (var f in src.GetFiles()
                                     .OrderBy(f => f.Name, StringComparer.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        ctx.Logger.Write("\t[FILE] '{0}'", f.Name);
                        ++ctx.ProcessedFiles;

                        var destFile = new FileInfo(Path.Combine(dest.FullName,
                                                                 f.Name));

                        if (destFile.Exists)
                        {
                            var differences = 0;
                            var doHash = true;
                            var logDifference = true;

                            if (f.Attributes != destFile.Attributes)
                            {
                                ++differences;

                                ctx.Logger.WriteLine();
                                ctx.Logger.Write("\t\t{{Unique attributes: '{0}'/'{1}'}}",
                                                 FileAttribsListToString(f.Attributes),
                                                 FileAttribsListToString(destFile.Attributes));
                            }

                            if (NormalizeDateTime(f.LastWriteTimeUtc) != NormalizeDateTime(destFile.LastWriteTimeUtc))
                            {
                                ++differences;
                                doHash = true;

                                ctx.Logger.WriteLine();
                                ctx.Logger.Write("\t\t{{Unique UTC timestamps: '{0}'/'{1}'}}",
                                                 NormalizeDateTime(f.LastWriteTimeUtc).ToString("{yyyy-MM-dd HH:mm:ss.fffff}"),
                                                 NormalizeDateTime(destFile.LastWriteTimeUtc).ToString("{yyyy-MM-dd HH:mm:ss.fffff}"));
                            }

                            if (f.Length != destFile.Length)
                            {
                                ++differences;
                                doHash = false;

                                ctx.Logger.WriteLine();
                                ctx.Logger.Write("\t\t{{Unique sizes: '{0}'/'{1}'}}",
                                                     f.Length,
                                                     destFile.Length);
                            }

                            if (doHash)
                            {
                                if (ctx.Hash)
                                {
                                    ctx.Logger.WriteLine();
                                    ctx.Logger.Write("\t\t{Hashing");

                                    // hash SOURCE
                                    byte[] hashSrc;
                                    using (var srcStream = f.OpenRead())
                                    {
                                        using (var md5 = new MD5CryptoServiceProvider())
                                        {
                                            ctx.Logger.Write(" Source");

                                            hashSrc = md5.ComputeHash(srcStream);
                                        }
                                    }

                                    // HASH destination
                                    ctx.Logger.Write(" Destination");
                                    byte[] hashDest;
                                    using (var destStream = destFile.OpenRead())
                                    {
                                        using (var md5 = new MD5CryptoServiceProvider())
                                        {
                                            hashDest = md5.ComputeHash(destStream);
                                        }
                                    }

                                    if (hashSrc.SequenceEqual(hashDest) == false)
                                    {
                                        ++differences;

                                        ctx.Logger.Write("\tUnique: '{0}'/'{1}'}}",
                                                         hashSrc.AsHexString(),
                                                         hashDest.AsHexString());
                                    }
                                    else
                                    {
                                        ctx.Logger.Write("\tSame");
                                    }

                                    ctx.Logger.Write("}");
                                }
                            }

                            if (differences < 1)
                            {
                                ++ctx.SameFiles;

                                ctx.Logger.WriteLine();
                                ctx.Logger.Write("\t\t{Same}");
                            }
                            else
                            {
                                ++ctx.DifferentFiles;

                                if (logDifference)
                                {
                                    ctx.DIFFERENT_FILE_LIST.Add(destFile.FullName);
                                }
                            }
                        }
                        else
                        {
                            ++ctx.MissingFiles;
                            ctx.MISSING_FILE_LIST.Add(f.FullName);

                            ctx.Logger.WriteLine();
                            ctx.Logger.Write("\t\t{Source only}");
                        }
                    }
                    catch (Exception ex)
                    {
                        ++ctx.Errors;

                        ctx.LogException(ex);
                    }
                    finally
                    {
                        ctx.Logger.WriteLine();
                    }
                }

                // extra files
                foreach (var f in dest.GetFiles()
                                      .OrderBy(f => f.Name, StringComparer.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        var srcDir = new FileInfo(Path.Combine(src.FullName,
                                                               f.Name));

                        if (srcDir.Exists == false)
                        {
                            ++ctx.ExtraFiles;
                            ++ctx.DifferentFiles;
                            ctx.EXTRA_FILE_LIST.Add(f.FullName);

                            ctx.Logger.WriteLine("\t[EXTRA FILE] '{0}'",
                                                 f.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        ++ctx.Errors;

                        ctx.LogException(ex);
                    }
                }

                if (ctx.Recursive)
                {
                    foreach (var sd in src.GetDirectories())
                    {
                        CompareDirectories(sd,
                                           new DirectoryInfo(Path.Combine(dest.FullName, sd.Name)),
                                           ctx);
                    }
                }
            }
            catch (Exception ex)
            {
                ++ctx.Errors;

                ctx.LogException(ex);
            }
        }

        private static string FileAttribsListToString(FileAttributes attribs)
        {
            var list = new HashSet<FileAttributes>();
            foreach (FileAttributes value in Enum.GetValues(typeof(FileAttributes))
                                                 .Cast<FileAttributes>()
                                                 .OrderBy(fa => fa.ToString(), StringComparer.InvariantCultureIgnoreCase))
            {
                if (attribs.HasFlag(value))
                {
                    list.Add(value);
                }
            }

            return string.Join("|", list);
        }

        private static int Main(string[] args)
        {
            var now = DateTimeOffset.Now;

            try
            {
                if (args.Length < 2)
                {
                    ShowHelpScreen();
                    return 2;
                }

                var src = new DirectoryInfo(args[0]);
                if (src.Exists == false)
                {
                    return 3;
                }

                var dest = new DirectoryInfo(args[1]);
                if (dest.Exists == false)
                {
                    return 3;
                }

                var currentDir = new DirectoryInfo(Environment.CurrentDirectory);

                var logPrefix = string.Format("log_{0}",
                                              now.ToString("yyyyMMdd_HHmmss_fffff"));

                var logFile = new FileInfo(Path.Combine(currentDir.FullName,
                                                        string.Format("{0}.txt", logPrefix)));

                var ctx = new CompareContext();
                using (var loggerStream = logFile.OpenWrite())
                {
                    using (var loggerWriter = new StreamWriter(loggerStream, Encoding.UTF8))
                    {
                        ctx.Logger = new Logger(loggerWriter);

                        for (var i = 2; i < args.Length; i++)
                        {
                            var lArg = args[i].ToLower().Trim();

                            if (lArg == "/hash" ||
                                lArg == "/h")
                            {
                                ctx.Hash = true;
                            }
                            else if (lArg == "/recursive" ||
                                     lArg == "/r")
                            {
                                ctx.Recursive = true;
                            }
                            else if (lArg == "/json" ||
                                     lArg == "/j")
                            {
                                ctx.ListFormat = ListFormat.Json;
                            }
                            else if (lArg == "/xml" ||
                                     lArg == "/x")
                            {
                                ctx.ListFormat = ListFormat.Xml;
                            }
                        }

                        ctx.Logger.WriteLine("Source     : {0}", src.FullName);
                        ctx.Logger.WriteLine("Destination: {0}", src.FullName);
                        ctx.Logger.WriteLine();

                        ctx.Logger.WriteLine("Hash files: {0}", ctx.Hash ? "yes" : "no");
                        ctx.Logger.WriteLine("Recursive : {0}", ctx.Recursive ? "yes" : "no");

                        var startTime = DateTimeOffset.Now;
                        CompareDirectories(src, dest, ctx);
                        var endTime = DateTimeOffset.Now;

                        ctx.Logger.WriteLine();
                        ctx.Logger.WriteLine();

                        double differentFilesPercentage = 0;
                        double missingFilesPercentage = 0;
                        double sameFilesPercentage = 0;
                        if (ctx.ProcessedFiles != 0)
                        {
                            missingFilesPercentage = (double)ctx.MissingFiles /
                                                     (double)ctx.ProcessedFiles;

                            sameFilesPercentage = (double)ctx.SameFiles /
                                                  (double)ctx.ProcessedFiles;

                            differentFilesPercentage = (double)ctx.DifferentFiles /
                                                       (double)ctx.ProcessedFiles;
                        }

                        double differentDirsPercentage = 0;
                        double missingDirsPercentage = 0;
                        double sameDirsPercentage = 0;
                        if (ctx.ProcessedDirs != 0)
                        {
                            missingDirsPercentage = (double)ctx.MissingDirs /
                                                    (double)ctx.ProcessedDirs;

                            sameDirsPercentage = (double)ctx.SameDirs /
                                                 (double)ctx.ProcessedDirs;

                            differentDirsPercentage = (double)ctx.DifferentDirs /
                                                   (double)ctx.ProcessedDirs;
                        }

                        ctx.Logger.WriteLine("Summary:");
                        ctx.Logger.WriteLine("=============");
                        ctx.Logger.WriteLine("Errors: {0}", ctx.Errors);
                        ctx.Logger.WriteLine();

                        ctx.Logger.WriteLine("Processed dirs : {0}", ctx.ProcessedDirs);
                        ctx.Logger.WriteLine("Processed files: {0}", ctx.ProcessedFiles);
                        ctx.Logger.WriteLine();

                        ctx.Logger.WriteLine("Extra dirs     : {0}", ctx.ExtraDirs);
                        ctx.Logger.WriteLine("Extra files    : {0}", ctx.ExtraFiles);
                        ctx.Logger.WriteLine("Missing dirs   : {0} ({1:0.00%})", ctx.MissingDirs
                                                                               , missingDirsPercentage);
                        ctx.Logger.WriteLine("Missing files  : {0} ({1:0.00%})", ctx.MissingFiles
                                                                               , missingFilesPercentage);
                        ctx.Logger.WriteLine("Same dirs      : {0} ({1:0.00%})", ctx.SameDirs
                                                                               , sameDirsPercentage);
                        ctx.Logger.WriteLine("Same files     : {0} ({1:0.00%})", ctx.SameFiles
                                                                               , sameFilesPercentage);
                        ctx.Logger.WriteLine("Different dirs : {0} ({1:0.00%})", ctx.DifferentDirs
                                                                               , differentDirsPercentage);
                        ctx.Logger.WriteLine("Different files: {0} ({1:0.00%})", ctx.DifferentFiles
                                                                               , differentFilesPercentage);

                        ctx.Logger.WriteLine();
                        ctx.Logger.WriteLine("Start   : {0}", startTime.ToString("yyyy-MM-dd HH:mm:ss.fffff (zzz)"));
                        ctx.Logger.WriteLine("End     : {0}", endTime.ToString("yyyy-MM-dd HH:mm:ss.fffff (zzz)"));
                        ctx.Logger.WriteLine("Duration: {0}", endTime - startTime);

                        loggerStream.Flush();
                    }
                }

                FileInfo listFile = null;
                switch (ctx.ListFormat)
                {
                    case ListFormat.Json:
                        listFile = new FileInfo(Path.Combine(currentDir.FullName,
                                                             string.Format("{0}.json", logPrefix)));
                        break;

                    case ListFormat.Xml:
                        listFile = new FileInfo(Path.Combine(currentDir.FullName,
                                                             string.Format("{0}.xml", logPrefix)));
                        break;
                }

                if (listFile != null)
                {
                    ctx.SaveList(listFile);
                }

                return 0;
            }
            catch (Exception ex)
            {
                GlobalConsole.Current.WriteLine(ex.GetBaseException() ?? ex);

                return 1;
            }
            finally
            {
#if DEBUG
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.WriteLine();
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.WriteLine();
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.WriteLine("===== ENTER ====");
                global::MarcelJoachimKloubert.CLRToolbox.IO.GlobalConsole.Current.ReadLine();
#endif
            }
        }

        private static DateTime NormalizeDateTime(DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day,
                                input.Hour, input.Minute, input.Second);
        }

        private static void ShowHelpScreen()
        {
        }

        #endregion Methods
    }
}