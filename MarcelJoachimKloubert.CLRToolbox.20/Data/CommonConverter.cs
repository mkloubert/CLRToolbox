// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Common implementation of an <see cref="IConverter" /> object.
    /// </summary>
    public partial class CommonConverter : ConverterBase
    {
        #region Constructors (2)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="CommonConverter" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public CommonConverter(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonConverter" /> class.
        /// </summary>
        public CommonConverter()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (3)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConverterBase.OnChangeType(Type, ref object, IFormatProvider)" />
        protected override void OnChangeType(Type targetType, ref object targetValue, IFormatProvider provider)
        {
            this.ParseInputValueForChangeType(targetType, ref targetValue, provider);

            if (targetValue != null)
            {
                Type valueType = targetValue.GetType();
                if (valueType.Equals(targetType) ||
                    targetType.IsAssignableFrom(valueType))
                {
                    // no need to convert
                    return;
                }
            }

            if (targetType.Equals(typeof(string)) ||
                targetType.Equals(typeof(global::System.Collections.Generic.IEnumerable<char>)))
            {
                // force to convert to string

                string str = StringHelper.AsString(targetValue);
                if (str != null &&
                    provider != null)
                {
                    str = str.ToString(provider);
                }

                targetValue = str;
                return;
            }

            if (targetValue == null)
            {
                if (targetType.IsValueType &&
                    Nullable.GetUnderlyingType(targetType) == null)
                {
                    // a (non-nullable) struct, so create instance by use the default parameter-less constructor
                    targetValue = Activator.CreateInstance(targetType);
                }

                return;
            }

            bool changeTypeExtensionHandled = false;
            this.OnChangeTypeExtension(targetType,
                                       ref targetValue,
                                       provider,
                                       ref changeTypeExtensionHandled);

            if (!changeTypeExtensionHandled)
            {
                // use BCL logic
                targetValue = global::System.Convert.ChangeType(targetValue, targetType, provider);
            }
        }
        // Private Methods (2) 

        partial void OnChangeTypeExtension(Type targetType, ref object targetValue, IFormatProvider provider, ref bool handled);

        partial void ParseInputValueForChangeType(Type targetType, ref object targetValue, IFormatProvider provider);

        #endregion Methods
    }
}
