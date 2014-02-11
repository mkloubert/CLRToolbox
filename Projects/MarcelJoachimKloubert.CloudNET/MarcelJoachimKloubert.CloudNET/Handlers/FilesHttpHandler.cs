// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.IO;
using MarcelJoachimKloubert.CloudNET.Classes.Web;
using MarcelJoachimKloubert.CLRToolbox.Data;
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
        #region Methods (10)

        // Protected Methods (1) 

        protected override void OnProcessRequest(ICloudRequest request)
        {
            Action<ICloudRequest> actionToInvoke = null;

            switch ((request.Context.Request.HttpMethod ?? string.Empty).ToUpper().Trim())
            {
                case "DELETE":
                    actionToInvoke = this.Delete;
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
                    request.Context.Response.Headers["Allow"] = "DELETE,GET,OPTIONS,PATCH,PUT";
                    break;

                case "PATCH":
                    {
                        switch ((request.Context.Request.Params["type"] ?? string.Empty).ToLower().Trim())
                        {
                            case "creationtime":
                                actionToInvoke = this.UpdateCreationWriteTime;
                                break;

                            case "writetime":
                                actionToInvoke = this.UpdateWriteTime;
                                break;

                            default:
                                request.Context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                                break;
                        }
                    }
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
        // Private Methods (9) 

        private void Delete(ICloudRequest request)
        {
            this.DeleteFile(request);
            this.DeleteDirectory(request);
        }

        private void DeleteDirectory(ICloudRequest request)
        {
            var fm = request.Principal.Files;
            var path = request.Context.Request.Headers["X-MJKTM-CloudNET-Directory"].Trim();

            var dir = fm.GetDirectory(path);
            if (dir != null)
            {
                dir.Delete();
            }
            else
            {
                request.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

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
                            creationTime = subDir.CreationTime,
                            lastWriteTime = subDir.WriteTime,
                            isRoot = subDir.IsRoot,
                            name = subDir.Name,
                            path = subDir.FullPath,
                        });
                }

                var fileList = new List<object>();
                foreach (var file in dir.GetFiles())
                {
                    fileList.Add(new
                        {
                            creationTime = file.CreationTime,
                            lastWriteTime = file.WriteTime,
                            name = file.Name,
                            path = file.FullPath,
                            size = file.Size < 0 ? (long?)null : file.Size,
                        });
                }

                var jsonResult = new
                    {
                        creationTime = dir.CreationTime,
                        dirs = dirList,
                        files = fileList,
                        isRootDir = dir.IsRoot,
                        lastWriteTime = dir.WriteTime,
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

        private void UpdateCreationWriteTime(ICloudRequest request)
        {
            this.UpdateTime(request,
                            (item, time) => item.CreationTime = time);
        }

        private void UpdateTime(ICloudRequest request,
                                Action<IFileSystemItem, DateTime?> updateAction)
        {
            // file
            {
                var path = request.Context.Request.Headers["X-MJKTM-CloudNET-File"].Trim();
                if (path != null)
                {
                    var fm = request.Principal.Files;

                    var strTicks = request.Context.Request.Headers["X-MJKTM-CloudNET-FileTime"];
                    long? ticks = null;
                    if (string.IsNullOrWhiteSpace(strTicks) == false)
                    {
                        ticks = GlobalConverter.Current
                                               .ChangeType<long>(strTicks.Trim());
                    }

                    var file = fm.GetFile(path);
                    if (file != null)
                    {
                        updateAction(file,
                                     ticks.HasValue ? new DateTime(ticks.Value, DateTimeKind.Utc) : (DateTime?)null);
                    }
                    else
                    {
                        request.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                }
            }

            // directory
            {
                var path = request.Context.Request.Headers["X-MJKTM-CloudNET-Directory"];
                if (path != null)
                {
                    var fm = request.Principal.Files;

                    var strTicks = request.Context.Request.Headers["X-MJKTM-CloudNET-DirectoryTime"];
                    long? ticks = null;
                    if (string.IsNullOrWhiteSpace(strTicks) == false)
                    {
                        ticks = GlobalConverter.Current
                                               .ChangeType<long>(strTicks.Trim());
                    }

                    var dir = fm.GetDirectory(path);
                    if (dir != null)
                    {
                        updateAction(dir,
                                     ticks.HasValue ? new DateTime(ticks.Value, DateTimeKind.Utc) : (DateTime?)null);
                    }
                    else
                    {
                        request.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                }
            }
        }

        private void UpdateWriteTime(ICloudRequest request)
        {
            this.UpdateTime(request,
                            (item, time) => item.WriteTime = time);
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
