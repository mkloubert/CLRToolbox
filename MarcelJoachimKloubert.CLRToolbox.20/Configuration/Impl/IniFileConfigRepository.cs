// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Data;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration.Impl
{
    /// <summary>
    /// A config repository based on an INI file.
    /// </summary>
    public class IniFileConfigRepository : KeyValuePairConfigRepository
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
        public IniFileConfigRepository(IEnumerable<char> filePath, bool isReadOnly)
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

            this._CAN_WRITE = !isReadOnly;

            this._FILE_PATH = Path.GetFullPath(fp);
            if (File.Exists(this._FILE_PATH))
            {
                this.LoadIniFile();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFileConfigRepository"/> class.
        /// </summary>
        /// <param name="file">The INI file.</param>
        /// <param name="isReadOnly">Repository is readonly or writable.</param>
        /// <exception cref="NullReferenceException"><paramref name="file" /> is <see langword="null" />.</exception>
        public IniFileConfigRepository(FileInfo file, bool isReadOnly)
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
        public IniFileConfigRepository(IEnumerable<char> filePath)
            : this(filePath, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFileConfigRepository"/> class.
        /// </summary>
        /// <param name="file">The INI file.</param>
        /// <exception cref="NullReferenceException"><paramref name="file" /> is <see langword="null" />.</exception>
        /// <remarks>Repository becomes readonly.</remarks>
        public IniFileConfigRepository(FileInfo file)
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

        #region Methods (11)

        // Protected Methods (10) 

        /// <summary>
        /// Converts back a value for use in that object as value.
        /// </summary>
        /// <param name="input">The input value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual IEnumerable<char> FromIniSectionValue(string input)
        {
            string result = (input ?? string.Empty).Replace("\\n", "\n")
                                                   .Replace("\\r", "\r")
                                                   .Replace("\\0", "\0")
                                                   .Replace("\\a", "\a")
                                                   .Replace("\\b", "\b")
                                                   .Replace("\\t", "\t")
                                                   .Replace("\\;", ";")
                                                   .Replace("\\#", "#")
                                                   .Replace("\\=", "=")
                                                   .Replace("\\:", ":")
                                                   .Replace("\\\\", "\\")
                                                   .Trim();

            return result != string.Empty ? result : null;
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
        /// 
        /// </summary>
        /// <see cref="KeyValuePairConfigRepository.OnSetValue{T}(string, string, T, ref bool)"/>
        protected override void OnSetValue<T>(string category, string name, T value, ref bool valueWasSet, bool invokeOnUpdated)
        {
            string strValue = StringHelper.AsString(this.ToIniSectionValue(value));
            if (string.IsNullOrEmpty(strValue))
            {
                strValue = null;
            }

            base.OnSetValue<string>(category,
                                    name,
                                    strValue,
                                    ref valueWasSet,
                                    invokeOnUpdated);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="KeyValuePairConfigRepository.OnTryGetValue{T}(string, string, ref T, ref bool)"/>
        protected override void OnTryGetValue<T>(string category, string name, ref T foundValue, ref bool valueWasFound)
        {
            IEnumerable<char> innerValue = null;
            base.OnTryGetValue<IEnumerable<char>>(category, name,
                                                  ref innerValue, ref valueWasFound);

            if (!valueWasFound)
            {
                return;
            }

            bool throwException = false;
            string strValue = StringHelper.AsString(innerValue);
            object valueToReturn = null;

            if (strValue != null)
            {
                Type targetType = typeof(T);

                if (targetType.Equals(typeof(bool)) ||
                    targetType.Equals(typeof(bool?)))
                {
                    switch (strValue.ToLower().Trim())
                    {
                        case "0":
                        case "no":
                        case "false":
                            valueToReturn = false;
                            break;

                        case "1":
                        case "yes":
                        case "true":
                            valueToReturn = true;
                            break;

                        case "":
                            if (Nullable.GetUnderlyingType(targetType) == null)
                            {
                                // no nullable boolean
                                throwException = true;
                            }
                            break;
                    }
                }
            }

            if (throwException)
            {
                throw new InvalidCastException();
            }

            foundValue = GlobalConverter.Current.ChangeType<T>(valueToReturn);
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
                    foreach (KeyValuePair<string, IDictionary<string, object>> categoryValues in this._CONFIG_VALUES)
                    {
                        // create section
                        writer.WriteLine("[{0}]",
                                         StringHelper.AsString(this.ParseIniSectionName(categoryValues.Key)));

                        // writes values
                        foreach (KeyValuePair<string, object> item in categoryValues.Value)
                        {
                            writer.WriteLine(string.Format("{0}={1}",
                                                           StringHelper.AsString(this.ParseIniSectionKey(item.Key)),
                                                           item.Value));
                        }
                        writer.WriteLine();
                    }

                    writer.Flush();
                    writer.Close();
                }

                FileInfo iniFile = new FileInfo(this.FilePath);
                if (iniFile.Exists)
                {
                    iniFile.Delete();
                    iniFile.Refresh();
                }

                temp.Position = 0;
                using (FileStream iniFileStream = iniFile.OpenWrite())
                {
                    IOHelper.CopyTo(temp, iniFileStream);
                }
            }
        }

        /// <summary>
        /// Parses back the name of an INI section key for use in this object.
        /// </summary>
        /// <param name="input">The input expression.</param>
        /// <returns>The parsed name of the section key.</returns>
        protected virtual IEnumerable<char> ParseBackIniSectionKey(string input)
        {
            return (input ?? string.Empty).Replace("\\=", "=")
                                          .Trim();
        }

        /// <summary>
        /// Parses back the name of an INI section for use in this object.
        /// </summary>
        /// <param name="input">The input expression.</param>
        /// <returns>The parsed name of the section.</returns>
        protected virtual IEnumerable<char> ParseBackIniSectionName(string input)
        {
            return (input ?? string.Empty).Replace("\\[", "[")
                                          .Replace("\\]", "]")
                                          .Trim();
        }

        /// <summary>
        /// Parses the name of a section key for use in an INI file.
        /// </summary>
        /// <param name="input">The input expression.</param>
        /// <returns>The parsed name of the section key.</returns>
        protected virtual IEnumerable<char> ParseIniSectionKey(string input)
        {
            return (input ?? string.Empty).Replace("=", "\\=")
                                          .Trim();
        }

        /// <summary>
        /// Parses the name of a section for use in an INI file.
        /// </summary>
        /// <param name="input">The input expression.</param>
        /// <returns>The parsed name of the section.</returns>
        protected virtual IEnumerable<char> ParseIniSectionName(string input)
        {
            return (input ?? string.Empty).Replace("[", "\\[")
                                          .Replace("]", "\\]")
                                          .Trim();
        }

        /// <summary>
        /// Converts a value for use in an INI file as section value.
        /// </summary>
        /// <param name="input">The input value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual IEnumerable<char> ToIniSectionValue(object input)
        {
            return (StringHelper.AsString(input, true) ?? string.Empty).Replace("\\", "\\\\")
                                                                       .Replace("\n", "\\n")
                                                                       .Replace("\r", "\\r")
                                                                       .Replace("\0", "\\0")
                                                                       .Replace("\a", "\\a")
                                                                       .Replace("\b", "\\b")
                                                                       .Replace(";", "\\;")
                                                                       .Replace("#", "\\#")
                                                                       .Replace("=", "\\=")
                                                                       .Replace(":", "\\:")
                                                                       .Replace("\t", "\\t")
                                                                       .Trim();
        }
        // Private Methods (1) 

        private void LoadIniFile()
        {
            lock (this._SYNC)
            {
                using (FileStream iniFile = File.OpenRead(this.FilePath))
                {
                    using (StreamReader reader = new StreamReader(iniFile, this.GetEncoding()))
                    {
                        string currentSection = null;

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            line = line.TrimStart();
                            if (line == string.Empty ||
                                line[0] == ';')
                            {
                                // empty line
                                continue;
                            }

                            if (line[0] == ';' ||
                                line[0] == '#')
                            {
                                // comment
                                continue;
                            }

                            if (line[0] == '[')
                            {
                                string section = line.TrimEnd();
                                if (section[section.Length - 1] == ']')
                                {
                                    // (new) section started
                                    currentSection = section.Substring(1, section.Length - 2).Trim();
                                    continue;
                                }
                            }

                            if (currentSection == null)
                            {
                                // no section defined
                                continue;
                            }

                            string name;
                            string value;

                            int equalCharIndex = line.IndexOf('=');
                            if (equalCharIndex > -1)
                            {
                                name = line.Substring(0, equalCharIndex);
                                value = line.Substring(equalCharIndex + 1,
                                                       line.Length - equalCharIndex - 1);
                            }
                            else
                            {
                                // no value defined

                                name = line.TrimEnd();
                                value = null;
                            }

                            if (value != null)
                            {
                                // extract until comment

                                int sharpIndex = value.IndexOf('#');
                                if (sharpIndex > -1)
                                {
                                    value = value.Substring(0, sharpIndex);
                                }

                                int semicolonIndex = value.IndexOf(';');
                                if (semicolonIndex > -1)
                                {
                                    value = value.Substring(0, semicolonIndex);
                                }
                            }

                            string configCat;
                            string configName;
                            this.PrepareCategoryAndName(this.ParseBackIniSectionName(currentSection), this.ParseBackIniSectionKey(name),
                                                        out configCat, out configName);

                            bool valueWasSet = false;
                            this.OnSetValue<string>(configCat,
                                                    configName,
                                                    StringHelper.AsString(this.FromIniSectionValue(value)),
                                                    ref valueWasSet,
                                                    false);
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}
