// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// The mother of all objects.
    /// </summary>
    public partial class TMObject : MarshalByRefObject, ITMObject
    {
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMObject.Tag" />
        public virtual object Tag
        {
            get;
            set;
        }

        #endregion Properties
    }
}
