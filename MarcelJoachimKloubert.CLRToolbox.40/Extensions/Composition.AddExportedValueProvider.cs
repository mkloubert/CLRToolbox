// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace MarcelJoachimKloubert.CLRToolbox.Extensions.Composition
{
    static partial class ClrToolboxCompositionExtensionMethods
    {
        #region Methods (6)

        // Public Methods (6) 

        /// <summary>
        /// Adds a provider delete that returns an exported value.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <param name="container">The composition container where the value(s) should be exported.</param>
        /// <param name="exportedValueProvider">The function that returns the exported value.</param>
        /// <returns>The underlying created composable part object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> and/or <paramref name="exportedValueProvider" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static ComposablePart AddExportedValueProvider<T>(this CompositionContainer container, Func<CompositionContainer, string, T> exportedValueProvider)
        {
            if (exportedValueProvider == null)
            {
                throw new ArgumentNullException("exportedValueProvider");
            }

            return AddExportedValueProvider<T, object>(container: container,
                                                       exportedValueProvider: (c, cn, s) => exportedValueProvider(c, cn),
                                                       providerState: null);
        }

        /// <summary>
        /// Adds a provider delete that returns an exported value.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <param name="container">The composition container where the value(s) should be exported.</param>
        /// <param name="contractName">The nae of the contract.</param>
        /// <param name="exportedValueProvider">The function that returns the exported value.</param>
        /// <returns>The underlying created composable part object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> and/or <paramref name="exportedValueProvider" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static ComposablePart AddExportedValueProvider<T>(this CompositionContainer container, IEnumerable<char> contractName, Func<CompositionContainer, string, T> exportedValueProvider)
        {
            if (exportedValueProvider == null)
            {
                throw new ArgumentNullException("exportedValueProvider");
            }

            return AddExportedValueProvider<T, object>(container: container,
                                                       contractName: contractName,
                                                       exportedValueProvider: (c, cn, s) => exportedValueProvider(c, cn),
                                                       providerState: null);
        }

        /// <summary>
        /// Adds a provider delete that returns an exported value.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <typeparam name="S">Type of the state object for the provider function.</typeparam>
        /// <param name="container">The composition container where the value(s) should be exported.</param>
        /// <param name="exportedValueProvider">The function that returns the exported value.</param>
        /// <param name="providerState">
        /// the state object for <paramref name="exportedValueProvider" />.
        /// </param>
        /// <returns>The underlying created composable part object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> and/or <paramref name="exportedValueProvider" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static ComposablePart AddExportedValueProvider<T, S>(this CompositionContainer container, Func<CompositionContainer, string, S, T> exportedValueProvider, S providerState)
        {
            return AddExportedValueProvider<T, S>(container: container,
                                                  exportedValueProvider: exportedValueProvider,
                                                  providerStateFactory: (c, cn) => providerState);
        }

        /// <summary>
        /// Adds a provider delete that returns an exported value.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <typeparam name="S">Type of the state object for the provider function.</typeparam>
        /// <param name="container">The composition container where the value(s) should be exported.</param>
        /// <param name="contractName">The nae of the contract.</param>
        /// <param name="exportedValueProvider">The function that returns the exported value.</param>
        /// <param name="providerState">
        /// the state object for <paramref name="exportedValueProvider" />.
        /// </param>
        /// <returns>The underlying created composable part object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" /> and/or <paramref name="exportedValueProvider" />
        /// are <see langword="null" /> references.
        /// </exception>
        public static ComposablePart AddExportedValueProvider<T, S>(this CompositionContainer container, IEnumerable<char> contractName, Func<CompositionContainer, string, S, T> exportedValueProvider, S providerState)
        {
            return AddExportedValueProvider<T, S>(container: container,
                                                  contractName: contractName,
                                                  exportedValueProvider: exportedValueProvider,
                                                  providerStateFactory: (c, cn) => providerState);
        }

        /// <summary>
        /// Adds a provider delete that returns an exported value.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <typeparam name="S">Type of the state object for the provider function.</typeparam>
        /// <param name="container">The composition container where the value(s) should be exported.</param>
        /// <param name="exportedValueProvider">The function that returns the exported value.</param>
        /// <param name="providerStateFactory">
        /// The function that returns the state object for <paramref name="exportedValueProvider" />.
        /// </param>
        /// <returns>The underlying created composable part object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" />, <paramref name="exportedValueProvider" /> and/or
        /// <paramref name="providerStateFactory" /> are <see langword="null" /> references.
        /// </exception>
        public static ComposablePart AddExportedValueProvider<T, S>(this CompositionContainer container, Func<CompositionContainer, string, S, T> exportedValueProvider, Func<CompositionContainer, string, S> providerStateFactory)
        {
            return AddExportedValueProvider<T, S>(container: container,
                                                  contractName: AttributedModelServices.GetContractName(typeof(T)),
                                                  exportedValueProvider: exportedValueProvider,
                                                  providerStateFactory: providerStateFactory);
        }

        /// <summary>
        /// Adds a provider delete that returns an exported value.
        /// </summary>
        /// <typeparam name="T">Type of the exported value.</typeparam>
        /// <typeparam name="S">Type of the state object for the provider function.</typeparam>
        /// <param name="container">The composition container where the value(s) should be exported.</param>
        /// <param name="contractName">The nae of the contract.</param>
        /// <param name="exportedValueProvider">The function that returns the exported value.</param>
        /// <param name="providerStateFactory">
        /// The function that returns the state object for <paramref name="exportedValueProvider" />.
        /// </param>
        /// <returns>The underlying created composable part object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="container" />, <paramref name="exportedValueProvider" /> and/or
        /// <paramref name="providerStateFactory" /> are <see langword="null" /> references.
        /// </exception>
        public static ComposablePart AddExportedValueProvider<T, S>(this CompositionContainer container, IEnumerable<char> contractName, Func<CompositionContainer, string, S, T> exportedValueProvider, Func<CompositionContainer, string, S> providerStateFactory)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (exportedValueProvider == null)
            {
                throw new ArgumentNullException("exportedValueProvider");
            }

            if (providerStateFactory == null)
            {
                throw new ArgumentNullException("providerStateFactory");
            }

            var cn = contractName.AsString();

            var batch = new CompositionBatch();
            var result = batch.AddExport(new Export(cn, new Dictionary<string, object>
                {
                    { "ExportTypeIdentity", AttributedModelServices.GetTypeIdentity(typeof(T)) },
                }, () =>
                   {
                       return exportedValueProvider(container,
                                                    cn,
                                                    providerStateFactory(container, cn));
                   }));

            container.Compose(batch);
            return result;
        }

        #endregion Methods
    }
}