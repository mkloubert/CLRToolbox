// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


namespace MarcelJoachimKloubert.CLRToolbox
{
    /// <summary>
    /// The mother of all objects.
    /// </summary>
    public partial class TMObject : ITMObject
    {
        #region Fields (1)

        private object _tag;

        #endregion Fields

        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="ITMObject.Tag" />
        public virtual object Tag
        {
            get { return this._tag; }

            set { this._tag = value; }
        }

        #endregion Properties
    }
}
