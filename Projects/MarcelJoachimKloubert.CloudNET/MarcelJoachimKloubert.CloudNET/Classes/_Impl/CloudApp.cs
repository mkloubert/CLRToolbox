// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de



using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Configuration;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System.Collections.Generic;
using System.Configuration;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl
{
    internal sealed class CloudApp : TMObject, ICloudApp
    {
        #region Properties (1)

        public IConfigRepository Config
        {
            get;
            internal set;
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        public string GetSettingValue(IEnumerable<char> key)
        {
            return ConfigurationManager.AppSettings[key.AsString()];
        }

        #endregion Methods
    }
}
