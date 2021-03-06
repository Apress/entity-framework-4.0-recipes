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
[assembly: EdmRelationshipAttribute("EFRecipesModel", "FK_Product_Category", "Category", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Recipe1.Category), "Product", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Recipe1.Product), true)]
#endregion

namespace Recipe1
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
        public ObjectSet<Category> Categories
        {
            get
            {
                if ((_Categories == null))
                {
                    _Categories = base.CreateObjectSet<Category>("Categories");
                }
                return _Categories;
            }
        }
        private ObjectSet<Category> _Categories;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Product> Products
        {
            get
            {
                if ((_Products == null))
                {
                    _Products = base.CreateObjectSet<Product>("Products");
                }
                return _Products;
            }
        }
        private ObjectSet<Product> _Products;
    
        #endregion
        #region AddTo Methods
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Categories EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCategories(Category category)
        {
            base.AddObject("Categories", category);
        }
            
        /// <summary>
        /// Deprecated Method for adding a new object to the Products EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToProducts(Product product)
        {
            base.AddObject("Products", product);
        }
        #endregion
    }
    
    #endregion
    
    
    #region Entities
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Category")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Category : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="categoryId">Initial value of the CategoryId property.</param>
        /// <param name="categoryName">Initial value of the CategoryName property.</param>
        public static Category CreateCategory(global::System.Int32 categoryId, global::System.String categoryName)
        {
            Category category = new Category();
            category.CategoryId = categoryId;
            
            category.CategoryName = categoryName;
            
            return category;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set
            {
                if (_CategoryId != value)
                {
                    OnCategoryIdChanging(value);
                    ReportPropertyChanging("CategoryId");
                    _CategoryId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("CategoryId");
                    OnCategoryIdChanged();
                }
            }
                
        }
        private global::System.Int32 _CategoryId;
        partial void OnCategoryIdChanging(global::System.Int32 value);
        partial void OnCategoryIdChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String CategoryName
        {
            get
            {
                return _CategoryName;
            }
            set
            {
                OnCategoryNameChanging(value);
                ReportPropertyChanging("CategoryName");
                _CategoryName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("CategoryName");
                OnCategoryNameChanged();
            }
                
        }
        private global::System.String _CategoryName;
        partial void OnCategoryNameChanging(global::System.String value);
        partial void OnCategoryNameChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Product_Category", "Product")] 
        public EntityCollection<Product> Products
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Product>("EFRecipesModel.FK_Product_Category", "Product");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Product>("EFRecipesModel.FK_Product_Category", "Product", value);
                }
            }
        }
        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Product")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Product : EntityObject
    {
        #region Factory Method
        /// <summary>
        /// Create a new Product object.
        /// </summary>
        /// <param name="productId">Initial value of the ProductId property.</param>
        /// <param name="productName">Initial value of the ProductName property.</param>
        /// <param name="unitPrice">Initial value of the UnitPrice property.</param>
        /// <param name="categoryId">Initial value of the CategoryId property.</param>
        public static Product CreateProduct(global::System.Int32 productId, global::System.String productName, global::System.Decimal unitPrice, global::System.Int32 categoryId)
        {
            Product product = new Product();
            product.ProductId = productId;
            
            product.ProductName = productName;
            
            product.UnitPrice = unitPrice;
            
            product.CategoryId = categoryId;
            
            return product;
        }
        #endregion

        #region Primitive Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                if (_ProductId != value)
                {
                    OnProductIdChanging(value);
                    ReportPropertyChanging("ProductId");
                    _ProductId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ProductId");
                    OnProductIdChanged();
                }
            }
                
        }
        private global::System.Int32 _ProductId;
        partial void OnProductIdChanging(global::System.Int32 value);
        partial void OnProductIdChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                OnProductNameChanging(value);
                ReportPropertyChanging("ProductName");
                _ProductName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ProductName");
                OnProductNameChanged();
            }
                
        }
        private global::System.String _ProductName;
        partial void OnProductNameChanging(global::System.String value);
        partial void OnProductNameChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal UnitPrice
        {
            get
            {
                return _UnitPrice;
            }
            set
            {
                OnUnitPriceChanging(value);
                ReportPropertyChanging("UnitPrice");
                _UnitPrice = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("UnitPrice");
                OnUnitPriceChanged();
            }
                
        }
        private global::System.Decimal _UnitPrice;
        partial void OnUnitPriceChanging(global::System.Decimal value);
        partial void OnUnitPriceChanged();
        
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set
            {
                OnCategoryIdChanging(value);
                ReportPropertyChanging("CategoryId");
                _CategoryId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CategoryId");
                OnCategoryIdChanged();
            }
                
        }
        private global::System.Int32 _CategoryId;
        partial void OnCategoryIdChanging(global::System.Int32 value);
        partial void OnCategoryIdChanged();
        
        #endregion
    
        #region Navigation Properties
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("EFRecipesModel", "FK_Product_Category", "Category")] 
        public Category Category
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("EFRecipesModel.FK_Product_Category", "Category").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("EFRecipesModel.FK_Product_Category", "Category").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Category> CategoryReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("EFRecipesModel.FK_Product_Category", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Category>("EFRecipesModel.FK_Product_Category", "Category", value);
                }
            }
        }
        #endregion
    }
    
    #endregion
    
}
