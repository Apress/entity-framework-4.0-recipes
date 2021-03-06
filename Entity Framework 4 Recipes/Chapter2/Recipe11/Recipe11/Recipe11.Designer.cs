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
[assembly: EdmRelationshipAttribute("EFRecipesModel", "FK_Park_Location1", "Location", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Recipe11.Location), "Park", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe11.Park), true)]
#endregion

namespace Recipe11
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
        public ObjectSet<Location> Locations
        {
            get
            {
                if ((_Locations == null))
                {
                    _Locations = base.CreateObjectSet<Location>("Locations");
                }
                return _Locations;
            }
        }
        private ObjectSet<Location> _Locations;
    
        #endregion
        #region AddTo Methods
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Locations EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLocations(Location location)
        {
            base.AddObject("Locations", location);
        }
        #endregion
    }
    
    #endregion
    
    
    #region Entities
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Location")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    [KnownTypeAttribute(typeof(Park))]
    public partial class Location : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Location object.
        /// </summary>
        /// <param name="locationId">Initial value of the LocationId property.</param>
        /// <param name="address">Initial value of the Address property.</param>
        /// <param name="city">Initial value of the City property.</param>
        /// <param name="state">Initial value of the State property.</param>
        /// <param name="zIPCode">Initial value of the ZIPCode property.</param>
        public static Location CreateLocation(global::System.Int32 locationId, global::System.String address, global::System.String city, global::System.String state, global::System.String zIPCode)
        {
            Location location = new Location();
            location.LocationId = locationId;
            
            location.Address = address;
            
            location.City = city;
            
            location.State = state;
            
            location.ZIPCode = zIPCode;
            
            return location;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 LocationId
        {
            get
            {
                return _LocationId;
            }
            set
            {
                if (_LocationId != value)
                {
                    OnLocationIdChanging(value);
                    ReportPropertyChanging("LocationId");
                    _LocationId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("LocationId");
                    OnLocationIdChanged();
                }
            }
                
        }
        private global::System.Int32 _LocationId;
        partial void OnLocationIdChanging(global::System.Int32 value);
        partial void OnLocationIdChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Address
        {
            get
            {
                return _Address;
            }
            set
            {
                OnAddressChanging(value);
                ReportPropertyChanging("Address");
                _Address = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Address");
                OnAddressChanged();
            }
                
        }
        private global::System.String _Address;
        partial void OnAddressChanging(global::System.String value);
        partial void OnAddressChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String City
        {
            get
            {
                return _City;
            }
            set
            {
                OnCityChanging(value);
                ReportPropertyChanging("City");
                _City = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("City");
                OnCityChanged();
            }
                
        }
        private global::System.String _City;
        partial void OnCityChanging(global::System.String value);
        partial void OnCityChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String State
        {
            get
            {
                return _State;
            }
            set
            {
                OnStateChanging(value);
                ReportPropertyChanging("State");
                _State = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("State");
                OnStateChanged();
            }
                
        }
        private global::System.String _State;
        partial void OnStateChanging(global::System.String value);
        partial void OnStateChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ZIPCode
        {
            get
            {
                return _ZIPCode;
            }
            set
            {
                OnZIPCodeChanging(value);
                ReportPropertyChanging("ZIPCode");
                _ZIPCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ZIPCode");
                OnZIPCodeChanged();
            }
                
        }
        private global::System.String _ZIPCode;
        partial void OnZIPCodeChanging(global::System.String value);
        partial void OnZIPCodeChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Park_Location1", "Park")] 
        public EntityCollection<Park> Parks
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Park>("EFRecipesModel.FK_Park_Location1", "Park");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Park>("EFRecipesModel.FK_Park_Location1", "Park", value);
                }
            }
        }
        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Park")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Park : Location
    {
        #region Factory Method
        /// <summary>
        /// Create a new Park object.
        /// </summary>
        /// <param name="locationId">Initial value of the LocationId property.</param>
        /// <param name="address">Initial value of the Address property.</param>
        /// <param name="city">Initial value of the City property.</param>
        /// <param name="state">Initial value of the State property.</param>
        /// <param name="zIPCode">Initial value of the ZIPCode property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="officeLocationId">Initial value of the OfficeLocationId property.</param>
        public static Park CreatePark(global::System.Int32 locationId, global::System.String address, global::System.String city, global::System.String state, global::System.String zIPCode, global::System.String name, global::System.Int32 officeLocationId)
        {
            Park park = new Park();
            park.LocationId = locationId;
            
            park.Address = address;
            
            park.City = city;
            
            park.State = state;
            
            park.ZIPCode = zIPCode;
            
            park.Name = name;
            
            park.OfficeLocationId = officeLocationId;
            
            return park;
        }
        #endregion

        #region Primitive Properties
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
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 OfficeLocationId
        {
            get
            {
                return _OfficeLocationId;
            }
            set
            {
                OnOfficeLocationIdChanging(value);
                ReportPropertyChanging("OfficeLocationId");
                _OfficeLocationId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("OfficeLocationId");
                OnOfficeLocationIdChanged();
            }
                
        }
        private global::System.Int32 _OfficeLocationId;
        partial void OnOfficeLocationIdChanging(global::System.Int32 value);
        partial void OnOfficeLocationIdChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Park_Location1", "Location")] 
        public Location Office
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Location>("EFRecipesModel.FK_Park_Location1", "Location").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Location>("EFRecipesModel.FK_Park_Location1", "Location").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Location> OfficeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Location>("EFRecipesModel.FK_Park_Location1", "Location");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Location>("EFRecipesModel.FK_Park_Location1", "Location", value);
                }
            }
        }
        #endregion
    }
    
    #endregion
    
}
