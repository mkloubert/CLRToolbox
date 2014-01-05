// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using MarcelJoachimKloubert.CLRToolbox.Security;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp.Security
{
    internal sealed class DelegateUserNamePasswordValidator : UserNamePasswordValidator
    {
        #region Fields (1)

        private readonly UsernamePasswordValidator _DELEGATE;

        #endregion Fields

        #region Constructors (1)

        internal DelegateUserNamePasswordValidator(UsernamePasswordValidator @delegate)
        {
            this._DELEGATE = @delegate;
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        [DebuggerStepThrough]
        public override void Validate(string userName, string password)
        {
            bool succeeded;
            try
            {
                succeeded = this._DELEGATE(userName, password);
            }
            catch
            {
                succeeded = false;
            }

            if (!succeeded)
            {
                throw new SecurityTokenValidationException();
            }
        }

        #endregion Methods
    }
}
