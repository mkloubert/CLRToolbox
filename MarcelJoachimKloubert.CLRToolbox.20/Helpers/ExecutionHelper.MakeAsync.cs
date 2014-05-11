// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

#if !(NET2 || NET20 || NET35 || WINDOWS_PHONE || MONO2 || MONO20)
#define KNOWS_TASKS
#endif

using MarcelJoachimKloubert.CLRToolbox.Execution;
using MarcelJoachimKloubert.CLRToolbox.Execution.Workflows;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    partial class ExecutionHelper
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Makes the execution of an <see cref="IWorkflow" /> async.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
        /// <returns>The async logic.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> is <see langword="null" />.
        /// </exception>
        public static Action MakeAsync(IWorkflow workflow)
        {
            return MakeAsync(workflow,
                             (Action<IAsyncExecutionResult>)null);
        }

        /// <summary>
        /// Makes the execution of an <see cref="IWorkflow" /> async.
        /// </summary>
        /// <param name="workflow">The workflow.</param>
        /// <param name="completedAction">
        /// The optional action that is invoked after the operation has been completed.
        /// </param>
        /// <returns>The async logic.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> is <see langword="null" />.
        /// </exception>
        public static Action MakeAsync(IWorkflow workflow, Action<IAsyncExecutionResult> completedAction)
        {
            return MakeAsync<object>(workflow,
                                     (object)null,
                                     delegate(IAsyncExecutionResult<object> result)
                                     {
                                         if (completedAction != null)
                                         {
                                             completedAction(result);
                                         }
                                     });
        }

        /// <summary>
        /// Makes the execution of an <see cref="IWorkflow" /> async.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="workflow">The workflow.</param>
        /// <param name="state">
        /// The state object for <paramref name="completedAction" />.
        /// </param>
        /// <param name="completedAction">
        /// The optional action that is invoked after the operation has been completed.
        /// </param>
        /// <returns>The async logic.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> is <see langword="null" />.
        /// </exception>
        public static Action MakeAsync<S>(IWorkflow workflow, S state, Action<IAsyncExecutionResult<S>> completedAction)
        {
            return MakeAsync<S>(workflow,
                                delegate(IWorkflow wf)
                                {
                                    return state;
                                }, completedAction);
        }

        /// <summary>
        /// Makes the execution of an <see cref="IWorkflow" /> async.
        /// </summary>
        /// <typeparam name="S">Type of the state object.</typeparam>
        /// <param name="workflow">The workflow.</param>
        /// <param name="stateFactory">
        /// The function that produces the state object for <paramref name="completedAction" />.
        /// </param>
        /// <param name="completedAction">
        /// The optional action that is invoked after the operation has been completed.
        /// </param>
        /// <returns>The async logic.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="workflow" /> and/or <paramref name="stateFactory" /> are <see langword="null" />.
        /// </exception>
        public static Action MakeAsync<S>(IWorkflow workflow, Func<IWorkflow, S> stateFactory, Action<IAsyncExecutionResult<S>> completedAction)
        {
            if (workflow == null)
            {
                throw new ArgumentNullException("workflow");
            }

            if (stateFactory == null)
            {
                throw new ArgumentNullException("stateFactory");
            }

            return delegate()
                {
                    List<Exception> occuredErrors = new List<Exception>();
                    object syncErrList = new object();

                    Action<object> invokeCompletedAction = delegate(object res)
                        {
                            if (completedAction != null)
                            {
                                SimpleAsyncExecutionResult<S> ctx = new SimpleAsyncExecutionResult<S>();
                                ctx.Errors = occuredErrors;
                                ctx.Result = res;
                                ctx.State = stateFactory(workflow);

                                completedAction(ctx);
                            }
                        };

                    Action asyncAction = delegate()
                        {
                            object result = null;

                            try
                            {
                                result = workflow.Execute();
                            }
                            catch (Exception ex)
                            {
                                result = null;

                                lock (syncErrList)
                                {
                                    occuredErrors.Add(ex);
                                }
                            }
                            finally
                            {
                                invokeCompletedAction(result);
                            }
                        };

                    try
                    {
#if KNOWS_TASKS
                        global::System.Threading.Tasks.Task.Factory.StartNew(asyncAction);
#else
                        global::System.Threading.ThreadPool.QueueUserWorkItem(delegate(object state)
                            {
                                asyncAction();
                            });
#endif
                    }
                    catch (Exception ex)
                    {
                        lock (syncErrList)
                        {
                            occuredErrors.Add(ex);
                        }

                        invokeCompletedAction(null);
                    }
                };
        }

        #endregion Methods
    }
}