// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Globalization;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// A basic role of/for an access control list.
    /// </summary>
    public abstract class AclRoleBase : TMObject,
                                        IAclRole
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AclRoleBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AclRoleBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AclRoleBase" /> class.
        /// </summary>
        protected AclRoleBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.DisplayName" />
        public string DisplayName
        {
            get { return this.GetDisplayName(CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.Name" />
        public abstract string Name
        {
            get;
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (6) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IEnumerable<char> other)
        {
            return this.Equals(StringHelper.AsString(other));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.Equals(object)" />
        public override bool Equals(object other)
        {
            if (other is IEnumerable<char>)
            {
                return this.Equals((IEnumerable<char>)other);
            }

            return base.Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IAclRole other)
        {
            return other != null ? this.Equals(other.Name) : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IHasName.GetDisplayName(CultureInfo)" />
        public string GetDisplayName(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return StringHelper.AsString(this.OnGetDisplayName(culture));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.GetHashCode()" />
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="object.ToString()" />
        public override string ToString()
        {
            return this.DisplayName ?? string.Empty;
        }
        // Protected Methods (2) 

        /// <summary>
        /// Checks that instance with another string that represents a role name.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <returns>Is equal to that instance.</returns>
        protected virtual bool Equals(string roleName)
        {
            return (this.Name ?? string.Empty).ToLower().Trim() ==
                   (roleName ?? string.Empty).ToLower().Trim();
        }

        /// <summary>
        /// The logic for <see cref="AclRoleBase.GetDisplayName(CultureInfo)" /> method.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns>The name based on <paramref name="culture" />.</returns>
        protected virtual IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            return this.Name;
        }

        #endregion Methods
    }
}
