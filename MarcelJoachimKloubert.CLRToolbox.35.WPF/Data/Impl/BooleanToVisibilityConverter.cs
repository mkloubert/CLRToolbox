// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Data.Impl
{
    /// <summary>
    /// Converts a <see cref="Nullable{Boolean}" /> value to a <see cref="Nullable{Visibility}" />.
    /// </summary>
    public class BooleanToVisibilityConverter : MultiParamValueConverterBase<bool?, Visibility?>
    {
        #region Fields (17)

        /// <summary>
        /// Name of the parameter that returns <see cref="Visibility.Collapsed" /> value if
        /// input is <see langword="null" />.
        /// </summary>
        protected const string _PARAM_COLLAPSED_IF_NULL = "collapsed_if_null";

        /// <summary>
        /// Name of the parameter that returns  value if <see langword="false" />
        /// input is <see cref="Visibility.Collapsed" />.
        /// </summary>
        protected const string _PARAM_FALSE_IF_COLLAPSED = "false_if_collapsed";

        /// <summary>
        /// Name of the parameter that returns  value if <see langword="false" />
        /// input is <see cref="Visibility.Hidden" />.
        /// </summary>
        protected const string _PARAM_FALSE_IF_HIDDEN = "false_if_hidden";

        /// <summary>
        /// Name of the parameter that returns  value if <see langword="false" />
        /// input is <see cref="Visibility.Visible" />.
        /// </summary>
        protected const string _PARAM_FALSE_IF_VISIBLE = "false_if_visible";

        /// <summary>
        /// Name of the parameter that returns <see cref="Visibility.Hidden" /> value if
        /// input is <see langword="false" />.
        /// </summary>
        protected const string _PARAM_HIDDEN_IF_FALSE = "hidden_if_false";

        /// <summary>
        /// Name of the parameter that returns <see cref="Visibility.Hidden" /> value if
        /// input is <see langword="null" />.
        /// </summary>
        protected const string _PARAM_HIDDEN_IF_NULL = "hidden_if_null";

        /// <summary>
        /// Name of the parameter that inverts result values.
        /// </summary>
        protected const string _PARAM_INVERT = "invert";

        /// <summary>
        /// Name of the parameter that returns <see langword="null" /> if
        /// input is <see cref="Visibility.Collapsed" />.
        /// </summary>
        protected const string _PARAM_NULL_IF_COLLAPSED = "null_if_collapsed";

        /// <summary>
        /// Name of the parameter that returns <see langword="null" /> if
        /// input is <see langword="false" />.
        /// </summary>
        protected const string _PARAM_NULL_IF_FALSE = "null_if_false";

        /// <summary>
        /// Name of the parameter that returns <see langword="null" /> if
        /// input is <see cref="Visibility.Hidden" />.
        /// </summary>
        protected const string _PARAM_NULL_IF_HIDDEN = "null_if_hidden";

        /// <summary>
        /// Name of the parameter that returns <see langword="null" /> if
        /// input is also <see langword="null" />.
        /// </summary>
        protected const string _PARAM_NULL_IF_NULL = "null_if_null";

        /// <summary>
        /// Name of the parameter that returns <see langword="null" /> if
        /// input is <see langword="true" />.
        /// </summary>
        protected const string _PARAM_NULL_IF_TRUE = "null_if_true";

        /// <summary>
        /// Name of the parameter that returns <see langword="null" /> if
        /// input is <see cref="Visibility.Visible" />.
        /// </summary>
        protected const string _PARAM_NULL_IF_VISIBLE = "null_if_visible";

        /// <summary>
        /// Name of the parameter that returns  value if <see langword="true" />
        /// input is <see cref="Visibility.Collapsed" />.
        /// </summary>
        protected const string _PARAM_TRUE_IF_COLLAPSED = "true_if_collapsed";

        /// <summary>
        /// Name of the parameter that returns  value if <see langword="true" />
        /// input is <see cref="Visibility.Hidden" />.
        /// </summary>
        protected const string _PARAM_TRUE_IF_HIDDEN = "true_if_hidden";

        /// <summary>
        /// Name of the parameter that returns  value if <see langword="true" />
        /// input is <see cref="Visibility.Visible" />.
        /// </summary>
        protected const string _PARAM_TRUE_IF_VISIBLE = "true_if_visible";

        /// <summary>
        /// Name of the parameter that returns <see cref="Visibility.Visible" /> value if
        /// input is <see langword="null" />.
        /// </summary>
        protected const string _PARAM_VISIBLE_IF_NULL = "visible_if_null";

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter" /> class.
        /// </summary>
        /// <param name="sync">The asynchronous object.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        public BooleanToVisibilityConverter(object sync)
            : base(sync)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter" /> class.
        /// </summary>
        public BooleanToVisibilityConverter()
            : base()
        {
        }

        #endregion Constructors

        #region Methods (2)

        // Protected Methods (2) 

        /// <inheriteddoc />
        protected override Visibility? Convert(bool? value, IList<string> @params, CultureInfo culture)
        {
            Visibility? valueIfNull = null;
            Visibility? valueIfFalse = Visibility.Collapsed;
            bool invert = false;

            if (@params.Contains(_PARAM_INVERT))
            {
                invert = true;
            }

            if (@params.Contains(_PARAM_COLLAPSED_IF_NULL))
            {
                valueIfNull = Visibility.Collapsed;
            }

            if (@params.Contains(_PARAM_HIDDEN_IF_FALSE))
            {
                valueIfFalse = Visibility.Hidden;
            }

            if (@params.Contains(_PARAM_HIDDEN_IF_NULL))
            {
                valueIfNull = Visibility.Hidden;
            }

            if (@params.Contains(_PARAM_VISIBLE_IF_NULL))
            {
                valueIfNull = Visibility.Visible;
            }

            if (value.HasValue)
            {
                if (invert)
                {
                    return value.Value ? valueIfFalse : Visibility.Visible;
                }
                else
                {
                    return value.Value ? Visibility.Visible : valueIfFalse;
                }
            }

            return valueIfNull;
        }

        /// <inheriteddoc />
        protected override bool? ConvertBack(Visibility? value, IList<string> @params, CultureInfo culture)
        {
            bool? valueIfCollapsed = false;
            bool? valueIfHidden = false;
            bool? valueIfVisible = true;

            bool? result = null;

            if (@params.Contains(_PARAM_COLLAPSED_IF_NULL))
            {
                valueIfCollapsed = null;
            }

            if (@params.Contains(_PARAM_VISIBLE_IF_NULL))
            {
                valueIfVisible = null;
            }

            if (@params.Contains(_PARAM_HIDDEN_IF_NULL))
            {
                valueIfHidden = null;
            }

            // hidden
            valueIfHidden = GetLastParamValue(@params,
                                              valueIfHidden,
                                              new Dictionary<string, Func<string, bool?>>
                                              {
                                                  { _PARAM_HIDDEN_IF_NULL, (p) => null },
                                                  { _PARAM_HIDDEN_IF_FALSE, (p) => false },
                                              });

            // collapsed
            valueIfCollapsed = GetLastParamValue(@params,
                                                 valueIfCollapsed,
                                                 new Dictionary<string, Func<string, bool?>>
                                                 {
                                                     { _PARAM_FALSE_IF_COLLAPSED, (p) => false },
                                                     { _PARAM_NULL_IF_COLLAPSED, (p) => null },
                                                     { _PARAM_TRUE_IF_COLLAPSED, (p) => true },
                                                 });

            // hidden
            valueIfHidden = GetLastParamValue(@params,
                                              valueIfHidden,
                                              new Dictionary<string, Func<string, bool?>>
                                              {
                                                  { _PARAM_FALSE_IF_HIDDEN, (p) => false },
                                                  { _PARAM_NULL_IF_HIDDEN, (p) => null },
                                                  { _PARAM_TRUE_IF_HIDDEN, (p) => true },
                                              });

            // visible
            valueIfVisible = GetLastParamValue(@params,
                                               valueIfVisible,
                                               new Dictionary<string, Func<string, bool?>>
                                               {
                                                   { _PARAM_FALSE_IF_VISIBLE, (p) => false },
                                                   { _PARAM_NULL_IF_VISIBLE, (p) => null },
                                                   { _PARAM_TRUE_IF_VISIBLE, (p) => true },
                                               });

            if (value.HasValue)
            {
                switch (value.Value)
                {
                    case Visibility.Collapsed:
                        result = valueIfCollapsed;
                        break;

                    case Visibility.Hidden:
                        result = valueIfHidden;
                        break;

                    case Visibility.Visible:
                        result = valueIfVisible;
                        break;

                    default:
                        throw new NotSupportedException("value");
                }
            }
            else
            {
                result = GetLastParamValue(@params,
                                           result,
                                           new Dictionary<string, Func<string, bool?>>
                                           {
                                               { _PARAM_NULL_IF_FALSE, (p) => false },
                                               { _PARAM_NULL_IF_NULL, (p) => null },
                                               { _PARAM_NULL_IF_TRUE, (p) => true },
                                           });
            }

            return result;
        }

        #endregion Methods
    }
}