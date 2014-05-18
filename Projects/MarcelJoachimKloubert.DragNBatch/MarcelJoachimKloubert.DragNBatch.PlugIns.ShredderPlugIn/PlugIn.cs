// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;

namespace MarcelJoachimKloubert.DragNBatch.PlugIns.ShredderPlugIn
{
    [Export(typeof(global::MarcelJoachimKloubert.DragNBatch.PlugIns.IPlugIn))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class PlugIn : PlugInBase
    {
        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="PlugIn" /> class.
        /// </summary>
        public PlugIn()
            : base(id: new Guid("{AF6B4523-41E9-4F73-8113-EE377B84607A}"))
        {
        }

        #endregion Constructors

        #region Methods (3)

        // Protected Methods (4) 

        /// <inheriteddoc />
        protected override void OnDispose(bool disposing)
        {
            // dummy
        }

        /// <inheriteddoc />
        protected override IEnumerable<char> OnGetDropText(CultureInfo culture)
        {
            switch (culture.ThreeLetterISOLanguageName)
            {
                case "deu":
                    return "Ziehe Dateien und Verzeichnisse hierher, um sie zu sauber zu löschen...";
            }

            return "Drop files and folder here if you want to shredder them...";
        }

        /// <inheriteddoc />
        protected override IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            switch (culture.ThreeLetterISOLanguageName)
            {
                case "deu":
                    return "Shredder Plug-In";
            }

            return "Shredder PlugIn";
        }

        /// <inheriteddoc />
        protected override void OnHandleFiles(IHandleFilesContext context)
        {
        }

        #endregion Methods
    }
}