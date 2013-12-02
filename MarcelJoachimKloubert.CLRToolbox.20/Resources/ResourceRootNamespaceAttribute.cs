// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Resources
{
    /// <summary>
    /// Defines the name of the root namespace for the resources.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly,
                    AllowMultiple = false,
                    Inherited = false)]
    public sealed class ResourceRootNamespaceAttribute : Attribute
    {
        #region Fields (1)

        private string _namespace;

        #endregion Fields

        #region Constructors (3)

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRootNamespaceAttribute"/> class.
        /// </summary>
        /// <param name="ns">The value for <see cref="ResourceRootNamespaceAttribute.Namespace" /> property.</param>
        public ResourceRootNamespaceAttribute(string ns)
        {
            ns = (ns ?? string.Empty).Trim();
            if (ns != string.Empty)
            {
                this._namespace = ns;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRootNamespaceAttribute"/> class.
        /// </summary>
        /// <param name="type">
        /// The type from where the value for <see cref="ResourceRootNamespaceAttribute.Namespace" /> property
        /// is extracted from.
        /// </param>
        /// <exception cref="NullReferenceException"><paramref name="type" /> is <see langword="null" />.</exception>
        public ResourceRootNamespaceAttribute(Type type)
            : this(type.Namespace)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRootNamespaceAttribute"/> class.
        /// </summary>
        public ResourceRootNamespaceAttribute()
            : this((string)null)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the normalized name of the root namespace or <see langword="null" /> id not defined.
        /// </summary>
        public string Namespace
        {
            get { return this._namespace; }
        }

        #endregion Properties
    }
}
