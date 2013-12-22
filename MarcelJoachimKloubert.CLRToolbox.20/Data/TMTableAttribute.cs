// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Marks a type as a table.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,
                    AllowMultiple = false,
                    Inherited = false)]
    public sealed class TMTableAttribute : Attribute
    {
        #region Fields (2)

        private string _name;
        private string _schema;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMTableAttribute"/> class.
        /// </summary>
        public TMTableAttribute()
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string Name
        {
            get { return this._name; }

            set { this._name = value; }
        }

        /// <summary>
        /// Gets or sets the name of the schema where the table is stored.
        /// </summary>
        public string Schema
        {
            get { return this._schema; }

            set { this._schema = value; }
        }

        #endregion Properties
    }
}
