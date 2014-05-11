// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Tests
{
    /// <summary>
    /// Exception that is thrown if an assert operation failed.
    /// </summary>
    public class AssertException : Exception
    {
        #region Constructors (3)

        /// <inheriteddoc />
        public AssertException()
            : base()
        {
        }

        /// <inheriteddoc />
        public AssertException(string message)
            : base(message)
        {
        }
#if !WINDOWS_PHONE

        /// <inheriteddoc />
        protected AssertException(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

#endif
        #endregion Constructors
    }
}