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
                global::System.Int64 DOCDB_UserID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'IsActive'.
                /// </summary>
                global::System.Boolean IsActive { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'UserID'.
                /// </summary>
                global::System.Int64 UserID { get; set; }
     
    
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
    
                private global::System.Int64 _sf_DOCDB_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_Users.DOCDB_UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = true, IsNullable = false, Name = "DOCDB_UserID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "DOCDB_UserID")]
                public global::System.Int64 DOCDB_UserID
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
    
                private global::System.Boolean _sf_IsActive;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_Users.IsActive" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Boolean), IsKey = false, IsNullable = false, Name = "IsActive")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "IsActive")]
                public global::System.Boolean IsActive
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
    
                private global::System.Int64 _sf_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.AppServer.Modules.DocDB.Data.Entities.General.DocDBService.IDOCDB_Users.UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = false, IsNullable = false, Name = "UserID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "UserID")]
                public global::System.Int64 UserID
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
     
                    this.DOCDB_UserID = (global::System.Int64)(!rec.IsDBNull(oDOCDB_UserID) ? rec.GetValue(oDOCDB_UserID) : null);
                    this.IsActive = (global::System.Boolean)(!rec.IsDBNull(oIsActive) ? rec.GetValue(oIsActive) : null);
                    this.UserID = (global::System.Int64)(!rec.IsDBNull(oUserID) ? rec.GetValue(oUserID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
    
        }
     
    }
}
