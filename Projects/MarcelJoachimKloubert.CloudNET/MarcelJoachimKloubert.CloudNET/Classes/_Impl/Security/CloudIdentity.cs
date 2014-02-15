// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using MarcelJoachimKloubert.CloudNET.Classes.Security;
using System.Security;

namespace MarcelJoachimKloubert.CloudNET.Classes._Impl.Security
{
    internal sealed class CloudIdentity : ICloudIdentity
    {
        #region Constructors (1)

        ~CloudIdentity()
        {
            this.Dispose(false);
        }

        #endregion Constructors

        #region Properties (4)

        public string AuthenticationType
        {
            get;
            internal set;
        }

        public bool IsAuthenticated
        {
            get;
            internal set;
        }

        public string Name
        {
            get;
            internal set;
        }

        internal SecureString Password
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods (2)

        // Private Methods (1) 

        private void Dispose(bool disposing)
        {
            using (var pwd = this.Password)
            { }
        }
        // Internal Methods (1) 

        internal CloudIdentity Clone(bool? markAsAuthenticated = null)
        {
            CloudIdentity result = new CloudIdentity()
            {
                AuthenticationType = this.AuthenticationType,
                IsAuthenticated = this.IsAuthenticated,
                Name = this.Name,
            };

            if (markAsAuthenticated.HasValue)
            {
                // change value of 'IsAuthenticated'
                result.IsAuthenticated = markAsAuthenticated.Value;
            }

            return result;
        }

        #endregion Methods
    }
}
