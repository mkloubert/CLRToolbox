// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.CLRToolbox.Objects
{
    #region CLASS: ObjectContextBase

    /// <summary>
    /// A basic object context.
    /// </summary>
    public abstract partial class ObjectContextBase : ServiceLocatorBase, IObjectContext
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextBase" /> class.
        /// </summary>
        /// <param name="obj">The value for <see cref="ObjectContextBase.Object" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        protected ObjectContextBase(object obj, object syncRoot)
            : base(syncRoot)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            this.Object = obj;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextBase" /> class.
        /// </summary>
        /// <param name="obj">The value for <see cref="ObjectContextBase.Object" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        protected ObjectContextBase(object obj)
            : this(obj, new object())
        {

        }

        #endregion Constructors

        #region Properties (2)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext.Assembly" />
        public virtual Assembly Assembly
        {
            get { return this.Object.GetType().Assembly; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext.Object" />
        public object Object
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext.CalculateHash()" />
        public virtual byte[] CalculateHash()
        {
            lock (this._SYNC)
            {
                MemoryStream result = new MemoryStream();
                try
                {
                    this.OnCalculateHash(ref result);
                    if (result == null)
                    {
                        return null;
                    }

                    using (SHA256Managed sha256 = new SHA256Managed())
                    {
                        result.Position = 0;
                        return sha256.ComputeHash(result);
                    }
                }
                finally
                {
                    if (result != null)
                    {
                        result.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext.GetHashAsHexString()" />
        public string GetHashAsHexString()
        {
            return StringHelper.AsHexString(this.CalculateHash());
        }
        // Protected Methods (1) 

        /// <summary>
        /// Collects the data for <see cref="ObjectContextBase.CalculateHash()" /> that should be hashed.
        /// </summary>
        /// <param name="dataToHash">The target stream witht the data that should be hashed.</param>
        protected virtual void OnCalculateHash(ref MemoryStream dataToHash)
        {
            // assembly file path
            string asmFile = this.AssemblyFile;
            if (!StringHelper.IsNullOrWhiteSpace(asmFile))
            {
                try
                {
                    // check if Unix operating system like Linux or MacOS
                    //
                    // 4: Unix
                    // 6: MacOSX
                    // 128: Mono ID for Unix
                    int p = (int)Environment.OSVersion.Platform;
                    if (!((p == 4) || (p == 6) || (p == 128)))
                    {
                        // no operating system that handles file paths
                        // case sensitive, so convert path to lower case characters

                        asmFile = asmFile.ToLower().Trim();
                    }
                }
                catch
                {
                    // ignore
                }

                byte[] data = Encoding.UTF8.GetBytes(asmFile);
                dataToHash.Write(data, 0, data.Length);
            }

            // assembly full name
            Assembly asm = this.Assembly;
            if (asm != null)
            {
                string asmName = asm.FullName;
                if (!StringHelper.IsNullOrWhiteSpace(asmName))
                {
                    byte[] data = Encoding.UTF8.GetBytes(asmName);
                    dataToHash.Write(data, 0, data.Length);
                }
            }

            // full name of the type of the underlying object
            string objTypeName = this.Object.GetType().FullName;
            if (!StringHelper.IsNullOrWhiteSpace(objTypeName))
            {
                byte[] data = Encoding.UTF8.GetBytes(objTypeName);
                dataToHash.Write(data, 0, data.Length);
            }
        }

        #endregion Methods
    }

    #endregion

    #region CLASS: ObjectContextBase<TObj>

    /// <summary>
    /// 
    /// </summary>
    /// <see cref="ObjectContextBase" />
    public abstract partial class ObjectContextBase<TObj> : ObjectContextBase, IObjectContext<TObj>
    {
        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextBase{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        protected ObjectContextBase(TObj obj, object syncRoot)
            : base(obj, syncRoot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContextBase{TObj}" /> class.
        /// </summary>
        /// <param name="obj">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj" /> is <see langword="null" />.
        /// </exception>
        protected ObjectContextBase(TObj obj)
            : base(obj)
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext{TObj}.Object" />
        public new TObj Object
        {
            get { return (TObj)base.Object; }
        }

        #endregion Properties
    }

    #endregion
}
