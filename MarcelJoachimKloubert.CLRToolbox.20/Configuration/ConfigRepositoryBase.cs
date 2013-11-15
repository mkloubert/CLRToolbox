using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration
{
    /// <summary>
    /// A basic repository that provides configuration data stored in categories as key/value pairs.
    /// </summary>
    public abstract partial class ConfigRepositoryBase : IConfigRepository
    {
        #region Fields (1)

        /// <summary>
        /// An unique object for sync operations.
        /// </summary>
        protected readonly object _SYNC = new object();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryBase" /> class.
        /// </summary>
        /// <param name="sync">The value for <see cref="ConfigRepositoryBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        protected ConfigRepositoryBase(object sync)
        {
            if (sync == null)
            {
                throw new ArgumentNullException("sync");
            }

            this._SYNC = sync;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryBase" /> class.
        /// </summary>
        protected ConfigRepositoryBase()
            : this(new object())
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.CanRead" />
        public virtual bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.CanWrite" />
        public virtual bool CanWrite
        {
            get { return true; }
        }

        #endregion Properties

        #region Methods (21)

        // Public Methods (9) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.Clear()" />
        public bool Clear()
        {
            lock (this._SYNC)
            {
                this.ThrowIfNotWritable();

                bool result = false;
                this.OnClear(ref result);

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.ContainsValue(IEnumerable{char}, IEnumerable{char})" />
        public bool ContainsValue(IEnumerable<char> name, IEnumerable<char> category = null)
        {
            lock (this._SYNC)
            {
                string configCategory;
                string configName;
                this.PrepareCategoryAndName(category, name,
                                            out configCategory, out configName);

                bool result = false;
                this.OnContainsValue(configCategory, configName, ref result);

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.DeleteValue(IEnumerable{char}, IEnumerable{char})" />
        public bool DeleteValue(IEnumerable<char> name, IEnumerable<char> category = null)
        {
            lock (this._SYNC)
            {
                this.ThrowIfNotWritable();

                string configCategory;
                string configName;
                this.PrepareCategoryAndName(category, name,
                                            out configCategory, out configName);

                bool result = false;
                this.OnDeleteValue(configCategory, configName, ref result);

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.GetCategoryNames()" />
        public IList<string> GetCategoryNames()
        {
            lock (this._SYNC)
            {
                List<IEnumerable<char>> names = new List<IEnumerable<char>>();
                this.OnGetCategoryNames(names);

                List<string> result = new List<string>();
                foreach (IEnumerable<char> name in names)
                {
                    result.Add(StringHelper.AsString(name));
                }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.GetValue(IEnumerable{char}, IEnumerable{char})" />
        public object GetValue(IEnumerable<char> name, IEnumerable<char> category = null)
        {
            return this.GetValue<object>(name, category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.GetValue{T}(IEnumerable{char}, IEnumerable{char})" />
        public T GetValue<T>(IEnumerable<char> name, IEnumerable<char> category = null)
        {
            T result;
            if (!this.TryGetValue<T>(name, out result, category))
            {
                throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.SetValue(IEnumerable{char}, object, IEnumerable{char})" />
        public bool SetValue(IEnumerable<char> name, object value, IEnumerable<char> category = null)
        {
            return this.SetValue<object>(name, value, category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.SetValue{T}(IEnumerable{char}, T, IEnumerable{char})" />
        public bool SetValue<T>(IEnumerable<char> name, T value, IEnumerable<char> category = null)
        {
            lock (this._SYNC)
            {
                this.ThrowIfNotWritable();

                string configCategory;
                string configName;
                this.PrepareCategoryAndName(category, name,
                                            out configCategory, out configName);

                bool result = false;
                this.OnSetValue<T>(configCategory, configName,
                                   value,
                                   ref result);

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.TryGetValue(IEnumerable{char}, out object, IEnumerable{char}, object)" />
        public bool TryGetValue(IEnumerable<char> name, out object value, IEnumerable<char> category = null, object defaultVal = null)
        {
            return this.TryGetValue<object>(name,
                                            out value,
                                            category,
                                            defaultVal);
        }
        // Protected Methods (12) 

        /// <summary>
        /// Creates a key/value pair collection for a new category.
        /// </summary>
        /// <param name="category">The name of the category.</param>
        /// <returns>The dictionary of the category or <see langword="null" /> if no category should be created.</returns>
        protected virtual IDictionary<string, object> CreateEmptyDictionaryForCategory(IEnumerable<char> category)
        {
            return new Dictionary<string, object>();
        }

        /// <summary>
        /// Converts an object that is stored in that repository to a target type.
        /// </summary>
        /// <typeparam name="T">Target type of the value.</typeparam>
        /// <param name="input">The input value.</param>
        /// <param name="category">The category from where the input value comes from.</param>
        /// <param name="name">The name where the input value is stored in.</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"><paramref name="input" /> could not be converted.</exception>
        protected virtual T FromConfigValue<T>(string category, string name, object input)
        {
            if (DBNull.Value.Equals(input))
            {
                input = null;
            }

            return input != null ? (T)input : default(T);
        }

        /// <summary>
        /// Stores the logic for <see cref="ConfigRepositoryBase.Clear()" /> method.
        /// </summary>
        /// <param name="wasCleared">Was cleared or not.</param>
        protected abstract void OnClear(ref bool wasCleared);

        /// <summary>
        /// The logic for <see cref="ConfigRepositoryBase.ContainsValue(IEnumerable{char}, IEnumerable{char})" /> method.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="exists">
        /// Defines if value was found or not.
        /// That value is <see langword="false" /> by default.
        /// </param>
        protected virtual void OnContainsValue(string category, string name, ref bool exists)
        {
            object dummy;
            exists = this.TryGetValue(name, out dummy, category);
        }

        /// <summary>
        /// The logic for <see cref="ConfigRepositoryBase.DeleteValue(IEnumerable{char}, IEnumerable{char})" /> method.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="deleted">
        /// Defines if value was deleted or not.
        /// That value is <see langword="false" /> by default.
        /// </param>
        protected abstract void OnDeleteValue(string category, string name, ref bool deleted);

        /// <summary>
        /// The logic for <see cref="ConfigRepositoryBase.GetCategoryNames()" />
        /// </summary>
        /// <param name="names">The target list where the names should be stored.</param>
        protected abstract void OnGetCategoryNames(IList<IEnumerable<char>> names);

        /// <summary>
        /// Stores the logic for <see cref="ConfigRepositoryBase.SetValue{T}(IEnumerable{char}, T, IEnumerable{char})" /> method.
        /// </summary>
        /// <typeparam name="T">Type of rhe value to set.</typeparam>
        /// <param name="category">The category where the value should be stored to.</param>
        /// <param name="name">The name in the category where the value should be stored to.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="valueWasSet">
        /// Defines if value was set or not.
        /// Is <see langword="false" /> by default.
        /// </param>
        protected abstract void OnSetValue<T>(string category, string name, T value, ref bool valueWasSet);

        /// <summary>
        /// Stores the logic for <see cref="ConfigRepositoryBase.TryGetValue{T}(IEnumerable{char}, out T, IEnumerable{char}, T)" /> method.
        /// </summary>
        /// <typeparam name="T">Result type.</typeparam>
        /// <param name="category">The category.</param>
        /// <param name="name">The name.</param>
        /// <param name="foundValue">Defines the found value.</param>
        /// <param name="valueWasFound">
        /// Defines if value was found or not.
        /// Is <see langword="false" /> bye default.
        /// </param>
        protected abstract void OnTryGetValue<T>(string category, string name, ref T foundValue, ref bool valueWasFound);

        /// <summary>
        /// Parses a category and name value for use in this config repository.
        /// </summary>
        /// <param name="category">The category value to parse.</param>
        /// <param name="name">The name value to parse.</param>
        /// <param name="newCategory">The target category value.</param>
        /// <param name="newName">The target name value.</param>
        protected virtual void PrepareCategoryAndName(IEnumerable<char> category, IEnumerable<char> name,
                                                      out string newCategory, out string newName)
        {
            newCategory = (StringHelper.AsString(category) ?? string.Empty).ToLower().Trim();
            if (newCategory == string.Empty)
            {
                newCategory = null;
            }

            newName = (StringHelper.AsString(name) ?? string.Empty).ToLower().Trim();
            if (newName == string.Empty)
            {
                newName = null;
            }
        }

        /// <summary>
        /// Throws an exception if that repository is not readable.
        /// </summary>
        /// <exception cref="InvalidOperationException">Repository cannot be read.</exception>
        protected void ThrowIfNotReadable()
        {
            if (!this.CanRead)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Throws an exception if that repository is not written.
        /// </summary>
        /// <exception cref="InvalidOperationException">Repository cannot be written.</exception>
        protected void ThrowIfNotWritable()
        {
            if (!this.CanWrite)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Converts a value
        /// </summary>
        /// <param name="category">The category where the input value should be stored in.</param>
        /// <param name="name">The name in the category where the input value should be stored in.</param>
        /// <param name="input">The input value</param>
        /// <returns></returns>
        protected virtual object ToConfigValue(string category, string name, object input)
        {
            object result = input;
            if (DBNull.Value.Equals(result))
            {
                result = null;
            }

            return result;
        }

        #endregion Methods

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConfigRepository.TryGetValue{T}(IEnumerable{char}, out T, IEnumerable{char}, T)" />
        public bool TryGetValue<T>(IEnumerable<char> name, out T value, IEnumerable<char> category = null, T defaultVal = default(T))
        {
            lock (this._SYNC)
            {
                this.ThrowIfNotReadable();

                bool result = false;

                string configCategory;
                string configName;
                this.PrepareCategoryAndName(category, name,
                                            out configCategory, out configName);

                T foundValue = default(T);
                this.OnTryGetValue<T>(configCategory, configName,
                                      ref foundValue,
                                      ref result);

                if (result)
                {
                    value = foundValue;
                }
                else
                {
                    // not found => use default
                    value = defaultVal;
                }

                return result;
            }
        }
    }
}
