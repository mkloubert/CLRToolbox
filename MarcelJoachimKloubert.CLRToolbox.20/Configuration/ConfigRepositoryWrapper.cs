// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Configuration
{
    #region CLASS: ConfigRepositoryWrapper<TConf>

    /// <summary>
    /// A wrapper for an <see cref="IConfigRepository" /> object that normally uses
    /// an additional prefix and suffix for category names.
    /// </summary>
    /// <typeparam name="TConf">Tye of the config repository.</typeparam>
    public class ConfigRepositoryWrapper<TConf> : ConfigRepositoryBase where TConf : global::MarcelJoachimKloubert.CLRToolbox.Configuration.IConfigRepository
    {
        #region Fields (3)

        private readonly TConf _INNER_CONF;
        private readonly string _PREFIX;
        private readonly string _SUFFIX;

        #endregion

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryWrapper{TConf}" /> class.
        /// </summary>
        /// <param name="innerConf">The value for the <see cref="ConfigRepositoryWrapper{TConf}.InnerConfig" /> property.</param>
        /// <param name="prefix">The value for the <see cref="ConfigRepositoryWrapper{TConf}.Prefix" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="innerConf" /> is <see langword="null" />.
        /// </exception>
        public ConfigRepositoryWrapper(TConf innerConf, IEnumerable<char> prefix)
            : this(innerConf, prefix, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryWrapper{TConf}" /> class.
        /// </summary>
        /// <param name="innerConf">The value for the <see cref="ConfigRepositoryWrapper{TConf}.InnerConfig" /> property.</param>
        /// <param name="prefix">The value for the <see cref="ConfigRepositoryWrapper{TConf}.Prefix" /> property.</param>
        /// <param name="suffix">The value for the <see cref="ConfigRepositoryWrapper{TConf}.Suffix" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="innerConf" /> is <see langword="null" />.
        /// </exception>
        public ConfigRepositoryWrapper(TConf innerConf, IEnumerable<char> prefix, IEnumerable<char> suffix)
            : base(new object(), false)
        {
            if (innerConf == null)
            {
                throw new ArgumentNullException("innerConf");
            }

            this._INNER_CONF = innerConf;

            this._PREFIX = (StringHelper.AsString(prefix) ?? string.Empty).Trim();
            if (this._PREFIX == string.Empty)
            {
                this._PREFIX = null;
            }

            this._SUFFIX = (StringHelper.AsString(suffix) ?? string.Empty).Trim();
            if (this._SUFFIX == string.Empty)
            {
                this._SUFFIX = null;
            }
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the wrapped config repository.
        /// </summary>
        public TConf InnerConfig
        {
            get { return this._INNER_CONF; }
        }

        /// <summary>
        /// Gets the prefix for the categories.
        /// </summary>
        public string Prefix
        {
            get { return this._PREFIX; }
        }

        /// <summary>
        /// Gets the suffix for the categories.
        /// </summary>
        public string Suffix
        {
            get { return this._SUFFIX; }
        }

        #endregion Properties

        #region Methods (6)

        /// <inheriteddoc />
        protected override void OnClear(ref bool wasCleared)
        {
            wasCleared = this._INNER_CONF
                             .Clear();
        }

        /// <inheriteddoc />
        protected override void OnDeleteValue(string category, string name, ref bool deleted)
        {
            deleted = this._INNER_CONF
                          .DeleteValue(name, category);
        }

        /// <inheriteddoc />
        protected override void OnGetCategoryNames(ICollection<IEnumerable<char>> names)
        {
            IEnumerable<string> catNames = this._INNER_CONF
                                               .GetCategoryNames() ?? CollectionHelper.Empty<string>();

            CollectionHelper.AddRange(names,
                                      CollectionHelper.Cast<IEnumerable<char>>(catNames));
        }

        /// <inheriteddoc />
        protected override void OnSetValue<T>(string category, string name, T value, ref bool valueWasSet)
        {
            valueWasSet = this._INNER_CONF
                              .SetValue<T>(name, value, category);
        }

        /// <inheriteddoc />
        protected override void OnTryGetValue<T>(string category, string name, ref T foundValue, ref bool valueWasFound)
        {
            valueWasFound = this._INNER_CONF
                                .TryGetValue<T>(name, out foundValue, category);
        }
        
        /// <inheriteddoc />
        protected override void PrepareCategoryAndName(IEnumerable<char> category, IEnumerable<char> name,
                                                       out string newCategory, out string newName)
        {
            base.PrepareCategoryAndName(string.Format("{0}{1}{2}",
                                                      this.Prefix,
                                                      (StringHelper.AsString(category) ?? string.Empty).Trim(),
                                                      this.Suffix),
                                        name,
                                        out newCategory, out newName);
        }

        #endregion
    }

    #endregion

    #region CLASS: ConfigRepositoryWrapper

    /// <summary>
    /// A simple wrapper for an <see cref="IConfigRepository" /> object.
    /// </summary>
    public sealed class ConfigRepositoryWrapper : ConfigRepositoryWrapper<global::MarcelJoachimKloubert.CLRToolbox.Configuration.IConfigRepository>
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryWrapper" /> class.
        /// </summary>
        /// <param name="innerConf">The value for the <see cref="ConfigRepositoryWrapper{TConf}.InnerConfig" /> property.</param>
        /// <param name="prefix">The value for the <see cref="ConfigRepositoryWrapper{TConf}.Prefix" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="innerConf" /> is <see langword="null" />.
        /// </exception>
        public ConfigRepositoryWrapper(IConfigRepository innerConf, IEnumerable<char> prefix)
            : base(innerConf, prefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigRepositoryWrapper" /> class.
        /// </summary>
        /// <param name="innerConf">The value for the <see cref="ConfigRepositoryWrapper{TConf}.InnerConfig" /> property.</param>
        /// <param name="prefix">The value for the <see cref="ConfigRepositoryWrapper{TConf}.Prefix" /> property.</param>
        /// <param name="suffix">The value for the <see cref="ConfigRepositoryWrapper{TConf}.Suffix" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="innerConf" /> is <see langword="null" />.
        /// </exception>
        public ConfigRepositoryWrapper(IConfigRepository innerConf, IEnumerable<char> prefix, IEnumerable<char> suffix)
            : base(innerConf, prefix, suffix)
        {
        }

        #endregion Constructors (4)
    }

    #endregion
}