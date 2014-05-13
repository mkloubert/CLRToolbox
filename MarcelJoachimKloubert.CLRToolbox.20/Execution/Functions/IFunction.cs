// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Functions
{
    /// <summary>
    /// Describes a function.
    /// </summary>
    public interface IFunction : IIdentifiable, IHasName
    {
        #region Data Members (2)

        /// <summary>
        /// Gets the full name of the function.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the namespace the function belongs to.
        /// </summary>
        string Namespace { get; }

        #endregion Data Members

        #region Operations (4)

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <returns>The execution context.</returns>
        /// <remarks>Logic is executed automatically.</remarks>
        /// <exception cref="ArgumentException">Empty input parameters are invalid.</exception>
        IFunctionExecutionContext Execute();

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <param name="autoStart">Auto start logic or not.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ArgumentException">Empty input parameters are invalid.</exception>
        IFunctionExecutionContext Execute(bool autoStart);

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <param name="parameters">The inut parameters for the execution.</param>
        /// <returns>The execution context.</returns>
        /// <remarks>Logic is executed automatically.</remarks>
        /// <exception cref="ArgumentException">Input parameters are invalid.</exception>
        IFunctionExecutionContext Execute(IEnumerable<KeyValuePair<string, object>> parameters);

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <param name="parameters">The inut parameters for the execution.</param>
        /// <param name="autoStart">Auto start logic or not.</param>
        /// <returns>The execution context.</returns>
        /// <exception cref="ArgumentException">Input parameters are invalid.</exception>
        IFunctionExecutionContext Execute(IEnumerable<KeyValuePair<string, object>> parameters,
                                          bool autoStart);

        #endregion Operations
    }
}