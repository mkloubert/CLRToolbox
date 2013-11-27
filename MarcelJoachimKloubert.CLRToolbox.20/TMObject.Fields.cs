using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    partial class TMObject
    {
        #region Fields (2)

        /// <summary>
        /// An unique object for sync operations.
        /// </summary>
        [NonSerializedAttribute]
        protected readonly object _SYNC;
        [NonSerializedAttribute]
        private object _tag;

        #endregion Fields
    }
}
