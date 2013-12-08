// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security.Cryptography;

namespace MarcelJoachimKloubert.CLRToolbox.Objects
{
    partial class ObjectContextBase
    {
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext.AssemblyFile" />
        public virtual string AssemblyFile
        {
            get { return null; }
        }

        #endregion Properties

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// Creates the object that uses the algorithm to hash that context.
        /// </summary>
        /// <returns>The created hash algorithm.</returns>
        protected HashAlgorithm CreateHasher()
        {
            return new SHA256Managed();
        }

        #endregion Methods
    }
}
