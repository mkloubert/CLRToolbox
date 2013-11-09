// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting.Export
{
    /// <summary>
    /// Marks a method to export for a script.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,
                    AllowMultiple = false,
                    Inherited = false)]
    public sealed class ExportScriptFuncAttribute : Attribute
    {
        #region Fields (1)

        private string _alias;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportScriptFuncAttribute" /> class.
        /// </summary>
        /// <param name="alias">The value for <see cref="ExportScriptFuncAttribute.Alias" /> property.</param>
        public ExportScriptFuncAttribute(string alias)
        {
            this.Alias = alias;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportScriptFuncAttribute" /> class.
        /// </summary>
        public ExportScriptFuncAttribute()
            : this(null)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the alias that should be used when register method as function in script.
        /// </summary>
        public string Alias
        {
            get { return this._alias; }

            private set { this._alias = value; }
        }

        #endregion Properties
    }
}
