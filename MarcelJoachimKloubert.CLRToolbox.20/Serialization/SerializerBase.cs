// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Serialization
{
    /// <summary>
    /// A basic serializer.
    /// </summary>
    public abstract class SerializerBase : TMObject,
                                           ISerializer
    {
        #region Constructors (2)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="SerializerBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected SerializerBase(object syncRoot)
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializerBase" /> class.
        /// </summary>
        protected SerializerBase()
            : base()
        {
        }

        #endregion Constructors

        #region Methods (5)

        // Public Methods (3) 

        /// <inheriteddoc />
        public IDictionary<string, object> FromJson(IEnumerable<char> json)
        {
            return this.FromJson<IDictionary<string, object>>(json);
        }

        /// <inheriteddoc />
        public T FromJson<T>(IEnumerable<char> json)
        {
            string jsonStr = StringHelper.AsString(json);
            if (StringHelper.IsNullOrWhiteSpace(jsonStr))
            {
                return default(T);
            }

            T result = default(T);
            this.OnFromJson<T>(jsonStr, ref result);

            return result;
        }

        /// <inheriteddoc />
        public string ToJson<T>(T obj)
        {
            if (obj == null ||
                DBNull.Value.Equals(obj))
            {
                return "null";
            }

            StringBuilder jsonBuilder = new StringBuilder();
            this.OnToJson<T>(obj, ref jsonBuilder);

            return jsonBuilder != null ? jsonBuilder.ToString() : null;
        }

        // Protected Methods (2) 

        /// <summary>
        /// The logic for <see cref="SerializerBase.FromJson{T}(IEnumerable{char})" /> method.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <param name="json">The JSON string.</param>
        /// <param name="deserializedObj">The deserialized object.</param>
        protected abstract void OnFromJson<T>(string json, ref T deserializedObj);

        /// <summary>
        /// The logic for <see cref="SerializerBase.ToJson{T}(T)" /> method.
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize.</typeparam>
        /// <param name="objToSerialize">Object to serialize.</param>
        /// <param name="jsonBuilder">
        /// The <see cref="StringBuilder" /> to write the JSON data to.
        /// <see langword="null" /> indicates to return <see langword="null" /> in
        /// <see cref="SerializerBase.ToJson{T}(T)" /> method.
        /// </param>
        protected abstract void OnToJson<T>(T objToSerialize, ref StringBuilder jsonBuilder);

        #endregion Methods
    }
}