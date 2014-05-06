// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Handles operations for the current application / process.
    /// </summary>
    public static class TMApplication
    {
        #region Fields (3)

        private const string _FILE_IEEXEC_EXE = "ieexec.exe";
        private static bool _isExiting;
        private static readonly object _SYNC = new object();

        #endregion Fields

        #region Properties (5)

        /// <summary>
        /// Gets the underlying application domain.
        /// </summary>
        public static AppDomain AppDomain
        {
            get { return AppDomain.CurrentDomain; }
        }

        /// <summary>
        /// Gets the entry assembly (if available).
        /// </summary>
        public static Assembly EntryAssembly
        {
            get { return Assembly.GetEntryAssembly(); }
        }

        /// <summary>
        /// Gets if the application is currently exiting or not.
        /// </summary>
        public static bool IsExiting
        {
            get { return _isExiting; }

            private set { _isExiting = value; }
        }

        /// <summary>
        /// Gets the underlying process.
        /// </summary>
        public static Process Process
        {
            get { return Process.GetCurrentProcess(); }
        }

        /// <summary>
        /// Gets the number of bits the current process is using for accessing memory.
        /// </summary>
        public static int ProcessBits
        {
            get { return IntPtr.Size * 8; }
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// Restarts the application.
        /// </summary>
        /// <exception cref="NotSupportedException">Restart is not supported here.</exception>
        public static void Restart()
        {
            Assembly entryAsm = EntryAssembly;
            if (entryAsm == null)
            {
                throw new NotSupportedException();
            }

            bool flag = false;

            Process currentProcess = Process;
            if (string.Equals(currentProcess.MainModule.ModuleName, _FILE_IEEXEC_EXE, StringComparison.OrdinalIgnoreCase))
            {
                string str = string.Empty;

                new FileIOPermission(PermissionState.Unrestricted).Assert();
                try
                {
                    str = Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName);
                }
                finally
                {
#if !MONO2 && !MONO20 && !MONO4 && !MONO40
                    global::System.Security.CodeAccessPermission.RevertAssert();
#endif
                }

                if (string.Equals(str + "\\" + _FILE_IEEXEC_EXE, currentProcess.MainModule.FileName, StringComparison.OrdinalIgnoreCase))
                {
                    flag = true;

                    IEnumerable<char> appLaunchUrl = AppDomain.GetData("APP_LAUNCH_URL") as IEnumerable<char>;
                    if (appLaunchUrl != null)
                    {
                        AppDomain.ProcessExit += delegate(object sender, EventArgs e)
                            {
                                Process.Start(currentProcess.MainModule.FileName, StringHelper.AsString(appLaunchUrl));
                            };
                    }

                    ExitInternal();
                }
            }

            if (!flag)
            {
                /*
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    string updatedApplicationFullName = ApplicationDeployment.CurrentDeployment.UpdatedApplicationFullName;
                    uint hostTypeFromMetaData = (uint)Application.ClickOnceUtility.GetHostTypeFromMetaData(updatedApplicationFullName);
                    Application.ExitInternal();
                    UnsafeNativeMethods.CorLaunchApplication(hostTypeFromMetaData, updatedApplicationFullName, 0, null, 0, null, new UnsafeNativeMethods.PROCESS_INFORMATION());
                    return;
                }*/

                string[] commandLineArgs = Environment.GetCommandLineArgs();
                StringBuilder stringBuilder = new StringBuilder((commandLineArgs.Length - 1) * 16);
                for (int i = 1; i < commandLineArgs.Length - 1; i++)
                {
                    stringBuilder.Append('"');
                    stringBuilder.Append(commandLineArgs[i]);
                    stringBuilder.Append("\" ");
                }

                if (commandLineArgs.Length > 1)
                {
                    stringBuilder.Append('"');
                    stringBuilder.Append(commandLineArgs[commandLineArgs.Length - 1]);
                    stringBuilder.Append('"');
                }

                ProcessStartInfo startInfo = Process.StartInfo;
                startInfo.FileName = Path.GetFullPath(entryAsm.Location);

                if (stringBuilder.Length > 0)
                {
                    startInfo.Arguments = stringBuilder.ToString();
                }

                AppDomain.ProcessExit += delegate(object sender, EventArgs e)
                    {
                        Process.Start(startInfo);
                    };

                ExitInternal();
            }
        }
        // Private Methods (1) 

        private static bool ExitInternal()
        {
            bool result = false;

            lock (_SYNC)
            {
                if (IsExiting)
                {
                    return false;
                }

                IsExiting = true;
                try
                {
                    Process.Close();

                    //if (Application.forms != null)
                    //{
                    //    foreach (Form form in Application.OpenFormsInternal)
                    //    {
                    //        if (form.RaiseFormClosingOnAppExit())
                    //        {
                    //            flag = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (!flag)
                    //{
                    //    if (Application.forms != null)
                    //    {
                    //        while (Application.OpenFormsInternal.Count > 0)
                    //        {
                    //            Application.OpenFormsInternal[0].RaiseFormClosedOnAppExit();
                    //        }
                    //    }
                    //    Application.ThreadContext.ExitApplication();
                    //}

                    result = true;
                }
                finally
                {
                    IsExiting = false;
                }
            }

            return result;
        }

        #endregion Methods
    }
}
