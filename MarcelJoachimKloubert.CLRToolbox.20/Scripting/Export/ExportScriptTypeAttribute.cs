// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Scripting.Export
{
    /// <summary>
    /// Marks a type to expose in a script.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Delegate |
                    AttributeTargets.Enum |
                    AttributeTargets.Struct,
                    AllowMultiple = false,
                    Inherited = false)]
    public sealed class ExportScriptTypeAttribute : Attribute
    {
        #region Fields (1)

        private string _alias;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportScriptTypeAttribute" /> class.
        /// </summary>
        /// <param name="alias">The value for <see cref="ExportScriptTypeAttribute.Alias" /> property.</param>
        public ExportScriptTypeAttribute(string alias)
        {
            this.Alias = alias;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportScriptTypeAttribute" /> class.
        /// </summary>
        public ExportScriptTypeAttribute()
            : this(null)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the alias that should be used when expose type in script.
        /// </summary>
        public string Alias
        {
            get { return this._alias; }

            private set { this._alias = value; }
        }

        #endregion Properties
    }
}
