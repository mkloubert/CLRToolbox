// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.SDK.Helpers;
using System;

namespace MarcelJoachimKloubert.CloudNET.SDK.IO
{
    #region CLASS: SyncCloudDirectoryProgressEventHandler

    /// <summary>
    /// Describes an event that is invoked when synchronizing a cloud directory.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void SyncCloudDirectoryProgressEventHandler(object sender, SyncCloudDirectoryProgressEventArgs e);

    #endregion

    #region CLASS: SyncCloudDirectoryProgressEventArgs

    /// <summary>
    /// The arguments for <see cref="SyncCloudDirectoryProgressEventHandler" />.
    /// </summary>
    public sealed class SyncCloudDirectoryProgressEventArgs : EventArgs
    {
        #region Fields (2)

        private readonly double? _PROGRESS;
        private readonly string _STATUS_TEXT;

        #endregion Fields

        #region Constructors (1)

        internal SyncCloudDirectoryProgressEventArgs(double? progress,
                                                     string statusText)
        {
            if (progress < 0)
            {
                progress = 0;
            }

            if (progress > 1)
            {
                progress = 1;
            }

            this._PROGRESS = progress;

            if (StringHelper.IsNullOrWhitespace(statusText) == false)
            {
                this._STATUS_TEXT = statusText.Trim();
            }
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the progress between 0 and 1.
        /// </summary>
        public double? Progress
        {
            get { return this._PROGRESS; }
        }

        /// <summary>
        /// Gets the normalized status text if available.
        /// </summary>
        public string StatusText
        {
            get { return this._STATUS_TEXT; }
        }

        #endregion Properties
    }

    #endregion
}
