// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.Linq;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp.Wcf
{
    internal sealed class PasswordTokenAuthenticator : CustomUserNameSecurityTokenAuthenticator
    {
        #region Constructors (1)

        internal PasswordTokenAuthenticator(UserNamePasswordValidator validator)
            : base(validator)
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Protected Methods (1) 

        protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateUserNamePasswordCore(string userName, string password)
        {
            var newPolicies = new List<IAuthorizationPolicy>();

            var currentPolicies = base.ValidateUserNamePasswordCore(userName, password);
            if (currentPolicies != null)
            {
                newPolicies.AddRange(currentPolicies.OfType<IAuthorizationPolicy>());
            }

            newPolicies.Add(new PasswordAuthPolicy(userName, password));

            return new ReadOnlyCollection<IAuthorizationPolicy>(newPolicies);
        }

        #endregion Methods
    }
}
