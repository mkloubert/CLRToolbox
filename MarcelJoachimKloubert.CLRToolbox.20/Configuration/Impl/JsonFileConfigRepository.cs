// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration.Impl
{
    /// <summary>
    /// A config repository based on an INI file.
    /// </summary>
    public class JsonFileConfigRepository : KeyValuePairConfigRepository
    {
        #region Fields (2)

        private readonly bool _CAN_WRITE;
        private readonly string _FILE_PATH;

        #endregion Fields

        #region Constructors (4)

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFileConfigRepository"/> class.
        /// </summary>
        /// <param name="filePath">The path of the INI file.</param>
        /// <param name="isReadOnly">Repository is readonly or writable.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath" /> is invalid.</exception>
        public JsonFileConfigRepository(IEnumerable<char> filePath, bool isReadOnly)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            string fp = StringHelper.AsString(filePath);
            if (StringHelper.IsNullOrWhiteSpace(fp))
            {
                throw new ArgumentException("filePath");
            }

            this._CAN_WRITE = isReadOnly == false;

            this._FILE_PATH = Path.GetFullPath(fp);
            if (File.Exists(this._FILE_PATH))
            {
                this.LoadJsonFile();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFileConfigRepository"/> class.
        /// </summary>
        /// <param name="file">The INI file.</param>
        /// <param name="isReadOnly">Repository is readonly or writable.</param>
        /// <exception cref="NullReferenceException"><paramref name="file" /> is <see langword="null" />.</exception>
        public JsonFileConfigRepository(FileInfo file, bool isReadOnly)
            : this(file.FullName, isReadOnly)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFileConfigRepository"/> class.
        /// </summary>
        /// <param name="filePath">The path of the INI file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filePath" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath" /> is invalid.</exception>
        /// <remarks>Repository becomes readonly.</remarks>
        public JsonFileConfigRepository(IEnumerable<char> filePath)
            : this(filePath, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFileConfigRepository"/> class.
        /// </summary>
        /// <param name="file">The INI file.</param>
        /// <exception cref="NullReferenceException"><paramref name="file" /> is <see langword="null" />.</exception>
        /// <remarks>Repository becomes readonly.</remarks>
        public JsonFileConfigRepository(FileInfo file)
            : this(file, true)
        {
        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        ///
        /// </summary>
        /// <see cref="ConfigRepositoryBase.CanWrite" />
        public override bool CanWrite
        {
            get { return this._CAN_WRITE; }
        }

        /// <summary>
        /// Gets the full path of the underlying INI file.
        /// </summary>
        public string FilePath
        {
            get { return this._FILE_PATH; }
        }

        #endregion Properties

        #region Methods (5)

        // Protected Methods (4) 

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
        /// Gets the encoding for the INI file.
        /// </summary>
        /// <returns>The INI file encoding.</returns>
        protected virtual Encoding GetEncoding()
        {
            return Encoding.UTF8;
        }

        /// <summary>
        /// Returns the settings for a new <see cref="JsonSerializer" /> instance.
        /// </summary>
        /// <returns>The settings or <see langword="null" /> to take the default one.</returns>
        protected virtual JsonSerializerSettings GetJsonSerializerSettings()
        {
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <see cref="KeyValuePairConfigRepository.OnUpdated()" />
        protected override void OnUpdated()
        {
            base.OnUpdated();

            using (MemoryStream temp = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(temp, this.GetEncoding()))
                {
                    using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
                    {
                        JsonSerializer serializer = this.CreateJsonSerializer();

                        serializer.Serialize(jsonWriter,
                                             this._CONFIG_VALUES,
                                             typeof(global::System.Collections.Generic.IDictionary<string, object>));

                        jsonWriter.Flush();

                        FileInfo jsonFile = new FileInfo(this.FilePath);
                        if (jsonFile.Exists)
                        {
                            jsonFile.Delete();
                            jsonFile.Refresh();
                        }

                        temp.Position = 0;
                        using (FileStream jsonFileStream = jsonFile.OpenWrite())
                        {
                            IOHelper.CopyTo(temp, jsonFileStream);
                        }
                    }
                }
            }
        }

        // Private Methods (1) 

        private void LoadJsonFile()
        {
            lock (this._SYNC)
            {
                using (FileStream jsonFile = File.OpenRead(this.FilePath))
                {
                    using (StreamReader reader = new StreamReader(jsonFile, this.GetEncoding()))
                    {
                        using (JsonTextReader jsonReader = new JsonTextReader(reader))
                        {
                            JsonSerializer serializer = this.CreateJsonSerializer();

                            this._CONFIG_VALUES.Clear();
                            {
                                IDictionary<string, object> values = serializer.Deserialize<IDictionary<string, object>>(jsonReader);
                                if (values != null)
                                {
                                    CollectionHelper.ForEach(values,
                                                             delegate(IForEachItemExecutionContext<KeyValuePair<string, object>> ctx)
                                                             {
                                                                 string category = ctx.Item.Key;
                                                                 IDictionary<string, object> keyValues = GlobalConverter.Current
                                                                                                                        .ChangeType<IDictionary<string, object>>(ctx.Item.Value);

                                                                 this._CONFIG_VALUES[category ?? string.Empty] =
                                                                     keyValues ?? this.CreateEmptyDictionaryForCategory(category);
                                                             });
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}