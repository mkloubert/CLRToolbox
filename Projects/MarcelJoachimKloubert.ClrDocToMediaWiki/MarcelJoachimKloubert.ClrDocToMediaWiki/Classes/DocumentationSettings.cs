// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace MarcelJoachimKloubert.ClrDocToMediaWiki.Classes
{
    /// <summary>
    /// Stores settings for app / documentation.
    /// </summary>
    public sealed class DocumentationSettings : IDocumentationSettings
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="DocumentationSettings" /> class
        /// with default data.
        /// </summary>
        public DocumentationSettings()
        {
            this.DocumentPrivateTypes = false;
            this.DocumentPublicTypes = true;

            this.DocumentPrivateMethods = false;
            this.DocumentPublicMethods = true;
        }

        #endregion Constructors

        #region Properties (11)

        /// <inheriteddoc />
        public string AssemblyFile
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public bool DocumentPrivateMethods
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public bool DocumentPrivateTypes
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public bool DocumentPublicMethods
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public bool DocumentPublicTypes
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public bool IsActive
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public string Namespace
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public string NamespaceFilter
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public SecureString Password
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public string Username
        {
            get;
            set;
        }

        /// <inheriteddoc />
        public string WikiUrl
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Creates a list of new instances from an <see cref="IConfigRepository" /> object.
        /// </summary>
        /// <param name="config">The config repository.</param>
        /// <returns>The new instances.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="config" /> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<DocumentationSettings> FromConfig(IConfigRepository config)
        {
            foreach (var taskName in config.GetCategoryNames())
            {
                yield return FromConfig(config, taskName);
            }
        }

        /// <summary>
        /// Creates a new instance from an <see cref="IConfigRepository" /> object.
        /// </summary>
        /// <param name="config">The config repository.</param>
        /// <param name="category">The category where the settings are stored.</param>
        /// <returns>The new instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="config" /> is <see langword="null" />.
        /// </exception>
        public static DocumentationSettings FromConfig(IConfigRepository config,
                                                       IEnumerable<char> category)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            var result = new DocumentationSettings();

            // namespace
            {
                result.AssemblyFile = Path.GetFullPath(config.GetValue<string>("assembly", category));
                result.WikiUrl = new Uri(config.GetValue<string>("site", category)).ToString();

                result.Username = config.GetValue<string>("user", category).Trim();
                result.Password = new SecureString();
                result.Password.Append(config.GetValue<string>("password", category));

                string nsFilter;
                config.TryGetValue<string>("namespaceFilter", out nsFilter, category);

                bool isActive;
                config.TryGetValue<bool>("is_active", out isActive, category, true);

                result.NamespaceFilter = string.IsNullOrEmpty(nsFilter) ? null : nsFilter;
                result.IsActive = isActive;

                string @namespace;
                config.TryGetValue<string>("namespace", out @namespace, category);

                if (string.IsNullOrWhiteSpace(@namespace))
                {
                    @namespace = null;
                }
                else
                {
                    @namespace = @namespace.Trim();
                }

                if (@namespace != null)
                {
                    while (@namespace.EndsWith("/"))
                    {
                        @namespace = @namespace.Substring(0, @namespace.Length - 1).Trim();
                    }
                }

                if (string.IsNullOrWhiteSpace(@namespace))
                {
                    @namespace = null;
                }
                else
                {
                    @namespace = @namespace.Trim();
                }

                result.Namespace = @namespace;
            }

            return result;
        }

        #endregion Methods
    }
}