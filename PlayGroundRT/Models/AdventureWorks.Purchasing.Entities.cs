
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace AdventureWorks.Purchasing
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string Vendor = "Vendor";
			public const string PurchaseOrderHeader = "PurchaseOrderHeader";
			public const string ProductVendor = "ProductVendor";
			public const string ShipMethod = "ShipMethod";
		}

		public static partial class Relations
		{
			public const string VendorAddress = "VendorAddress";
			public const string VendorContact = "VendorContact";
			public const string PurchaseOrderHeaderVendor = "PurchaseOrderHeaderVendor";
			public const string ProductVendorVendor = "ProductVendorVendor";
			public const string PurchaseOrderHeaderShipMethod = "PurchaseOrderHeaderShipMethod";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}


	[DataDefinition(PhysicalName = "Vendor", PhysicalSchema = "Purchasing")]
	public class Vendor : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string VendorID = "VendorID";
			public const string AccountNumber = "AccountNumber";
			public const string Name = "Name";
			public const string CreditRating = "CreditRating";
			public const string PreferredVendorStatus = "PreferredVendorStatus";
			public const string ActiveFlag = "ActiveFlag";
			public const string PurchasingWebServiceURL = "PurchasingWebServiceURL";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(PhysicalName = "VendorID", IsPrimaryKey = true)]
		public int VendorID
		{
			get { return getValue<int>("VendorID"); }
			set { setValue<int>("VendorID", value); }
		}

		[Data(Size = 15, PhysicalName = "AccountNumber")]
		public string AccountNumber
		{
			get { return getValue<string>("AccountNumber"); }
			set { setValue<string>("AccountNumber", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(PhysicalName = "CreditRating")]
		public byte CreditRating
		{
			get { return getValue<byte>("CreditRating"); }
			set { setValue<byte>("CreditRating", value); }
		}

		[Data(PhysicalName = "PreferredVendorStatus")]
		public bool PreferredVendorStatus
		{
			get { return getValue<bool>("PreferredVendorStatus"); }
			set { setValue<bool>("PreferredVendorStatus", value); }
		}

		[Data(PhysicalName = "ActiveFlag")]
		public bool ActiveFlag
		{
			get { return getValue<bool>("ActiveFlag"); }
			set { setValue<bool>("ActiveFlag", value); }
		}

		[Data(Size = 1024, IsNullable = true, PhysicalName = "PurchasingWebServiceURL")]
		public string PurchasingWebServiceURL
		{
			get { return getValue<string>("PurchasingWebServiceURL"); }
			set { setValue<string>("PurchasingWebServiceURL", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "PurchaseOrderHeader", PhysicalSchema = "Purchasing")]
	public class PurchaseOrderHeader : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string PurchaseOrderID = "PurchaseOrderID";
			public const string RevisionNumber = "RevisionNumber";
			public const string Status = "Status";
			public const string OrderDate = "OrderDate";
			public const string ShipDate = "ShipDate";
			public const string SubTotal = "SubTotal";
			public const string TaxAmt = "TaxAmt";
			public const string Freight = "Freight";
			public const string TotalDue = "TotalDue";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(PhysicalName = "PurchaseOrderID", IsPrimaryKey = true)]
		public int PurchaseOrderID
		{
			get { return getValue<int>("PurchaseOrderID"); }
			set { setValue<int>("PurchaseOrderID", value); }
		}

		[Data(PhysicalName = "RevisionNumber")]
		public byte RevisionNumber
		{
			get { return getValue<byte>("RevisionNumber"); }
			set { setValue<byte>("RevisionNumber", value); }
		}

		[Data(PhysicalName = "Status")]
		public byte Status
		{
			get { return getValue<byte>("Status"); }
			set { setValue<byte>("Status", value); }
		}

		[Data(PhysicalName = "OrderDate")]
		public DateTime OrderDate
		{
			get { return getValue<DateTime>("OrderDate"); }
			set { setValue<DateTime>("OrderDate", value); }
		}

		[Data(IsNullable = true, PhysicalName = "ShipDate")]
		public DateTime? ShipDate
		{
			get { return getValue<DateTime?>("ShipDate"); }
			set { setValue<DateTime?>("ShipDate", value); }
		}

		[Data(PhysicalName = "SubTotal")]
		public decimal SubTotal
		{
			get { return getValue<decimal>("SubTotal"); }
			set { setValue<decimal>("SubTotal", value); }
		}

		[Data(PhysicalName = "TaxAmt")]
		public decimal TaxAmt
		{
			get { return getValue<decimal>("TaxAmt"); }
			set { setValue<decimal>("TaxAmt", value); }
		}

		[Data(PhysicalName = "Freight")]
		public decimal Freight
		{
			get { return getValue<decimal>("Freight"); }
			set { setValue<decimal>("Freight", value); }
		}

		[Data(PhysicalName = "TotalDue")]
		public decimal TotalDue
		{
			get { return getValue<decimal>("TotalDue"); }
			set { setValue<decimal>("TotalDue", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "ProductVendor", PhysicalSchema = "Purchasing")]
	public class ProductVendor : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string ProductID = "ProductID";
			public const string VendorID = "VendorID";
			public const string AverageLeadTime = "AverageLeadTime";
			public const string StandardPrice = "StandardPrice";
			public const string LastReceiptCost = "LastReceiptCost";
			public const string LastReceiptDate = "LastReceiptDate";
			public const string MinOrderQty = "MinOrderQty";
			public const string MaxOrderQty = "MaxOrderQty";
			public const string OnOrderQty = "OnOrderQty";
			public const string ModifiedDate = "ModifiedDate";
			public const string UnitMeasureCode = "UnitMeasureCode";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[Data(PhysicalName = "ProductID", IsPrimaryKey = true)]
		public int ProductID
		{
			get { return getValue<int>("ProductID"); }
			set { setValue<int>("ProductID", value); }
		}

		[Data(PhysicalName = "VendorID", IsPrimaryKey = true)]
		public int VendorID
		{
			get { return getValue<int>("VendorID"); }
			set { setValue<int>("VendorID", value); }
		}

		[Data(PhysicalName = "AverageLeadTime")]
		public int AverageLeadTime
		{
			get { return getValue<int>("AverageLeadTime"); }
			set { setValue<int>("AverageLeadTime", value); }
		}

		[Data(PhysicalName = "StandardPrice")]
		public decimal StandardPrice
		{
			get { return getValue<decimal>("StandardPrice"); }
			set { setValue<decimal>("StandardPrice", value); }
		}

		[Data(IsNullable = true, PhysicalName = "LastReceiptCost")]
		public decimal? LastReceiptCost
		{
			get { return getValue<decimal?>("LastReceiptCost"); }
			set { setValue<decimal?>("LastReceiptCost", value); }
		}

		[Data(IsNullable = true, PhysicalName = "LastReceiptDate")]
		public DateTime? LastReceiptDate
		{
			get { return getValue<DateTime?>("LastReceiptDate"); }
			set { setValue<DateTime?>("LastReceiptDate", value); }
		}

		[Data(PhysicalName = "MinOrderQty")]
		public int MinOrderQty
		{
			get { return getValue<int>("MinOrderQty"); }
			set { setValue<int>("MinOrderQty", value); }
		}

		[Data(PhysicalName = "MaxOrderQty")]
		public int MaxOrderQty
		{
			get { return getValue<int>("MaxOrderQty"); }
			set { setValue<int>("MaxOrderQty", value); }
		}

		[Data(IsNullable = true, PhysicalName = "OnOrderQty")]
		public int? OnOrderQty
		{
			get { return getValue<int?>("OnOrderQty"); }
			set { setValue<int?>("OnOrderQty", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

		[Data(DefaultValue = "CTN")]
		public AdventureWorks.Production.UnitMeasure UnitMeasureCode
		{
			get { return getValue<AdventureWorks.Production.UnitMeasure>("UnitMeasureCode"); }
			set { setValue<AdventureWorks.Production.UnitMeasure>("UnitMeasureCode", value); }
		}

	}

	[DataDefinition(PhysicalName = "ShipMethod", PhysicalSchema = "Purchasing")]
	public class ShipMethod : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string ShipMethodID = "ShipMethodID";
			public const string Name = "Name";
			public const string ShipBase = "ShipBase";
			public const string ShipRate = "ShipRate";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(PhysicalName = "ShipMethodID", IsPrimaryKey = true)]
		public int ShipMethodID
		{
			get { return getValue<int>("ShipMethodID"); }
			set { setValue<int>("ShipMethodID", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(PhysicalName = "ShipBase")]
		public decimal ShipBase
		{
			get { return getValue<decimal>("ShipBase"); }
			set { setValue<decimal>("ShipBase", value); }
		}

		[Data(PhysicalName = "ShipRate")]
		public decimal ShipRate
		{
			get { return getValue<decimal>("ShipRate"); }
			set { setValue<decimal>("ShipRate", value); }
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

	[DataDefinition(PhysicalName = "VendorAddress", PhysicalSchema = "Purchasing")]
	public class VendorAddress : DataWrapper, IDataWrapper, IRelation
	{
		public static partial class Fields
		{
			public const string AddressType = "AddressType";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[RelationEnd(Type = typeof(Vendor), Role = typeof(Vendor), Multiplicity = Multiplicity.One, FkNames = "VendorID")]
		public IEntity Vendor;

		[RelationEnd(Type = typeof(AdventureWorks.Person.Address), Role = typeof(AdventureWorks.Person.Address), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "AddressID")]
		public IEntity Address;

		[Data(DefaultValue = 1, PhysicalName = "AddressTypeID")]
		public AdventureWorks.Person.AddressType AddressType
		{
			get { return getValue<AdventureWorks.Person.AddressType>("AddressType"); }
			set { setValue<AdventureWorks.Person.AddressType>("AddressType", value); }
		}

	}

	[DataDefinition(PhysicalName = "VendorContact", PhysicalSchema = "Purchasing")]
	public class VendorContact : DataWrapper, IDataWrapper, IRelation
	{
		public static partial class Fields
		{
			public const string ContactType = "ContactType";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[RelationEnd(Type = typeof(Vendor), Role = typeof(Vendor), Multiplicity = Multiplicity.One, FkNames = "VendorID")]
		public IEntity Vendor;

		[RelationEnd(Type = typeof(AdventureWorks.Person.Contact), Role = typeof(AdventureWorks.Person.Contact), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "ContactID")]
		public IEntity Contact;

		[Data(DefaultValue = 19, PhysicalName = "ContactTypeID")]
		public AdventureWorks.Person.ContactType ContactType
		{
			get { return getValue<AdventureWorks.Person.ContactType>("ContactType"); }
			set { setValue<AdventureWorks.Person.ContactType>("ContactType", value); }
		}

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class PurchaseOrderHeaderVendor : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(PurchaseOrderHeader), Role = typeof(PurchaseOrderHeader), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity PurchaseOrderHeader;

		[RelationEnd(Type = typeof(Vendor), Role = typeof(Vendor), Multiplicity = Multiplicity.One, FkNames = "VendorID")]
		public IEntity Vendor;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class ProductVendorVendor : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(ProductVendor), Role = typeof(ProductVendor), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity ProductVendor;

		[RelationEnd(Type = typeof(Vendor), Role = typeof(Vendor), Multiplicity = Multiplicity.One, FkNames = "VendorID")]
		public IEntity Vendor;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class PurchaseOrderHeaderShipMethod : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(PurchaseOrderHeader), Role = typeof(PurchaseOrderHeader), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity PurchaseOrderHeader;

		[RelationEnd(Type = typeof(ShipMethod), Role = typeof(ShipMethod), Multiplicity = Multiplicity.One, FkNames = "ShipMethodID")]
		public IEntity ShipMethod;

	}

}


  
