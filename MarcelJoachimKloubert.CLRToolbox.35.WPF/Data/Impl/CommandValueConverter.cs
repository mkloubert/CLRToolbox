// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Data.Impl
{
    /// <summary>
    /// Simple implementation of <see cref="CommandValueConverterBase{TParam}" /> class.
    /// </summary>
    public sealed class CommandValueConverter : CommandValueConverterBase<object>
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValueConverter" /> class.
        /// </summary>
        /// <param name="sync">The asynchronous object.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        public CommandValueConverter(object sync)
            : base(sync)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValueConverter" /> class.
        /// </summary>
        public CommandValueConverter()
            : base()
        {

        }

        #endregion Constructors
    }
}
