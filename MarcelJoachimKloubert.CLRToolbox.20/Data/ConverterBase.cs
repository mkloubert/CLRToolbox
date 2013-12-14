// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Threading;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// A basic converter.
    /// </summary>
    public abstract class ConverterBase : TMObject, IConverter
    {
        #region Constructors (2)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="ConverterBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected ConverterBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConverterBase" /> class.
        /// </summary>
        protected ConverterBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConverter.ChangeType{T}(object)" />
        public virtual T ChangeType<T>(object value)
        {
            return (T)this.ChangeType(typeof(T), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConverter.ChangeType{T}(object, IFormatProvider)" />
        public T ChangeType<T>(object value, IFormatProvider provider)
        {
            return (T)this.ChangeType(typeof(T), value, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConverter.ChangeType(Type, object)" />
        public object ChangeType(Type type, object value)
        {
            return this.ChangeType(type, value, Thread.CurrentThread.CurrentCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConverter.ChangeType(Type, object, IFormatProvider)" />
        public object ChangeType(Type type, object value, IFormatProvider provider)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            object result = value;
            this.OnChangeType(type, ref result, provider);

            return result;
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for the <see cref="ConverterBase.ChangeType(Type, object, IFormatProvider)" /> method.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        /// <param name="targetValue">The value where to write the target value to.</param>
        /// <param name="provider">The optional format provider to use.</param>
        protected abstract void OnChangeType(Type targetType, ref object targetValue, IFormatProvider provider);

        #endregion Methods
    }
}
