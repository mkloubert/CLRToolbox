// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Security.Principal;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// A basic <see cref="IPrincipal" /> that uses an <see cref="IAccessControlList" />.
    /// </summary>
    public abstract class AclPrincipalBase : TMObject, IAclPrincipal
    {
        #region Fields (1)

        private readonly IAccessControlList _ACCESS_CONTROL_LIST;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="AclPrincipalBase" /> class.
        /// </summary>
        /// <param name="acl">The value for the <see cref="AclPrincipalBase.AccessControlList" /> property.</param>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="acl" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        protected AclPrincipalBase(IAccessControlList acl, object syncRoot)
            : base(syncRoot)
        {
            if (acl == null)
            {
                throw new ArgumentNullException("acl");
            }

            this._ACCESS_CONTROL_LIST = acl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AclPrincipalBase" /> class.
        /// </summary>
        /// <param name="acl">The value for the <see cref="AclPrincipalBase.AccessControlList" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="acl" /> is <see langword="null" />.
        /// </exception>
        protected AclPrincipalBase(IAccessControlList acl)
            : this(acl, new object())
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAclPrincipal.AccessControlList" />
        public IAccessControlList AccessControlList
        {
            get { return this._ACCESS_CONTROL_LIST; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IPrincipal.Identity" />
        public abstract IIdentity Identity { get; }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IPrincipal.IsInRole(string)" />
        public bool IsInRole(string role)
        {
            return this._ACCESS_CONTROL_LIST[role] != null;
        }

        #endregion Methods
    }
}
