// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using MarcelJoachimKloubert.CLRToolbox.Net.Http.Modules;
using MarcelJoachimKloubert.CLRToolbox.Serialization;
using MarcelJoachimKloubert.CLRToolbox.Serialization.Json;
using MarcelJoachimKloubert.CLRToolbox.ServiceLocation;

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
            var serializer = ServiceLocator.Current.GetInstance<ISerializer>();

            var json = serializer.ToJson(new JsonParameterResult()
            {
                code = 666,
                msg = "Master of Darkness",
                tag = new Dictionary<string, object>()
                {
                    {"a", 1},
                    {"b", 2},
                }
            });

            var o = serializer.FromJson<JsonParameterResult>(json);

            using (var stream = this.Module.Context.TryGetResourceStream("web.Goethe.TXT"))
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
