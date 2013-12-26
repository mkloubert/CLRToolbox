// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Text;

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
        public string ReadPassword()
        {
            StringBuilder pwd = new StringBuilder();
            this.OnReadPassword(ref pwd);

            return pwd != null ? pwd.ToString() : null;
        }
        // Protected Methods (1) 

        /// <summary>
        /// The logic for <see cref="ConsoleBase.ReadPassword()" /> method.
        /// </summary>
        /// <param name="pwd">The result for <see cref="ConsoleBase.ReadPassword()" />.</param>
        protected virtual void OnReadPassword(ref StringBuilder pwd)
        {
            string line = this.ReadLine();
            if (line != null)
            {
                pwd.Append(line);
            }
            else
            {
                pwd = null;
            }
        }

        #endregion Methods
    }
}
