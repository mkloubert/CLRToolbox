// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Impl
{
    /// <summary>
    /// Logs a message asnychron.
    /// </summary>
    public sealed partial class AsyncLogger : LoggerFacadeWrapperBase
    {


        #region Methods (1)

        // Private Methods (1) 

        private void OnLogAsync(object state)
        {
            try
            {
                this._INNER_LOGGER
                    .Log((ILogMessage)state);
            }
            catch
            {
                // ignore errors here
            }
        }

        #endregion Methods
    }
}
