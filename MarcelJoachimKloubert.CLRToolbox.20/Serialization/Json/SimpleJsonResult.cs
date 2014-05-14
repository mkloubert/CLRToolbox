// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

namespace MarcelJoachimKloubert.CLRToolbox.Serialization.Json
{
    #region CLASS: SimpleJsonResult<T>

    /// <summary>
    /// Describes a simple result object that can be (de)serialized via JSON.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="SimpleJsonResult{T}.tag" /> property.</typeparam>
    public partial class SimpleJsonResult<T>
    {
        #region Fields (3)

        private int? _code;
        private string _msg;
        private T _tag;

        #endregion Fields

        #region Properties (3)

        /// <summary>
        /// Gets or sets the result code.
        /// </summary>
        public int? code
        {
            get { return this._code; }

            set { this._code = value; }
        }

        /// <summary>
        /// Gets or sets the result message.
        /// </summary>
        public string msg
        {
            get { return this._msg; }

            set { this._msg = value; }
        }

        /// <summary>
        /// Gets or sets the optional object to serialize.
        /// </summary>
        public T tag
        {
            get { return this._tag; }

            set { this._tag = value; }
        }

        #endregion Properties
    }

    #endregion

    #region CLASS: SimpleJsonResult

    /// <summary>
    /// Describes a simple result object that can be (de)serialized via JSON.
    /// </summary>
    public sealed partial class SimpleJsonResult : SimpleJsonResult<object>
    {
    }

    #endregion
}