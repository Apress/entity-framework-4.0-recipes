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
        public ObjectSet<Person> People
        {
            get
            {
                if ((_People == null))
                {
                    _People = base.CreateObjectSet<Person>("People");
                }
                return _People;
            }
        }
        private ObjectSet<Person> _People;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the People EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPeople(Person person)
        {
            base.AddObject("People", person);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Instructor")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Instructor : Person
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Instructor object.
        /// </summary>
        /// <param name="personId">Initial value of the PersonId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="timeStamp">Initial value of the TimeStamp property.</param>
        /// <param name="hireDate">Initial value of the HireDate property.</param>
        public static Instructor CreateInstructor(global::System.Int32 personId, global::System.String name, global::System.Byte[] timeStamp, global::System.DateTime hireDate)
        {
            Instructor instructor = new Instructor();
            instructor.PersonId = personId;
            instructor.Name = name;
            instructor.TimeStamp = timeStamp;
            instructor.HireDate = hireDate;
            return instructor;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime HireDate
        {
            get
            {
                return _HireDate;
            }
            set
            {
                OnHireDateChanging(value);
                ReportPropertyChanging("HireDate");
                _HireDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("HireDate");
                OnHireDateChanged();
            }
        }
        private global::System.DateTime _HireDate;
        partial void OnHireDateChanging(global::System.DateTime value);
        partial void OnHireDateChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Person")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    [KnownTypeAttribute(typeof(Instructor))]
    [KnownTypeAttribute(typeof(Student))]
    public partial class Person : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Person object.
        /// </summary>
        /// <param name="personId">Initial value of the PersonId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="timeStamp">Initial value of the TimeStamp property.</param>
        public static Person CreatePerson(global::System.Int32 personId, global::System.String name, global::System.Byte[] timeStamp)
        {
            Person person = new Person();
            person.PersonId = personId;
            person.Name = name;
            person.TimeStamp = timeStamp;
            return person;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 PersonId
        {
            get
            {
                return _PersonId;
            }
            set
            {
                if (_PersonId != value)
                {
                    OnPersonIdChanging(value);
                    ReportPropertyChanging("PersonId");
                    _PersonId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("PersonId");
                    OnPersonIdChanged();
                }
            }
        }
        private global::System.Int32 _PersonId;
        partial void OnPersonIdChanging(global::System.Int32 value);
        partial void OnPersonIdChanged();
    
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
        public global::System.Byte[] TimeStamp
        {
            get
            {
                return StructuralObject.GetValidValue(_TimeStamp);
            }
            set
            {
                OnTimeStampChanging(value);
                ReportPropertyChanging("TimeStamp");
                _TimeStamp = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("TimeStamp");
                OnTimeStampChanged();
            }
        }
        private global::System.Byte[] _TimeStamp;
        partial void OnTimeStampChanging(global::System.Byte[] value);
        partial void OnTimeStampChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EFRecipesModel", Name="Student")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Student : Person
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Student object.
        /// </summary>
        /// <param name="personId">Initial value of the PersonId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="timeStamp">Initial value of the TimeStamp property.</param>
        /// <param name="enrollmentDate">Initial value of the EnrollmentDate property.</param>
        public static Student CreateStudent(global::System.Int32 personId, global::System.String name, global::System.Byte[] timeStamp, global::System.DateTime enrollmentDate)
        {
            Student student = new Student();
            student.PersonId = personId;
            student.Name = name;
            student.TimeStamp = timeStamp;
            student.EnrollmentDate = enrollmentDate;
            return student;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime EnrollmentDate
        {
            get
            {
                return _EnrollmentDate;
            }
            set
            {
                OnEnrollmentDateChanging(value);
                ReportPropertyChanging("EnrollmentDate");
                _EnrollmentDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EnrollmentDate");
                OnEnrollmentDateChanged();
            }
        }
        private global::System.DateTime _EnrollmentDate;
        partial void OnEnrollmentDateChanging(global::System.DateTime value);
        partial void OnEnrollmentDateChanged();

        #endregion
    
    }

    #endregion
    
}
