// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ListOutputFormat = MarcelJoachimKloubert.FileCompare.ListFormat;

namespace MarcelJoachimKloubert.FileCompare
{
    public sealed class CompareContext
    {
        #region Fields (21) 

        public readonly ICollection<string> DIFFERENT_DIR_LIST = new List<string>();
        public readonly ICollection<string> DIFFERENT_FILE_LIST = new List<string>();
        public ulong DifferentDirs;
        public ulong DifferentFiles;
        public ulong Errors;
        public readonly ICollection<string> EXTRA_DIR_LIST = new List<string>();
        public readonly ICollection<string> EXTRA_FILE_LIST = new List<string>();
        public ulong ExtraDirs;
        public ulong ExtraFiles;
        public bool Hash;
        public ListFormat? ListFormat = ListOutputFormat.Xml;
        public TextWriter Logger;
        public readonly ICollection<string> MISSING_DIR_LIST = new List<string>();
        public readonly ICollection<string> MISSING_FILE_LIST = new List<string>();
        public ulong MissingDirs;
        public ulong MissingFiles;
        public ulong ProcessedDirs;
        public ulong ProcessedFiles;
        public bool Recursive;
        public ulong SameDirs;
        public ulong SameFiles;

        #endregion Fields 

        #region Methods (5) 

        // Public Methods (2) 

        public void LogException(Exception ex)
        {
            this.Logger
                .WriteLine(ex.GetBaseException() ?? ex);
        }

        public bool SaveList(FileInfo file)
        {
            Action<Stream> actionToInvoke = null;

            var format = this.ListFormat;
            if (format.HasValue)
            {
                switch (format.Value)
                {
                    case ListOutputFormat.Json:
                        actionToInvoke = this.SaveListsAsJson;
                        break;

                    case ListOutputFormat.Xml:
                        actionToInvoke = this.SaveListsAsXml;
                        break;
                }
            }

            if (actionToInvoke != null)
            {
                using (var stream = new FileStream(file.FullName,
                                                   FileMode.Create,
                                                   FileAccess.ReadWrite))
                {
                    actionToInvoke(stream);
                }

                return true;
            }

            return false;
        }
        // Private Methods (3) 

        private void SaveListsAsJson(Stream stream)
        {
            var obj = new
            {
                directories = new
                {
                    differenes = (from d in this.DIFFERENT_DIR_LIST
                                  select d).ToArray(),

                    extras = (from d in this.EXTRA_DIR_LIST
                              select d).ToArray(),

                    missing = (from d in this.MISSING_DIR_LIST
                               select d).ToArray(),
                },

                files = new
                {
                    differenes = (from f in this.DIFFERENT_FILE_LIST
                                  select f).ToArray(),

                    extras = (from f in this.EXTRA_FILE_LIST
                              select f).ToArray(),

                    missing = (from f in this.MISSING_FILE_LIST
                               select f).ToArray(),
                },
            };

            var serializer = new JsonSerializer();
            using (var txtWriter = new StreamWriter(stream, Encoding.UTF8))
            {
                using (var jsonWriter = new JsonTextWriter(txtWriter))
                {
                    serializer.Serialize(jsonWriter, obj);
                }
            }
        }

        private void SaveListsAsXml(Stream stream)
        {
            var xmlDoc = new XDocument();
            xmlDoc.Add(new XElement("fileCompareResult"));

            // dirs
            {
                var directoriesElement = new XElement("directories");
                SaveXmlData(directoriesElement,
                            differences: this.DIFFERENT_DIR_LIST,
                            extras: this.EXTRA_DIR_LIST,
                            missings: this.MISSING_DIR_LIST);

                xmlDoc.Root.Add(directoriesElement);
            }

            // files
            {
                var filesElement = new XElement("files");
                SaveXmlData(filesElement,
                            differences: this.DIFFERENT_FILE_LIST,
                            extras: this.EXTRA_FILE_LIST,
                            missings: this.MISSING_FILE_LIST);

                xmlDoc.Root.Add(filesElement);
            }

            xmlDoc.Save(stream);
        }

        private static void SaveXmlData(XElement parentElement,
                                        IEnumerable<string> differences,
                                        IEnumerable<string> extras,
                                        IEnumerable<string> missings)
        {
            // differences
            foreach (var d in differences)
            {
                var differenceElement = new XElement("difference");
                differenceElement.Value = d;

                parentElement.Add(differenceElement);
            }

            // extras
            foreach (var e in extras)
            {
                var extraElement = new XElement("extra");
                extraElement.Value = e;

                parentElement.Add(extraElement);
            }

            // missings
            foreach (var m in missings)
            {
                var missingElement = new XElement("missing");
                missingElement.Value = m;

                parentElement.Add(missingElement);
            }
        }

        #endregion Methods 
    }
}