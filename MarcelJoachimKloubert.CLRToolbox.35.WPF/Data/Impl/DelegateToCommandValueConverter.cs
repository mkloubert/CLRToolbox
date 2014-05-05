// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Execution.Impl;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Input;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Data.Impl
{
    /// <summary>
    /// Converts a delegate to an WPF <see cref="ICommand" /> object.
    /// </summary>
    public class DelegateToCommandValueConverter : ValueConverterBase<Delegate, ICommand, object>
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateToCommandValueConverter" /> class.
        /// </summary>
        /// <param name="sync">The asynchronous object.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        public DelegateToCommandValueConverter(object sync)
            : base(sync)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateToCommandValueConverter" /> class.
        /// </summary>
        public DelegateToCommandValueConverter()
            : base()
        {
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <inheriteddoc />
        public override ICommand Convert(Delegate @delegate, object parameter, CultureInfo culture)
        {
            ICommand result = null;

            if (@delegate != null)
            {
                MethodInfo method = @delegate.Method;
                if (method != null)
                {
                    ParameterInfo[] @params = method.GetParameters();
                    if (@params.LongLength < 2)
                    {
                        object obj = @delegate.Target;

                        if (@params.LongLength == 0)
                        {
                            result = new SimpleCommand(new DelegateCommand.ExecuteHandlerNoParameter(delegate()
                                {
                                    method.Invoke(obj,
                                                  new object[0]);
                                }));
                        }
                        else
                        {
                            // one parameter

                            result = new SimpleCommand(new DelegateCommand.ExecuteHandler(delegate(object cp)
                                {
                                    method.Invoke(obj,
                                                  new object[] { parameter });
                                }));
                        }
                    }
                    else
                    {
                        throw new InvalidCastException();
                    }
                }
            }

            return result;
        }

        #endregion Methods
    }
}