using System;

namespace MarcelJoachimKloubert.ApplicationServer.DataModels.Entities
{
    namespace AppServer
    {
        namespace Security
        {
            #region SCHEMA ENTITIES: Security
    
            /// <summary>
            /// Describes an entity for the 'Security' schema.
            /// </summary>
            public partial interface ISecurityEntity : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'Security' schema.
            /// </summary>
            public abstract partial class SecurityEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase,
                                                                                        ISecurityEntity
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
    
     
            #region ENTITY: Users
    
            /// <summary>
            /// Describes an 'Users' entity.
            /// </summary>
            public partial interface IUsers : ISecurityEntity
            {
                #region Scalar fields (2)
                /// <summary>
                /// Gets or sets the scalar field 'Password'.
                /// </summary>
                byte[] Password { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'UserID'.
                /// </summary>
                long UserID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'Users' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "Users", Schema = "Security")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class Users : SecurityEntityBase,
                                               IUsers
            {
                #region Scalar fields (2)
    
                private byte[] _sf_Password;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IUsers.Password" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = false, IsNullable = true, Name = "Password")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Password")]
                public byte[] Password
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
    
                private long _sf_UserID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IUsers.UserID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = true, IsNullable = false, Name = "UserID")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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
            }
            
            #endregion
     
            
        }
     
        namespace Structure
        {
            #region SCHEMA ENTITIES: Structure
    
            /// <summary>
            /// Describes an entity for the 'Structure' schema.
            /// </summary>
            public partial interface IStructureEntity : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'Structure' schema.
            /// </summary>
            public abstract partial class StructureEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase,
                                                                                        IStructureEntity
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
            public partial interface IPersons : IStructureEntity
            {
                #region Scalar fields (3)
                /// <summary>
                /// Gets or sets the scalar field 'PersonExportID'.
                /// </summary>
                System.Guid PersonExportID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'PersonID'.
                /// </summary>
                long PersonID { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'PersonTypeID'.
                /// </summary>
                Nullable<short> PersonTypeID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'Persons' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "Persons", Schema = "Structure")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class Persons : StructureEntityBase,
                                               IPersons
            {
                #region Scalar fields (3)
    
                private System.Guid _sf_PersonExportID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersons.PersonExportID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = false, IsNullable = false, Name = "PersonExportID")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonExportID")]
                public System.Guid PersonExportID
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
    
                private long _sf_PersonID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersons.PersonID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = true, IsNullable = false, Name = "PersonID")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonID")]
                public long PersonID
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
    
                private Nullable<short> _sf_PersonTypeID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersons.PersonTypeID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = false, IsNullable = true, Name = "PersonTypeID")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonTypeID")]
                public Nullable<short> PersonTypeID
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
            }
            
            #endregion
     
            
        }
     
        namespace Types
        {
            #region SCHEMA ENTITIES: Types
    
            /// <summary>
            /// Describes an entity for the 'Types' schema.
            /// </summary>
            public partial interface ITypesEntity : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.IAppServerEntity
            {
    
            }
    
            /// <summary>
            /// A basic entity for the 'Types' schema.
            /// </summary>
            public abstract partial class TypesEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase,
                                                                                        ITypesEntity
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
            public partial interface IPersonTypes : ITypesEntity
            {
                #region Scalar fields (3)
                /// <summary>
                /// Gets or sets the scalar field 'Description'.
                /// </summary>
                string Description { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'Name'.
                /// </summary>
                string Name { get; set; }
     
                /// <summary>
                /// Gets or sets the scalar field 'PersonTypeID'.
                /// </summary>
                short PersonTypeID { get; set; }
     
    
                #endregion
            }
            
            /// <summary>
            /// An 'PersonTypes' entity.
            /// </summary>
            [global::MarcelJoachimKloubert.CLRToolbox.Data.TMTable(Name = "PersonTypes", Schema = "Types")]
            [global::System.Runtime.Serialization.DataContract(IsReference = true)]
            [global::System.Serializable]
            public partial class PersonTypes : TypesEntityBase,
                                               IPersonTypes
            {
                #region Scalar fields (3)
    
                private string _sf_Description;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersonTypes.Description" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = false, IsNullable = true, Name = "Description")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "Description")]
                public string Description
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
    
                private string _sf_Name;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersonTypes.Name" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = false, IsNullable = false, Name = "Name")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
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
    
                private short _sf_PersonTypeID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersonTypes.PersonTypeID" />
                [global::MarcelJoachimKloubert.CLRToolbox.Data.TMColumn(IsKey = true, IsNullable = false, Name = "PersonTypeID")]
                [global::System.Data.Objects.DataClasses.EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
                [global::System.Runtime.Serialization.DataMember(EmitDefaultValue = true, Name = "PersonTypeID")]
                public short PersonTypeID
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
            }
            
            #endregion
     
            
        }
     
    }
}
