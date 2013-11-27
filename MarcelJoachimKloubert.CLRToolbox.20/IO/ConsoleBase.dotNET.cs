// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class ConsoleBase
    {
        #region Fields (2)

        private ConsoleColor? _backgroundColor;
        private ConsoleColor? _foregroundColor;

        #endregion Fields

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.BackgroundColor" />
        public virtual ConsoleColor? BackgroundColor
        {
            get { return this._backgroundColor; }

            set { this._backgroundColor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.ForegroundColor" />
        public virtual ConsoleColor? ForegroundColor
        {
            get { return this._foregroundColor; }

            set { this._foregroundColor = value; }
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.Clear()" />
        public ConsoleBase Clear()
        {
            lock (this._SYNC)
            {
                this.OnClear();
                return this;
            }
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for <see cref="ConsoleBase.Clear()" /> method.
        /// </summary>
        protected abstract void OnClear();
        // Private Methods (1) 

        IConsole IConsole.Clear()
        {
            return this.Clear();
        }

        #endregion Methods
    }
}
