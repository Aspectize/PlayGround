
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace AdventureWorks.Person
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string Contact = "Contact";
			public const string Address = "Address";
			public const string StateProvince = "StateProvince";
		}

		public static partial class Relations
		{
			public const string EmployeeContact = "EmployeeContact";
			public const string AddressStateProvince = "AddressStateProvince";
		}

		public static partial class EnumsInBase
		{
			public const string ContactType = "ContactType";
			public const string AddressType = "AddressType";
			public const string CountryRegion = "CountryRegion";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}


	[DataDefinition(PhysicalName = "ContactType", PhysicalSchema = "Person")]
	public class ContactType : DataWrapper, IDataWrapper, IDatabaseEnum
	{
		public static partial class Fields
		{
			public const string ContactTypeID = "ContactTypeID";
			public const string Name = "Name";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(IsPrimaryKey = true, AutoIdentity = true)]
		public int ContactTypeID
		{
			get { return getValue<int>("ContactTypeID"); }
			set { setValue<int>("ContactTypeID", value); }
		}

		[Data]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		public static Type GetUnderlyingType()
		{
			return typeof(Int32);
		}
	}

	[DataDefinition(PhysicalName = "AddressType", PhysicalSchema = "Person")]
	public class AddressType : DataWrapper, IDataWrapper, IDatabaseEnum
	{
		public static partial class Fields
		{
			public const string AddressTypeID = "AddressTypeID";
			public const string Name = "Name";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(IsPrimaryKey = true, AutoIdentity = true)]
		public int AddressTypeID
		{
			get { return getValue<int>("AddressTypeID"); }
			set { setValue<int>("AddressTypeID", value); }
		}

		[Data]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		public static Type GetUnderlyingType()
		{
			return typeof(Int32);
		}
	}

	[DataDefinition(PhysicalName = "CountryRegion", PhysicalSchema = "Person")]
	public class CountryRegion : DataWrapper, IDataWrapper, IDatabaseEnum
	{
		public static partial class Fields
		{
			public const string CountryRegionCode = "CountryRegionCode";
			public const string Name = "Name";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(Size = 3, PhysicalName = "CountryRegionCode", IsPrimaryKey = true)]
		public string CountryRegionCode
		{
			get { return getValue<string>("CountryRegionCode"); }
			set { setValue<string>("CountryRegionCode", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		public static Type GetUnderlyingType()
		{
			return typeof(String);
		}
	}

	[DataDefinition(PhysicalName = "Contact", PhysicalSchema = "Person")]
	public class Contact : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string ContactID = "ContactID";
			public const string NameStyle = "NameStyle";
			public const string Title = "Title";
			public const string FirstName = "FirstName";
			public const string MiddleName = "MiddleName";
			public const string LastName = "LastName";
			public const string Suffix = "Suffix";
			public const string EmailAddress = "EmailAddress";
			public const string EmailPromotion = "EmailPromotion";
			public const string Phone = "Phone";
			public const string PasswordHash = "PasswordHash";
			public const string PasswordSalt = "PasswordSalt";
			public const string AdditionalContactInfo = "AdditionalContactInfo";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "ContactID", IsPrimaryKey = true)]
		public int ContactID
		{
			get { return getValue<int>("ContactID"); }
			set { setValue<int>("ContactID", value); }
		}

		[Data(PhysicalName = "NameStyle")]
		public bool NameStyle
		{
			get { return getValue<bool>("NameStyle"); }
			set { setValue<bool>("NameStyle", value); }
		}

		[Data(Size = 8, IsNullable = true, PhysicalName = "Title")]
		public string Title
		{
			get { return getValue<string>("Title"); }
			set { setValue<string>("Title", value); }
		}

		[Data(Size = 50, PhysicalName = "FirstName")]
		public string FirstName
		{
			get { return getValue<string>("FirstName"); }
			set { setValue<string>("FirstName", value); }
		}

		[Data(Size = 50, IsNullable = true, PhysicalName = "MiddleName")]
		public string MiddleName
		{
			get { return getValue<string>("MiddleName"); }
			set { setValue<string>("MiddleName", value); }
		}

		[Data(Size = 50, PhysicalName = "LastName")]
		public string LastName
		{
			get { return getValue<string>("LastName"); }
			set { setValue<string>("LastName", value); }
		}

		[Data(Size = 10, IsNullable = true, PhysicalName = "Suffix")]
		public string Suffix
		{
			get { return getValue<string>("Suffix"); }
			set { setValue<string>("Suffix", value); }
		}

		[Data(Size = 50, IsNullable = true, PhysicalName = "EmailAddress")]
		public string EmailAddress
		{
			get { return getValue<string>("EmailAddress"); }
			set { setValue<string>("EmailAddress", value); }
		}

		[Data(PhysicalName = "EmailPromotion")]
		public int EmailPromotion
		{
			get { return getValue<int>("EmailPromotion"); }
			set { setValue<int>("EmailPromotion", value); }
		}

		[Data(Size = 25, IsNullable = true, PhysicalName = "Phone")]
		public string Phone
		{
			get { return getValue<string>("Phone"); }
			set { setValue<string>("Phone", value); }
		}

		[Data(Size = 128, PhysicalName = "PasswordHash")]
		public string PasswordHash
		{
			get { return getValue<string>("PasswordHash"); }
			set { setValue<string>("PasswordHash", value); }
		}

		[Data(Size = 10, PhysicalName = "PasswordSalt")]
		public string PasswordSalt
		{
			get { return getValue<string>("PasswordSalt"); }
			set { setValue<string>("PasswordSalt", value); }
		}

		[Data(Size = -1, IsNullable = true, PhysicalName = "AdditionalContactInfo")]
		public string AdditionalContactInfo
		{
			get { return getValue<string>("AdditionalContactInfo"); }
			set { setValue<string>("AdditionalContactInfo", value); }
		}

		[Data(PhysicalName = "rowguid")]
		public Guid rowguid
		{
			get { return getValue<Guid>("rowguid"); }
			set { setValue<Guid>("rowguid", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "Address", PhysicalSchema = "Person")]
	public class Address : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string AddressID = "AddressID";
			public const string AddressLine1 = "AddressLine1";
			public const string AddressLine2 = "AddressLine2";
			public const string City = "City";
			public const string PostalCode = "PostalCode";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "AddressID", IsPrimaryKey = true)]
		public int AddressID
		{
			get { return getValue<int>("AddressID"); }
			set { setValue<int>("AddressID", value); }
		}

		[Data(Size = 60, PhysicalName = "AddressLine1")]
		public string AddressLine1
		{
			get { return getValue<string>("AddressLine1"); }
			set { setValue<string>("AddressLine1", value); }
		}

		[Data(Size = 60, IsNullable = true, PhysicalName = "AddressLine2")]
		public string AddressLine2
		{
			get { return getValue<string>("AddressLine2"); }
			set { setValue<string>("AddressLine2", value); }
		}

		[Data(Size = 30, PhysicalName = "City")]
		public string City
		{
			get { return getValue<string>("City"); }
			set { setValue<string>("City", value); }
		}

		[Data(Size = 15, PhysicalName = "PostalCode")]
		public string PostalCode
		{
			get { return getValue<string>("PostalCode"); }
			set { setValue<string>("PostalCode", value); }
		}

		[Data(PhysicalName = "rowguid")]
		public Guid rowguid
		{
			get { return getValue<Guid>("rowguid"); }
			set { setValue<Guid>("rowguid", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "StateProvince", PhysicalSchema = "Person")]
	public class StateProvince : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string StateProvinceID = "StateProvinceID";
			public const string StateProvinceCode = "StateProvinceCode";
			public const string IsOnlyStateProvinceFlag = "IsOnlyStateProvinceFlag";
			public const string Name = "Name";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
			public const string CountryRegionCode = "CountryRegionCode";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[Data(AutoIdentity = true, PhysicalName = "StateProvinceID", IsPrimaryKey = true)]
		public int StateProvinceID
		{
			get { return getValue<int>("StateProvinceID"); }
			set { setValue<int>("StateProvinceID", value); }
		}

		[Data(Size = 3, PhysicalName = "StateProvinceCode")]
		public string StateProvinceCode
		{
			get { return getValue<string>("StateProvinceCode"); }
			set { setValue<string>("StateProvinceCode", value); }
		}

		[Data(PhysicalName = "IsOnlyStateProvinceFlag")]
		public bool IsOnlyStateProvinceFlag
		{
			get { return getValue<bool>("IsOnlyStateProvinceFlag"); }
			set { setValue<bool>("IsOnlyStateProvinceFlag", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(PhysicalName = "rowguid")]
		public Guid rowguid
		{
			get { return getValue<Guid>("rowguid"); }
			set { setValue<Guid>("rowguid", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

		[Data(DefaultValue = "US")]
		public CountryRegion CountryRegionCode
		{
			get { return getValue<CountryRegion>("CountryRegionCode"); }
			set { setValue<CountryRegion>("CountryRegionCode", value); }
		}

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class EmployeeContact : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Contact), Role = typeof(Contact), Multiplicity = Multiplicity.One, FkNames = "ContactID")]
		public IEntity Contact;

		[RelationEnd(Type = typeof(AdventureWorks.HumanResources.Employee), Role = typeof(AdventureWorks.HumanResources.Employee), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity Employee;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class AddressStateProvince : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(StateProvince), Role = typeof(StateProvince), Multiplicity = Multiplicity.One, FkNames = "StateProvinceID")]
		public IEntity StateProvince;

		[RelationEnd(Type = typeof(Address), Role = typeof(Address), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity Address;

	}

}


  
