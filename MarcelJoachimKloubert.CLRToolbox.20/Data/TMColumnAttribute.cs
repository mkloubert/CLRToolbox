// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Marks a property or field as a column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
                    AllowMultiple = false,
                    Inherited = false)]
    public sealed class TMColumnAttribute : Attribute
    {
        #region Fields (5)

        private Type _clrType;
        private string _dataType;
        private bool _isKey;
        private bool _isNullable;
        private string _name;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="TMColumnAttribute" /> class.
        /// </summary>
        public TMColumnAttribute()
        {

        }

        #endregion Constructors

        #region Properties (5)

        /// <summary>
        /// Gets or sets the CLR data type of the column.
        /// </summary>
        public Type ClrType
        {
            get { return this._clrType; }

            set { this._clrType = value; }
        }

        /// <summary>
        /// Gets or sets the name of the data type of the column.
        /// </summary>
        public string DataType
        {
            get { return this._dataType; }

            set { this._dataType = value; }
        }

        /// <summary>
        /// Gets or sets if that column is a (primary) key or not.
        /// </summary>
        public bool IsKey
        {
            get { return this._isKey; }

            set { this._isKey = value; }
        }

        /// <summary>
        /// Gets or sets if that column is nullable or not.
        /// </summary>
        public bool IsNullable
        {
            get { return this._isNullable; }

            set { this._isNullable = value; }
        }

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string Name
        {
            get { return this._name; }

            set { this._name = value; }
        }

        #endregion Properties
    }
}
