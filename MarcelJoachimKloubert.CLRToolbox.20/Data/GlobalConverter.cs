// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Data
{
    /// <summary>
    /// Access to a global <see cref="IConverter" /> object.
    /// </summary>
    public static partial class GlobalConverter
    {
        #region Fields (1)

        private static ConverterProvider _provider;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes the <see cref="GlobalConverter" /> class.
        /// </summary>
        static GlobalConverter()
        {
            SetConverter(new CommonConverter());
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the global converter instance.
        /// </summary>
        public static IConverter Current
        {
            get { return _provider(); }
        }

        #endregion Properties

        #region Delegates and Events (1)

        // Delegates (1) 

        /// <summary>
        /// Describes the logic that returns the global converter.
        /// </summary>
        /// <returns>The provided <see cref="IConverter" /> object.</returns>
        public delegate IConverter ConverterProvider();

        #endregion Delegates and Events

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Sets the value for <see cref="GlobalConverter.Current" />.
        /// </summary>
        /// <param name="newConverter">The new converter.</param>
        public static void SetConverter(IConverter newConverter)
        {
            SetConverterProvider(newConverter == null ? null : new ConverterProvider(delegate()
            {
                return newConverter;
            }));
        }

        /// <summary>
        /// Sets the logic that returns the value for <see cref="GlobalConverter.Current" />.
        /// </summary>
        /// <param name="newProvider">The new provider delegate.</param>
        public static void SetConverterProvider(ConverterProvider newProvider)
        {
            _provider = newProvider;
        }

        #endregion Methods
    }
}
