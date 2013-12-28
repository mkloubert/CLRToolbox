// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    partial class HttpRequest
    {
        #region Nested Classes (1)

        private sealed class HttpMultipartContentParser
        {
            #region Fields (12)

            private readonly byte[] _BOUNDARY;
            private bool _lastBoundaryWasFound;
            private int _lineLen = -1;
            private int _lineStart = -1;
            private string _partContentType;
            private int _partDataLen = -1;
            private int _partDataStart = -1;
            private string _partFilename;
            private string _partName;
            private int _pos;
            private readonly byte[] _RAW_DATA;
            internal readonly HttpRequestMultipartContent[] PARTS;

            #endregion Fields

            #region Constructors (1)

            internal HttpMultipartContentParser(byte[] rawData, byte[] boundary)
            {
                this._RAW_DATA = rawData ?? new byte[0];
                this._BOUNDARY = boundary ?? new byte[0];

                // initialize and extract parts
                {
                    var partList = new List<HttpRequestMultipartContent>();
                    this.Initialize(partList);

                    this.PARTS = partList.ToArray();
                }
            }

            #endregion Constructors

            #region Methods (8)

            // Private Methods (8) 

            private static bool EqualsIgnoreCase(string x, string y)
            {
                if (string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y))
                {
                    return true;
                }

                if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
                {
                    return false;
                }

                if (x.Length != y.Length)
                {
                    return false;
                }

                return string.Compare(x, 0, y, 0, y.Length,
                                      StringComparison.OrdinalIgnoreCase) == 0;
            }

            private string ExtractValueFromHeaderLine(string line, int pos, string name)
            {
                var prefix = name + "=\"";

                var startIndex = CultureInfo.InvariantCulture
                                            .CompareInfo
                                            .IndexOf(line, prefix, pos, CompareOptions.IgnoreCase);
                if (startIndex < 0)
                {
                    return null;
                }

                startIndex += prefix.Length;

                var index = line.IndexOf('"', startIndex);
                if (index < 0)
                {
                    return null;
                }

                if (index == startIndex)
                {
                    return string.Empty;
                }

                return line.Substring(startIndex, index - startIndex);
            }

            private void Initialize(ICollection<HttpRequestMultipartContent> partList)
            {
                while (this.TryGetNextLine())
                {
                    if (this.IsBoundaryLine())
                    {
                        break;
                    }
                }

                if (this.IsEndOfData())
                {
                    return;
                }

                this.ParsePartHeaders();
                while (!this.IsEndOfData())
                {
                    this.ParsePartData();
                    if (this._partDataLen < 0)
                    {
                        continue;
                    }

                    if (this._partName != null)
                    {
                        partList.Add(new HttpRequestMultipartContent(this._partName,
                                                                     this._partFilename,
                                                                     this._partContentType,
                                                                     this._RAW_DATA
                                                                         .Skip(this._partDataStart)
                                                                         .Take(this._partDataLen)
                                                                         .ToArray()));
                    }

                    if (!this.IsEndOfData())
                    {
                        this.ParsePartHeaders();
                    }
                }
            }

            private bool IsBoundaryLine()
            {
                var len = this._BOUNDARY.Length;

                if ((this._lineLen != len) &&
                    (this._lineLen != (len + 2)))
                {
                    return false;
                }

                for (var i = 0; i < len; i++)
                {
                    if (this._RAW_DATA[this._lineStart + i] != this._BOUNDARY[i])
                    {
                        return false;
                    }
                }

                if (this._lineLen != len)
                {
                    if ((this._RAW_DATA[this._lineStart + len] != 45) ||
                        (this._RAW_DATA[(this._lineStart + len) + 1] != 45))
                    {
                        return false;
                    }

                    this._lastBoundaryWasFound = true;
                }

                return true;
            }

            private bool IsEndOfData()
            {
                if (this._pos < this._RAW_DATA.Length)
                {
                    return this._lastBoundaryWasFound;
                }

                return true;
            }

            private void ParsePartData()
            {
                this._partDataStart = this._pos;
                this._partDataLen = -1;

                while (this.TryGetNextLine())
                {
                    if (!this.IsBoundaryLine())
                    {
                        continue;
                    }

                    var i = this._lineStart - 1;

                    if (this._RAW_DATA[i] == 10)
                    {
                        --i;
                    }

                    if (this._RAW_DATA[i] == 13)
                    {
                        --i;
                    }

                    this._partDataLen = i - this._partDataStart + 1;
                    return;
                }
            }

            private void ParsePartHeaders()
            {
                this._partName = null;
                this._partFilename = null;
                this._partContentType = null;

                while (this.TryGetNextLine())
                {
                    if (this._lineLen < 1)
                    {
                        return;
                    }

                    var buffer = this._RAW_DATA.Skip(this._lineStart).Take(this._lineLen).ToArray();
                    var line = Encoding.ASCII.GetString(buffer);

                    var index = line.IndexOf(':');
                    if (index < 0)
                    {
                        continue;
                    }

                    var name = line.Substring(0, index);
                    if (EqualsIgnoreCase(name, "Content-disposition"))
                    {
                        this._partName = this.ExtractValueFromHeaderLine(line, index + 1, "name");
                        this._partFilename = this.ExtractValueFromHeaderLine(line, index + 1, "filename");
                    }
                    else if (EqualsIgnoreCase(name, "Content-type"))
                    {
                        this._partContentType = line.Substring(index + 1).Trim();
                    }
                }
            }

            private bool TryGetNextLine()
            {
                var i = this._pos;
                this._lineStart = -1;

                while (i < this._RAW_DATA.Length)
                {
                    if (this._RAW_DATA[i] == 10)
                    {
                        this._lineStart = this._pos;
                        this._lineLen = i - this._pos;
                        this._pos = i + 1;

                        if ((this._lineLen > 0) && (this._RAW_DATA[i - 1] == 13))
                        {
                            --this._lineLen;
                        }

                        break;
                    }

                    if (++i == this._RAW_DATA.Length)
                    {
                        this._lineLen = i - this._pos;
                        this._lineStart = this._pos;
                        this._pos = this._RAW_DATA.Length;
                    }
                }

                return this._lineStart >= 0;
            }

            #endregion Methods
        }

        #endregion Nested Classes
    }
}
