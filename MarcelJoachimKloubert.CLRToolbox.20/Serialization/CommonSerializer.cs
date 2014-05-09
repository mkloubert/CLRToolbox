// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

#if !(NET2 || NET20 || NET35 || WINDOWS_PHONE || MONO2 || MONO20)
#define KNOWS_EXPANDO_OBJECT
#endif

using MarcelJoachimKloubert.CLRToolbox.Data;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Serialization
{
    /// <summary>
    /// A common serializer.
    /// </summary>
    public class CommonSerializer : SerializerBase
    {
        #region Constructors (2)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="CommonSerializer" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public CommonSerializer(object syncRoot)
            : base(syncRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializerBase" /> class.
        /// </summary>
        public CommonSerializer()
            : base()
        {
        }

        #endregion Constructors

        #region Methods (4)

        /// <summary>
        /// Creates a new <see cref="JsonSerializer" /> instance.
        /// </summary>
        /// <returns>The created instance.</returns>
        protected JsonSerializer CreateJsonSerializer()
        {
            JsonSerializerSettings settings = this.GetJsonSerializerSettings();

            return settings == null ? JsonSerializer.Create() : JsonSerializer.Create(settings);
        }

        /// <summary>
        /// Returns the settings for a new <see cref="JsonSerializer" /> instance.
        /// </summary>
        /// <returns>The settings or <see langword="null" /> to take the default one.</returns>
        protected virtual JsonSerializerSettings GetJsonSerializerSettings()
        {
            return null;
        }

        /// <inheriteddoc />
        protected override void OnFromJson<T>(string json, ref T deserializedObj)
        {
            JsonSerializer serializer = this.CreateJsonSerializer();

            using (StringReader strReader = new StringReader(json))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(strReader))
                {
                    Type deserializesAs = typeof(T);
#if KNOWS_EXPANDO_OBJECT
                    if (typeof(T).Equals(typeof(global::System.Collections.Generic.IDictionary<string, object>)))
                    {
                        deserializesAs = typeof(global::System.Dynamic.ExpandoObject);
                    }
#endif

                    deserializedObj = GlobalConverter.Current
                                                     .ChangeType<T>(serializer.Deserialize(jsonReader, deserializesAs));
                }
            }
        }

        /// <inheriteddoc />
        protected override void OnToJson<T>(T objToSerialize, ref StringBuilder jsonBuilder)
        {
            JsonSerializer serializer = this.CreateJsonSerializer();

            using (StringWriter strWriter = new StringWriter(jsonBuilder))
            {
                using (JsonTextWriter jsonWriter = new JsonTextWriter(strWriter))
                {
                    Type serializesAs = typeof(T);
#if KNOWS_EXPANDO_OBJECT
                    if (typeof(T).Equals(typeof(global::System.Dynamic.ExpandoObject)))
                    {
                        serializesAs = typeof(global::System.Collections.Generic.IDictionary<string, object>);
                    }
#endif

                    serializer.Serialize(jsonWriter, objToSerialize, serializesAs);
                }
            }
        }

        #endregion Methods
    }
}