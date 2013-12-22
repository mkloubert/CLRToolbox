// LICENSE: LGPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de


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
            public abstract partial class SecurityEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.ISecurityEntity
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
            public partial interface IAccessControlLists : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.ISecurityEntity
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
            public partial class AccessControlLists : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.IAccessControlLists
            {
                #region Scalar fields (1)
    
                private global::System.Int64 _sf_AccessControlListID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IAccessControlLists.AccessControlListID" />
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
            }
            
            #endregion
     
             
            #region ENTITY: AclResources
    
            /// <summary>
            /// Describes an 'AclResources' entity.
            /// </summary>
            public partial interface IAclResources : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.ISecurityEntity
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
            public partial class AclResources : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.IAclResources
            {
                #region Scalar fields (4)
    
                private global::System.Int64 _sf_AclResourceID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IAclResources.AclResourceID" />
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
                /// <see cref="IAclResources.AclRoleID" />
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
                /// <see cref="IAclResources.Description" />
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
                /// <see cref="IAclResources.Name" />
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
            }
            
            #endregion
     
             
            #region ENTITY: AclRoles
    
            /// <summary>
            /// Describes an 'AclRoles' entity.
            /// </summary>
            public partial interface IAclRoles : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.ISecurityEntity
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
            public partial class AclRoles : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.IAclRoles
            {
                #region Scalar fields (4)
    
                private global::System.Int64 _sf_AccessControlListID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IAclRoles.AccessControlListID" />
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
                /// <see cref="IAclRoles.AclRoleID" />
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
                /// <see cref="IAclRoles.Description" />
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
                /// <see cref="IAclRoles.Name" />
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
            }
            
            #endregion
     
             
            #region ENTITY: Users
    
            /// <summary>
            /// Describes an 'Users' entity.
            /// </summary>
            public partial interface IUsers : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.ISecurityEntity
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
            public partial class Users : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.SecurityEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Security.IUsers
            {
                #region Scalar fields (3)
    
                private global::System.Byte[] _sf_Password;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IUsers.Password" />
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
                /// <see cref="IUsers.PersonID" />
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
                /// <see cref="IUsers.UserID" />
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
            public abstract partial class StructureEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Structure.IStructureEntity
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
            public partial interface IPersons : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Structure.IStructureEntity
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
            public partial class Persons : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Structure.StructureEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Structure.IPersons
            {
                #region Scalar fields (3)
    
                private global::System.Guid _sf_PersonExportID;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersons.PersonExportID" />
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
                /// <see cref="IPersons.PersonID" />
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
                /// <see cref="IPersons.PersonTypeID" />
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
            public abstract partial class TypesEntityBase : global::MarcelJoachimKloubert.ApplicationServer.Data.Entities.AppServerEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Types.ITypesEntity
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
            public partial interface IPersonTypes : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Types.ITypesEntity
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
            public partial class PersonTypes : global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Types.TypesEntityBase, global::MarcelJoachimKloubert.ApplicationServer.DataModels.Entities.AppServer.Types.IPersonTypes
            {
                #region Scalar fields (3)
    
                private global::System.String _sf_Description;
    
                /// <summary>
                /// 
                /// </summary>
                /// <see cref="IPersonTypes.Description" />
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
                /// <see cref="IPersonTypes.Name" />
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
                /// <see cref="IPersonTypes.PersonTypeID" />
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
            }
            
            #endregion
     
            
        }
     
    }
}
