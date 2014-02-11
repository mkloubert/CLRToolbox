// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MarcelJoachimKloubert.CloudNET.Handlers
{
    internal sealed class FilesHttpHandler : BasicAuthHttpHandlerBase
    {
        #region Methods (5)

        // Protected Methods (1) 

        protected override void OnProcessRequest(ICloudRequest request)
        {
            Action<ICloudRequest> actionToInvoke = null;

            switch ((request.Context.Request.HttpMethod ?? string.Empty).ToUpper().Trim())
            {
                case "DELETE":
                    actionToInvoke = this.DeleteFile;
                    break;

                case "GET":
                    {
                        switch ((request.Context.Request.Params["action"] ?? string.Empty).ToLower().Trim())
                        {
                            case "":
                                actionToInvoke = this.DownloadFile;
                                break;

                            case "list":
                                actionToInvoke = this.ListDirectory;
                                break;

                            default:
                                request.Context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                                break;
                        }
                    }
                    break;

                case "OPTIONS":
                    request.Context.Response.Headers["Allow"] = "DELETE,GET,OPTIONS,PUT";
                    break;

                case "PUT":
                    actionToInvoke = this.UploadFile;
                    break;

                default:
                    request.Context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    break;
            }

            if (actionToInvoke != null)
            {
                actionToInvoke(request);
            }
        }
        // Private Methods (4) 

        private void DeleteFile(ICloudRequest request)
        {
            var fm = request.Principal.Files;
            var path = request.Context.Request.Headers["X-MJKTM-CloudNET-File"].Trim();

            var file = fm.GetFile(path);
            if (file != null)
            {
                file.Delete();
            }
            else
            {
                request.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        private void DownloadFile(ICloudRequest request)
        {
            var fm = request.Principal.Files;
            var path = request.Context.Request.Headers["X-MJKTM-CloudNET-File"].Trim();

            var file = fm.GetFile(path);
            if (file != null)
            {
                using (var stream = file.OpenRead())
                {
                    stream.CopyTo(request.Context.Response.OutputStream);
                }
            }
            else
            {
                request.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        private void ListDirectory(ICloudRequest request)
        {
            var fm = request.Principal.Files;

            var path = request.Context.Request.Headers["X-MJKTM-CloudNET-Directory"];
            var dir = fm.GetDirectory(path, false);

            if (dir != null)
            {
                var dirList = new List<object>();
                foreach (var subDir in dir.GetDirectories())
                {
                    dirList.Add(new
                        {
                            name = subDir.Name,
                            path = subDir.FullPath,
                        });
                }

                var fileList = new List<object>();
                foreach (var file in dir.GetFiles())
                {
                    fileList.Add(new
                        {
                            name = file.Name,
                            path = file.FullPath,
                            size = file.Size,
                        });
                }

                var jsonResult = new
                    {
                        dirs = dirList,
                        files = fileList,
                        isRootDir = dir.IsRoot,
                        parentPath = dir.Parent != null ? dir.Parent.FullPath : null,
                        path = dir.FullPath,
                    };

                var enc = Encoding.UTF8;
                request.Context.Response.ContentType = "application/json; charset=" + enc.WebName;

                using (var writer = new StreamWriter(request.Context.Response.OutputStream, enc))
                {
                    var serializer = new JsonSerializer();

                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        serializer.Serialize(jsonWriter, jsonResult);

                        jsonWriter.Flush();
                    }
                }
            }
            else
            {
                request.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        private void UploadFile(ICloudRequest request)
        {
            var fm = request.Principal.Files;
            var path = request.Context.Request.Headers["X-MJKTM-CloudNET-File"].Trim();

            var dirPathParts = path.Split('/');

            var dirPath = string.Join("/",
                                      dirPathParts.Take(dirPathParts.Length - 1));

            var dir = fm.GetDirectory(dirPath, true);
            using (var stream = request.Context.Request.GetBufferlessInputStream(disableMaxRequestLength: true))
            {
                dir.SaveFile(dirPathParts.Last().Trim(),
                             stream);
            }
        }

        #endregion Methods
    }
}
