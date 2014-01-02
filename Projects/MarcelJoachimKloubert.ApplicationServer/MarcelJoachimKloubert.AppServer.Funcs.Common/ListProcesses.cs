// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Execution.Functions;
using MarcelJoachimKloubert.CLRToolbox.Extensions;

namespace MarcelJoachimKloubert.AppServer.Funcs.Common
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Execution.Functions.IFunction))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal sealed class ListProcesses : CommonFunctionBase
    {
        #region Fields (2)

        private const bool _DEFAULT_INPUT_PARAM_ALL_MODULES = false;
        private const bool _DEFAULT_INPUT_PARAM_WITH_ICONS = false;

        #endregion Fields

        #region Constructors (1)

        internal ListProcesses()
            : base(id: new Guid("{0F4EC862-D337-46E1-BB6D-A54734DF0D7B}"))
        {

        }

        #endregion Constructors

        #region Methods (4)

        // Protected Methods (1) 

        protected override void OnExecute(FunctionBase.OnExecuteContext context)
        {
            var processList = new List<IDictionary<string, object>>();

            // use filter?
            IEnumerable<Process> processesToHandle = Process.GetProcesses();
            if (context.InputParameters.ContainsKey("Filter"))
            {
                var whitelist = new List<int>();

                var temp = context.InputParameters["Filter"];
                if (temp is IEnumerable &&
                    !(temp is IEnumerable<char>))
                {
                    // list fo IDs

                    whitelist.AddRange(((IEnumerable)temp).Cast<object>()
                                                          .Select(i => i.AsString(true))
                                                          .Where(str => !string.IsNullOrWhiteSpace(str))
                                                          .Select(str => GlobalConverter.Current
                                                                                        .ChangeType<int>(str.Trim()))
                                                          .Distinct());
                }
                else
                {
                    // single ID

                    whitelist.Add(GlobalConverter.Current
                                                 .ChangeType<int>(temp.AsString(true)
                                                                      .Trim()));
                }

                processesToHandle = processesToHandle.Where(p =>
                    {
                        try
                        {
                            return whitelist.Contains(p.Id);
                        }
                        catch
                        {
                            return false;
                        }
                    });
            }

            foreach (var p in processesToHandle)
            {
                try
                {
                    var processData = new Dictionary<string, object>();
                    ExtractProcessData(context, p, processData);

                    processList.Add(processData);
                }
                catch
                {
                    // ignore here
                }
            }

            context.ResultParameters["processes"] = processList;
        }
        // Private Methods (3) 

        private static void AddIfDataExist<T>(ICollection<T> coll,
                                              string dictName, IDictionary<string, object> dict)
        {
            if (coll.Count > 0)
            {
                dict[dictName] = coll;
            }
        }

        [DebuggerStepThrough]
        private static void ExtractProcessData(FunctionBase.OnExecuteContext context,
                                               Process p, IDictionary<string, object> processData)
        {
            bool? withIcons = null;
            {
                object temp;
                if (context.InputParameters.TryGetValue("WithIcon", out temp))
                {
                    withIcons = GlobalConverter.Current
                                               .ChangeType<bool?>(temp);
                }
            }

            bool? allModules = null;
            {
                object temp;
                if (context.InputParameters.TryGetValue("AllModules", out temp))
                {
                    allModules = GlobalConverter.Current
                                                .ChangeType<bool?>(temp);
                }
            }

            TrySetDictionaryData(() => p.BasePriority, "basePriority", processData);
            TrySetDictionaryData(() => (DateTimeOffset)p.ExitTime, "exitTime", processData);
            TrySetDictionaryData(() => p.HasExited, "hasExited", processData);
            TrySetDictionaryData(() => p.Id, "id", processData);
            TrySetDictionaryData(() => p.MachineName, "machine", processData);
            TrySetDictionaryData(() => p.ProcessName, "name", processData);
            TrySetDictionaryData(() => p.PriorityClass.ToString(), "priorityClass", processData);
            TrySetDictionaryData(() => p.SessionId, "sessionId", processData);
            TrySetDictionaryData(() => (DateTimeOffset)p.StartTime, "startTime", processData);

            // icon
            if (withIcons ?? _DEFAULT_INPUT_PARAM_WITH_ICONS)
            {
                var iconData = new Dictionary<string, object>();

                try
                {
                    using (var icon = Icon.ExtractAssociatedIcon(Path.GetFullPath(p.MainModule.FileName)))
                    {
                        if (icon != null)
                        {
                            using (var bmp = icon.ToBitmap())
                            {
                                using (var temp = new MemoryStream())
                                {
                                    bmp.Save(temp, ImageFormat.Png);

                                    iconData["mime"] = "image/png";
                                    iconData["data"] = temp.ToArray();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // ignore
                }

                AddIfDataExist(iconData, "icon", processData);
            }

            // main window
            var mainWindowData = new Dictionary<string, object>();
            {
                TrySetDictionaryData(() => p.MainWindowTitle, "title", mainWindowData);
            }

            // modules
            var modulesData = new List<IDictionary<string, object>>();
            {
                try
                {
                    IEnumerable<ProcessModule> modules;
                    if (allModules ?? _DEFAULT_INPUT_PARAM_ALL_MODULES)
                    {
                        modules = p.Modules
                                   .Cast<ProcessModule>();
                    }
                    else
                    {
                        modules = new List<ProcessModule>() { p.MainModule };
                    }

                    int index = 0;
                    foreach (var pm in modules.Where(x => x != null))
                    {
                        var newItem = new Dictionary<string, object>();
                        TrySetDictionaryData(() => pm.FileName, "filePath", newItem);
                        TrySetDictionaryData(() => index++, "index", newItem);
                        TrySetDictionaryData(() => pm.ModuleName, "name", newItem);

                        var fileVersionData = new Dictionary<string, object>();
                        {
                            TrySetDictionaryData(() => pm.FileVersionInfo.FileDescription, "description", fileVersionData);
                            TrySetDictionaryData(() => pm.FileVersionInfo.InternalName, "internalName", fileVersionData);
                            TrySetDictionaryData(() => pm.FileVersionInfo.IsDebug, "isDebug", fileVersionData);
                            TrySetDictionaryData(() => pm.FileVersionInfo.Language, "language", fileVersionData);
                            TrySetDictionaryData(() => pm.FileVersionInfo.ProductName, "productName", fileVersionData);
                            TrySetDictionaryData(() => pm.FileVersionInfo.FileVersion, "version", fileVersionData);
                        }

                        AddIfDataExist(fileVersionData, "fileVersion", newItem);

                        if (newItem.Count > 0)
                        {
                            modulesData.Add(newItem);
                        }
                    }
                }
                catch
                {
                    // ignore
                }
            }

            // user
            var userData = new Dictionary<string, object>();
            {
                TrySetDictionaryData(() => p.StartInfo.Domain, "domain", userData);
                TrySetDictionaryData(() => p.StartInfo.UserName, "name", userData);
            }

            AddIfDataExist(mainWindowData, "mainWindow", processData);
            AddIfDataExist(modulesData, "modules", processData);
            AddIfDataExist(userData, "user", processData);
        }

        private static bool TrySetDictionaryData<T>(Func<T> func,
                                                    string dictKey, IDictionary<string, object> dict)
        {
            try
            {
                dict[dictKey] = func();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Methods
    }
}
