﻿// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration.Impl
{
    /// <summary>
    /// A simple config repository that stores its data into dictionaries.
    /// </summary>
    public class KeyValuePairConfigRepository : ConfigRepositoryBase
    {
        #region Fields (1)

        /// <summary>
        /// Stores all categories and their values.
        /// </summary>
        protected readonly IDictionary<string, IDictionary<string, object>> _CONFIG_VALUES = new Dictionary<string, IDictionary<string, object>>();

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValuePairConfigRepository" /> class.
        /// </summary>
        /// <param name="sync">The value for <see cref="ConfigRepositoryBase._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sync" /> is <see langword="null" />.
        /// </exception>
        public KeyValuePairConfigRepository(object sync)
            : base(sync)
        {
            this._CONFIG_VALUES = this.CreateInitalConfigValueCollection();
            if (this._CONFIG_VALUES == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValuePairConfigRepository" /> class.
        /// </summary>
        public KeyValuePairConfigRepository()
            : this(new object())
        {

        }

        #endregion Constructors

        #region Methods (6)

        // Protected Methods (6) 

        /// <summary>
        /// Creates the initial value for <see cref="KeyValuePairConfigRepository._CONFIG_VALUES" /> field.
        /// </summary>
        /// <returns>The inital value for <see cref="KeyValuePairConfigRepository._CONFIG_VALUES" />.</returns>
        protected virtual IDictionary<string, IDictionary<string, object>> CreateInitalConfigValueCollection()
        {
            return new Dictionary<string, IDictionary<string, object>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConfigRepositoryBase.OnClear(ref bool)" />
        protected override void OnClear(ref bool wasCleared)
        {
            if (this._CONFIG_VALUES.Count > 0)
            {
                this._CONFIG_VALUES.Clear();
                wasCleared = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConfigRepositoryBase.OnDeleteValue(string, string, ref bool)" />
        protected override void OnDeleteValue(string category, string name, ref bool deleted)
        {
            IDictionary<string, object> catValues;
            if (this._CONFIG_VALUES.TryGetValue(category, out catValues))
            {
                deleted = catValues.Remove(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConfigRepositoryBase.OnSetValue{T}(string, string, T, ref bool)"/>
        protected override void OnSetValue<T>(string category, string name, T value, ref bool valueWasSet)
        {
            IDictionary<string, object> catValues;
            if (!this._CONFIG_VALUES.TryGetValue(category, out catValues))
            {
                catValues = this.CreateEmptyDictionaryForCategory(category);
                if (catValues == null)
                {
                    return;
                }

                this._CONFIG_VALUES
                    .Add(category, catValues);
            }

            catValues[category] = this.ToConfigValue(category, name, value);
            valueWasSet = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConfigRepositoryBase.OnTryGetValue{T}(string, string, ref T, ref bool)" />
        protected override void OnTryGetValue<T>(string category, string name, ref T foundValue, ref bool valueWasFound)
        {
            IDictionary<string, object> catValues;
            if (this._CONFIG_VALUES.TryGetValue(category, out catValues))
            {
                object value;
                if (catValues.TryGetValue(name, out value))
                {
                    foundValue = this.FromConfigValue<T>(category, name, value);
                    valueWasFound = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConfigRepositoryBase.PrepareCategoryAndName(IEnumerable{char}, IEnumerable{char}, out string, out string)" />
        protected override void PrepareCategoryAndName(IEnumerable<char> category, IEnumerable<char> name,
                                                       out string newCategory, out string newName)
        {
            base.PrepareCategoryAndName(category, name,
                                        out newCategory, out newName);

            if (newCategory == null)
            {
                newCategory = string.Empty;
            }

            if (newName == null)
            {
                newName = string.Empty;
            }
        }

        #endregion Methods

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConfigRepositoryBase.OnContainsValue(string, string, ref bool)" />
        protected override sealed void OnContainsValue(string category, string name, ref bool exists)
        {
            IDictionary<string, object> catValues;
            if (this._CONFIG_VALUES.TryGetValue(category, out catValues))
            {
                exists = catValues.ContainsKey(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ConfigRepositoryBase.OnGetCategoryNames(ICollection{IEnumerable{char}})" />
        protected override sealed void OnGetCategoryNames(ICollection<IEnumerable<char>> names)
        {
            foreach (string name in this._CONFIG_VALUES.Keys)
            {
                names.Add(name);
            }
        }
    }
}
