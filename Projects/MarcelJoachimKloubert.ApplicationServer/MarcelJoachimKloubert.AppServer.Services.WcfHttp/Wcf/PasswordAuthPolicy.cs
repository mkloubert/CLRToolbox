// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace MarcelJoachimKloubert.AppServer.Services.WcfHttp.Wcf
{
    internal sealed class PasswordAuthPolicy : IAuthorizationPolicy
    {
        #region Constructors (1)

        internal PasswordAuthPolicy(string user, string pwd)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user = user.Trim();
            if (user == string.Empty)
            {
                throw new ArgumentException("user");
            }

            this.Id = Guid.NewGuid().ToString();
            this.Issuer = ClaimSet.System;
            this.Password = pwd;
            this.User = user;
        }

        #endregion Constructors

        #region Properties (4)

        public string Id
        {
            get;
            private set;
        }

        public ClaimSet Issuer
        {
            get;
            private set;
        }

        internal string Password
        {
            get;
            private set;
        }

        internal string User
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            throw new NotImplementedException();

            // http://www.neovolve.com/post/2008/04/07/wcf-security-getting-the-password-of-the-user.aspx

            //const String IdentitiesKey = "Identities";

            //// Check if the properties of the context has the identities list 
            //if (evaluationContext.Properties.Count == 0
            //    || evaluationContext.Properties.ContainsKey(IdentitiesKey) == false
            //    || evaluationContext.Properties[IdentitiesKey] == null)
            //{
            //    return false;
            //}

            //// Get the identities list 
            //List<IIdentity> identities = evaluationContext.Properties[IdentitiesKey] as List<IIdentity>;

            //// Validate that the identities list is valid 
            //if (identities == null)
            //{
            //    return false;
            //}

            //// Get the current identity 
            //IIdentity currentIdentity =
            //    identities.Find(
            //        identityMatch =>
            //        identityMatch is GenericIdentity
            //        && String.Equals(identityMatch.Name, UserName, StringComparison.OrdinalIgnoreCase));

            //// Check if an identity was found 
            //if (currentIdentity == null)
            //{
            //    return false;
            //}

            //// Create new identity 
            //PasswordIdentity newIdentity = new PasswordIdentity(
            //    UserName, Password, currentIdentity.IsAuthenticated, currentIdentity.AuthenticationType);
            //const String PrimaryIdentityKey = "PrimaryIdentity";

            //// Update the list and the context with the new identity 
            //identities.Remove(currentIdentity);
            //identities.Add(newIdentity);
            //evaluationContext.Properties[PrimaryIdentityKey] = newIdentity;

            //// Create a new principal for this identity 
            //PasswordPrincipal newPrincipal = new PasswordPrincipal(newIdentity, null);
            //const String PrincipalKey = "Principal";

            //// Store the new principal in the context 
            //evaluationContext.Properties[PrincipalKey] = newPrincipal;

            //// This policy has successfully been evaluated and doesn't need to be called again 
            //return true;
        }

        #endregion Methods
    }
}
