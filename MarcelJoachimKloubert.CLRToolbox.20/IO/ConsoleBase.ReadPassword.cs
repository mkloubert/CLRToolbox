// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Security;

namespace MarcelJoachimKloubert.CLRToolbox.IO
{
    partial class ConsoleBase
    {
        #region Methods (2)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IConsole.ReadPassword()" />
        public SecureString ReadPassword()
        {
            SecureString pwd = new SecureString();
            this.OnReadPassword(ref pwd);

            return pwd;
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for <see cref="ConsoleBase.ReadPassword()" /> method.
        /// </summary>
        /// <param name="pwd">The result for <see cref="ConsoleBase.ReadPassword()" />.</param>
        protected virtual void OnReadPassword(ref SecureString pwd)
        {
            string line = this.ReadLine();
            if (line != null)
            {
                foreach (char c in line)
                {
                    pwd.AppendChar(c);
                }
            }
            else
            {
                pwd = null;
            }
        }

        #endregion Methods
    }
}
