// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class SystemConsole
    {
        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.BackgroundColor" />
        public override ConsoleColor? BackgroundColor
        {
            get { return global::System.Console.BackgroundColor; }

            set
            {
                if (value.HasValue)
                {
                    global::System.Console.BackgroundColor = value.Value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.ForegroundColor" />
        public override ConsoleColor? ForegroundColor
        {
            get { return global::System.Console.ForegroundColor; }

            set
            {
                if (value.HasValue)
                {
                    global::System.Console.ForegroundColor = value.Value;
                }
            }
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.OnClear()" />
        protected override void OnClear()
        {
            global::System.Console.Clear();
        }

        #endregion Methods
    }
}
