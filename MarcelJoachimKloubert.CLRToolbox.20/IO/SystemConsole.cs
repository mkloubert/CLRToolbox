// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// Wrapper for <see cref="Console" />.
    /// </summary>
    public class SystemConsole : ConsoleBase
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemConsole" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field..</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SystemConsole(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemConsole" /> class.
        /// </summary>
        public SystemConsole()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConsoleBase.BackgroundColor" />
        public override ConsoleColor? BackgroundColor
        {
            get
            {
                return global::System.Console.BackgroundColor;
            }

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
            get
            {
                return global::System.Console.ForegroundColor;
            }

            set
            {
                if (value.HasValue)
                {
                    global::System.Console.ForegroundColor = value.Value;
                }
            }
        }

        #endregion Properties
    }
}
