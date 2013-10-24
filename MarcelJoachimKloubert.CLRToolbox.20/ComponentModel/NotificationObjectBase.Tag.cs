// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.Generic;

namespace MarcelJoachimKloubert.CLRToolbox.ComponentModel
{
    partial class NotificationObjectBase
    {
        #region Properties (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="TMObject.Tag" />
        public override object Tag
        {
            get { return base.Tag; }

            set
            {
                if (!EqualityComparer<object>.Default.Equals(base.Tag, value))
                {
                    this.OnPropertyChanging("Tag");
                    base.Tag = value;
                    this.OnPropertyChanged("Tag");
                }
            }
        }

        #endregion Properties
    }
}
