// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IdentityModel.Selectors;
using System.ServiceModel.Security;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp.Wcf
{
    internal sealed class PasswordTokenManager : ServiceCredentialsSecurityTokenManager
    {
        #region Constructors (1)

        internal PasswordTokenManager(PasswordCredentials credentials)
            : base(credentials)
        {

        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement,
                                                                                    out SecurityTokenResolver outOfBandTokenResolver)
        {
            outOfBandTokenResolver = null;

            return new PasswordTokenAuthenticator(this.ServiceCredentials
                                                      .UserNameAuthentication
                                                      .CustomUserNamePasswordValidator);
        }

        #endregion Methods
    }
}
