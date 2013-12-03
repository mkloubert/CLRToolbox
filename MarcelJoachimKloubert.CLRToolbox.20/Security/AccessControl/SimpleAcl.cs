// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// Simple implementation of an <see cref="IAccessControlList" /> object.
    /// </summary>
    public class SimpleAcl : AccessControlListBase
    {
        #region Fields (1)

        private AclRoleProvider _getRolesFunc;

        #endregion Fields

        #region Constructors (2)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAcl" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        public SimpleAcl(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAcl" /> class.
        /// </summary>
        public SimpleAcl()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets or sets the logic for <see cref="SimpleAcl.GetRoles()" /> method.
        /// </summary>
        public AclRoleProvider GetRolesFunc
        {
            get { return this._getRolesFunc; }

            set { this._getRolesFunc = value; }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes logic for the <see cref="SimpleAcl.GetRoles()" /> method.
        /// </summary>
        /// <returns>The roles.</returns>
        public delegate IEnumerable<IAclRole> AclRoleProvider();

        #endregion Delegates and Events

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="AccessControlListBase.GetRoles()" />
        public override IEnumerable<IAclRole> GetRoles()
        {
            AclRoleProvider func = this.GetRolesFunc;
            return func != null ? func() : null;
        }

        #endregion Methods
    }
}
