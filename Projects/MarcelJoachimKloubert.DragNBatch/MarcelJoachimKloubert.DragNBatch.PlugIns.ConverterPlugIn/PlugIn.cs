﻿// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading;

namespace MarcelJoachimKloubert.DragNBatch.PlugIns.ConverterPlugIn
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
            : base(id: new Guid("{5420CA3F-6A8D-4123-82CD-2DD1F4712248}"))
        {
        }

        #endregion Constructors

        #region Methods (3)

        // Protected Methods (3) 

        /// <inheriteddoc />
        protected override void OnDispose(bool disposing)
        {
            // dummy
        }

        /// <inheriteddoc />
        protected override IEnumerable<char> OnGetDisplayName(CultureInfo culture)
        {
            switch (culture.ThreeLetterISOLanguageName)
            {
                case "deu":
                    return "Konverter Plug-In";
            }

            return "Converter PlugIn";
        }

        /// <inheriteddoc />
        protected override void OnHandleFiles(IHandleFilesContext context)
        {
            for (var i = 0; i < 2; i++)
            {
                for (var ii = 0; ii < 10; ii++)
                {
                    context.StatusText = string.Format("{0} / 10; {1} / 2",
                                                       ii + 1,
                                                       i + 1);
                    Thread.Sleep(1000);

                    context.SetCurrentStepProgess(ii + 1, 10);
                }

                context.SetOverallProgess(i + 1, 2);
            }
        }

        #endregion Methods
    }
}