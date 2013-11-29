// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Objects
{
    /// <summary>
    /// A basic context for an <see cref="IIdentifiable" /> object.
    /// </summary>
    /// <typeparam name="TObj">The type of the object.</typeparam>
    public abstract class IdentifiableObjectContextBase<TObj> : ObjectContextBase<TObj>
        where TObj : global::MarcelJoachimKloubert.CLRToolbox.IIdentifiable
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiableObjectContextBase{TObj}" /> class.
        /// </summary>
        /// <param name="idObj">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="idObj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        protected IdentifiableObjectContextBase(TObj idObj, object syncRoot)
            : base(idObj, syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiableObjectContextBase{TObj}" /> class.
        /// </summary>
        /// <param name="idObj">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="idObj" /> is <see langword="null" />.
        /// </exception>
        protected IdentifiableObjectContextBase(TObj idObj)
            : base(idObj)
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObjectContextBase.OnCalculateHash(ref MemoryStream)" />
        protected override void OnCalculateHash(ref MemoryStream dataToHash)
        {
            base.OnCalculateHash(ref dataToHash);

            // object ID
            {
                byte[] data = this.Object.Id.ToByteArray();
                dataToHash.Write(data, 0, data.Length);
            }
        }

        #endregion Methods
    }
}
