// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Diagnostics;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Objects;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;
using MarcelJoachimKloubert.CLRToolbox.Timing;

namespace MarcelJoachimKloubert.ApplicationServer.Modules
{
    /// <summary>
    /// Simple implementation of <see cref="IAppServerModuleContext" /> interface.
    /// </summary>
    public sealed class SimpleAppServerModuleContext : IdentifiableObjectContextBase<IAppServerModule>,
                                                       IAppServerModuleContext
    {
        #region Fields (2)

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
            this.ResetLazyHash();
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

        #region Properties (7)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ObjectContextBase.AssemblyFile" />
        public override string AssemblyFile
        {
            get { return this._assemblyFile; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerModuleContext.Config" />
        public IConfigRepository Config
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the inner service locator.
        /// </summary>
        public IServiceLocator InnerServiceLocator
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerModuleContext.Logger" />
        public ILoggerFacade Logger
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITimeProvider.Now" />
        public DateTimeOffset Now
        {
            get { return (this.NowProvider ?? Default_NowProvider)(); }
        }

        /// <summary>
        /// Gets or sets the logic for the <see cref="SimpleAppServerModuleContext.Now" /> property.
        /// </summary>
        public Func<DateTimeOffset> NowProvider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the items for <see cref="SimpleAppServerModuleContext.GetOtherModules()" /> method.
        /// </summary>
        public IEnumerable<IAppServerModule> OtherModules
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (7)

        // Public Methods (2) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IAppServerModuleContext.GetOtherModules()" />
        public IList<IAppServerModule> GetOtherModules()
        {
            var otherModules = this.OtherModules ?? Enumerable.Empty<IAppServerModule>();

            return new List<IAppServerModule>(otherModules.OfType<IAppServerModule>());
        }

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
        // Private Methods (2) 

        private static DateTimeOffset Default_NowProvider()
        {
            return DateTimeOffset.Now;
        }

        private void ResetLazyHash()
        {
            this._lazyHash = new Lazy<byte[]>(() =>
                {
                    var dataToHash = new MemoryStream();
                    try
                    {
                        base.OnCalculateHash(ref dataToHash);
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
