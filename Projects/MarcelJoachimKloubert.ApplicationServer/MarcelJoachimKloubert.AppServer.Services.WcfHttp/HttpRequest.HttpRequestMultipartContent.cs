// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp
{
    partial class HttpRequest
    {
        #region Nested Classes (1)

        private sealed class HttpRequestMultipartContent
        {
            #region Fields (4)

            internal readonly string CONTENT_TYPE;
            internal readonly byte[] DATA;
            internal readonly string FILE_NAME;
            internal readonly string NAME;

            #endregion Fields

            #region Constructors (1)

            internal HttpRequestMultipartContent(string name,
                                                 string fileName,
                                                 string contentType,
                                                 byte[] data)
            {
                this.CONTENT_TYPE = contentType;
                this.DATA = data;
                this.FILE_NAME = fileName;
                this.NAME = name;
            }

            #endregion Constructors
        }

        #endregion Nested Classes
    }
}
