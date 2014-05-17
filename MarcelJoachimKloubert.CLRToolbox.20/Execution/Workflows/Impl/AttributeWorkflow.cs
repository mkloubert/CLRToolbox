// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Workflows.Impl
{
    #region CLASS: AttributeWorkflow

    /// <summary>
    /// A workflow that works with attributes.
    /// </summary>
    public class AttributeWorkflow : WorkflowBase
    {
        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(bool isThreadSafe, object syncRoot)
            : base(isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow" /> class.
        /// </summary>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        public AttributeWorkflow(bool isThreadSafe)
            : base(isThreadSafe)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public AttributeWorkflow(object syncRoot)
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow" /> class.
        /// </summary>
        /// <remarks>Object will NOT work thread safe.</remarks>
        public AttributeWorkflow()
            : base()
        {
        }

        #endregion CLASS: AttributeWorkflow

        #region Methods (19)
        
        // Public Methods (15) 

        /// <summary>
        /// Creates a new instance for a specific object.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj)
        {
            return new AttributeWorkflow<T>(obj);
        }
        
        /// <summary>
        /// Creates a new instance for a specific object and contract.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj, IEnumerable<char> contractName)
        {
            return new AttributeWorkflow<T>(obj, contractName);
        }

        /// <summary>
        /// Creates a new instance for a specific object and contract.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj, IEnumerable<char> contractName, bool isThreadSafe)
        {
            return new AttributeWorkflow<T>(obj, contractName, isThreadSafe);
        }

        /// <summary>
        /// Creates a new instance for a specific object and contract.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj, IEnumerable<char> contractName, object syncRoot)
        {
            return new AttributeWorkflow<T>(obj, contractName, syncRoot);
        }

        /// <summary>
        /// Creates a new instance for a specific object and contract.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj, IEnumerable<char> contractName, bool isThreadSafe, object syncRoot)
        {
            return new AttributeWorkflow<T>(obj, contractName, isThreadSafe, syncRoot);
        }

        /// <summary>
        /// Creates a new instance for a specific object and contract.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contract">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj, Type contract, bool isThreadSafe)
        {
            return new AttributeWorkflow<T>(obj, contract, isThreadSafe);
        }

        /// <summary>
        /// Creates a new instance for a specific object and contract.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contract">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj, Type contract, object syncRoot)
        {
            return new AttributeWorkflow<T>(obj, contract, syncRoot);
        }

        /// <summary>
        /// Creates a new instance for a specific object and contract.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contract">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public static AttributeWorkflow<T> Create<T>(T obj, Type contract, bool isThreadSafe, object syncRoot)
        {
            return new AttributeWorkflow<T>(obj, contract, isThreadSafe, syncRoot);
        }

        /// <summary>
        /// Starts execution an uses that instance as object with a specific a contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>The execution result.</returns>
        /// <exception cref="NullReferenceException">
        /// <paramref name="contract" /> is <see langword="null" />.
        /// </exception>
        public object Execute(Type contract)
        {
            return this.Execute(contract.FullName);
        }

        /// <summary>
        /// Starts execution and uses that instance as related object with a specific a contract.
        /// </summary>
        /// <param name="contractName">The name of the contract.</param>
        /// <returns>The execution result.</returns>
        public object Execute(IEnumerable<char> contractName)
        {
            return this.ExecuteFor(this, contractName);
        }

        /// <summary>
        /// Starts execution by using a specific object with a specific a contract.
        /// </summary>
        /// <param name="obj">The object to use.</param>
        /// <returns>The execution result.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public object ExecuteFor(object obj)
        {
            return this.ExecuteFor(obj, (IEnumerable<char>)null);
        }

        /// <summary>
        /// Starts execution by using a specific object with a specific a contract.
        /// </summary>
        /// <param name="obj">The object to use.</param>
        /// <param name="contract">The contract.</param>
        /// <returns>The execution result.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// <paramref name="contract" /> is <see langword="null" />.
        /// </exception>
        public object ExecuteFor(object obj, Type contract)
        {
            return this.ExecuteFor(obj, contract.FullName);
        }

        /// <summary>
        /// Starts execution by using a specific object WITHOUT a contract.
        /// </summary>
        /// <param name="obj">The object to use.</param>
        /// <param name="contractName">The name of the contract.</param>
        /// <returns>The execution result.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public object ExecuteFor(object obj, IEnumerable<char> contractName)
        {
            object result = null;
            CollectionHelper.ForEach(this.GetFunctionIterator(obj, contractName),
                                     delegate(IForEachItemExecutionContext<WorkflowFunc> ctx)
                                     {
                                         result = ctx.Item();
                                     });

            return result;
        }

        // Protected Methods (2) 

        /// <inheriteddoc />
        protected override IEnumerable<WorkflowFunc> GetFunctionIterator()
        {
            return this.GetFunctionIterator(this, null);
        }

        /// <summary>
        /// Returns the iterator for <see cref="WorkflowBase.GetEnumerator()" />.
        /// </summary>
        /// <param name="obj">The underlying object.</param>
        /// <param name="contractName">The name of the contract.</param>
        /// <returns>The iterator.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        protected IEnumerable<WorkflowFunc> GetFunctionIterator(object obj, IEnumerable<char> contractName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            string contract = WorkflowAttributeBase.ParseContractName(contractName);

            Type type = obj.GetType();
            IEnumerable<MethodInfo> allMethods = GetMethodsByType(type);

            List<Exception> occuredErrors = new List<Exception>();

            // find start method
            MethodInfo currentMethod = null;
            {
                IEnumerable<MethodInfo> methodsWithAttribs =
                    CollectionHelper.Where(allMethods,
                                           delegate(MethodInfo method)
                                           {
                                               // search for 'ReceiveNotificationFromAttribute'
                                               object[] attribs = method.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Execution.Workflows.WorkflowStartAttribute),
                                                                                             true);

                                               // strong typed sequence
                                               IEnumerable<WorkflowStartAttribute> wfStartAttribs =
                                                   CollectionHelper.OfType<WorkflowStartAttribute>(attribs);

                                               // filter by contract name
                                               IEnumerable<WorkflowStartAttribute> wfStartAttribForMethod =
                                                   CollectionHelper.Where(wfStartAttribs,
                                                                          delegate(WorkflowStartAttribute a)
                                                                          {
                                                                              return a.Contract == contract;
                                                                          });

                                               return CollectionHelper.Any(wfStartAttribForMethod);
                                           });

                currentMethod = CollectionHelper.SingleOrDefault(methodsWithAttribs);
            }

            IDictionary<string, object> execVars = this.CreateVarStorage();
            bool hasBeenCanceled = false;
            long index = -1;
            IReadOnlyDictionary<string, object> previousVars = null;
            object result = null;
            object syncRoot = new object();
            bool throwErrors = true;
            while ((hasBeenCanceled == false) &&
                   (currentMethod != null))
            {
                yield return delegate(object[] args)
                    {
                        SimpleWorkflowExecutionContext ctx = new SimpleWorkflowExecutionContext();
                        ctx.Cancel = false;
                        ctx.ContinueOnError = false;
                        ctx.ExecutionArguments = new TMReadOnlyList<object>(args ?? new object[] { null });
                        ctx.ExecutionVars = execVars;
                        ctx.HasBeenCanceled = hasBeenCanceled;
                        ctx.Index = ++index;
                        ctx.NextVars = this.CreateVarStorage();
                        ctx.PreviousVars = previousVars;
                        ctx.Result = result;
                        ctx.SyncRoot = syncRoot;
                        ctx.ThrowErrors = throwErrors;
                        ctx.Workflow = this;
                        ctx.WorkflowVars = this.Vars;

                        // first try to find method for next step
                        MethodInfo nextMethod = null;
                        {
                            // search for 'NextWorkflowStepAttribute'
                            object[] attribs = currentMethod.GetCustomAttributes(typeof(global::MarcelJoachimKloubert.CLRToolbox.Execution.Workflows.NextWorkflowStepAttribute),
                                                                                 true);

                            // strong typed sequence
                            IEnumerable<NextWorkflowStepAttribute> nextStepAttribs = CollectionHelper.OfType<NextWorkflowStepAttribute>(attribs);

                            // filter by contract name
                            NextWorkflowStepAttribute nextStep = CollectionHelper.SingleOrDefault(nextStepAttribs,
                                                                                                  delegate(NextWorkflowStepAttribute a)
                                                                                                  {
                                                                                                      return a.Contract == contract;
                                                                                                  });

                            if (nextStep != null)
                            {
                                IEnumerable<MethodInfo> nextMethods = CollectionHelper.Where(allMethods,
                                                                                             delegate(MethodInfo m)
                                                                                             {
                                                                                                 return m.Name == nextStep.Member;
                                                                                             });

                                nextMethod = CollectionHelper.SingleOrDefault(nextMethods,
                                                                              delegate(MethodInfo m)
                                                                              {
                                                                                  return m.GetParameters().Length > 0;
                                                                              });

                                if (nextMethod == null)
                                {
                                    nextMethod = CollectionHelper.Single(nextMethods,
                                                                         delegate(MethodInfo m)
                                                                         {
                                                                             return m.GetParameters().Length < 1;
                                                                         });
                                }
                            }
                        }

                        // execution
                        InvokeWorkflowMethod(obj, currentMethod,
                                             ctx,
                                             occuredErrors);

                        WorkflowActionNoState nextAction = ctx.Next;
                        if (nextAction == null)
                        {
                            currentMethod = nextMethod;
                        }
                        else
                        {
                            obj = nextAction.Target;
                            currentMethod = nextAction.Method;

                            type = currentMethod.ReflectedType;
                            allMethods = GetMethodsByType(type);
                        }

                        previousVars = new TMReadOnlyDictionary<string, object>(ctx.NextVars);
                        throwErrors = ctx.ThrowErrors;

                        if (ctx.Cancel)
                        {
                            hasBeenCanceled = true;
                            ctx.HasBeenCanceled = hasBeenCanceled;
                        }

                        return ctx;
                    };
            }

            if (throwErrors &&
                (occuredErrors.Count > 0))
            {
                throw new AggregateException(occuredErrors);
            }
        }

        // Private Methods (4) 

        private static IEnumerable<MethodInfo> GetMethodsByType(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                   BindingFlags.Instance | BindingFlags.Static);
        }

        private static void InvokeWorkflowMethod(object obj, MethodInfo method,
                                                 IWorkflowExecutionContext ctx,
                                                 ICollection<Exception> occuredErrors)
        {
            try
            {
                object[] methodParams;
                if (method.GetParameters().Length < 1)
                {
                    methodParams = new object[0];
                }
                else
                {
                    methodParams = new object[] { ctx };
                }

                method.Invoke(obj, methodParams);
            }
            catch (Exception ex)
            {
                occuredErrors.Add(ex);

                if (ctx.ContinueOnError == false)
                {
                    throw;
                }
            }
        }

        private object ExecutFor_NonThreadSafe(object obj, IEnumerable<char> contract)
        {
            Func<object, IEnumerable<char>, object> funcToInvoke;
            if (this.Synchronized)
            {
                funcToInvoke = this.ExecutFor_ThreadSafe;
            }
            else
            {
                funcToInvoke = this.ExecutFor_NonThreadSafe;
            }

            return funcToInvoke(obj, contract);
        }

        private object ExecutFor_ThreadSafe(object obj, IEnumerable<char> contract)
        {
            object result;

            lock (this._SYNC)
            {
                result = this.ExecutFor_NonThreadSafe(obj, contract);
            }

            return result;
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: AttributeWorkflow<TObj>

    /// <summary>
    /// An <see cref="AttributeWorkflow" /> with a default object and contract.
    /// </summary>
    /// <typeparam name="TObj">Type of the default object.</typeparam>
    public class AttributeWorkflow<TObj> : AttributeWorkflow
    {
        #region Fields (2)

        private readonly TObj _OBJECT;
        private readonly string _CONTRACT;

        #endregion Fields

        #region Constructors (11)

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contract">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, Type contract, bool isThreadSafe, object syncRoot)
            : this(obj, WorkflowAttributeBase.GetContractName(contract), isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contract">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, Type contract, bool isThreadSafe)
            : this(obj, contract, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contract">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, Type contract, object syncRoot)
            : this(obj, contract, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, IEnumerable<char> contractName, bool isThreadSafe, object syncRoot)
            : base(isThreadSafe, syncRoot)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            this._OBJECT = obj;
            this._CONTRACT = StringHelper.AsString(contractName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, IEnumerable<char> contractName, bool isThreadSafe)
            : this(obj, contractName, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, IEnumerable<char> contractName, object syncRoot)
            : this(obj, contractName, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, bool isThreadSafe, object syncRoot)
            : this(obj, (IEnumerable<char>)null, isThreadSafe, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="isThreadSafe">Instance should work thread safe or not.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, bool isThreadSafe)
            : this(obj, isThreadSafe, new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="syncRoot">The value for <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, object syncRoot)
            : this(obj, false, syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <param name="contractName">The value for the <see cref="AttributeWorkflow{TObj}.Contract" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj, IEnumerable<char> contractName)
            : this(obj, contractName, false)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWorkflow{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for the <see cref="AttributeWorkflow{TObj}.Object" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        public AttributeWorkflow(TObj obj)
            : this(obj, (IEnumerable<char>)null)
        {
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets the default contract name.
        /// </summary>
        public string Contract
        {
            get { return this._CONTRACT; }
        }

        /// <summary>
        /// Gets the default object.
        /// </summary>
        public TObj Object
        {
            get { return this._OBJECT; }
        }

        #endregion Methods

        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override IEnumerable<WorkflowFunc> GetFunctionIterator()
        {
            return this.GetFunctionIterator(this.Object,
                                            this.Contract);
        }

        #endregion Methods
    }

    #endregion
}