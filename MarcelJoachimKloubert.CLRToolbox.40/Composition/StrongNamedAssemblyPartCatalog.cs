// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Security;
using MarcelJoachimKloubert.CLRToolbox.Collections.Generic;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;

namespace MarcelJoachimKloubert.CLRToolbox.Composition
{
    /// <summary>
    /// A <see cref="ComposablePartCatalog" /> that uses a list of public assembly keys
    /// to verify if an <see cref="Assembly" /> or <see cref="Type" /> is allowed to use
    /// for composition operations.
    /// </summary>
    public class StrongNamedAssemblyPartCatalog : ComposablePartCatalog
    {
        #region Fields (2)

        private readonly AggregateCatalog _INNER_CATALOG = new AggregateCatalog();
        private readonly byte[][] _TRUSTED_ASM_KEYS;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of <see cref="StrongNamedAssemblyPartCatalog" /> class.
        /// </summary>
        /// <param name="trustedKeys">The list of trusted keys.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="trustedKeys" /> is <see langword="null" />.
        /// </exception>
        public StrongNamedAssemblyPartCatalog(IEnumerable<byte[]> trustedKeys)
        {
            if (trustedKeys == null)
            {
                throw new ArgumentNullException("trustedKeys");
            }

            this._TRUSTED_ASM_KEYS = trustedKeys.Select(x => x ?? new byte[0])
                                                .Distinct(new DelegateEqualityComparer<byte[]>(equalsHandler: (x, y) =>
                                                     {
                                                         return CollectionHelper.SequenceEqual(x, y);
                                                     }))
                                                .ToArray();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="StrongNamedAssemblyPartCatalog" /> class.
        /// </summary>
        /// <param name="trustedKeys">The list of trusted keys.</param>
        /// <exception cref="NullReferenceException">
        /// <paramref name="trustedKeys" /> is <see langword="null" />.
        /// </exception>
        public StrongNamedAssemblyPartCatalog(IEnumerable<IEnumerable<byte>> trustedKeys)
            : this(trustedKeys.Select(k => k.AsArray()))
        {

        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ComposablePartCatalog.Parts" />
        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return this._INNER_CATALOG.Parts; }
        }

        #endregion Properties

        #region Methods (10)

        // Public Methods (10) 

        /// <summary>
        /// Adds a list of assemblies.
        /// </summary>
        /// <param name="asms">The list of assemblies to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asms" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// At least one assembly is NOT trusted.
        /// </exception>
        public void AddAssemblies(IEnumerable<Assembly> asms)
        {
            if (asms == null)
            {
                throw new ArgumentNullException("asms");
            }

            foreach (var a in asms)
            {
                this.AddAssembly(a);
            }
        }

        /// <summary>
        /// Adds a list of assemblies.
        /// </summary>
        /// <param name="asms">The list of assemblies to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asms" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// At least one assembly is NOT trusted.
        /// </exception>
        public void AddAssemblies(params Assembly[] asms)
        {
            this.AddAssemblies(asms as IEnumerable<Assembly>);
        }

        /// <summary>
        /// Adds an assembly.
        /// </summary>
        /// <param name="asm">The assembly to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asm" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="asm" /> is NO trusted assembly.
        /// </exception>
        public void AddAssembly(Assembly asm)
        {
            if (asm == null)
            {
                throw new ArgumentNullException("asm");
            }

            if (!this.IsTrustedAssembly(asm))
            {
                throw new InvalidOperationException(string.Format("'{0}' is NO trusted assembly!",
                                                                  asm.FullName));
            }

            this._INNER_CATALOG
                .Catalogs
                .Add(new AssemblyCatalog(asm));
        }

        /// <summary>
        /// Adds a type.
        /// </summary>
        /// <param name="type">The type to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="type" /> is NO trusted type.
        /// </exception>
        public void AddType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!this.IsTrustedType(type))
            {
                throw new InvalidOperationException(string.Format("'{0}' is NO trusted type!",
                                                                  type.FullName));
            }

            this._INNER_CATALOG
                .Catalogs
                .Add(new TypeCatalog(type));
        }

        /// <summary>
        /// Adds a list of types.
        /// </summary>
        /// <param name="types">The list of types to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="types" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// At least one type is NOT trusted.
        /// </exception>
        public void AddTypes(IEnumerable<Type> types)
        {
            if (types == null)
            {
                throw new ArgumentNullException("types");
            }

            foreach (var t in types)
            {
                this.AddType(t);
            }
        }

        /// <summary>
        /// Adds a list of types.
        /// </summary>
        /// <param name="types">The list of types to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="types" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// At least one type is NOT trusted.
        /// </exception>
        public void AddTypes(params Type[] types)
        {
            this.AddTypes(types as IEnumerable<Type>);
        }

        /// <summary>
        /// Clears the list of assemblies and types.
        /// </summary>
        public void Clear()
        {
            this._INNER_CATALOG.Catalogs.Clear();
        }

        /// <summary>
        /// Checks if an assembly is trusted or not.
        /// </summary>
        /// <param name="asm">The assembly to check.</param>
        /// <returns>Is trusted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="asm" /> is <see langword="null" />.
        /// </exception>
        public bool IsTrustedAssembly(Assembly asm)
        {
            if (asm == null)
            {
                throw new ArgumentNullException("asm");
            }

            return this.IsTrustedAssembly(name: asm.GetName());
        }

        /// <summary>
        /// Checks an assembly (by its name) if it is trusted or not.
        /// </summary>
        /// <param name="name">The assembly name to check.</param>
        /// <returns>Underlying assembly is trusted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name" /> is <see langword="null" />.
        /// </exception>
        public bool IsTrustedAssembly(AssemblyName name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            try
            {
                var publicKey = name.GetPublicKey() ?? new byte[0];

                return this._TRUSTED_ASM_KEYS
                           .Any(tak => CollectionHelper.SequenceEqual(tak, publicKey));
            }
            catch (SecurityException)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a type is trusted or not.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>Is trusted or not.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />.
        /// </exception>
        public bool IsTrustedType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return this.IsTrustedAssembly(type.Assembly);
        }

        #endregion Methods
    }
}
