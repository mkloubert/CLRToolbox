// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
    [Serializable]
    partial class AggregateException
    {
        #region Constructors (1)

        /// <summary>
        /// 
        /// </summary>
        /// <see href="http://msdn.microsoft.com/en-us/library/dd384355%28v=vs.110%29.aspx" />
        [SecurityCritical]
        protected AggregateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Exception[] array = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
            if (array == null)
            {
                throw new SerializationException("Cannot deserialize!");
            }

            this._INNER_EXCEPTIONS = new ReadOnlyCollection<Exception>(array);
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="Exception.GetObjectData(SerializationInfo, StreamingContext)" />
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            base.GetObjectData(info, context);

            Exception[] array = new Exception[this._INNER_EXCEPTIONS.Count];
            this._INNER_EXCEPTIONS.CopyTo(array, 0);

            info.AddValue("InnerExceptions", array, typeof(Exception[]));
        }

        #endregion Methods
    }
}
