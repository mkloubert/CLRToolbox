// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A basic console.
    /// </summary>
    public abstract class ConsoleBase : TMObject, IConsole
    {
        #region Fields (2)

        private ConsoleColor? _backgroundColor;
        private ConsoleColor? _foregroundColor;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected ConsoleBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleBase" /> class.
        /// </summary>
        protected ConsoleBase()
            : base()
        {

        }

        #endregion Constructors

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
    }
}
