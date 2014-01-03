// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Globalization;
using System.Windows.Input;
using MarcelJoachimKloubert.CLRToolbox.Execution;
using MarcelJoachimKloubert.CLRToolbox.Windows.Input;

namespace MarcelJoachimKloubert.CLRToolbox.Windows.Data
{
    /// <summary>
    /// Basic value converter that converts <see cref="global::MarcelJoachimKloubert.CLRToolbox.Execution.ICommand{TParam}" /> to
    /// <see cref="global::System.Windows.Input.ICommand" /> and back.
    /// </summary>
    /// <typeparam name="TParam">Type of the command parameters.</typeparam>
    public abstract class CommandValueConverterBase<TParam> : ValueConverterBase<global::MarcelJoachimKloubert.CLRToolbox.Execution.ICommand<TParam>, global::System.Windows.Input.ICommand, TParam>
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValueConverterBase{TParam}" /> class.
        /// </summary>
        /// <param name="sync">The asynchronous object.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected CommandValueConverterBase(object sync)
            : base(sync)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValueConverterBase{TParam}" /> class.
        /// </summary>
        protected CommandValueConverterBase()
            : base()
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase{TInput, TOutput, TParam}.Convert(TInput, TParam, CultureInfo)" />
        public override sealed ICommand Convert(ICommand<TParam> inputCmd, TParam parameter, CultureInfo culture)
        {
            ICommand result = null;

            if (inputCmd != null)
            {
                result = inputCmd as ICommand;
                if (result == null)
                {
                    // needs wrapper

                    result = new SimpleCommand<TParam>(
                        delegate(TParam p)
                        {
                            inputCmd.Execute(parameter);
                        },
                        delegate(TParam p)
                        {
                            return inputCmd.CanExecute(parameter);
                        });
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ValueConverterBase{TInput, TOutput, TParam}.ConvertBack(TOutput, TParam, CultureInfo)" />
        public override sealed ICommand<TParam> ConvertBack(ICommand inputCmd, TParam parameter, CultureInfo culture)
        {
            ICommand<TParam> result = null;

            if (inputCmd != null)
            {
                result = inputCmd as ICommand<TParam>;
                if (result == null)
                {
                    // needs wrapper

                    result = new SimpleCommand<TParam>(
                        delegate(TParam p)
                        {
                            inputCmd.Execute(parameter);
                        },
                        delegate(TParam p)
                        {
                            return inputCmd.CanExecute(parameter);
                        });
                }
            }

            return result;
        }

        #endregion Methods
    }
}
