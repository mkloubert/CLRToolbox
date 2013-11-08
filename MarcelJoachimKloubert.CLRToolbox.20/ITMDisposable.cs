// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// An extension of <see cref="IDisposable" />.
    /// </summary>
    public interface ITMDisposable : ITMObject, IDisposable
    {
        #region Data Members (1)

        /// <summary>
        /// Gets if that object has been disposed or not.
        /// </summary>
        bool IsDisposed { get; }

        #endregion Data Members

        #region Delegates and Events (2)

        // Events (2) 

        /// <summary>
        /// Is invoked after dispose logic finished.
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// Is invoked before dispose logic starts.
        /// </summary>
        event EventHandler Disposing;

        #endregion Delegates and Events
    }
}
