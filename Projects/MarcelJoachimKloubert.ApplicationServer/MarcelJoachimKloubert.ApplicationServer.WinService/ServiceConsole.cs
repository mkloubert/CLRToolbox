// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.IO;

namespace MarcelJoachimKloubert.ApplicationServer.WinService
{
    internal sealed class ServiceConsole : ConsoleBase
    {
        #region Fields (2)

        private string _currentFile;
        private readonly MainService _SERVICE;

        #endregion Fields

        #region Constructors (1)

        internal ServiceConsole(MainService service)
        {
            this._SERVICE = service;

            this.OnClear();
        }

        #endregion Constructors

        #region Methods (3)

        // Protected Methods (3) 

        protected override void OnClear()
        {
            lock (this._SYNC)
            {
                var now = DateTimeOffset.Now;

                this._currentFile = Path.Combine(this._SERVICE.LogDirectory,
                                                 string.Format("console_{0:yyyyMMdd}{0:HHmmss}_{0:zzz}.txt",
                                                               now).Replace("+", string.Empty)
                                                                   .Replace(":", string.Empty));
            }
        }

        protected override void OnReadLine(TextWriter line)
        {
            throw new NotImplementedException();
        }

        protected override void OnWrite(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            lock (this._SYNC)
            {
                try
                {
                    File.AppendAllText(path: this._currentFile,
                                       contents: text,
                                       encoding: Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    this._SERVICE
                        .EventLog
                        .WriteEntry(string.Format("Console error:{0}{0}{1}",
                                                  Environment.NewLine,
                                                  ex.GetBaseException() ?? ex),
                                    EventLogEntryType.Error);
                }
            }
        }

        #endregion Methods
    }
}
