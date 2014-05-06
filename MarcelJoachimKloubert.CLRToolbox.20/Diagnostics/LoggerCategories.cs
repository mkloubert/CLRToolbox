// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// List of log categories.
    /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
    [global::System.Runtime.Serialization.DataContract]
#endif
    [Flags]
    public enum LoggerFacadeCategories
    {
        /// <summary>
        /// Unknown
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Unknown = 0,

        /// <summary>
        /// Information
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Information = 1,

        /// <summary>
        /// Warnings
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Warnings = 2,

        /// <summary>
        /// Errors
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Errors = 4,

        /// <summary>
        /// Fatal errors
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        FatalErrors = 8,

        /// <summary>
        /// Debug
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Debug = 16,

        /// <summary>
        /// Verbose
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Verbose = 32,

        /// <summary>
        /// Trace
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Trace = 64,

        /// <summary>
        /// Tests
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        Assert = 128,

        /// <summary>
        /// TODOs
        /// </summary>
#if !NET2 && !NET20 && !MONO2 && !MONO20
        [global::System.Runtime.Serialization.EnumMember]
#endif
        TODO = 256,
    }
}
