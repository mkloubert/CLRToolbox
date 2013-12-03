// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Security.AccessControl
{
    /// <summary>
    /// A basic access control list.
    /// </summary>
    public abstract class AccessControlListBase : TMObject, IAccessControlList
    {
        #region Constructors (2)

        /// /// <summary>
        /// Initializes a new instance of the <see cref="AccessControlListBase" /> class.
        /// </summary>
        /// <param name="syncRoot">The value for the <see cref="TMObject._SYNC" /> field.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="syncRoot" /> is <see langword="null" />.
        /// </exception>
        protected AccessControlListBase(object syncRoot)
            : base(syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessControlListBase" /> class.
        /// </summary>
        protected AccessControlListBase()
            : base()
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAccessControlList.this[IEnumerable{char}]" />
        public IAclRole this[IEnumerable<char> name]
        {
            get
            {
                return CollectionHelper.SingleOrDefault(this.GetRoles() ?? CollectionHelper.Empty<IAclRole>(),
                                                        delegate(IAclRole role)
                                                        {
                                                            return role != null &&
                                                                   role.Equals(name);
                                                        });
            }
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAccessControlList.GetRoles()" />
        public abstract IEnumerable<IAclRole> GetRoles();

        #endregion Methods
    }
}
