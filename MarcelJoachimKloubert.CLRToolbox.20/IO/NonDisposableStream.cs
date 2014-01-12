// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A wrapper for a <see cref="Stream" /> that keeps sure that <see cref="Stream.Dispose()" /> method of
    /// <see cref="StreamWrapperBase._BASE_STREAM" /> field is NOT called from here.
    /// </summary>
    /// <remarks>
    /// The finalizer logic 
    /// </remarks>
    public sealed partial class NonDisposableStream : StreamWrapperBase
    {
        #region Fields (1)

        private readonly CallBehaviour _CALL_BEHAVIOUR;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="NonDisposableStream" /> class.
        /// </summary>
        /// <param name="baseStream">The value for <see cref="StreamWrapperBase._BASE_STREAM" /> field.</param>
        /// <param name="callBehaviour">
        /// The behaviour of <see cref="Stream.Dispose(bool)" /> method.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseStream" /> is <see langword="null" />.
        /// </exception>
        public NonDisposableStream(Stream baseStream, CallBehaviour callBehaviour)
            : base(baseStream)
        {
            this._CALL_BEHAVIOUR = callBehaviour;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonDisposableStream" /> class.
        /// </summary>
        /// <param name="baseStream">The value for <see cref="StreamWrapperBase._BASE_STREAM" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseStream" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// The behaviour of <see cref="Stream.Dispose(bool)" /> method is set to <see cref="CallBehaviour.CallFinalizerPart" />.
        /// </remarks>
        public NonDisposableStream(Stream baseStream)
            : this(baseStream, CallBehaviour.CallFinalizerPart)
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="StreamWrapperBase.Dispose(bool)" />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((this._CALL_BEHAVIOUR & CallBehaviour.CallDisposePart) != 0)
                {
                    this.InvokeDispose(true);
                }
            }
            else
            {
                if ((this._CALL_BEHAVIOUR & CallBehaviour.CallFinalizerPart) != 0)
                {
                    this.InvokeDispose(false);
                }
            }
        }

        #endregion Methods
    }
}
