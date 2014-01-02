// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities
{
    namespace General
    {
        #region BASE ENTITIES: General
    
        /// <summary>
        /// Describes a 'General' entity.
        /// </summary>
        public partial interface IGeneralEntity : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity
        {
    
        }
    
        /// <summary>
        /// A basic 'General' entity.
        /// </summary>
        public abstract partial class GeneralEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.IGeneralEntity
        {
            #region Constructors (1)
    
            /// <summary>
            /// Initializes a new instance of the <see cref="GeneralEntityBase" /> class.
            /// </summary>
            protected GeneralEntityBase()
            {
    
            }
    
            #endregion
        }
    
        #endregion
     
        namespace RemoteCommService
        {
            #region SCHEMA ENTITIES: RemoteCommService
    
            /// <summary>
            /// Describes an entity for the 'RemoteCommService' schema.
            /// </summary>
            public partial interface IRemoteCommServiceEntity : global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.IGeneralEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'RemoteCommService' schema.
            /// </summary>
            public abstract partial class RemoteCommServiceEntityBase : global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.GeneralEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IRemoteCommServiceEntity
            {
                #region Constructors (1)
    
                /// <summary>
                /// Initializes a new instance of the <see cref="RemoteCommServiceEntityBase" /> class.
                /// </summary>
                protected RemoteCommServiceEntityBase()
                {
    
                }
    
                #endregion
            }
    
            #endregion
     
            #region ENTITY: REMCOMM_UserFunctions
    
            /// <summary>
            /// Describes an 'REMCOMM_UserFunctions' entity.
            /// </summary>
            public partial interface IREMCOMM_UserFunctions : global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IRemoteCommServiceEntity
            {
                #region Columns (4)
                /// <summary>
                /// Gets or sets the scalar field 'CanExecute'.
                /// </summary>
                bool CanExecute { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'REMCOMM_UserFunctionID'.
                /// </summary>
                long REMCOMM_UserFunctionID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'REMCOMM_UserID'.
                /// </summary>
                long REMCOMM_UserID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'ServerFunctionID'.
                /// </summary>
                long ServerFunctionID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'REMCOMM_UserFunctions' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "REMCOMM_UserFunctions", Schema = "RemoteCommService")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class REMCOMM_UserFunctions : global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.RemoteCommServiceEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_UserFunctions
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="REMCOMM_UserFunctions" /> class.
                /// </summary>
                public REMCOMM_UserFunctions()
                {
                    
                }
    
                #endregion
    
                #region Columns (4)
    
                private bool _sf_CanExecute;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_UserFunctions.CanExecute" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(bool), IsKey = false, IsNullable = false, Name = "CanExecute")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "CanExecute")]
                public bool CanExecute
                {
                    get { return this._sf_CanExecute; }
                    set
                    {
                        if (!object.Equals(this._sf_CanExecute, value))
                        {
                            this.OnPropertyChanging("CanExecute");
                            this._sf_CanExecute = value;
                            this.OnPropertyChanged("CanExecute");
                        }
                    }
                }
    
                private long _sf_REMCOMM_UserFunctionID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_UserFunctions.REMCOMM_UserFunctionID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = true, IsNullable = false, Name = "REMCOMM_UserFunctionID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "REMCOMM_UserFunctionID")]
                public long REMCOMM_UserFunctionID
                {
                    get { return this._sf_REMCOMM_UserFunctionID; }
                    set
                    {
                        if (!object.Equals(this._sf_REMCOMM_UserFunctionID, value))
                        {
                            this.OnPropertyChanging("REMCOMM_UserFunctionID");
                            this._sf_REMCOMM_UserFunctionID = value;
                            this.OnPropertyChanged("REMCOMM_UserFunctionID");
                        }
                    }
                }
    
                private long _sf_REMCOMM_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_UserFunctions.REMCOMM_UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = false, IsNullable = false, Name = "REMCOMM_UserID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "REMCOMM_UserID")]
                public long REMCOMM_UserID
                {
                    get { return this._sf_REMCOMM_UserID; }
                    set
                    {
                        if (!object.Equals(this._sf_REMCOMM_UserID, value))
                        {
                            this.OnPropertyChanging("REMCOMM_UserID");
                            this._sf_REMCOMM_UserID = value;
                            this.OnPropertyChanged("REMCOMM_UserID");
                        }
                    }
                }
    
                private long _sf_ServerFunctionID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_UserFunctions.ServerFunctionID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = false, IsNullable = false, Name = "ServerFunctionID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "ServerFunctionID")]
                public long ServerFunctionID
                {
                    get { return this._sf_ServerFunctionID; }
                    set
                    {
                        if (!object.Equals(this._sf_ServerFunctionID, value))
                        {
                            this.OnPropertyChanging("ServerFunctionID");
                            this._sf_ServerFunctionID = value;
                            this.OnPropertyChanged("ServerFunctionID");
                        }
                    }
                }
    
                #endregion
     
                #region Navigation properties (1)
    
                /// <summary>
                /// Gets or sets the linked <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.REMCOMM_Users" /> item.
                /// </summary>
                [global::System.Runtime.Serialization.DataMember]
                public global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.REMCOMM_Users REMCOMM_Users { get; set; }
    
                #endregion
     
                #region Methods (9)
    
                /// <summary>
                /// Builds a new <see cref="REMCOMM_UserFunctions" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static REMCOMM_UserFunctions Build<TRec>(TRec rec, global::System.Action<REMCOMM_UserFunctions, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new REMCOMM_UserFunctions();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="REMCOMM_UserFunctions" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupState">The last parameter for <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static REMCOMM_UserFunctions Build<TRec, S>(TRec rec, global::System.Action<REMCOMM_UserFunctions, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new REMCOMM_UserFunctions();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="REMCOMM_UserFunctions" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupStateFactory">The optional factory for last parameter of <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static REMCOMM_UserFunctions Build<TRec, S>(TRec rec, global::System.Action<REMCOMM_UserFunctions, TRec, S> setup = null, global::System.Func<REMCOMM_UserFunctions, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new REMCOMM_UserFunctions();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="REMCOMM_UserFunctions" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<REMCOMM_UserFunctions> BuildAll<TReader>(TReader reader, global::System.Action<REMCOMM_UserFunctions, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="REMCOMM_UserFunctions" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupState">The last parameter for <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<REMCOMM_UserFunctions> BuildAll<TReader, S>(TReader reader, global::System.Action<REMCOMM_UserFunctions, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="REMCOMM_UserFunctions" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupStateFactory">The optional factory for last parameter of <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<REMCOMM_UserFunctions> BuildAll<TReader, S>(TReader reader, global::System.Action<REMCOMM_UserFunctions, TReader, S> setup = null, global::System.Func<REMCOMM_UserFunctions, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupStateFactory: setupStateFactory);
                }
    
                /// <summary>
                /// Loads data into this object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                public void LoadFrom<TRec>(TRec rec, global::System.Action<REMCOMM_UserFunctions, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<REMCOMM_UserFunctions, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
                }
    
                /// <summary>
                /// Loads data into this object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupState">The last parameter for <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<REMCOMM_UserFunctions, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: (e, r) => setupState);
                }
    
                /// <summary>
                /// Loads data into this object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupStateFactory">The optional factory for last parameter of <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<REMCOMM_UserFunctions, TRec, S> setup = null, global::System.Func<REMCOMM_UserFunctions, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oCanExecute = rec.GetOrdinal("CanExecute");
                    var oREMCOMM_UserFunctionID = rec.GetOrdinal("REMCOMM_UserFunctionID");
                    var oREMCOMM_UserID = rec.GetOrdinal("REMCOMM_UserID");
                    var oServerFunctionID = rec.GetOrdinal("ServerFunctionID");
     
                    this.CanExecute = (bool)(!rec.IsDBNull(oCanExecute) ? rec.GetValue(oCanExecute) : null);
                    this.REMCOMM_UserFunctionID = (long)(!rec.IsDBNull(oREMCOMM_UserFunctionID) ? rec.GetValue(oREMCOMM_UserFunctionID) : null);
                    this.REMCOMM_UserID = (long)(!rec.IsDBNull(oREMCOMM_UserID) ? rec.GetValue(oREMCOMM_UserID) : null);
                    this.ServerFunctionID = (long)(!rec.IsDBNull(oServerFunctionID) ? rec.GetValue(oServerFunctionID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
     
            #region ENTITY: REMCOMM_Users
    
            /// <summary>
            /// Describes an 'REMCOMM_Users' entity.
            /// </summary>
            public partial interface IREMCOMM_Users : global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IRemoteCommServiceEntity
            {
                #region Columns (3)
                /// <summary>
                /// Gets or sets the scalar field 'IsActive'.
                /// </summary>
                bool IsActive { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'REMCOMM_UserID'.
                /// </summary>
                long REMCOMM_UserID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'UserID'.
                /// </summary>
                long UserID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'REMCOMM_Users' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "REMCOMM_Users", Schema = "RemoteCommService")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class REMCOMM_Users : global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.RemoteCommServiceEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_Users
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="REMCOMM_Users" /> class.
                /// </summary>
                public REMCOMM_Users()
                {
                    
                }
    
                #endregion
    
                #region Columns (3)
    
                private bool _sf_IsActive;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_Users.IsActive" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(bool), IsKey = false, IsNullable = false, Name = "IsActive")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "IsActive")]
                public bool IsActive
                {
                    get { return this._sf_IsActive; }
                    set
                    {
                        if (!object.Equals(this._sf_IsActive, value))
                        {
                            this.OnPropertyChanging("IsActive");
                            this._sf_IsActive = value;
                            this.OnPropertyChanged("IsActive");
                        }
                    }
                }
    
                private long _sf_REMCOMM_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_Users.REMCOMM_UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = true, IsNullable = false, Name = "REMCOMM_UserID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "REMCOMM_UserID")]
                public long REMCOMM_UserID
                {
                    get { return this._sf_REMCOMM_UserID; }
                    set
                    {
                        if (!object.Equals(this._sf_REMCOMM_UserID, value))
                        {
                            this.OnPropertyChanging("REMCOMM_UserID");
                            this._sf_REMCOMM_UserID = value;
                            this.OnPropertyChanged("REMCOMM_UserID");
                        }
                    }
                }
    
                private long _sf_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.IREMCOMM_Users.UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = false, IsNullable = false, Name = "UserID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "UserID")]
                public long UserID
                {
                    get { return this._sf_UserID; }
                    set
                    {
                        if (!object.Equals(this._sf_UserID, value))
                        {
                            this.OnPropertyChanging("UserID");
                            this._sf_UserID = value;
                            this.OnPropertyChanged("UserID");
                        }
                    }
                }
    
                #endregion
     
                #region Navigation properties (1)
    
                /// <summary>
                /// Gets or sets the list of linked <see cref="global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.REMCOMM_UserFunctions" /> items.
                /// </summary>
                [global::System.Runtime.Serialization.DataMember]
                public global::System.Collections.Generic.IList<global::MarcelJoachimKloubert.AppServer.Modules.RemoteComm.Data.Entities.General.RemoteCommService.REMCOMM_UserFunctions> REMCOMM_UserFunctions { get; set; }
    
                #endregion
     
                #region Methods (9)
    
                /// <summary>
                /// Builds a new <see cref="REMCOMM_Users" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static REMCOMM_Users Build<TRec>(TRec rec, global::System.Action<REMCOMM_Users, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new REMCOMM_Users();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="REMCOMM_Users" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupState">The last parameter for <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static REMCOMM_Users Build<TRec, S>(TRec rec, global::System.Action<REMCOMM_Users, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new REMCOMM_Users();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="REMCOMM_Users" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupStateFactory">The optional factory for last parameter of <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static REMCOMM_Users Build<TRec, S>(TRec rec, global::System.Action<REMCOMM_Users, TRec, S> setup = null, global::System.Func<REMCOMM_Users, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new REMCOMM_Users();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="REMCOMM_Users" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<REMCOMM_Users> BuildAll<TReader>(TReader reader, global::System.Action<REMCOMM_Users, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="REMCOMM_Users" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupState">The last parameter for <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<REMCOMM_Users> BuildAll<TReader, S>(TReader reader, global::System.Action<REMCOMM_Users, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="REMCOMM_Users" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupStateFactory">The optional factory for last parameter of <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<REMCOMM_Users> BuildAll<TReader, S>(TReader reader, global::System.Action<REMCOMM_Users, TReader, S> setup = null, global::System.Func<REMCOMM_Users, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupStateFactory: setupStateFactory);
                }
    
                /// <summary>
                /// Loads data into this object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                public void LoadFrom<TRec>(TRec rec, global::System.Action<REMCOMM_Users, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<REMCOMM_Users, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
                }
    
                /// <summary>
                /// Loads data into this object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupState">The last parameter for <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<REMCOMM_Users, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: (e, r) => setupState);
                }
    
                /// <summary>
                /// Loads data into this object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <typeparam name="S">The type of the last parameter for <paramref name="setup" />.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <param name="setupStateFactory">The optional factory for last parameter of <paramref name="setup" />.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<REMCOMM_Users, TRec, S> setup = null, global::System.Func<REMCOMM_Users, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oIsActive = rec.GetOrdinal("IsActive");
                    var oREMCOMM_UserID = rec.GetOrdinal("REMCOMM_UserID");
                    var oUserID = rec.GetOrdinal("UserID");
     
                    this.IsActive = (bool)(!rec.IsDBNull(oIsActive) ? rec.GetValue(oIsActive) : null);
                    this.REMCOMM_UserID = (long)(!rec.IsDBNull(oREMCOMM_UserID) ? rec.GetValue(oREMCOMM_UserID) : null);
                    this.UserID = (long)(!rec.IsDBNull(oUserID) ? rec.GetValue(oUserID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
    
        }
     
    }
}
