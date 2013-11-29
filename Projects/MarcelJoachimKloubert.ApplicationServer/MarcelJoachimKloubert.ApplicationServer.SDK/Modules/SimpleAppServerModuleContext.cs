// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// Simple implementation of <see cref="IAppServerModuleContext" /> interface.
    /// </summary>
    public sealed class SimpleAppServerModuleContext : IdentifiableObjectContextBase<IAppServerModule>,
                                                       IAppServerModuleContext
    {
        #region Fields (3)

        private byte[] _assemblyContent;
        private string _assemblyFile;
        private Lazy<byte[]> _lazyHash;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAppServerModuleContext" /> class.
        /// </summary>
        /// <param name="module">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <param name="syncRoot">The unique object for sync operations.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="module" /> and/or <paramref name="syncRoot" /> are <see langword="null" />.
        /// </exception>
        public SimpleAppServerModuleContext(IAppServerModule module, object syncRoot)
            : base(module, syncRoot)
        {
            this.ResetLazyHash(this._assemblyContent);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAppServerModuleContext" /> class.
        /// </summary>
        /// <param name="module">The value for <see cref="ObjectContextBase{TObj}.Object" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="module" /> is <see langword="null" />.
        /// </exception>
        public SimpleAppServerModuleContext(IAppServerModule module)
            : this(module, new object())
        {

        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets or sets the binary content of the underlying assembly file.
        /// </summary>
        public byte[] AssemblyContent
        {
            get
            {
                lock (this._SYNC)
                {
                    return this._assemblyContent;
                }
            }

            set
            {
                lock (this._SYNC)
                {
                    this._assemblyContent = value;
                    this.ResetLazyHash(value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObjectContextBase.AssemblyFile" />
        public override string AssemblyFile
        {
            get { return this._assemblyFile; }
        }

        /// <summary>
        /// Gets or sets the inner service locator.
        /// </summary>
        public IServiceLocator InnerServiceLocator
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (5)

        // Public Methods (1) 

        /// <summary>
        /// Sets the value for <see cref="SimpleAppServerModuleContext.AssemblyFile" /> property.
        /// </summary>
        /// <param name="asmFile">The new value.</param>
        public void SetAssemblyFile(IEnumerable<char> asmFile)
        {
            this._assemblyFile = asmFile.AsString();
        }
        // Protected Methods (3) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IdentifiableObjectContextBase{IAppServerModule}.OnCalculateHash(ref MemoryStream)" />
        protected override void OnCalculateHash(ref MemoryStream dataToHash)
        {
            var data = this._lazyHash.Value;
            if (data != null)
            {
                dataToHash.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetAllInstances(Type, object)" />
        protected override IEnumerable<object> OnGetAllInstances(Type serviceType, object key)
        {
            var innerLoc = this.InnerServiceLocator;
            if (innerLoc != null)
            {
                return innerLoc.GetAllInstances(serviceType, key);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ServiceLocatorBase.OnGetInstance(Type, object)" />
        protected override object OnGetInstance(Type serviceType, object key)
        {
            var innerLoc = this.InnerServiceLocator;
            if (innerLoc != null)
            {
                return innerLoc.GetInstance(serviceType, key);
            }

            return null;
        }
        // Private Methods (1) 

        private void ResetLazyHash(byte[] asmContent)
        {
            this._lazyHash = new Lazy<byte[]>(() =>
                {
                    var dataToHash = new MemoryStream();
                    try
                    {
                        base.OnCalculateHash(ref dataToHash);
                        if (dataToHash != null)
                        {
                            if (asmContent != null)
                            {
                                dataToHash.Write(asmContent, 0, asmContent.Length);
                            }
                        }

                        return dataToHash != null ? dataToHash.ToArray() : null;
                    }
                    finally
                    {
                        if (dataToHash != null)
                        {
                            dataToHash.Dispose();
                        }
                    }
                }, isThreadSafe: false);
        }

        #endregion Methods
    }
}
