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

[assembly: EdmRelationshipAttribute("EFRecipesModel", "FK_ServiceCall_Technician", "Technician", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Recipe5.Technician), "ServiceCall", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe5.ServiceCall), true)]

#endregion

namespace Recipe5
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
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EFRecipesEntities object.
        /// </summary>
        public EFRecipesEntities(string connectionString) : base(connectionString, "EFRecipesEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EFRecipesEntities object.
        /// </summary>
        public EFRecipesEntities(EntityConnection connection) : base(connection, "EFRecipesEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
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
        public ObjectSet<ServiceCall> ServiceCalls
        {
            get
            {
                if ((_ServiceCalls == null))
                {
                    _ServiceCalls = base.CreateObjectSet<ServiceCall>("ServiceCalls");
                }
                return _ServiceCalls;
            }
        }
        private ObjectSet<ServiceCall> _ServiceCalls;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Technician> Technicians
        {
            get
            {
                if ((_Technicians == null))
                {
                    _Technicians = base.CreateObjectSet<Technician>("Technicians");
                }
                return _Technicians;
            }
        }
        private ObjectSet<Technician> _Technicians;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ServiceCalls EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToServiceCalls(ServiceCall serviceCall)
        {
            base.AddObject("ServiceCalls", serviceCall);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Technicians EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTechnicians(Technician technician)
        {
            base.AddObject("Technicians", technician);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="ServiceCall")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ServiceCall : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ServiceCall object.
        /// </summary>
        /// <param name="callId">Initial value of the CallId property.</param>
        /// <param name="contactName">Initial value of the ContactName property.</param>
        /// <param name="issue">Initial value of the Issue property.</param>
        /// <param name="techId">Initial value of the TechId property.</param>
        public static ServiceCall CreateServiceCall(global::System.Int32 callId, global::System.String contactName, global::System.String issue, global::System.Int32 techId)
        {
            ServiceCall serviceCall = new ServiceCall();
            serviceCall.CallId = callId;
            serviceCall.ContactName = contactName;
            serviceCall.Issue = issue;
            serviceCall.TechId = techId;
            return serviceCall;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CallId
        {
            get
            {
                return _CallId;
            }
            set
            {
                if (_CallId != value)
                {
                    OnCallIdChanging(value);
                    ReportPropertyChanging("CallId");
                    _CallId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("CallId");
                    OnCallIdChanged();
                }
            }
        }
        private global::System.Int32 _CallId;
        partial void OnCallIdChanging(global::System.Int32 value);
        partial void OnCallIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ContactName
        {
            get
            {
                return _ContactName;
            }
            set
            {
                OnContactNameChanging(value);
                ReportPropertyChanging("ContactName");
                _ContactName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ContactName");
                OnContactNameChanged();
            }
        }
        private global::System.String _ContactName;
        partial void OnContactNameChanging(global::System.String value);
        partial void OnContactNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Issue
        {
            get
            {
                return _Issue;
            }
            set
            {
                OnIssueChanging(value);
                ReportPropertyChanging("Issue");
                _Issue = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Issue");
                OnIssueChanged();
            }
        }
        private global::System.String _Issue;
        partial void OnIssueChanging(global::System.String value);
        partial void OnIssueChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 TechId
        {
            get
            {
                return _TechId;
            }
            set
            {
                OnTechIdChanging(value);
                ReportPropertyChanging("TechId");
                _TechId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TechId");
                OnTechIdChanged();
            }
        }
        private global::System.Int32 _TechId;
        partial void OnTechIdChanging(global::System.Int32 value);
        partial void OnTechIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_ServiceCall_Technician", "Technician")]
        public Technician Technician
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technician>("EFRecipesModel.FK_ServiceCall_Technician", "Technician").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technician>("EFRecipesModel.FK_ServiceCall_Technician", "Technician").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Technician> TechnicianReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Technician>("EFRecipesModel.FK_ServiceCall_Technician", "Technician");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Technician>("EFRecipesModel.FK_ServiceCall_Technician", "Technician", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Technician")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Technician : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Technician object.
        /// </summary>
        /// <param name="techId">Initial value of the TechId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static Technician CreateTechnician(global::System.Int32 techId, global::System.String name)
        {
            Technician technician = new Technician();
            technician.TechId = techId;
            technician.Name = name;
            return technician;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 TechId
        {
            get
            {
                return _TechId;
            }
            set
            {
                if (_TechId != value)
                {
                    OnTechIdChanging(value);
                    ReportPropertyChanging("TechId");
                    _TechId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("TechId");
                    OnTechIdChanged();
                }
            }
        }
        private global::System.Int32 _TechId;
        partial void OnTechIdChanging(global::System.Int32 value);
        partial void OnTechIdChanged();
    
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
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_ServiceCall_Technician", "ServiceCall")]
        public EntityCollection<ServiceCall> ServiceCalls
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<ServiceCall>("EFRecipesModel.FK_ServiceCall_Technician", "ServiceCall");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<ServiceCall>("EFRecipesModel.FK_ServiceCall_Technician", "ServiceCall", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
