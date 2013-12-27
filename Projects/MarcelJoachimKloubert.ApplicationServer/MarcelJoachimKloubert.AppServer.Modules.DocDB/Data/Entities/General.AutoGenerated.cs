// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities
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
        public abstract partial class GeneralEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.IGeneralEntity
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
     
        namespace DocDBService
        {
            #region SCHEMA ENTITIES: DocDBService
    
            /// <summary>
            /// Describes an entity for the 'DocDBService' schema.
            /// </summary>
            public partial interface IDocDBServiceEntity : global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.IGeneralEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'DocDBService' schema.
            /// </summary>
            public abstract partial class DocDBServiceEntityBase : global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.GeneralEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDocDBServiceEntity
            {
                #region Constructors (1)
    
                /// <summary>
                /// Initializes a new instance of the <see cref="DocDBServiceEntityBase" /> class.
                /// </summary>
                protected DocDBServiceEntityBase()
                {
    
                }
    
                #endregion
            }
    
            #endregion
     
            #region ENTITY: DOCDB_UserData
    
            /// <summary>
            /// Describes an 'DOCDB_UserData' entity.
            /// </summary>
            public partial interface IDOCDB_UserData : global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDocDBServiceEntity
            {
                #region Columns (8)
                /// <summary>
                /// Gets or sets the scalar field 'CreationDate'.
                /// </summary>
                System.DateTimeOffset CreationDate { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Data'.
                /// </summary>
                byte[] Data { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'DOCDB_UserDataID'.
                /// </summary>
                long DOCDB_UserDataID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'DOCDB_UserID'.
                /// </summary>
                Nullable<long> DOCDB_UserID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'LastUpdate'.
                /// </summary>
                Nullable<System.DateTimeOffset> LastUpdate { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'MimeTypeID'.
                /// </summary>
                Nullable<short> MimeTypeID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Name'.
                /// </summary>
                string Name { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Namespace'.
                /// </summary>
                string Namespace { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'DOCDB_UserData' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "DOCDB_UserData", Schema = "DocDBService")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class DOCDB_UserData : global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.DocDBServiceEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="DOCDB_UserData" /> class.
                /// </summary>
                public DOCDB_UserData()
                {
                    
                }
    
                #endregion
    
                #region Columns (8)
    
                private System.DateTimeOffset _sf_CreationDate;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.CreationDate" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(System.DateTimeOffset), IsKey = false, IsNullable = false, Name = "CreationDate")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "CreationDate")]
                public System.DateTimeOffset CreationDate
                {
                    get { return this._sf_CreationDate; }
                    set
                    {
                        if (!object.Equals(this._sf_CreationDate, value))
                        {
                            this.OnPropertyChanging("CreationDate");
                            this._sf_CreationDate = value;
                            this.OnPropertyChanged("CreationDate");
                        }
                    }
                }
    
                private byte[] _sf_Data;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.Data" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(byte[]), IsKey = false, IsNullable = true, Name = "Data")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Data")]
                public byte[] Data
                {
                    get { return this._sf_Data; }
                    set
                    {
                        if (!object.Equals(this._sf_Data, value))
                        {
                            this.OnPropertyChanging("Data");
                            this._sf_Data = value;
                            this.OnPropertyChanged("Data");
                        }
                    }
                }
    
                private long _sf_DOCDB_UserDataID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.DOCDB_UserDataID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = true, IsNullable = false, Name = "DOCDB_UserDataID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "DOCDB_UserDataID")]
                public long DOCDB_UserDataID
                {
                    get { return this._sf_DOCDB_UserDataID; }
                    set
                    {
                        if (!object.Equals(this._sf_DOCDB_UserDataID, value))
                        {
                            this.OnPropertyChanging("DOCDB_UserDataID");
                            this._sf_DOCDB_UserDataID = value;
                            this.OnPropertyChanged("DOCDB_UserDataID");
                        }
                    }
                }
    
                private Nullable<long> _sf_DOCDB_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.DOCDB_UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(Nullable<long>), IsKey = false, IsNullable = true, Name = "DOCDB_UserID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "DOCDB_UserID")]
                public Nullable<long> DOCDB_UserID
                {
                    get { return this._sf_DOCDB_UserID; }
                    set
                    {
                        if (!object.Equals(this._sf_DOCDB_UserID, value))
                        {
                            this.OnPropertyChanging("DOCDB_UserID");
                            this._sf_DOCDB_UserID = value;
                            this.OnPropertyChanged("DOCDB_UserID");
                        }
                    }
                }
    
                private Nullable<System.DateTimeOffset> _sf_LastUpdate;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.LastUpdate" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(Nullable<System.DateTimeOffset>), IsKey = false, IsNullable = true, Name = "LastUpdate")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "LastUpdate")]
                public Nullable<System.DateTimeOffset> LastUpdate
                {
                    get { return this._sf_LastUpdate; }
                    set
                    {
                        if (!object.Equals(this._sf_LastUpdate, value))
                        {
                            this.OnPropertyChanging("LastUpdate");
                            this._sf_LastUpdate = value;
                            this.OnPropertyChanged("LastUpdate");
                        }
                    }
                }
    
                private Nullable<short> _sf_MimeTypeID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.MimeTypeID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(Nullable<short>), IsKey = false, IsNullable = true, Name = "MimeTypeID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "MimeTypeID")]
                public Nullable<short> MimeTypeID
                {
                    get { return this._sf_MimeTypeID; }
                    set
                    {
                        if (!object.Equals(this._sf_MimeTypeID, value))
                        {
                            this.OnPropertyChanging("MimeTypeID");
                            this._sf_MimeTypeID = value;
                            this.OnPropertyChanged("MimeTypeID");
                        }
                    }
                }
    
                private string _sf_Name;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.Name" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(string), IsKey = false, IsNullable = false, Name = "Name")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Name")]
                public string Name
                {
                    get { return this._sf_Name; }
                    set
                    {
                        if (!object.Equals(this._sf_Name, value))
                        {
                            this.OnPropertyChanging("Name");
                            this._sf_Name = value;
                            this.OnPropertyChanged("Name");
                        }
                    }
                }
    
                private string _sf_Namespace;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_UserData.Namespace" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(string), IsKey = false, IsNullable = true, Name = "Namespace")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Namespace")]
                public string Namespace
                {
                    get { return this._sf_Namespace; }
                    set
                    {
                        if (!object.Equals(this._sf_Namespace, value))
                        {
                            this.OnPropertyChanging("Namespace");
                            this._sf_Namespace = value;
                            this.OnPropertyChanged("Namespace");
                        }
                    }
                }
    
                #endregion
     
                #region Navigation properties (1)
    
                /// <summary>
                /// Gets or sets the linked <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.DOCDB_Users" /> item.
                /// </summary>
                [global::System.Runtime.Serialization.DataMember]
                public global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.DOCDB_Users DOCDB_Users { get; set; }
    
                #endregion
     
                #region Methods (9)
    
                /// <summary>
                /// Builds a new <see cref="DOCDB_UserData" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static DOCDB_UserData Build<TRec>(TRec rec, global::System.Action<DOCDB_UserData, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new DOCDB_UserData();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="DOCDB_UserData" /> object from a data record.
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
                public static DOCDB_UserData Build<TRec, S>(TRec rec, global::System.Action<DOCDB_UserData, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new DOCDB_UserData();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="DOCDB_UserData" /> object from a data record.
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
                public static DOCDB_UserData Build<TRec, S>(TRec rec, global::System.Action<DOCDB_UserData, TRec, S> setup = null, global::System.Func<DOCDB_UserData, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new DOCDB_UserData();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="DOCDB_UserData" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<DOCDB_UserData> BuildAll<TReader>(TReader reader, global::System.Action<DOCDB_UserData, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="DOCDB_UserData" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<DOCDB_UserData> BuildAll<TReader, S>(TReader reader, global::System.Action<DOCDB_UserData, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="DOCDB_UserData" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<DOCDB_UserData> BuildAll<TReader, S>(TReader reader, global::System.Action<DOCDB_UserData, TReader, S> setup = null, global::System.Func<DOCDB_UserData, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<DOCDB_UserData, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<DOCDB_UserData, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<DOCDB_UserData, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<DOCDB_UserData, TRec, S> setup = null, global::System.Func<DOCDB_UserData, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oCreationDate = rec.GetOrdinal("CreationDate");
                    var oData = rec.GetOrdinal("Data");
                    var oDOCDB_UserDataID = rec.GetOrdinal("DOCDB_UserDataID");
                    var oDOCDB_UserID = rec.GetOrdinal("DOCDB_UserID");
                    var oLastUpdate = rec.GetOrdinal("LastUpdate");
                    var oMimeTypeID = rec.GetOrdinal("MimeTypeID");
                    var oName = rec.GetOrdinal("Name");
                    var oNamespace = rec.GetOrdinal("Namespace");
     
                    this.CreationDate = (System.DateTimeOffset)(!rec.IsDBNull(oCreationDate) ? rec.GetValue(oCreationDate) : null);
                    this.Data = (byte[])(!rec.IsDBNull(oData) ? rec.GetValue(oData) : null);
                    this.DOCDB_UserDataID = (long)(!rec.IsDBNull(oDOCDB_UserDataID) ? rec.GetValue(oDOCDB_UserDataID) : null);
                    this.DOCDB_UserID = (Nullable<long>)(!rec.IsDBNull(oDOCDB_UserID) ? rec.GetValue(oDOCDB_UserID) : null);
                    this.LastUpdate = (Nullable<System.DateTimeOffset>)(!rec.IsDBNull(oLastUpdate) ? rec.GetValue(oLastUpdate) : null);
                    this.MimeTypeID = (Nullable<short>)(!rec.IsDBNull(oMimeTypeID) ? rec.GetValue(oMimeTypeID) : null);
                    this.Name = (string)(!rec.IsDBNull(oName) ? rec.GetValue(oName) : null);
                    this.Namespace = (string)(!rec.IsDBNull(oNamespace) ? rec.GetValue(oNamespace) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
     
            #region ENTITY: DOCDB_Users
    
            /// <summary>
            /// Describes an 'DOCDB_Users' entity.
            /// </summary>
            public partial interface IDOCDB_Users : global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDocDBServiceEntity
            {
                #region Columns (3)
                /// <summary>
                /// Gets or sets the scalar field 'DOCDB_UserID'.
                /// </summary>
                long DOCDB_UserID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'IsActive'.
                /// </summary>
                bool IsActive { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'UserID'.
                /// </summary>
                long UserID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'DOCDB_Users' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "DOCDB_Users", Schema = "DocDBService")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class DOCDB_Users : global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.DocDBServiceEntityBase, global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_Users
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="DOCDB_Users" /> class.
                /// </summary>
                public DOCDB_Users()
                {
                    
                }
    
                #endregion
    
                #region Columns (3)
    
                private long _sf_DOCDB_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_Users.DOCDB_UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = true, IsNullable = false, Name = "DOCDB_UserID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "DOCDB_UserID")]
                public long DOCDB_UserID
                {
                    get { return this._sf_DOCDB_UserID; }
                    set
                    {
                        if (!object.Equals(this._sf_DOCDB_UserID, value))
                        {
                            this.OnPropertyChanging("DOCDB_UserID");
                            this._sf_DOCDB_UserID = value;
                            this.OnPropertyChanged("DOCDB_UserID");
                        }
                    }
                }
    
                private bool _sf_IsActive;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_Users.IsActive" />
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
    
                private long _sf_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_Users.UserID" />
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
                /// Gets or sets the list of linked <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.DOCDB_UserData" /> items.
                /// </summary>
                [global::System.Runtime.Serialization.DataMember]
                public global::System.Collections.Generic.IList<global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.DOCDB_UserData> DOCDB_UserData { get; set; }
    
                #endregion
     
                #region Methods (9)
    
                /// <summary>
                /// Builds a new <see cref="DOCDB_Users" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static DOCDB_Users Build<TRec>(TRec rec, global::System.Action<DOCDB_Users, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new DOCDB_Users();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="DOCDB_Users" /> object from a data record.
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
                public static DOCDB_Users Build<TRec, S>(TRec rec, global::System.Action<DOCDB_Users, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new DOCDB_Users();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="DOCDB_Users" /> object from a data record.
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
                public static DOCDB_Users Build<TRec, S>(TRec rec, global::System.Action<DOCDB_Users, TRec, S> setup = null, global::System.Func<DOCDB_Users, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new DOCDB_Users();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="DOCDB_Users" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<DOCDB_Users> BuildAll<TReader>(TReader reader, global::System.Action<DOCDB_Users, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="DOCDB_Users" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<DOCDB_Users> BuildAll<TReader, S>(TReader reader, global::System.Action<DOCDB_Users, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="DOCDB_Users" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<DOCDB_Users> BuildAll<TReader, S>(TReader reader, global::System.Action<DOCDB_Users, TReader, S> setup = null, global::System.Func<DOCDB_Users, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<DOCDB_Users, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<DOCDB_Users, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<DOCDB_Users, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<DOCDB_Users, TRec, S> setup = null, global::System.Func<DOCDB_Users, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oDOCDB_UserID = rec.GetOrdinal("DOCDB_UserID");
                    var oIsActive = rec.GetOrdinal("IsActive");
                    var oUserID = rec.GetOrdinal("UserID");
     
                    this.DOCDB_UserID = (long)(!rec.IsDBNull(oDOCDB_UserID) ? rec.GetValue(oDOCDB_UserID) : null);
                    this.IsActive = (bool)(!rec.IsDBNull(oIsActive) ? rec.GetValue(oIsActive) : null);
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
