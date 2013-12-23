namespace MarcelJoachimKloubert.ApplicationServer.DataModels.Enums.AppServer
{
    namespace Types
    {
        /// <summary>
        /// List of person types.
        /// </summary>
        [global::System.Serializable]
        [global::System.Runtime.Serialization.DataContract]
        public enum PersonType : short
        {
            /// <summary>
            /// juristic person, like a company
            /// </summary>
            [global::System.Runtime.Serialization.EnumMember]
            Juristic = 2,
 
            /// <summary>
            /// natural person
            /// </summary>
            [global::System.Runtime.Serialization.EnumMember]
            Natural = 1,
 
        }
    }
}
