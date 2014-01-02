// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace System.IO
{
    #region DELEGATE: ErrorEventHandler

    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.io.erroreventhandler%28v=vs.110%29.aspx" />
    public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);

    #endregion

    #region CLASS: ErrorEventArgs
    /// <summary>
    /// 
    /// </summary>
    /// <see href="http://msdn.microsoft.com/en-us/library/system.io.erroreventargs%28v=vs.110%29.aspx" />
    public class ErrorEventArgs : EventArgs
    {
        #region Fields (1)

        private readonly Exception _EXCEPTION;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.io.erroreventargs.erroreventargs%28v=vs.110%29.aspx" />
        public ErrorEventArgs(Exception exception)
        {
            this._EXCEPTION = exception;
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/system.io.erroreventargs.getexception%28v=vs.110%29.aspx" />
        public virtual Exception GetException()
        {
            return this._EXCEPTION;
        }

        #endregion Methods
    }

    #endregion
}
