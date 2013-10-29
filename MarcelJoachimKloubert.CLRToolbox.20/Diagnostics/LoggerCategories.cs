// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics
{
    /// <summary>
    /// List of log categories.
    /// </summary>
    [Flags]
    public enum LoggerFacadeCategories
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Information
        /// </summary>
        Information = 1,

        /// <summary>
        /// Warnings
        /// </summary>
        Warnings = 2,

        /// <summary>
        /// Errors
        /// </summary>
        Errors = 4,

        /// <summary>
        /// Fatal errors
        /// </summary>
        FatalErrors = 8,

        /// <summary>
        /// Debug
        /// </summary>
        Debug = 16,

        /// <summary>
        /// Verbose
        /// </summary>
        Verbose = 32,

        /// <summary>
        /// Trace
        /// </summary>
        Trace = 64,

        /// <summary>
        /// Tests
        /// </summary>
        Assert = 128,

        /// <summary>
        /// TODOs
        /// </summary>
        TODO = 256,
    }
}
