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
    /// Class that builds proxy types and object for an interface.
    /// </summary>
    /// <typeparam name="T">Type of the interface.</typeparam>
    public sealed class ProxyBuilder<T> : TMObject
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of <see cref="ProxyBuilder{T}" /> class.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Generic type does not represent an interface type.
        /// </exception>
        public ProxyBuilder()
        {
            if (typeof(T).IsInterface == false)
            {
                throw new ArgumentException("T");
            }
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the interface <see cref="Type" />.
        /// </summary>
        public Type InterfaceType
        {
            get { return typeof(T); }
        }

        #endregion Properties

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// Creates a propxy <see cref="Type" /> for the interface of <typeparamref name="T" />.
        /// </summary>
        /// <param name="modBuilder">The module builder to use.</param>
        /// <returns>The created type object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="modBuilder" /> is <see langword="null" />.
        /// </exception>
        public Type CreateType(ModuleBuilder modBuilder)
        {
            return this.CreateType(modBuilder,
                                   delegate(Type interfaceType)
                                   {
                                       return "TMImplOf_";
                                   });
        }

        /// <summary>
        /// Creates a propxy <see cref="Type" /> for the interface of <typeparamref name="T" />.
        /// </summary>
        /// <param name="modBuilder">The module builder to use.</param>
        /// <param name="proxyTypeNamePrefixProvider">The logic that returns the prefix for the full name of the proxy type to create.</param>
        /// <returns>The created type object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="modBuilder" /> is <see langword="null" />.
        /// </exception>
        public Type CreateType(ModuleBuilder modBuilder,
                               Func<Type, IEnumerable<char>> proxyTypeNamePrefixProvider)
        {
            return this.CreateType(modBuilder,
                                   proxyTypeNamePrefixProvider,
                                   delegate(Type interfaceType)
                                   {
                                       return string.Format("_{0:N}_{1}",
                                                            Guid.NewGuid(),
                                                            this.GetHashCode());
                                   });
        }

        /// <summary>
        /// Creates a propxy <see cref="Type" /> for the interface of <typeparamref name="T" />.
        /// </summary>
        /// <param name="modBuilder">The module builder to use.</param>
        /// <param name="proxyTypeNamePrefixProvider">The logic that returns the prefix for the full name of the proxy type to create.</param>
        /// <param name="proxyTypeNameSuffixProvider">The logic that returns the suffix for the full name of the proxy type to create.</param>
        /// <returns>The created type object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="modBuilder" /> is <see langword="null" />.
        /// </exception>
        public Type CreateType(ModuleBuilder modBuilder,
                               Func<Type, IEnumerable<char>> proxyTypeNamePrefixProvider,
                               Func<Type, IEnumerable<char>> proxyTypeNameSuffixProvider)
        {
            return this.CreateType(modBuilder,
                                   proxyTypeNamePrefixProvider,
                                   delegate(Type interfaceType)
                                   {
                                       return this.InterfaceType.Name;
                                   },
                                   proxyTypeNameSuffixProvider);
        }

        /// <summary>
        /// Creates a propxy <see cref="Type" /> for the interface of <typeparamref name="T" />.
        /// </summary>
        /// <param name="modBuilder">The module builder to use.</param>
        /// <param name="proxyTypeNamePrefixProvider">The logic that returns the prefix for the full name of the proxy type to create.</param>
        /// <param name="proxyTypeNameProvider">The logic that returns the name part of the proxy type to create.</param>
        /// <param name="proxyTypeNameSuffixProvider">The logic that returns the suffix for the full name of the proxy type to create.</param>
        /// <returns>The created type object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="modBuilder" /> is <see langword="null" />.
        /// </exception>
        public Type CreateType(ModuleBuilder modBuilder,
                               Func<Type, IEnumerable<char>> proxyTypeNamePrefixProvider,
                               Func<Type, IEnumerable<char>> proxyTypeNameProvider,
                               Func<Type, IEnumerable<char>> proxyTypeNameSuffixProvider)
        {
            if (modBuilder == null)
            {
                throw new ArgumentNullException("modBuilder");
            }

            string prefix = proxyTypeNamePrefixProvider == null ? null
                                                                : (StringHelper.AsString(proxyTypeNamePrefixProvider(this.InterfaceType)) ?? string.Empty).Trim();
            string name = proxyTypeNameProvider == null ? null
                                                        : (StringHelper.AsString(proxyTypeNameProvider(this.InterfaceType)) ?? string.Empty).Trim();
            string suffix = proxyTypeNameSuffixProvider == null ? null
                                                                : (StringHelper.AsString(proxyTypeNameSuffixProvider(this.InterfaceType)) ?? string.Empty).Trim();

            Type baseType = typeof(object);

            TypeBuilder typeBuilder = modBuilder.DefineType(string.Format("{0}{1}{2}",
                                                                          prefix,
                                                                          name,
                                                                          suffix),
                                                            TypeAttributes.Public | TypeAttributes.Class,
                                                            baseType);

            typeBuilder.AddInterfaceImplementation(this.InterfaceType);

            List<PropertyInfo> properties = new List<PropertyInfo>();
            CollectProperties(properties, this.InterfaceType);

            // properties
            {
                foreach (PropertyInfo p in properties)
                {
                    string propertyName = p.Name;
                    Type propertyType = p.PropertyType;

                    PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName,
                                                                                 PropertyAttributes.None,
                                                                                 propertyType,
                                                                                 Type.EmptyTypes);

                    string fieldName = char.ToLower(propertyName[0]) +
                                       new string(CollectionHelper.ToArray(CollectionHelper.Skip(propertyName, 1)));

                    FieldBuilder field = typeBuilder.DefineField("_" + fieldName,
                                                                 propertyType,
                                                                 FieldAttributes.Family);

                    // getter
                    {
                        MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + propertyName,
                                                                               MethodAttributes.Public | MethodAttributes.Virtual,
                                                                               propertyType,
                                                                               Type.EmptyTypes);

                        ILGenerator ilGen = methodBuilder.GetILGenerator();

                        ilGen.Emit(OpCodes.Ldarg_0);      // load "this"
                        ilGen.Emit(OpCodes.Ldfld, field); // load the property's underlying field onto the stack
                        ilGen.Emit(OpCodes.Ret);          // return the value on the stack

                        propertyBuilder.SetGetMethod(methodBuilder);
                    }

                    // setter
                    {
                        MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + propertyName,
                                                                               MethodAttributes.Public | MethodAttributes.Virtual,
                                                                               typeof(void),
                                                                               new Type[] { propertyType });

                        ILGenerator ilGen = methodBuilder.GetILGenerator();

                        ilGen.Emit(OpCodes.Ldarg_0);      // load "this"
                        ilGen.Emit(OpCodes.Ldarg_1);      // load "value" onto the stack
                        ilGen.Emit(OpCodes.Stfld, field); // set the field equal to the "value" on the stack
                        ilGen.Emit(OpCodes.Ret);          // return nothing

                        propertyBuilder.SetSetMethod(methodBuilder);
                    }
                }
            }

            // constructor
            {
                ConstructorInfo baseConstructor = baseType.GetConstructor(new Type[0]);

                ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public,
                                                                                      CallingConventions.Standard,
                                                                                      Type.EmptyTypes);

                ILGenerator ilGen = constructorBuilder.GetILGenerator();
                ilGen.Emit(OpCodes.Ldarg_0);                  // load "this"
                ilGen.Emit(OpCodes.Call, baseConstructor);    // call the base constructor

                //TODO
                // define initial values

                ilGen.Emit(OpCodes.Ret);    // return nothing
            }

            return typeBuilder.CreateType();
        }

        // Private Methods (1) 

        private static void CollectProperties(ICollection<PropertyInfo> properties, Type type)
        {
            CollectionHelper.AddRange(properties, type.GetProperties());

            foreach (Type subInterface in type.GetInterfaces())
            {
                CollectProperties(properties, subInterface);
            }
        }

        #endregion Methods
    }
}