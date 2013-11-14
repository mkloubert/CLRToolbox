// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.Scripting;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions
{
    static partial class ClrToolboxExtensionMethods
    {
        #region Methods (3)

        // Public Methods (3) 

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> as a wrapped <see cref="ScriptExecutorBase.SimplePredicate" />
        /// version of a <see cref="ScriptExecutorBase.SimpleAction" />.
        /// The <see cref="ScriptExecutorBase.SimpleAction" /> is wrapped in a try-catch-blocks and
        /// the wrapper returns <see langword="true" /> on succeed or <see langword="false" /> if execution fails.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the action in the script.</param>
        /// <param name="action">The action to invoke.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" /> and/or <paramref name="action" /> are <see langword="null" />.
        /// </exception>
        public static TExecutor SetSimplePredicateWrapper<TExecutor>(this TExecutor executor,
                                                                     IEnumerable<char> actionName,
                                                                     ScriptExecutorBase.SimpleAction action)
               where TExecutor : IScriptExecutor
        {
            ScriptHelper.SetSimplePredicateWrapper(executor,
                                                   actionName,
                                                   action);

            return executor;
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> as a wrapped <see cref="ScriptExecutorBase.SimplePredicate" />
        /// version of a <see cref="ScriptExecutorBase.SimpleAction" />.
        /// The <see cref="ScriptExecutorBase.SimpleAction" /> is wrapped in a try-catch-blocks and
        /// the wrapper returns <see langword="true" /> on succeed or <see langword="false" /> if execution fails.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the action in the script.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="beforeInvoke">The optional action to invoke BEFORE try-catch-block is executed.</param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" />, <paramref name="action" /> and/or <paramref name="beforeInvoke" /> are <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// An exception of <paramref name="beforeInvoke" /> invokation is rethrown.
        /// </remarks>
        public static TExecutor SetSimplePredicateWrapper<TExecutor>(this TExecutor executor,
                                                                     IEnumerable<char> actionName,
                                                                     ScriptExecutorBase.SimpleAction action,
                                                                     ScriptExecutorBase.SimpleAction beforeInvoke)
               where TExecutor : IScriptExecutor
        {
            ScriptHelper.SetSimplePredicateWrapper(executor,
                                                   actionName,
                                                   action,
                                                   beforeInvoke);

            return executor;
        }

        /// <summary>
        /// Sets a function for a <see cref="IScriptExecutor" /> as a wrapped <see cref="ScriptExecutorBase.SimplePredicate" />
        /// version of a <see cref="ScriptExecutorBase.SimpleAction" />.
        /// The <see cref="ScriptExecutorBase.SimpleAction" /> is wrapped in a try-catch-blocks and
        /// the wrapper returns <see langword="true" /> on succeed or <see langword="false" /> if execution fails.
        /// </summary>
        /// <typeparam name="TExecutor">Type of the script executor.</typeparam>
        /// <param name="executor">The script executor.</param>
        /// <param name="actionName">The name of the action in the script.</param>
        /// <param name="action">The action to invoke.</param>
        /// <param name="beforeInvoke">The optional action to invoke BEFORE try-catch-block is executed.</param>
        /// <param name="rethrowBeforeInvokeException">
        /// Rethrow an exception of <paramref name="beforeInvoke" /> invokation or not.
        /// If <see langword="false" /> the wrapper will return <see langword="null" /> instead of a boolean value.
        /// </param>
        /// <returns>The object in <paramref name="executor" />.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="executor" />, <paramref name="action" /> and/or <paramref name="beforeInvoke" /> are <see langword="null" />.
        /// </exception>
        public static TExecutor SetSimplePredicateWrapper<TExecutor>(this TExecutor executor,
                                                                     IEnumerable<char> actionName,
                                                                     ScriptExecutorBase.SimpleAction action,
                                                                     ScriptExecutorBase.SimpleAction beforeInvoke,
                                                                     bool rethrowBeforeInvokeException)
               where TExecutor : IScriptExecutor
        {
            ScriptHelper.SetSimplePredicateWrapper(executor,
                                                   actionName,
                                                   action,
                                                   beforeInvoke,
                                                   rethrowBeforeInvokeException);

            return executor;
        }

        #endregion Methods
    }
}
