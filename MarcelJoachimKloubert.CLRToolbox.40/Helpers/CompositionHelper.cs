// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    /// <summary>
    /// Helper class for MEF operations.
    /// </summary>
    public static class CompositionHelper
    {
        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// Exports a value as its export and explicit type.
        /// </summary>
        /// <typeparam name="T">Export type.</typeparam>
        /// <param name="container">The underlying container where to export the value to.</param>
        /// <param name="exportedValue">The value to export.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> is <see langword="null" />.
        /// </exception>
        public static void ComposeExportedValueEx<T>(CompositionContainer container, T exportedValue)
        {
            if (container == null)
            {
                throw new ArgumentNullException("conatiner");
            }

            var typesToExport = new HashSet<Type>();
            typesToExport.Add(typeof(T));

            if (exportedValue != null)
            {
                // also export by explicit type

                typesToExport.Add(exportedValue.GetType());
            }

            foreach (var type in typesToExport)
            {
                ComposeExportedValueForType(container: container,
                                            exportType: type,
                                            exportedValue: exportedValue);
            }
        }
        // Private Methods (1) 

        private static void ComposeExportedValueForType(CompositionContainer container,
                                                        Type exportType,
                                                        object exportedValue)
        {
            var attribModelServiceType = typeof(global::System.ComponentModel.Composition.AttributedModelServices);
            var compositionContainerType = typeof(global::System.ComponentModel.Composition.Hosting.CompositionContainer);

            // find static method
            // ComposeExportedValue<T>(this CompositionContainer, T)
            var composeExportedValueGenericMethod =
                attribModelServiceType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                      .Single(m =>
                                       {
                                           if (m.Name != "ComposeExportedValue")
                                           {
                                               return false;
                                           }

                                           var genericMethodArgs = m.GetGenericArguments();
                                           if (genericMethodArgs.Length != 1)
                                           {
                                               return false;
                                           }

                                           var methodParams = m.GetParameters();
                                           if (methodParams.Length != 2)
                                           {
                                               return false;
                                           }

                                           return methodParams[0].ParameterType.Equals(compositionContainerType) &&
                                                  methodParams[1].ParameterType.Equals(genericMethodArgs[0]);
                                       });

            // create specific typed version of generic method
            var composeExportedValueMethod = composeExportedValueGenericMethod.MakeGenericMethod(exportType);

            composeExportedValueMethod.Invoke(obj: null,
                                              parameters: new object[] { container, exportedValue });
        }

        #endregion Methods
    }
}
