// LICENSE: LGPL 3 - https://www.gnu.org/licenses/lgpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;
using System.Runtime.Serialization;

namespace MarcelJoachimKloubert.CLRToolbox.Diagnostics.Remoting
{
    /// <summary>
    /// Contains data of the sender.
    /// </summary>
    [DataContract]
    public sealed class RemoteLogMessageSender : MarshalByRefObject
    {
        #region Properties (5)

        /// <summary>
        /// Gets or sets the full name of the object.
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the instance hash (<see cref="object.GetHashCode()" />).
        /// </summary>
        [DataMember]
        public int Hash { get; set; }

        /// <summary>
        /// Gets or sets the ID of the object.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the (internal) name of the object.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full name of the object's type (<see cref="global::System.Type" />).
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        #endregion Properties
    }
}
