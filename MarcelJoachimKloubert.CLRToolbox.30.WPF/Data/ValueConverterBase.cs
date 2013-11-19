// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Data
{
    #region CLASS: ValueConverterBase<TInput, TOutput, TParam>

    /// <summary>
    /// A basic strong typed <see cref="IValueConverter" />.
    /// </summary>
    /// <typeparam name="TInput">The source / input data type.</typeparam>
    /// <typeparam name="TParam">The parameter data type.</typeparam>
    /// <typeparam name="TOutput">The destination / output data type.</typeparam>
    public abstract partial class ValueConverterBase<TInput, TOutput, TParam> : IValueConverter
    {
        #region Fields (1)

        /// <summary>
        /// The unique object for thread safe operations.
        /// </summary>
        protected readonly object _SYNC;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverterBase{TInput, TOutput, TParam}" /> class.
        /// </summary>
        /// <param name="sync">The asynchronous object.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected ValueConverterBase(object sync)
        {
            if (sync == null)
            {
                throw new ArgumentNullException("sync");
            }

            this._SYNC = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverterBase{TInput, TOutput, TParam}" /> class.
        /// </summary>
        protected ValueConverterBase()
            : this(new object())
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IValueConverter.Convert(object, Type, object, CultureInfo)" />
        public virtual TOutput Convert(TInput value, TParam parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)" />
        public virtual TInput ConvertBack(TOutput value, TParam parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: ValueConverterBase<TInput, TOutput>

    /// <summary>
    /// A simple version of <see cref="ValueConverterBase{TInput, TOutput, TParam}" /> that handles a parameter
    /// object as parsed string.
    /// </summary>
    /// <typeparam name="TInput">The source / input data type.</typeparam>
    /// <typeparam name="TOutput">The destination / output data type.</typeparam>
    /// <remarks>
    /// The parameter is parsed to a non-null, trimmed and lower case <see cref="string" /> by default.
    /// </remarks>
    public abstract partial class ValueConverterBase<TInput, TOutput> : ValueConverterBase<TInput, TOutput, object>
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverterBase{TInput, TOutput}" /> class.
        /// </summary>
        /// <param name="sync">The asynchronous object.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected ValueConverterBase(object sync)
            : base(sync)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueConverterBase{TInput, TOutput}" /> class.
        /// </summary>
        protected ValueConverterBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (6)

        // Public Methods (2) 
        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase{TInput, TOutput, TParam}.Convert(TInput, TParam, CultureInfo)" />
        public override sealed TOutput Convert(TInput value, object parameter, CultureInfo culture)
        {
            return this.Convert(value,
                                StringHelper.AsString(this.ParseConvertParameter(parameter,
                                                                                 culture)),
                                culture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase{TInput, TOutput, TParam}.ConvertBack(TOutput, TParam, CultureInfo)" />
        public override sealed TInput ConvertBack(TOutput value, object parameter, CultureInfo culture)
        {
            return this.ConvertBack(value,
                                    StringHelper.AsString(this.ParseConvertBackParameter(parameter,
                                                                                         culture)),
                                    culture);
        }

        // Protected Methods (4) 

        /// <summary>
        /// The logic for <see cref="ValueConverterBase{TInput, TOutput}.Convert(TInput, object, CultureInfo)" /> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <param name="parameter">The parameter as <see cref="String" />.</param>
        /// <param name="culture">The underlying culture.</param>
        /// <returns>The output value.</returns>
        protected virtual TOutput Convert(TInput value, string parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The logic for <see cref="ValueConverterBase{TInput, TOutput}.ConvertBack(TOutput, object, CultureInfo)" /> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <param name="parameter">The parameter as <see cref="String" />.</param>
        /// <param name="culture">The underlying culture.</param>
        /// <returns>The output value.</returns>
        protected virtual TInput ConvertBack(TOutput value, string parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parses a parameter for <see cref="ValueConverterBase{TInput, TOutput}.Convert(TInput, string, CultureInfo)" /> method.
        /// </summary>
        /// <param name="input">The input parameter value.</param>
        /// <param name="culture">The underlying culture.</param>
        /// <returns>The parsed parameter value.</returns>
        protected virtual IEnumerable<char> ParseConvertParameter(object input, CultureInfo culture)
        {
            return (StringHelper.AsString(input, true) ?? string.Empty).ToLower()
                                                                       .Trim();
        }

        /// <summary>
        /// Parses a parameter for <see cref="ValueConverterBase{TInput, TOutput}.ConvertBack(TOutput, string, CultureInfo)" /> method.
        /// </summary>
        /// <param name="input">The input parameter value.</param>
        /// <param name="culture">The underlying culture.</param>
        /// <returns>The parsed parameter value.</returns>
        protected virtual IEnumerable<char> ParseConvertBackParameter(object input, CultureInfo culture)
        {
            return this.ParseConvertParameter(input, culture);
        }

        #endregion Methods
    }

    #endregion
}
