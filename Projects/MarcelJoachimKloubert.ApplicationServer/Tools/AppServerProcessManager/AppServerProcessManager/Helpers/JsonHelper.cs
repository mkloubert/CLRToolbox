// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using Newtonsoft.Json;

namespace AppServerProcessManager.Helpers
{
    /// <summary>
    /// Helper class for JSON operations.
    /// </summary>
    public static class JsonHelper
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Deserializes an object.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="stream">The stream that contains the JSON string (UTF-8).</param>
        /// <param name="enc">The charset to use the JSON string is encoded by.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> is <see langword="null" />.
        /// </exception>
        public static T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>(stream, Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes a JSON string.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="jsonChars">The serialuzed object as JSON string.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(IEnumerable<char> jsonChars)
        {
            var json = jsonChars.AsString();
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }

            var serializer = new JsonSerializer();

            using (var sr = new StringReader(json))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<T>(reader);
                }
            }
        }

        /// <summary>
        /// Deserializes an object.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="stream">The stream that contains the JSON string.</param>
        /// <param name="enc">The charset to use the JSON string is encoded by.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="stream" /> and/or <paramref name="enc" /> are <see langword="null" />.
        /// </exception>
        public static T Deserialize<T>(Stream stream, Encoding enc)
        {
            stream.ThrowIfNull(() => stream);

            MemoryStream ms = null;
            var disposeMs = false;

            try
            {
                ms = stream as MemoryStream;
                if (ms == null)
                {
                    disposeMs = true;

                    ms = new MemoryStream();
                    stream.CopyTo(ms);
                }

                return Deserialize<T>(enc.GetString(ms.ToArray()));
            }
            finally
            {
                if (ms != null && disposeMs)
                {
                    ms.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes an object as JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns><paramref name="obj" /> as JSON string.</returns>
        public static string Serialize<T>(T obj)
        {
            if (obj == null ||
                DBNull.Value.Equals(obj))
            {
                return "null";
            }

            var serializer = new JsonSerializer();

            using (var sw = new StringWriter())
            {
                using (var writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, obj);

                    writer.Flush();
                    writer.Close();
                }

                sw.Flush();
                sw.Close();

                return sw.ToString();
            }
        }

        #endregion Methods
    }
}
