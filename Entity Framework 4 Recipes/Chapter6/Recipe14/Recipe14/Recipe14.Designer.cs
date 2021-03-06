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

namespace Recipe14
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
        public ObjectSet<Invoice> Invoices
        {
            get
            {
                if ((_Invoices == null))
                {
                    _Invoices = base.CreateObjectSet<Invoice>("Invoices");
                }
                return _Invoices;
            }
        }
        private ObjectSet<Invoice> _Invoices;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Invoices EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToInvoices(Invoice invoice)
        {
            base.AddObject("Invoices", invoice);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="DeletedInvoice")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class DeletedInvoice : Invoice
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new DeletedInvoice object.
        /// </summary>
        /// <param name="invoiceId">Initial value of the InvoiceId property.</param>
        /// <param name="amount">Initial value of the Amount property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="date">Initial value of the Date property.</param>
        public static DeletedInvoice CreateDeletedInvoice(global::System.Int32 invoiceId, global::System.Decimal amount, global::System.String description, global::System.DateTime date)
        {
            DeletedInvoice deletedInvoice = new DeletedInvoice();
            deletedInvoice.InvoiceId = invoiceId;
            deletedInvoice.Amount = amount;
            deletedInvoice.Description = description;
            deletedInvoice.Date = date;
            return deletedInvoice;
        }

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Invoice")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    [KnownTypeAttribute(typeof(DeletedInvoice))]
    public partial class Invoice : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Invoice object.
        /// </summary>
        /// <param name="invoiceId">Initial value of the InvoiceId property.</param>
        /// <param name="amount">Initial value of the Amount property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="date">Initial value of the Date property.</param>
        public static Invoice CreateInvoice(global::System.Int32 invoiceId, global::System.Decimal amount, global::System.String description, global::System.DateTime date)
        {
            Invoice invoice = new Invoice();
            invoice.InvoiceId = invoiceId;
            invoice.Amount = amount;
            invoice.Description = description;
            invoice.Date = date;
            return invoice;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 InvoiceId
        {
            get
            {
                return _InvoiceId;
            }
            set
            {
                if (_InvoiceId != value)
                {
                    OnInvoiceIdChanging(value);
                    ReportPropertyChanging("InvoiceId");
                    _InvoiceId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("InvoiceId");
                    OnInvoiceIdChanged();
                }
            }
        }
        private global::System.Int32 _InvoiceId;
        partial void OnInvoiceIdChanging(global::System.Int32 value);
        partial void OnInvoiceIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                OnAmountChanging(value);
                ReportPropertyChanging("Amount");
                _Amount = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Amount");
                OnAmountChanged();
            }
        }
        private global::System.Decimal _Amount;
        partial void OnAmountChanging(global::System.Decimal value);
        partial void OnAmountChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                OnDateChanging(value);
                ReportPropertyChanging("Date");
                _Date = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Date");
                OnDateChanged();
            }
        }
        private global::System.DateTime _Date;
        partial void OnDateChanging(global::System.DateTime value);
        partial void OnDateChanged();

        #endregion
    
    }

    #endregion
    
}
