// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


using System;

namespace MarcelJoachimKloubert.ApplicationServer.DataModels.Entities
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
        public abstract partial class GeneralEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.IGeneralEntity
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
     
        namespace Security
        {
            #region SCHEMA ENTITIES: Security
    
            /// <summary>
            /// Describes an entity for the 'Security' schema.
            /// </summary>
            public partial interface ISecurityEntity : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.IGeneralEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'Security' schema.
            /// </summary>
            public abstract partial class SecurityEntityBase : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.GeneralEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.ISecurityEntity
            {
                #region Constructors (1)
    
                /// <summary>
                /// Initializes a new instance of the <see cref="SecurityEntityBase" /> class.
                /// </summary>
                protected SecurityEntityBase()
                {
    
                }
    
                #endregion
            }
    
            #endregion
     
            #region ENTITY: AccessControlLists
    
            /// <summary>
            /// Describes an 'AccessControlLists' entity.
            /// </summary>
            public partial interface IAccessControlLists : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.ISecurityEntity
            {
                #region Scalar fields (1)
                /// <summary>
                /// Gets or sets the scalar field 'AccessControlListID'.
                /// </summary>
                global::System.Int64 AccessControlListID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'AccessControlLists' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "AccessControlLists", Schema = "Security")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class AccessControlLists : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAccessControlLists
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="AccessControlLists" /> class.
                /// </summary>
                public AccessControlLists()
                {
                    
                }
    
                #endregion
    
                #region Scalar fields (1)
    
                private global::System.Int64 _sf_AccessControlListID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAccessControlLists.AccessControlListID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = true, IsNullable = false, Name = "AccessControlListID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "AccessControlListID")]
                public global::System.Int64 AccessControlListID
                {
                    get { return this._sf_AccessControlListID; }
                    set
                    {
                        if (!object.Equals(this._sf_AccessControlListID, value))
                        {
                            this.OnPropertyChanging("AccessControlListID");
                            this._sf_AccessControlListID = value;
                            this.OnPropertyChanged("AccessControlListID");
                        }
                    }
                }
    
                #endregion
    
                #region Methods (9)
                
                /// <summary>
                /// Builds a new <see cref="AccessControlLists" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static AccessControlLists Build<TRec>(TRec rec, global::System.Action<AccessControlLists, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AccessControlLists();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="AccessControlLists" /> object from a data record.
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
                public static AccessControlLists Build<TRec, S>(TRec rec, global::System.Action<AccessControlLists, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AccessControlLists();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="AccessControlLists" /> object from a data record.
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
                public static AccessControlLists Build<TRec, S>(TRec rec, global::System.Action<AccessControlLists, TRec, S> setup = null, global::System.Func<AccessControlLists, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AccessControlLists();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AccessControlLists" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<AccessControlLists> BuildAll<TReader>(TReader reader, global::System.Action<AccessControlLists, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AccessControlLists" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<AccessControlLists> BuildAll<TReader, S>(TReader reader, global::System.Action<AccessControlLists, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AccessControlLists" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<AccessControlLists> BuildAll<TReader, S>(TReader reader, global::System.Action<AccessControlLists, TReader, S> setup = null, global::System.Func<AccessControlLists, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<AccessControlLists, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<AccessControlLists, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<AccessControlLists, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<AccessControlLists, TRec, S> setup = null, global::System.Func<AccessControlLists, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oAccessControlListID = rec.GetOrdinal("AccessControlListID");
     
                    this.AccessControlListID = (global::System.Int64)(!rec.IsDBNull(oAccessControlListID) ? rec.GetValue(oAccessControlListID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
     
            #region ENTITY: AclResources
    
            /// <summary>
            /// Describes an 'AclResources' entity.
            /// </summary>
            public partial interface IAclResources : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.ISecurityEntity
            {
                #region Scalar fields (4)
                /// <summary>
                /// Gets or sets the scalar field 'AclResourceID'.
                /// </summary>
                global::System.Int64 AclResourceID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'AclRoleID'.
                /// </summary>
                global::System.Int64 AclRoleID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Description'.
                /// </summary>
                global::System.String Description { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Name'.
                /// </summary>
                global::System.String Name { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'AclResources' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "AclResources", Schema = "Security")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class AclResources : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclResources
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="AclResources" /> class.
                /// </summary>
                public AclResources()
                {
                    
                }
    
                #endregion
    
                #region Scalar fields (4)
    
                private global::System.Int64 _sf_AclResourceID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclResources.AclResourceID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = true, IsNullable = false, Name = "AclResourceID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "AclResourceID")]
                public global::System.Int64 AclResourceID
                {
                    get { return this._sf_AclResourceID; }
                    set
                    {
                        if (!object.Equals(this._sf_AclResourceID, value))
                        {
                            this.OnPropertyChanging("AclResourceID");
                            this._sf_AclResourceID = value;
                            this.OnPropertyChanged("AclResourceID");
                        }
                    }
                }
    
                private global::System.Int64 _sf_AclRoleID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclResources.AclRoleID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = false, IsNullable = false, Name = "AclRoleID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "AclRoleID")]
                public global::System.Int64 AclRoleID
                {
                    get { return this._sf_AclRoleID; }
                    set
                    {
                        if (!object.Equals(this._sf_AclRoleID, value))
                        {
                            this.OnPropertyChanging("AclRoleID");
                            this._sf_AclRoleID = value;
                            this.OnPropertyChanged("AclRoleID");
                        }
                    }
                }
    
                private global::System.String _sf_Description;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclResources.Description" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.String), IsKey = false, IsNullable = true, Name = "Description")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Description")]
                public global::System.String Description
                {
                    get { return this._sf_Description; }
                    set
                    {
                        if (!object.Equals(this._sf_Description, value))
                        {
                            this.OnPropertyChanging("Description");
                            this._sf_Description = value;
                            this.OnPropertyChanged("Description");
                        }
                    }
                }
    
                private global::System.String _sf_Name;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclResources.Name" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.String), IsKey = false, IsNullable = false, Name = "Name")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Name")]
                public global::System.String Name
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
    
                #endregion
    
                #region Methods (9)
                
                /// <summary>
                /// Builds a new <see cref="AclResources" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static AclResources Build<TRec>(TRec rec, global::System.Action<AclResources, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AclResources();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="AclResources" /> object from a data record.
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
                public static AclResources Build<TRec, S>(TRec rec, global::System.Action<AclResources, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AclResources();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="AclResources" /> object from a data record.
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
                public static AclResources Build<TRec, S>(TRec rec, global::System.Action<AclResources, TRec, S> setup = null, global::System.Func<AclResources, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AclResources();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AclResources" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<AclResources> BuildAll<TReader>(TReader reader, global::System.Action<AclResources, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AclResources" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<AclResources> BuildAll<TReader, S>(TReader reader, global::System.Action<AclResources, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AclResources" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<AclResources> BuildAll<TReader, S>(TReader reader, global::System.Action<AclResources, TReader, S> setup = null, global::System.Func<AclResources, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<AclResources, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<AclResources, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<AclResources, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<AclResources, TRec, S> setup = null, global::System.Func<AclResources, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oAclResourceID = rec.GetOrdinal("AclResourceID");
                    var oAclRoleID = rec.GetOrdinal("AclRoleID");
                    var oDescription = rec.GetOrdinal("Description");
                    var oName = rec.GetOrdinal("Name");
     
                    this.AclResourceID = (global::System.Int64)(!rec.IsDBNull(oAclResourceID) ? rec.GetValue(oAclResourceID) : null);
                    this.AclRoleID = (global::System.Int64)(!rec.IsDBNull(oAclRoleID) ? rec.GetValue(oAclRoleID) : null);
                    this.Description = (global::System.String)(!rec.IsDBNull(oDescription) ? rec.GetValue(oDescription) : null);
                    this.Name = (global::System.String)(!rec.IsDBNull(oName) ? rec.GetValue(oName) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
     
            #region ENTITY: AclRoles
    
            /// <summary>
            /// Describes an 'AclRoles' entity.
            /// </summary>
            public partial interface IAclRoles : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.ISecurityEntity
            {
                #region Scalar fields (4)
                /// <summary>
                /// Gets or sets the scalar field 'AccessControlListID'.
                /// </summary>
                global::System.Int64 AccessControlListID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'AclRoleID'.
                /// </summary>
                global::System.Int64 AclRoleID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Description'.
                /// </summary>
                global::System.String Description { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Name'.
                /// </summary>
                global::System.String Name { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'AclRoles' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "AclRoles", Schema = "Security")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class AclRoles : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclRoles
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="AclRoles" /> class.
                /// </summary>
                public AclRoles()
                {
                    
                }
    
                #endregion
    
                #region Scalar fields (4)
    
                private global::System.Int64 _sf_AccessControlListID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclRoles.AccessControlListID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = false, IsNullable = false, Name = "AccessControlListID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "AccessControlListID")]
                public global::System.Int64 AccessControlListID
                {
                    get { return this._sf_AccessControlListID; }
                    set
                    {
                        if (!object.Equals(this._sf_AccessControlListID, value))
                        {
                            this.OnPropertyChanging("AccessControlListID");
                            this._sf_AccessControlListID = value;
                            this.OnPropertyChanged("AccessControlListID");
                        }
                    }
                }
    
                private global::System.Int64 _sf_AclRoleID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclRoles.AclRoleID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = true, IsNullable = false, Name = "AclRoleID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "AclRoleID")]
                public global::System.Int64 AclRoleID
                {
                    get { return this._sf_AclRoleID; }
                    set
                    {
                        if (!object.Equals(this._sf_AclRoleID, value))
                        {
                            this.OnPropertyChanging("AclRoleID");
                            this._sf_AclRoleID = value;
                            this.OnPropertyChanged("AclRoleID");
                        }
                    }
                }
    
                private global::System.String _sf_Description;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclRoles.Description" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.String), IsKey = false, IsNullable = true, Name = "Description")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Description")]
                public global::System.String Description
                {
                    get { return this._sf_Description; }
                    set
                    {
                        if (!object.Equals(this._sf_Description, value))
                        {
                            this.OnPropertyChanging("Description");
                            this._sf_Description = value;
                            this.OnPropertyChanged("Description");
                        }
                    }
                }
    
                private global::System.String _sf_Name;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IAclRoles.Name" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.String), IsKey = false, IsNullable = false, Name = "Name")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Name")]
                public global::System.String Name
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
    
                #endregion
    
                #region Methods (9)
                
                /// <summary>
                /// Builds a new <see cref="AclRoles" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static AclRoles Build<TRec>(TRec rec, global::System.Action<AclRoles, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AclRoles();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="AclRoles" /> object from a data record.
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
                public static AclRoles Build<TRec, S>(TRec rec, global::System.Action<AclRoles, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AclRoles();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="AclRoles" /> object from a data record.
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
                public static AclRoles Build<TRec, S>(TRec rec, global::System.Action<AclRoles, TRec, S> setup = null, global::System.Func<AclRoles, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new AclRoles();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AclRoles" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<AclRoles> BuildAll<TReader>(TReader reader, global::System.Action<AclRoles, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AclRoles" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<AclRoles> BuildAll<TReader, S>(TReader reader, global::System.Action<AclRoles, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="AclRoles" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<AclRoles> BuildAll<TReader, S>(TReader reader, global::System.Action<AclRoles, TReader, S> setup = null, global::System.Func<AclRoles, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<AclRoles, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<AclRoles, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<AclRoles, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<AclRoles, TRec, S> setup = null, global::System.Func<AclRoles, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oAccessControlListID = rec.GetOrdinal("AccessControlListID");
                    var oAclRoleID = rec.GetOrdinal("AclRoleID");
                    var oDescription = rec.GetOrdinal("Description");
                    var oName = rec.GetOrdinal("Name");
     
                    this.AccessControlListID = (global::System.Int64)(!rec.IsDBNull(oAccessControlListID) ? rec.GetValue(oAccessControlListID) : null);
                    this.AclRoleID = (global::System.Int64)(!rec.IsDBNull(oAclRoleID) ? rec.GetValue(oAclRoleID) : null);
                    this.Description = (global::System.String)(!rec.IsDBNull(oDescription) ? rec.GetValue(oDescription) : null);
                    this.Name = (global::System.String)(!rec.IsDBNull(oName) ? rec.GetValue(oName) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
     
            #region ENTITY: Users
    
            /// <summary>
            /// Describes an 'Users' entity.
            /// </summary>
            public partial interface IUsers : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.ISecurityEntity
            {
                #region Scalar fields (3)
                /// <summary>
                /// Gets or sets the scalar field 'Password'.
                /// </summary>
                global::System.Byte[] Password { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'PersonID'.
                /// </summary>
                global::System.Int64 PersonID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'UserID'.
                /// </summary>
                global::System.Int64 UserID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'Users' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "Users", Schema = "Security")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class Users : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IUsers
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="Users" /> class.
                /// </summary>
                public Users()
                {
                    
                }
    
                #endregion
    
                #region Scalar fields (3)
    
                private global::System.Byte[] _sf_Password;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IUsers.Password" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Byte[]), IsKey = false, IsNullable = true, Name = "Password")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Password")]
                public global::System.Byte[] Password
                {
                    get { return this._sf_Password; }
                    set
                    {
                        if (!object.Equals(this._sf_Password, value))
                        {
                            this.OnPropertyChanging("Password");
                            this._sf_Password = value;
                            this.OnPropertyChanged("Password");
                        }
                    }
                }
    
                private global::System.Int64 _sf_PersonID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IUsers.PersonID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = false, IsNullable = false, Name = "PersonID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonID")]
                public global::System.Int64 PersonID
                {
                    get { return this._sf_PersonID; }
                    set
                    {
                        if (!object.Equals(this._sf_PersonID, value))
                        {
                            this.OnPropertyChanging("PersonID");
                            this._sf_PersonID = value;
                            this.OnPropertyChanged("PersonID");
                        }
                    }
                }
    
                private global::System.Int64 _sf_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Security.IUsers.UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = true, IsNullable = false, Name = "UserID")]
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
     
                    var oPassword = rec.GetOrdinal("Password");
                    var oPersonID = rec.GetOrdinal("PersonID");
                    var oUserID = rec.GetOrdinal("UserID");
     
                    this.Password = (global::System.Byte[])(!rec.IsDBNull(oPassword) ? rec.GetValue(oPassword) : null);
                    this.PersonID = (global::System.Int64)(!rec.IsDBNull(oPersonID) ? rec.GetValue(oPersonID) : null);
                    this.UserID = (global::System.Int64)(!rec.IsDBNull(oUserID) ? rec.GetValue(oUserID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
    
        }
     
        namespace Structure
        {
            #region SCHEMA ENTITIES: Structure
    
            /// <summary>
            /// Describes an entity for the 'Structure' schema.
            /// </summary>
            public partial interface IStructureEntity : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.IGeneralEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'Structure' schema.
            /// </summary>
            public abstract partial class StructureEntityBase : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.GeneralEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Structure.IStructureEntity
            {
                #region Constructors (1)
    
                /// <summary>
                /// Initializes a new instance of the <see cref="StructureEntityBase" /> class.
                /// </summary>
                protected StructureEntityBase()
                {
    
                }
    
                #endregion
            }
    
            #endregion
     
            #region ENTITY: Persons
    
            /// <summary>
            /// Describes an 'Persons' entity.
            /// </summary>
            public partial interface IPersons : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Structure.IStructureEntity
            {
                #region Scalar fields (3)
                /// <summary>
                /// Gets or sets the scalar field 'PersonExportID'.
                /// </summary>
                global::System.Guid PersonExportID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'PersonID'.
                /// </summary>
                global::System.Int64 PersonID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'PersonTypeID'.
                /// </summary>
                global::System.Int16 PersonTypeID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'Persons' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "Persons", Schema = "Structure")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class Persons : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Structure.StructureEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Structure.IPersons
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="Persons" /> class.
                /// </summary>
                public Persons()
                {
                    
                }
    
                #endregion
    
                #region Scalar fields (3)
    
                private global::System.Guid _sf_PersonExportID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Structure.IPersons.PersonExportID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Guid), IsKey = false, IsNullable = false, Name = "PersonExportID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonExportID")]
                public global::System.Guid PersonExportID
                {
                    get { return this._sf_PersonExportID; }
                    set
                    {
                        if (!object.Equals(this._sf_PersonExportID, value))
                        {
                            this.OnPropertyChanging("PersonExportID");
                            this._sf_PersonExportID = value;
                            this.OnPropertyChanged("PersonExportID");
                        }
                    }
                }
    
                private global::System.Int64 _sf_PersonID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Structure.IPersons.PersonID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int64), IsKey = true, IsNullable = false, Name = "PersonID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonID")]
                public global::System.Int64 PersonID
                {
                    get { return this._sf_PersonID; }
                    set
                    {
                        if (!object.Equals(this._sf_PersonID, value))
                        {
                            this.OnPropertyChanging("PersonID");
                            this._sf_PersonID = value;
                            this.OnPropertyChanged("PersonID");
                        }
                    }
                }
    
                private global::System.Int16 _sf_PersonTypeID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Structure.IPersons.PersonTypeID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int16), IsKey = false, IsNullable = true, Name = "PersonTypeID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonTypeID")]
                public global::System.Int16 PersonTypeID
                {
                    get { return this._sf_PersonTypeID; }
                    set
                    {
                        if (!object.Equals(this._sf_PersonTypeID, value))
                        {
                            this.OnPropertyChanging("PersonTypeID");
                            this._sf_PersonTypeID = value;
                            this.OnPropertyChanged("PersonTypeID");
                        }
                    }
                }
    
                #endregion
    
                #region Methods (9)
                
                /// <summary>
                /// Builds a new <see cref="Persons" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static Persons Build<TRec>(TRec rec, global::System.Action<Persons, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new Persons();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="Persons" /> object from a data record.
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
                public static Persons Build<TRec, S>(TRec rec, global::System.Action<Persons, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new Persons();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="Persons" /> object from a data record.
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
                public static Persons Build<TRec, S>(TRec rec, global::System.Action<Persons, TRec, S> setup = null, global::System.Func<Persons, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new Persons();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="Persons" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<Persons> BuildAll<TReader>(TReader reader, global::System.Action<Persons, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="Persons" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<Persons> BuildAll<TReader, S>(TReader reader, global::System.Action<Persons, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="Persons" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<Persons> BuildAll<TReader, S>(TReader reader, global::System.Action<Persons, TReader, S> setup = null, global::System.Func<Persons, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<Persons, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<Persons, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<Persons, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<Persons, TRec, S> setup = null, global::System.Func<Persons, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oPersonExportID = rec.GetOrdinal("PersonExportID");
                    var oPersonID = rec.GetOrdinal("PersonID");
                    var oPersonTypeID = rec.GetOrdinal("PersonTypeID");
     
                    this.PersonExportID = (global::System.Guid)(!rec.IsDBNull(oPersonExportID) ? rec.GetValue(oPersonExportID) : null);
                    this.PersonID = (global::System.Int64)(!rec.IsDBNull(oPersonID) ? rec.GetValue(oPersonID) : null);
                    this.PersonTypeID = (global::System.Int16)(!rec.IsDBNull(oPersonTypeID) ? rec.GetValue(oPersonTypeID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
    
        }
     
        namespace Types
        {
            #region SCHEMA ENTITIES: Types
    
            /// <summary>
            /// Describes an entity for the 'Types' schema.
            /// </summary>
            public partial interface ITypesEntity : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.IGeneralEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'Types' schema.
            /// </summary>
            public abstract partial class TypesEntityBase : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.GeneralEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.ITypesEntity
            {
                #region Constructors (1)
    
                /// <summary>
                /// Initializes a new instance of the <see cref="TypesEntityBase" /> class.
                /// </summary>
                protected TypesEntityBase()
                {
    
                }
    
                #endregion
            }
    
            #endregion
     
            #region ENTITY: PersonTypes
    
            /// <summary>
            /// Describes an 'PersonTypes' entity.
            /// </summary>
            public partial interface IPersonTypes : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.ITypesEntity
            {
                #region Scalar fields (3)
                /// <summary>
                /// Gets or sets the scalar field 'Description'.
                /// </summary>
                global::System.String Description { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Name'.
                /// </summary>
                global::System.String Name { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'PersonTypeID'.
                /// </summary>
                global::System.Int16 PersonTypeID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'PersonTypes' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "PersonTypes", Schema = "Types")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class PersonTypes : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.TypesEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.IPersonTypes
            {
                #region Constructors (1)
                
                /// <summary>
                /// Initializes a new instance of <see cref="PersonTypes" /> class.
                /// </summary>
                public PersonTypes()
                {
                    
                }
    
                #endregion
    
                #region Scalar fields (3)
    
                private global::System.String _sf_Description;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.IPersonTypes.Description" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.String), IsKey = false, IsNullable = true, Name = "Description")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Description")]
                public global::System.String Description
                {
                    get { return this._sf_Description; }
                    set
                    {
                        if (!object.Equals(this._sf_Description, value))
                        {
                            this.OnPropertyChanging("Description");
                            this._sf_Description = value;
                            this.OnPropertyChanged("Description");
                        }
                    }
                }
    
                private global::System.String _sf_Name;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.IPersonTypes.Name" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.String), IsKey = false, IsNullable = false, Name = "Name")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Name")]
                public global::System.String Name
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
    
                private global::System.Int16 _sf_PersonTypeID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.General.Types.IPersonTypes.PersonTypeID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(ClrType = typeof(global::System.Int16), IsKey = true, IsNullable = false, Name = "PersonTypeID")]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonTypeID")]
                public global::System.Int16 PersonTypeID
                {
                    get { return this._sf_PersonTypeID; }
                    set
                    {
                        if (!object.Equals(this._sf_PersonTypeID, value))
                        {
                            this.OnPropertyChanging("PersonTypeID");
                            this._sf_PersonTypeID = value;
                            this.OnPropertyChanged("PersonTypeID");
                        }
                    }
                }
    
                #endregion
    
                #region Methods (9)
                
                /// <summary>
                /// Builds a new <see cref="PersonTypes" /> object from a data record.
                /// </summary>
                /// <typeparam name="TRec">Type of the data record.</typeparam>
                /// <param name="rec">The data record from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="rec" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The created object.</returns>
                public static PersonTypes Build<TRec>(TRec rec, global::System.Action<PersonTypes, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new PersonTypes();
                    result.LoadFrom<TRec>(rec: rec, setup: setup);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="PersonTypes" /> object from a data record.
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
                public static PersonTypes Build<TRec, S>(TRec rec, global::System.Action<PersonTypes, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new PersonTypes();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupState: setupState);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a new <see cref="PersonTypes" /> object from a data record.
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
                public static PersonTypes Build<TRec, S>(TRec rec, global::System.Action<PersonTypes, TRec, S> setup = null, global::System.Func<PersonTypes, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
    
                    var result = new PersonTypes();
                    result.LoadFrom<TRec, S>(rec: rec, setup: setup, setupStateFactory: setupStateFactory);
    
                    return result;
                }
    
                /// <summary>
                /// Builds a list of new <see cref="PersonTypes" /> objects from a data reader.
                /// </summary>
                /// <typeparam name="TReader">Type of the data reader.</typeparam>
                /// <param name="reader">The data reader from where loading the data from.</param>
                /// <param name="setup">The optional setup action that is invoked after data have been loaded into that object.</param>
                /// <exception cref="global::System.ArgumentNullException">
                /// <paramref name="reader" /> is <see langword="null" />.
                /// </exception>
                /// <returns>The lazy loaded sequence of new objects.</returns>
                public static global::System.Collections.Generic.IEnumerable<PersonTypes> BuildAll<TReader>(TReader reader, global::System.Action<PersonTypes, TReader> setup = null) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader>(rec: reader, setup: setup);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="PersonTypes" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<PersonTypes> BuildAll<TReader, S>(TReader reader, global::System.Action<PersonTypes, TReader, S> setup = null, S setupState = default(S)) where TReader : global::System.Data.IDataReader
                {
                    if (reader == null)
                        throw new global::System.ArgumentNullException("reader");
    
                    while (reader.Read())
                        yield return Build<TReader, S>(rec: reader, setup: setup, setupState: setupState);
                }
    
                /// <summary>
                /// Builds a list of new <see cref="PersonTypes" /> objects from a data reader.
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
                public static global::System.Collections.Generic.IEnumerable<PersonTypes> BuildAll<TReader, S>(TReader reader, global::System.Action<PersonTypes, TReader, S> setup = null, global::System.Func<PersonTypes, TReader, S> setupStateFactory = null) where TReader : global::System.Data.IDataReader
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
                public void LoadFrom<TRec>(TRec rec, global::System.Action<PersonTypes, TRec> setup = null) where TRec : global::System.Data.IDataRecord
                {
                    this.LoadFrom<TRec, object>(rec: rec, setup: setup != null ? new global::System.Action<PersonTypes, TRec, object>((e, r, s) => setup(e, r)) : null, setupState: null);
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<PersonTypes, TRec, S> setup = null, S setupState = default(S)) where TRec : global::System.Data.IDataRecord
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
                public void LoadFrom<TRec, S>(TRec rec, global::System.Action<PersonTypes, TRec, S> setup = null, global::System.Func<PersonTypes, TRec, S> setupStateFactory = null) where TRec : global::System.Data.IDataRecord
                {
                    if (rec == null)
                        throw new global::System.ArgumentNullException("rec");
     
                    var oDescription = rec.GetOrdinal("Description");
                    var oName = rec.GetOrdinal("Name");
                    var oPersonTypeID = rec.GetOrdinal("PersonTypeID");
     
                    this.Description = (global::System.String)(!rec.IsDBNull(oDescription) ? rec.GetValue(oDescription) : null);
                    this.Name = (global::System.String)(!rec.IsDBNull(oName) ? rec.GetValue(oName) : null);
                    this.PersonTypeID = (global::System.Int16)(!rec.IsDBNull(oPersonTypeID) ? rec.GetValue(oPersonTypeID) : null);
     
                    if (setup != null)
                        setup(this, rec, setupStateFactory == null ? default(S) : setupStateFactory(this, rec));
                }
    
                #endregion
            }
            
            #endregion
    
        }
     
    }
}
