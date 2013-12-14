// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Templates
{
    /// <summary>
    /// A basic template.
    /// </summary>
    public abstract partial class TemplateBase : TMObject, ITemplate
    {
        #region Fields (1)

        private readonly Dictionary<string, object> _VARS = new Dictionary<string, object>();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected TemplateBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateBase" /> class.
        /// </summary>
        protected TemplateBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.this[IEnumerable{char}]" />
        public object this[IEnumerable<char> name]
        {
            get
            {
                object result;
                lock (this._SYNC)
                {
                    if (!this._VARS.TryGetValue(this.NormalizeVarName(name), out result))
                    {
                        throw new ArgumentOutOfRangeException("name");
                    }
                }

                return result;
            }

            set { this.SetVar(name, value); }
        }

        #endregion Properties

        #region Methods (9)

        // Public Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.GetAllVars()" />
        public virtual IDictionary<string, object> GetAllVars()
        {
            IDictionary<string, object> result;
            lock (this._SYNC)
            {
                result = new Dictionary<string, object>();
                CollectionHelper.AddRange(result, this._VARS);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.SetVar(IEnumerable{char}, object)" />
        public TemplateBase SetVar(IEnumerable<char> name, object value)
        {
            return this.SetVar<object>(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.SetVar{T}(IEnumerable{char}, T)" />
        public TemplateBase SetVar<T>(IEnumerable<char> name, T value)
        {
            lock (this._SYNC)
            {
                this.OnSetVar<T>(this.NormalizeVarName(name), value);
            }

            return this;
        }
        // Protected Methods (3) 

        /// <summary>
        /// Normalizes a variable name.
        /// </summary>
        /// <param name="name">The input value.</param>
        /// <returns>The normalized value.</returns>
        protected string NormalizeVarName(IEnumerable<char> name)
        {
            return (StringHelper.AsString(name) ?? string.Empty).Trim();
        }

        /// <summary>
        /// The logic for the <see cref="TemplateBase" /> method.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="varName">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        protected virtual void OnSetVar<T>(string varName, T value)
        {
            this._VARS[varName ?? string.Empty] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITemplate.Render()" />
        protected abstract object RenderContent();
        // Private Methods (3) 

        object ITemplate.Render()
        {
            return this.RenderContent();
        }

        ITemplate ITemplate.SetVar(IEnumerable<char> name, object value)
        {
            return this.SetVar(name, value);
        }

        ITemplate ITemplate.SetVar<T>(IEnumerable<char> name, T value)
        {
            return this.SetVar<T>(name, value);
        }

        #endregion Methods
    }
}
