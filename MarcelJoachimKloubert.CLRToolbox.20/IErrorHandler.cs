// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Describes an object that can handle exceptions via events, e.g.
    /// </summary>
    public interface IErrorHandler : ITMObject
    {
        #region Delegates and Events (1)

        // Events (1) 

        /// <summary>
        /// Is invoked when an error occured.
        /// </summary>
        event ErrorEventHandler Error;

        #endregion Delegates and Events
    }
}
