// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CryptoCopy.Mapping;

namespace MarcelJoachimKloubert.CryptoCopy.Helpers
{
    internal static class EncryptionHelper
    {
        #region Fields (1)

        private static readonly Random _RANDOM = new Random();

        #endregion Fields

        #region Methods (3)

        // Internal Methods (3) 

        internal static Task CreateEncryptionTask(CryptExecutionContext ctx)
        {
            return CreateEncryptionTask(ctx,
                                        ctx.Source, ctx.Destination);
        }

        internal static Task CreateEncryptionTask(CryptExecutionContext orgCtx,
                                                  DirectoryInfo src, DirectoryInfo dest)
        {
            var ctxClone = orgCtx.Clone();
            ctxClone.Source = src;
            ctxClone.Destination = dest;

            return new Task((state) =>
                {
                    var ctx = (CryptExecutionContext)state;

                    ctx.Destination.CreateDirectoryDeep();

                    var metaFile = new FileInfo(Path.Combine(ctx.Destination.FullName,
                                                             AppGlobals.FILENAME_META));

                    var metaXmlDoc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.WebName, "yes"));
                    metaXmlDoc.Add(new XElement("dir"));

                    metaXmlDoc.Root.SetAttributeValue("name", ctx.Source.Name);

                    // encrypt files
                    FileMapping[] fileMappings;
                    {
                        ulong index = 1;
                        fileMappings = ctx.Source
                                          .GetFiles()
                                          .Randomize(_RANDOM)
                                          .Select(file => new FileMapping(ctx,
                                                                          file,
                                                                          new FileInfo(Path.Combine(ctx.Destination.FullName,
                                                                                                    string.Format("{0}.bin",
                                                                                                                  index++)))))
                                          .ToArray();

                        foreach (var fm in fileMappings)
                        {
                            fm.TASK.Start();
                        }

                        TaskHelper.WaitAll(fileMappings.Select(m => m.TASK));
                    }

                    // handle sub directories
                    DirMapping[] dirMappings;
                    {
                        ulong index = 1;
                        dirMappings = ctx.Source
                                         .GetDirectories()
                                         .Randomize(_RANDOM)
                                         .Select(dir => new DirMapping(ctx,
                                                                       dir,
                                                                       new DirectoryInfo(Path.Combine(ctx.Destination.FullName,
                                                                                                      (index++).ToString()))))
                                         .ToArray();

                        foreach (var dm in dirMappings)
                        {
                            dm.TASK.Start();
                        }

                        TaskHelper.WaitAll(dirMappings.Select(m => m.TASK));
                    }

                    if (dirMappings.Length > 0)
                    {
                        var dirsElement = new XElement("dirs");
                        foreach (var dm in dirMappings.Randomize(_RANDOM))
                        {
                            var newDirElement = new XElement("dir");
                            newDirElement.SetAttributeValue("name", dm.SOURCE.Name);
                            newDirElement.SetAttributeValue("alias", dm.DESTINATION.Name);
                            newDirElement.SetAttributeValue("lastWrite", dm.SOURCE.LastWriteTimeUtc.Ticks);
                            newDirElement.SetAttributeValue("created", dm.SOURCE.CreationTimeUtc.Ticks);

                            dirsElement.Add(newDirElement);
                        }

                        metaXmlDoc.Root.Add(dirsElement);
                    }

                    if (fileMappings.Length > 0)
                    {
                        var filesElement = new XElement("files");
                        foreach (var fm in fileMappings.Randomize(_RANDOM))
                        {
                            var newFileElement = new XElement("file");
                            newFileElement.SetAttributeValue("name", fm.SOURCE.Name);
                            newFileElement.SetAttributeValue("alias", fm.DESTINATION.Name);
                            newFileElement.SetAttributeValue("len", fm.SOURCE.Length);
                            newFileElement.SetAttributeValue("pwd", Convert.ToBase64String(fm.PASSWORD));
                            newFileElement.SetAttributeValue("lastWrite", fm.SOURCE.LastWriteTimeUtc.Ticks);
                            newFileElement.SetAttributeValue("created", fm.SOURCE.CreationTimeUtc.Ticks);

                            filesElement.Add(newFileElement);
                        }

                        metaXmlDoc.Root.Add(filesElement);
                    }

                    using (var srcStream = new MemoryStream())
                    {
                        metaXmlDoc.Save(srcStream);
                        srcStream.Position = 0;

                        using (var destStream = new FileStream(metaFile.FullName,
                                                               FileMode.CreateNew,
                                                               FileAccess.ReadWrite))
                        {
                            EncryptionHelper.EncryptStream(srcStream, destStream,
                                                           ctx.Password, ctx.Salt);
                        }
                    }

                    FileSystemHelper.TrySetTimestampsUtc(metaFile,
                                                         ctx.StartTime);
                }, ctxClone);
        }

        internal static void EncryptStream(Stream input, Stream output,
                                           byte[] pwd, byte[] salt,
                                           int? interations = null)
        {
            var pdb = new Rfc2898DeriveBytes(pwd,
                                             salt,
                                             interations ?? AppGlobals.DEFAULT_PWD_INTERATIONS);

            var alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            var cryptoStream = new CryptoStream(output,
                                                alg.CreateEncryptor(),
                                                CryptoStreamMode.Write);
            {
                input.CopyTo(cryptoStream);

                cryptoStream.Flush();
                cryptoStream.Close();
            }
        }

        #endregion Methods
    }
}
