// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using MarcelJoachimKloubert.CLRToolbox.Helpers;
using MarcelJoachimKloubert.CryptoCopy.Mapping;

namespace MarcelJoachimKloubert.CryptoCopy.Helpers
{
    internal static class DecryptionHelper
    {
        #region Methods (4)

        // Internal Methods (4) 

        internal static Task CreateDecryptionTask(CryptExecutionContext ctx)
        {
            return CreateDecryptionTask(ctx: ctx,
                                        xml: null);
        }

        internal static Task CreateDecryptionTask(CryptExecutionContext ctx,
                                                  XElement xml)
        {
            return CreateDecryptionTask(ctx,
                                        ctx.Source, ctx.Destination,
                                        xml);
        }

        internal static Task CreateDecryptionTask(CryptExecutionContext orgCtx,
                                                  DirectoryInfo src, DirectoryInfo dest,
                                                  XElement xml)
        {
            var ctxClone = orgCtx.Clone();
            ctxClone.Source = src;
            ctxClone.Destination = dest;

            return new Task((state) =>
                {
                    var ctx = (CryptExecutionContext)state;

                    ctx.Destination.CreateDirectoryDeep();

                    var metaFile = CollectionHelper.SingleOrDefault(ctx.Source.GetFiles(),
                                                                    f => Globals.FILENAME_META == f.Name.ToLower().Trim());
                    if (metaFile != null)
                    {
                        XDocument metaXmlDoc;
                        using (var srcStream = metaFile.OpenRead())
                        {
                            using (var temp = new MemoryStream())
                            {
                                DecryptionHelper.DecryptStream(srcStream, temp,
                                                               ctx.Password, ctx.Salt);

                                temp.Position = 0;
                                metaXmlDoc = XDocument.Load(temp);
                            }
                        }

                        // directories
                        {
                            var dirMappings = metaXmlDoc.XPathSelectElements("//dir/dirs/dir")
                                                        .Select(de =>
                                                        {
                                                            return new DirMapping(ctx: ctx,
                                                                                  src: new DirectoryInfo(Path.Combine(ctx.Source.FullName,
                                                                                                                      de.Attribute("alias").Value.Trim())),
                                                                                  dest: new DirectoryInfo(Path.Combine(ctx.Destination.FullName,
                                                                                                                       de.Attribute("name").Value.Trim())),
                                                                                  xml: de);
                                                        }).ToArray();

                            foreach (var dm in dirMappings)
                            {
                                dm.TASK.Start();
                            }

                            TaskHelper.WaitAll(dirMappings.Select(dm => dm.TASK));
                        }

                        // files
                        {
                            var fileMappings = metaXmlDoc.XPathSelectElements("//dir/files/file")
                                                     .Select(fe =>
                                                      {
                                                          return new FileMapping(ctx: ctx,
                                                                                 src: new FileInfo(Path.Combine(ctx.Source.FullName,
                                                                                                                fe.Attribute("alias").Value.Trim())),
                                                                                 dest: new FileInfo(Path.Combine(ctx.Destination.FullName,
                                                                                                                 fe.Attribute("name").Value.Trim())),
                                                                                 xml: fe);
                                                      }).ToArray();

                            foreach (var fm in fileMappings)
                            {
                                fm.TASK.Start();
                            }

                            TaskHelper.WaitAll(fileMappings.Select(fm => fm.TASK));
                        }

                        if (xml != null)
                        {
                            FileSystemHelper.TrySetCreationTimeUtc(ctx.Destination,
                                                                   new DateTimeOffset(ticks: long.Parse(xml.Attribute("created").Value.Trim()),
                                                                                      offset: TimeSpan.Zero));

                            FileSystemHelper.TrySetLastWriteTimeUtc(ctx.Destination,
                                                                    new DateTimeOffset(ticks: long.Parse(xml.Attribute("lastWrite").Value.Trim()),
                                                                                       offset: TimeSpan.Zero));
                        }
                    }
                    else
                    {
                        //TODO show warning
                    }
                }, ctxClone);
        }

        internal static void DecryptStream(Stream input, Stream output,
                                           byte[] pwd, byte[] salt)
        {
            var pdb = new PasswordDeriveBytes(pwd,
                                              salt);

            var alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            var cryptoStream = new CryptoStream(input,
                                                alg.CreateDecryptor(),
                                                CryptoStreamMode.Read);
            {
                cryptoStream.CopyTo(output);
            }
        }

        #endregion Methods
    }
}
