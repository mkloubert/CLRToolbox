// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Windows.Forms;

namespace MarcelJoachimKloubert.CloudNET.FileSync.Classes.Sessions
{
    internal sealed class SyncLogEventArgs : EventArgs
    {
        #region Constructors (1)

        internal SyncLogEventArgs(ListViewItem logItem)
        {
            this.LogItem = logItem;
        }

        #endregion Constructors

        #region Properties (1)

        internal ListViewItem LogItem
        {
            get;
            private set;
        }

        #endregion Properties
    }
}
