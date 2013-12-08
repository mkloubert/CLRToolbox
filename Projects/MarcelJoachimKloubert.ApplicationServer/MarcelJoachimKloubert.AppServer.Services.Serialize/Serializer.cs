// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using Newtonsoft.Json;

namespace MarcelJoachimKloubert.AppServer.Services.Serialize
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Serialization.ISerializer))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class Serializer : SerializerBase
    {
        #region Methods (2)

        // Protected Methods (2) 

        protected override void OnFromJson<T>(string json, ref T deserializedObj)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StringReader(json))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    deserializedObj = serializer.Deserialize<T>(reader);
                }
            }
        }

        protected override void OnToJson<T>(T objToSerialize, ref StringBuilder jsonBuilder)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StringWriter(jsonBuilder))
            {
                serializer.Serialize(sw, objToSerialize, typeof(T));

                sw.Flush();
                sw.Close();
            }
        }

        #endregion Methods
    }
}
