// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.ComponentModel.Composition;
using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm.WebInterface.modules
{
    [Export(typeof(global::MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules.IHttpModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [DefaultHttpModule]
    internal sealed class index : WebModuleBase
    {
        #region Constructors (1)

        internal index()
            : base(new Guid("{C221157D-885B-44D2-81C9-F0A244565E6B}"))
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override void OnHandleRequest(IHandleRequestContext context)
        {
            using (var stream = this.Module.Context.TryGetResourceStream("Goethe.txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    var txt = reader.ReadToEnd();
                }
            }
        }

        #endregion Methods
    }
}
