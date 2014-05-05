// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Collections.Generic
{
    #region CLASS: ForAllItemExecutionException<T>

    /// <summary>
    /// An exception that occured by execution an <see cref="IForAllItemExecutionContext{T}" />.
    /// </summary>
    /// <typeparam name="T">Type of the underlying item.</typeparam>
    public class ForAllItemExecutionException<T> : Exception
    {
        #region Fields (1)

        private readonly IForAllItemExecutionContext<T> _CONTEXT;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="ForAllItemExecutionException{T}"/> class.
        /// </summary>
        /// <param name="context">The value for <see cref="ForAllItemExecutionException{T}.Context" /> property.</param>
        /// <param name="ex">The underyling exception.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="ex" /> is <see langword="null" />.
        /// </exception>
        public ForAllItemExecutionException(IForAllItemExecutionContext<T> context,
                                            Exception ex)
            : base(ex.Message, ex)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this._CONTEXT = context;
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the underlying context.
        /// </summary>
        public IForAllItemExecutionContext<T> Context
        {
            get { return this._CONTEXT; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: ForAllItemExecutionException<T, S>

    /// <summary>
    /// An exception that occured by execution an <see cref="IForAllItemExecutionContext{T, S}" />.
    /// </summary>
    /// <typeparam name="T">Type of the underlying item.</typeparam>
    /// <typeparam name="S">Type of the underlying state object.</typeparam>
    public class ForAllItemExecutionException<T, S> : ForAllItemExecutionException<T>
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="ForAllItemExecutionException{T, S}"/> class.
        /// </summary>
        /// <param name="context">The value for <see cref="ForAllItemExecutionException{T, S}.Context" /> property.</param>
        /// <param name="ex">The underyling exception.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="ex" /> is <see langword="null" />.
        /// </exception>
        public ForAllItemExecutionException(IForAllItemExecutionContext<T, S> context,
                                            Exception ex)
            : base(context, ex)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ForAllItemExecutionException{T}.Context" />
        public new IForAllItemExecutionContext<T, S> Context
        {
            get { return (IForAllItemExecutionContext<T, S>)base.Context; }
        }

        #endregion Properties
    }

    #endregion
}
