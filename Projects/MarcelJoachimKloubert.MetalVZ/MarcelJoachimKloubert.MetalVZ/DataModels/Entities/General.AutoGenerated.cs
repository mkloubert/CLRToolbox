// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.MetalVZ.DataModels.Entities
{
    namespace General
    {
        #region BASE ENTITIES: General
    
        /// <summary>
        /// Describes a 'General' entity.
        /// </summary>
        public partial interface IGeneralEntity : global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.IMVZEntity
        {
    
        }
    
        /// <summary>
        /// A basic 'General' entity.
        /// </summary>
        public abstract partial class GeneralEntityBase : global::MarcelJoachimKloubert.MetalVZ.Classes.Data.Entities.MVZEntityBase, global::MarcelJoachimKloubert.MetalVZ.DataModels.Entities.General.IGeneralEntity
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
     
     
            #region ENTITY: Users
    
            /// <summary>
            /// Describes an 'Users' entity.
            /// </summary>
            public partial interface IUsers : global::MarcelJoachimKloubert.MetalVZ.DataModels.Entities.General.IGeneralEntity
            {
                #region Columns (2)
                /// <summary>
                /// Gets or sets the scalar field 'UserExportID'.
                /// </summary>
                System.Guid UserExportID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'UserID'.
                /// </summary>
                long UserID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'Users' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "Users", Schema = "dbo")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class Users : global::MarcelJoachimKloubert.MetalVZ.DataModels.Entities.General.GeneralEntityBase, global::MarcelJoachimKloubert.MetalVZ.DataModels.Entities.General.IUsers
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="Users" /> class.
                /// </summary>
                public Users()
                {
                    
                }
    
                #endregion
    
                #region Columns (2)
    
                private System.Guid _sf_UserExportID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.MetalVZ.DataModels.Entities.General.IUsers.UserExportID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(System.Guid), IsKey = false, IsNullable = false, Name = "UserExportID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "UserExportID")]
                public System.Guid UserExportID
                {
                    get { return this._sf_UserExportID; }
                    set
                    {
                        if (!object.Equals(this._sf_UserExportID, value))
                        {
                            this.OnPropertyChanging("UserExportID");
                            this._sf_UserExportID = value;
                            this.OnPropertyChanged("UserExportID");
                        }
                    }
                }
    
                private long _sf_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.MetalVZ.DataModels.Entities.General.IUsers.UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(long), IsKey = true, IsNullable = false, Name = "UserID")]
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
     
                #region Methods (9)
    
                /// <summary>
                /// Builds a new <see cref="Users" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static Users Build<TRec>(TRec rec, global::System.Action<Users, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new Users();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="Users" /> object from a data record.
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
                public static Users Build<TRec, S>(TRec rec, global::System.Action<Users, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new Users();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="Users" /> object from a data record.
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
                public static Users Build<TRec, S>(TRec rec, global::System.Action<Users, TRec, S> setup = null, global::System.Func<Users, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new Users();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="Users" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<Users> BuildAll<TReader>(TReader reader, global::System.Action<Users, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="Users" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<Users> BuildAll<TReader, S>(TReader reader, global::System.Action<Users, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="Users" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<Users> BuildAll<TReader, S>(TReader reader, global::System.Action<Users, TReader, S> setup = null, global::System.Func<Users, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<Users, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<Users, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<Users, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<Users, TRec, S> setup = null, global::System.Func<Users, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oUserExportID = rec.GetOrdinal("UserExportID");
                    var oUserID = rec.GetOrdinal("UserID");
     
                    this.UserExportID = (System.Guid)(!rec.IsDBNull(oUserExportID) ? rec.GetValue(oUserExportID) : null);
                    this.UserID = (long)(!rec.IsDBNull(oUserID) ? rec.GetValue(oUserID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
    }
}
