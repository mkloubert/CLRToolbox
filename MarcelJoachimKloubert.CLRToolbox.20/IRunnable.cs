// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// Describes an object that can be started and stopped.
    /// </summary>
    public interface IRunnable : ITMObject
    {
        #region Data Members (4)

        /// <summary>
        /// Gets if <see cref="IRunnable.Restart()" /> method can be invoked or not.
        /// </summary>
        bool CanRestart { get; }

        /// <summary>
        /// Gets if <see cref="IRunnable.Start()" /> method can be invoked or not.
        /// </summary>
        bool CanStart { get; }

        /// <summary>
        /// Gets if <see cref="IRunnable.Stop()" /> method can be invoked or not.
        /// </summary>
        bool CanStop { get; }

        /// <summary>
        /// Gets if that object is running or not.
        /// </summary>
        bool IsRunning { get; }

        #endregion Data Members

        #region Operations (3)

        /// <summary>
        /// Restarts that object.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="IRunnable.CanRestart" /> is <see langword="false" />.
        /// </exception>
        void Restart();

        /// <summary>
        /// Starts that object.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="IRunnable.CanStart" /> is <see langword="false" />.
        /// </exception>
        void Start();

        /// <summary>
        /// Stops that object.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="IRunnable.CanStop" /> is <see langword="false" />.
        /// </exception>
        void Stop();

        #endregion Operations
    }
}
