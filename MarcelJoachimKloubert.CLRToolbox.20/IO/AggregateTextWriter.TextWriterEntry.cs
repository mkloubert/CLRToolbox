// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System.IO;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class AggregateTextWriter
    {
        #region Nested Classes (1)

        private sealed class TextWriterEntry
        {
            #region Fields (3)

            internal readonly bool CLOSE;
            internal readonly bool DISPOSE;
            internal readonly TextWriter WRITER;

            #endregion Fields

            #region Constructors (1)

            internal TextWriterEntry(TextWriter writer, bool close, bool dispose)
            {
                this.CLOSE = close;
                this.DISPOSE = dispose;
                this.WRITER = writer;
            }

            #endregion Constructors
        }

        #endregion Nested Classes
    }
}