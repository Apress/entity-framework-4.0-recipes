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
[assembly: EdmRelationshipAttribute("EFRecipesModel", "FK_Room_Hotel", "Hotel", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Recipe6.Hotel), "Room", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe6.Room), true)]
[assembly: EdmRelationshipAttribute("EFRecipesModel", "FK_Reservation_Room", "Room", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Recipe6.Room), "Reservation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe6.Reservation), true)]
#endregion

namespace Recipe6
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
        public ObjectSet<Hotel> Hotels
        {
            get
            {
                if ((_Hotels == null))
                {
                    _Hotels = base.CreateObjectSet<Hotel>("Hotels");
                }
                return _Hotels;
            }
        }
        private ObjectSet<Hotel> _Hotels;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Reservation> Reservations
        {
            get
            {
                if ((_Reservations == null))
                {
                    _Reservations = base.CreateObjectSet<Reservation>("Reservations");
                }
                return _Reservations;
            }
        }
        private ObjectSet<Reservation> _Reservations;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Room> Rooms
        {
            get
            {
                if ((_Rooms == null))
                {
                    _Rooms = base.CreateObjectSet<Room>("Rooms");
                }
                return _Rooms;
            }
        }
        private ObjectSet<Room> _Rooms;
    
        #endregion
        #region AddTo Methods
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Hotels EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToHotels(Hotel hotel)
        {
            base.AddObject("Hotels", hotel);
        }
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Reservations EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToReservations(Reservation reservation)
        {
            base.AddObject("Reservations", reservation);
        }
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Rooms EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToRooms(Room room)
        {
            base.AddObject("Rooms", room);
        }
        #endregion
    }
    
    #endregion
    
    
    #region Entities
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="ExecutiveSuite")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ExecutiveSuite : Room
    {
        #region Factory Method
        /// <summary>
        /// Create a new ExecutiveSuite object.
        /// </summary>
        /// <param name="roomId">Initial value of the RoomId property.</param>
        /// <param name="rate">Initial value of the Rate property.</param>
        /// <param name="hotelId">Initial value of the HotelId property.</param>
        public static ExecutiveSuite CreateExecutiveSuite(global::System.Int32 roomId, global::System.Decimal rate, global::System.Int32 hotelId)
        {
            ExecutiveSuite executiveSuite = new ExecutiveSuite();
            executiveSuite.RoomId = roomId;
            
            executiveSuite.Rate = rate;
            
            executiveSuite.HotelId = hotelId;
            
            return executiveSuite;
        }
        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Hotel")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Hotel : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Hotel object.
        /// </summary>
        /// <param name="hotelId">Initial value of the HotelId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static Hotel CreateHotel(global::System.Int32 hotelId, global::System.String name)
        {
            Hotel hotel = new Hotel();
            hotel.HotelId = hotelId;
            
            hotel.Name = name;
            
            return hotel;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 HotelId
        {
            get
            {
                return _HotelId;
            }
            set
            {
                if (_HotelId != value)
                {
                    OnHotelIdChanging(value);
                    ReportPropertyChanging("HotelId");
                    _HotelId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("HotelId");
                    OnHotelIdChanged();
                }
            }
                
        }
        private global::System.Int32 _HotelId;
        partial void OnHotelIdChanging(global::System.Int32 value);
        partial void OnHotelIdChanged();
        
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
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Room_Hotel", "Room")] 
        public EntityCollection<Room> Rooms
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Room>("EFRecipesModel.FK_Room_Hotel", "Room");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Room>("EFRecipesModel.FK_Room_Hotel", "Room", value);
                }
            }
        }
        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Reservation")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Reservation : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Reservation object.
        /// </summary>
        /// <param name="reservationId">Initial value of the ReservationId property.</param>
        /// <param name="startDate">Initial value of the StartDate property.</param>
        /// <param name="endDate">Initial value of the EndDate property.</param>
        /// <param name="contactName">Initial value of the ContactName property.</param>
        /// <param name="roomId">Initial value of the RoomId property.</param>
        public static Reservation CreateReservation(global::System.Int32 reservationId, global::System.DateTime startDate, global::System.DateTime endDate, global::System.String contactName, global::System.Int32 roomId)
        {
            Reservation reservation = new Reservation();
            reservation.ReservationId = reservationId;
            
            reservation.StartDate = startDate;
            
            reservation.EndDate = endDate;
            
            reservation.ContactName = contactName;
            
            reservation.RoomId = roomId;
            
            return reservation;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ReservationId
        {
            get
            {
                return _ReservationId;
            }
            set
            {
                if (_ReservationId != value)
                {
                    OnReservationIdChanging(value);
                    ReportPropertyChanging("ReservationId");
                    _ReservationId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ReservationId");
                    OnReservationIdChanged();
                }
            }
                
        }
        private global::System.Int32 _ReservationId;
        partial void OnReservationIdChanging(global::System.Int32 value);
        partial void OnReservationIdChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                OnStartDateChanging(value);
                ReportPropertyChanging("StartDate");
                _StartDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StartDate");
                OnStartDateChanged();
            }
                
        }
        private global::System.DateTime _StartDate;
        partial void OnStartDateChanging(global::System.DateTime value);
        partial void OnStartDateChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                OnEndDateChanging(value);
                ReportPropertyChanging("EndDate");
                _EndDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EndDate");
                OnEndDateChanged();
            }
                
        }
        private global::System.DateTime _EndDate;
        partial void OnEndDateChanging(global::System.DateTime value);
        partial void OnEndDateChanged();
        
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
        public global::System.Int32 RoomId
        {
            get
            {
                return _RoomId;
            }
            set
            {
                OnRoomIdChanging(value);
                ReportPropertyChanging("RoomId");
                _RoomId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RoomId");
                OnRoomIdChanged();
            }
                
        }
        private global::System.Int32 _RoomId;
        partial void OnRoomIdChanging(global::System.Int32 value);
        partial void OnRoomIdChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Reservation_Room", "Room")] 
        public Room Room
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Room>("EFRecipesModel.FK_Reservation_Room", "Room").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Room>("EFRecipesModel.FK_Reservation_Room", "Room").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Room> RoomReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Room>("EFRecipesModel.FK_Reservation_Room", "Room");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Room>("EFRecipesModel.FK_Reservation_Room", "Room", value);
                }
            }
        }
        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Room")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    [KnownTypeAttribute(typeof(ExecutiveSuite))]
    public partial class Room : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Room object.
        /// </summary>
        /// <param name="roomId">Initial value of the RoomId property.</param>
        /// <param name="rate">Initial value of the Rate property.</param>
        /// <param name="hotelId">Initial value of the HotelId property.</param>
        public static Room CreateRoom(global::System.Int32 roomId, global::System.Decimal rate, global::System.Int32 hotelId)
        {
            Room room = new Room();
            room.RoomId = roomId;
            
            room.Rate = rate;
            
            room.HotelId = hotelId;
            
            return room;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 RoomId
        {
            get
            {
                return _RoomId;
            }
            set
            {
                if (_RoomId != value)
                {
                    OnRoomIdChanging(value);
                    ReportPropertyChanging("RoomId");
                    _RoomId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("RoomId");
                    OnRoomIdChanged();
                }
            }
                
        }
        private global::System.Int32 _RoomId;
        partial void OnRoomIdChanging(global::System.Int32 value);
        partial void OnRoomIdChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Rate
        {
            get
            {
                return _Rate;
            }
            set
            {
                OnRateChanging(value);
                ReportPropertyChanging("Rate");
                _Rate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Rate");
                OnRateChanged();
            }
                
        }
        private global::System.Decimal _Rate;
        partial void OnRateChanging(global::System.Decimal value);
        partial void OnRateChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 HotelId
        {
            get
            {
                return _HotelId;
            }
            set
            {
                OnHotelIdChanging(value);
                ReportPropertyChanging("HotelId");
                _HotelId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("HotelId");
                OnHotelIdChanged();
            }
                
        }
        private global::System.Int32 _HotelId;
        partial void OnHotelIdChanging(global::System.Int32 value);
        partial void OnHotelIdChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Room_Hotel", "Hotel")] 
        public Hotel Hotel
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Hotel>("EFRecipesModel.FK_Room_Hotel", "Hotel").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Hotel>("EFRecipesModel.FK_Room_Hotel", "Hotel").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Hotel> HotelReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Hotel>("EFRecipesModel.FK_Room_Hotel", "Hotel");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Hotel>("EFRecipesModel.FK_Room_Hotel", "Hotel", value);
                }
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Reservation_Room", "Reservation")] 
        public EntityCollection<Reservation> Reservations
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Reservation>("EFRecipesModel.FK_Reservation_Room", "Reservation");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Reservation>("EFRecipesModel.FK_Reservation_Room", "Reservation", value);
                }
            }
        }
        #endregion
    }
    
    #endregion
    
}
