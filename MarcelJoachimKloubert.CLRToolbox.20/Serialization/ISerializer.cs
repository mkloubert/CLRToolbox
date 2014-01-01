// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Serialization
{
    /// <summary>
    /// Describes a serializer.
    /// </summary>
    public interface ISerializer : ITMObject
    {
        #region Operations (3)

        /// <summary>
        /// Deserializes an object from a JSON string.
        /// </summary>
        /// <typeparam name="T">Type of the target object.</typeparam>
        /// <param name="json">The string from where to build the object from.</param>
        /// <returns>The target object.</returns>
        T FromJson<T>(IEnumerable<char> json);

        /// <summary>
        /// Deserializes an object from a JSON string as dictionary.
        /// </summary>
        /// <param name="json">The string from where to build the object from.</param>
        /// <returns>The dictionary with the data.</returns>
        IDictionary<string, object> FromJson(IEnumerable<char> json);

        /// <summary>
        /// Converts an object to a JSON string.
        /// </summary>
        /// <typeparam name="T">Type of of the object that should be serialized.</typeparam>
        /// <param name="obj">The object that should be serialized.</param>
        /// <returns>The JSON string of the object.</returns>
        string ToJson<T>(T obj);

        #endregion Operations
    }
}
