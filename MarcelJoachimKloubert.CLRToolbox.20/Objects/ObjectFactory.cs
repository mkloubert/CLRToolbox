// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using MarcelJoachimKloubert.CLRToolbox.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MarcelJoachimKloubert.CLRToolbox.Objects
{
    /// <summary>
    /// A class for building (dynamic / proxy) object.
    /// </summary>
    public sealed class ObjectFactory : TMObject
    {
        #region Fields (4)

        private readonly AssemblyBuilder _ASM_BUILDER;
        private static ObjectFactory _instance;
        private readonly IDictionary<Type, Type> _INTERFACE_PROXY_TYPES = new Dictionary<Type, Type>();
        private readonly ModuleBuilder _MOD_BUILDER;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFactory" /> class.
        /// </summary>
        /// <param name="domain">The underlying app domain.</param>
        /// <param name="asmName">The name of the dynamic assembly for <see cref="ObjectFactory.AssemblyBuilder" /> property.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="domain" /> and/or <paramref name="asmName" /> are <see langword="null" /> references.
        /// </exception>
        public ObjectFactory(AppDomain domain, AssemblyName asmName)
        {
            if (domain == null)
            {
                throw new ArgumentNullException("domain");
            }

            if (asmName == null)
            {
                throw new ArgumentNullException("asmName");
            }

            this._ASM_BUILDER = domain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);

            this._MOD_BUILDER = this._ASM_BUILDER
                                    .DefineDynamicModule(string.Format("TMDynamicObjectFactoryModule_{0:N}_{1}",
                                                                       Guid.NewGuid(),
                                                                       this.GetHashCode()));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFactory" /> class.
        /// </summary>
        public ObjectFactory()
            : this(AppDomain.CurrentDomain, Assembly.GetCallingAssembly().GetName())
        {
        }

        #endregion Constructors

        #region Properties (3)

        /// <summary>
        /// Gets the underlying assembly builder.
        /// </summary>
        public AssemblyBuilder AssemblyBuilder
        {
            get { return this._ASM_BUILDER; }
        }

        /// <summary>
        /// Gets the default singleton instance of that class.
        /// </summary>
        public static ObjectFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObjectFactory(AppDomain.CurrentDomain,
                                                  Assembly.GetExecutingAssembly().GetName());
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets the underlying module builder.
        /// </summary>
        public ModuleBuilder ModuleBuilder
        {
            get { return this._MOD_BUILDER; }
        }

        #endregion Properties

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// Creates a new proxy object for an interface type.
        /// </summary>
        /// <typeparam name="T">Type of the interface.</typeparam>
        /// <returns>The proxy object.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="T" /> represents no interface.
        /// </exception>
        public T CreateProxyForInterface<T>()
        {
            return (T)this.CreateProxyForInterface(typeof(T));
        }

        /// <summary>
        /// Creates a new proxy object for an interface type.
        /// </summary>
        /// <param name="interfaceType">Type of the interface.</param>
        /// <returns>The proxy object.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="interfaceType" /> represents no interface.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="interfaceType" /> is a <see langword="null" /> reference.
        /// </exception>
        public object CreateProxyForInterface(Type interfaceType)
        {
            Type propxyType = this.GetProxyTypeForInterface(interfaceType);

            return Activator.CreateInstance(propxyType);
        }

        /// <summary>
        /// Returns the proxy type for an interface type.
        /// </summary>
        /// <typeparam name="T">Type of the interface.</typeparam>
        /// <returns>The proxy type.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="T" /> represents no interface.
        /// </exception>
        public Type GetProxyTypeForInterface<T>()
        {
            return this.GetProxyTypeForInterface(typeof(T));
        }

        /// <summary>
        /// Returns the proxy type for an interface type.
        /// </summary>
        /// <param name="interfaceType">Type of the interface.</param>
        /// <returns>The proxy type.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="interfaceType" /> represents no interface.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="interfaceType" /> is a <see langword="null" /> reference.
        /// </exception>
        public Type GetProxyTypeForInterface(Type interfaceType)
        {
            Type result;

            lock (this._SYNC)
            {
                if (interfaceType == null)
                {
                    throw new ArgumentNullException("interfaceType");
                }

                if (interfaceType.IsInterface == false)
                {
                    throw new ArgumentException("interfaceType");
                }

                if (this._INTERFACE_PROXY_TYPES.TryGetValue(interfaceType, out result) == false)
                {
                    result = this.CreateInterfaceImplementation(interfaceType);

                    this._INTERFACE_PROXY_TYPES
                        .Add(interfaceType, result);
                }
            }

            return result;
        }

        // Private Methods (1) 

        private Type CreateInterfaceImplementation(Type interfaceType)
        {
            Type genericPBType = typeof(ProxyBuilder<>);
            Type pbType = genericPBType.MakeGenericType(interfaceType);

            object pb = Activator.CreateInstance(pbType);

            return (Type)pbType.InvokeMember("CreateType",
                                             BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance,
                                             null,
                                             pb,
                                             new object[] { this.ModuleBuilder });
        }

        #endregion Methods
    }
}