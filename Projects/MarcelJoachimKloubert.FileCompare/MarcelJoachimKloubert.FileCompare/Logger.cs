// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.IO;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.FileCompare
{
    public sealed class Logger : TextWriter
    {
        #region Fields (2) 

        private readonly TextWriter _INNER_WRITER;
        private readonly object _SYNC = new object();

        #endregion Fields 

        #region Constructors (1) 

        public Logger(TextWriter innerWriter)
        {
            this._INNER_WRITER = innerWriter;
        }

        #endregion Constructors 

        #region Properties (1) 

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        #endregion Properties 

        #region Methods (1) 

        // Public Methods (1) 

        public override void Write(char value)
        {
            lock (this._SYNC)
            {
                try
                {
                    if (this._INNER_WRITER != null)
                    {
                        this._INNER_WRITER.Write(value);
                    }
                }
                catch
                {
                }

                try
                {
                    GlobalConsole.Current.Write(value);
                }
                catch
                {
                }
            }
        }

        #endregion Methods 
    }
}