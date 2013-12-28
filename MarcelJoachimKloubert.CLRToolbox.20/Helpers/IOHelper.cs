// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.Helpers
{
    /// <summary>
    /// Helper class for I/O operations.
    /// </summary>
    public static partial class IOHelper
    {
        #region Fields (1)

        /// <summary>
        /// Stores the default buffer size for <see cref="Stream" />s in byte.
        /// </summary>
        public const int DEFAULT_STREAM_BUFFER_SIZE = 81920;

        #endregion Fields
    }
}
