﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

#region EDM Relationship Metadata
[assembly: EdmRelationshipAttribute("EFRecipesModel", "Members", "Employee", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe3.Employee), "Project", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe3.Project))]
[assembly: EdmRelationshipAttribute("EFRecipesModel", "ProjectManager", "Employee", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Recipe3.Employee), "Project", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe3.Project))]
#endregion

namespace Recipe3
{
    #region Contexts
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class EFRecipesEntities : ObjectContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new EFRecipesEntities object using the connection string found in the 'EFRecipesEntities' section of the application configuration file.
        /// </summary>
        public EFRecipesEntities() : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EFRecipesEntities object.
        /// </summary>
        public EFRecipesEntities(string connectionString) : base(connectionString, "EFRecipesEntities")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EFRecipesEntities object.
        /// </summary>
        public EFRecipesEntities(EntityConnection connection) : base(connection, "EFRecipesEntities")
        {
            OnContextCreated();
        }
        #endregion
        
        #region Partial Methods
        partial void OnContextCreated();
        #endregion
        
        #region ObjectSet Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Employee> Employees
        {
            get
            {
                if ((_Employees == null))
                {
                    _Employees = base.CreateObjectSet<Employee>("Employees");
                }
                return _Employees;
            }
        }
        private ObjectSet<Employee> _Employees;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Project> Projects
        {
            get
            {
                if ((_Projects == null))
                {
                    _Projects = base.CreateObjectSet<Project>("Projects");
                }
                return _Projects;
            }
        }
        private ObjectSet<Project> _Projects;
    
        #endregion
        #region AddTo Methods
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Employees EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToEmployees(Employee employee)
        {
            base.AddObject("Employees", employee);
        }
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Projects EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToProjects(Project project)
        {
            base.AddObject("Projects", project);
        }
        #endregion
    }
    
    #endregion
    
    
    #region Entities
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Employee")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Employee : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Employee object.
        /// </summary>
        /// <param name="employeeId">Initial value of the EmployeeId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static Employee CreateEmployee(global::System.Int32 employeeId, global::System.String name)
        {
            Employee employee = new Employee();
            employee.EmployeeId = employeeId;
            
            employee.Name = name;
            
            return employee;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 EmployeeId
        {
            get
            {
                return _EmployeeId;
            }
            set
            {
                if (_EmployeeId != value)
                {
                    OnEmployeeIdChanging(value);
                    ReportPropertyChanging("EmployeeId");
                    _EmployeeId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("EmployeeId");
                    OnEmployeeIdChanged();
                }
            }
                
        }
        private global::System.Int32 _EmployeeId;
        partial void OnEmployeeIdChanging(global::System.Int32 value);
        partial void OnEmployeeIdChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
                
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "Members", "Project")] 
        public EntityCollection<Project> MemberProjects
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Project>("EFRecipesModel.Members", "Project");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Project>("EFRecipesModel.Members", "Project", value);
                }
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "ProjectManager", "Project")] 
        public EntityCollection<Project> ManagerProjects
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Project>("EFRecipesModel.ProjectManager", "Project");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Project>("EFRecipesModel.ProjectManager", "Project", value);
                }
            }
        }
        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Project")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Project : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Project object.
        /// </summary>
        /// <param name="projectId">Initial value of the ProjectId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static Project CreateProject(global::System.Int32 projectId, global::System.String name)
        {
            Project project = new Project();
            project.ProjectId = projectId;
            
            project.Name = name;
            
            return project;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ProjectId
        {
            get
            {
                return _ProjectId;
            }
            set
            {
                if (_ProjectId != value)
                {
                    OnProjectIdChanging(value);
                    ReportPropertyChanging("ProjectId");
                    _ProjectId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ProjectId");
                    OnProjectIdChanged();
                }
            }
                
        }
        private global::System.Int32 _ProjectId;
        partial void OnProjectIdChanging(global::System.Int32 value);
        partial void OnProjectIdChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
                
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "Members", "Employee")] 
        public EntityCollection<Employee> Members
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Employee>("EFRecipesModel.Members", "Employee");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Employee>("EFRecipesModel.Members", "Employee", value);
                }
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "ProjectManager", "Employee")] 
        public Employee ProjectManager
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Employee>("EFRecipesModel.ProjectManager", "Employee").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Employee>("EFRecipesModel.ProjectManager", "Employee").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Employee> ProjectManagerReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Employee>("EFRecipesModel.ProjectManager", "Employee");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Employee>("EFRecipesModel.ProjectManager", "Employee", value);
                }
            }
        }
        #endregion
    }
    
    #endregion
    
}
