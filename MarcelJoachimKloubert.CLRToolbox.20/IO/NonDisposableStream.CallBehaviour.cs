// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class NonDisposableStream
    {
        #region Enums (1)

        /// <summary>
        /// List of behaviours of <see cref="NonDisposableStream.Dispose(bool)" /> method.
        /// </summary>
        [Flags]
        public enum CallBehaviour
        {
            /// <summary>
            /// Call nothing
            /// </summary>
            Nothing = 0,

            /// <summary>
            /// Do NOT ignore <see cref="Stream.Dispose()" /> method of <see cref="StreamWrapperBase._BASE_STREAM" />.
            /// </summary>
            CallDisposePart = 1,

            /// <summary>
            /// Do NOT ignore finalizer of <see cref="StreamWrapperBase._BASE_STREAM" />.
            /// </summary>
            CallFinalizerPart = 2,
        }

        #endregion Enums
    }
}
