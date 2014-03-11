// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    /// <summary>
    /// A stream that only reads zero bytes.
    /// </summary>
    public sealed class ZeroByteStream : ReadOnlyStreamBase
    {
        #region Methods (1)

        // Protected Methods (1) 

        /// <inheriteddoc />
        protected override void OnRead(byte[] buffer, int offset, int count, ref int bytesRead)
        {
            for (int i = 0; i < count; i++)
            {
                buffer[offset + i] = 0;
            }
        }

        #endregion Methods
    }
}
