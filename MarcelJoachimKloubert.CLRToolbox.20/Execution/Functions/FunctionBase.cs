// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Globalization;
using MarcelJoachimKloubert.CLRToolbox.Collections.ObjectModel;
using MarcelJoachimKloubert.CLRToolbox.Factories;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Execution.Functions
{
    /// <summary>
    /// A basic function.
    /// </summary>
    public abstract partial class FunctionBase : TMObject,
                                                 IFunction
    {
        #region Fields (1)

        private Guid _id;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionBase" /> class.
        /// </summary>
        /// <param name="id">The ID of that function.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected FunctionBase(Guid id, object syncRoot)
            : base(syncRoot)
        {
            this._id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionBase" /> class.
        /// </summary>
        /// <param name="id">The ID of that function.</param>
        protected FunctionBase(Guid id)
            : this(id, new object())
        {

        }

        #endregion Constructors

        #region Properties (8)

        /// <summary>
        /// Gets the default result code that describes a successful execution of that function.
        /// </summary>
        public virtual int DefaultResultCodeForSuccessfulExecution
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the default result message that describes a successful execution of that function.
        /// </summary>
        public virtual IEnumerable<char> DefaultResultMessageForSuccessfulExecution
        {
            get { return null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.DisplayName" />
        public string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IFunction.FullName" />
        public string FullName
        {
            get
            {
                string result = string.Empty;

                string @ns = this.Namespace;
                if (@ns != null)
                {
                    result = @ns;
                }

                string name = this.Name;
                if (name != null)
                {
                    if (result.Length > 0)
                    {
                        result += ".";
                    }

                    result += name;
                }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IIdentifiable.Id" />
        public Guid Id
        {
            get { return this._id; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.Name" />
        public virtual string Name
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IFunction.Namespace" />
        public virtual string Namespace
        {
            get
            {
                string result = (this.GetType().Namespace ?? string.Empty).Trim();

                return result != string.Empty ? result : null;
            }
        }

        /// <summary>
        /// Gets the result code that describes an uncaught exception in an execution of that function.
        /// </summary>
        public virtual int ResultCodeForUncaughtException
        {
            get { return -1; }
        }

        #endregion Properties

        #region Methods (16)

        // Public Methods (10) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
        public override bool Equals(object other)
        {
            if (other is IIdentifiable)
            {
                return this.Equals(other as IIdentifiable);
            }

            if (other is Guid)
            {
                return this.Equals((Guid)other);
            }

            return base.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IIdentifiable other)
        {
            return other != null ? this.Equals(other.Id) : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(Guid other)
        {
            return this.Id == other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IFunction.Execute()" />
        public IFunctionExecutionContext Execute()
        {
            return this.Execute(CollectionHelper.Empty<KeyValuePair<string, object>>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IFunction.Execute(bool)" />
        public IFunctionExecutionContext Execute(bool autoStart)
        {
            return this.Execute(CollectionHelper.Empty<KeyValuePair<string, object>>(),
                                autoStart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IFunction.Execute(IEnumerable{KeyValuePair{string, object}}, bool)" />
        public IFunctionExecutionContext Execute(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return this.Execute(parameters, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IFunction.Execute(IEnumerable{KeyValuePair{string, object}}, bool)" />
        public IFunctionExecutionContext Execute(IEnumerable<KeyValuePair<string, object>> parameters,
                                                 bool autoStart)
        {
            FunctionExecutionContext result = new FunctionExecutionContext();
            result.Function = this;

            // prepare and check input parameters
            {
                IEnumerable<KeyValuePair<string, object>> normalizedParams = parameters ?? CollectionHelper.Empty<KeyValuePair<string, object>>();

                IDictionary<string, object> paramsDict = this.CreateEmptyParameterCollection();
                foreach (KeyValuePair<string, object> item in normalizedParams)
                {
                    paramsDict.Add(item.Key ?? string.Empty,
                                   this.ParseValueForParameter(item.Value));
                }

                result.InputParameters = new TMReadOnlyDictionary<string, object>(paramsDict);
                if (!this.CheckInputParameters(result.InputParameters))
                {
                    throw new ArgumentException("parameters");
                }
            }

            if (autoStart)
            {
                result.Start();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.GetDisplayName(CultureInfo)" />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return StringHelper.AsString(this.OnGetDisplayName(culture));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()" />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return string.Format("{0} ({1}) :: #{2}", this.FullName
                                                    , this.Id
                                                    , this.GetHashCode());
        }
        // Protected Methods (6) 

        /// <summary>
        /// Validates input parameters.
        /// </summary>
        /// <param name="inputParams">The parameters to validate.</param>
        /// <returns>Are valid or not.</returns>
        protected bool CheckInputParameters(IReadOnlyDictionary<string, object> inputParams)
        {
            return inputParams != null;
        }

        /// <summary>
        /// Creates an empty collection for parameters.
        /// </summary>
        /// <returns>The created collection.</returns>
        protected virtual IDictionary<string, object> CreateEmptyParameterCollection()
        {
            return new Dictionary<string, object>(EqualityComparerFactory.CreateCaseInsensitiveStringComparer(true, true));
        }

        /// <summary>
        /// Returns the result message for an uncaught exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>The result message.</returns>
        protected virtual IEnumerable<char> GetResultMessageForException(Exception ex)
        {
            Exception innerEx = ex != null ? (ex.GetBaseException() ?? ex) : null;
            return innerEx != null ? (innerEx.Message ?? string.Empty).Trim() : null;
        }

        /// <summary>
        /// The logic that executes that function.
        /// </summary>
        /// <param name="context">The context.</param>
        protected abstract void OnExecute(OnExecuteContext context);

        /// <summary>
        /// The logic for <see cref="FunctionBase.GetDisplayName(CultureInfo)" />.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The display name based on <paramref name="culture" />.</returns>
        protected virtual IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return string.Format("{0} ({1})", this.FullName
                                            , this.Id);
        }

        /// <summary>
        /// Parses an object for use as parameter value.
        /// </summary>
        /// <param name="obj">The object to parse / convert.</param>
        /// <returns>The parsed object.</returns>
        protected virtual object ParseValueForParameter(object obj)
        {
            object result = obj;
            if (DBNull.Value.Equals(result))
            {
                result = null;
            }

            return result;
        }

        #endregion Methods
    }
}
