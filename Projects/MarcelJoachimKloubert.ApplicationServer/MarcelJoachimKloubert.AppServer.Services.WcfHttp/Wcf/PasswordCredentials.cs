// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.IdentityModel.Selectors;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using MarcelJoachimKloubert.AppServer.Services.WcfHttp.Security;
using MarcelJoachimKloubert.CLRToolbox.Security;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp.Wcf
{
    class PasswordCredentials : ServiceCredentials
    {
        #region Constructors (2)

        internal PasswordCredentials(UsernamePasswordValidator validator)
        {
            this.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            this.UserNameAuthentication.CustomUserNamePasswordValidator = new DelegateUserNamePasswordValidator(validator);
        }

        private PasswordCredentials(PasswordCredentials objToClone)
            : base(objToClone)
        {

        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (1) 

        public override SecurityTokenManager CreateSecurityTokenManager()
        {
            if (this.UserNameAuthentication.UserNamePasswordValidationMode == UserNamePasswordValidationMode.Custom)
            {
                return new PasswordTokenManager(this);
            }

            return base.CreateSecurityTokenManager();
        }
        // Protected Methods (1) 

        protected override ServiceCredentials CloneCore()
        {
            return new PasswordCredentials(this);
        }

        #endregion Methods
    }
}
