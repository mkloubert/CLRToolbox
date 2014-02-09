// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Security;
using MarcelJoachimKloubert.CLRToolbox;
using MarcelJoachimKloubert.CLRToolbox.Extensions;
using System.Collections.Generic;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.Security
{
    internal sealed class PrincipalRepository : TMObject, IPrincipalRepository
    {
        #region Methods (1)

        // Public Methods (1) 

        public ICloudPrincipal TryFindPrincipalByLogin(IEnumerable<char> username, IEnumerable<char> password)
        {
            var user = username.AsString();
            var pwd = password.AsString();

            return null;
        }

        #endregion Methods
    }
}
