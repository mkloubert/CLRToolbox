// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox.Objects
{
    partial class ObjectContextBase
    {
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="IObjectContext.AssemblyFile" />
        public virtual string AssemblyFile
        {
            get { return this.Assembly.CodeBase; }
        }

        #endregion Properties
    }
}
