using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    partial class TMObject
    {
        #region Fields (2)

        /// <summary>
        /// An unique object for sync operations.
        /// </summary>
        [NonSerialized]
        protected readonly object _SYNC;
        [NonSerialized]
        private object _tag;

        #endregion Fields
    }
}
