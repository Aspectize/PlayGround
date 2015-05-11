
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace AdventureWorks.Sales
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string SalesPerson = "SalesPerson";
			public const string SalesOrderHeader = "SalesOrderHeader";
			public const string Customer = "Customer";
			public const string SalesTerritory = "SalesTerritory";
			public const string CurrencyRate = "CurrencyRate";
			public const string SalesReason = "SalesReason";
			public const string Store = "Store";
			public const string Currency = "Currency";
			public const string Individual = "Individual";
			public const string CreditCard = "CreditCard";
			public const string SalesTaxRate = "SalesTaxRate";
			public const string ShoppingCartItem = "ShoppingCartItem";
			public const string SpecialOffer = "SpecialOffer";
		}

		public static partial class Relations
		{
			public const string SalesOrderHeaderSalesPerson = "SalesOrderHeaderSalesPerson";
			public const string SalesOrderHeaderCustomer = "SalesOrderHeaderCustomer";
			public const string SalesOrderHeaderSalesTerritory = "SalesOrderHeaderSalesTerritory";
			public const string SalesOrderHeaderCurrencyRate = "SalesOrderHeaderCurrencyRate";
			public const string SalesOrderHeaderSalesReason = "SalesOrderHeaderSalesReason";
			public const string StoreContact = "StoreContact";
			public const string StoreCustomer = "StoreCustomer";
			public const string StoreSalesPerson = "StoreSalesPerson";
			public const string SalesPersonSalesTerritory = "SalesPersonSalesTerritory";
			public const string ToCurrencyRate = "ToCurrencyRate";
			public const string CustomerSalesTerritory = "CustomerSalesTerritory";
			public const string IndividualCustomer = "IndividualCustomer";
			public const string FromCurrencyRate = "FromCurrencyRate";
			public const string SalesOrderHeaderCreditCard = "SalesOrderHeaderCreditCard";
			public const string CustomerAddress = "CustomerAddress";
			public const string ContactSalesPerson = "ContactSalesPerson";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}


	public class SalesPersonQuotaHistory : DataWrapper, IDataWrapper, IStructuredData
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, namePrefix);
		}

		[Data(PhysicalName = "QuotaDate", IsAccessKey = true)]
		public DateTime QuotaDate
		{
			get { return getValue<DateTime>("QuotaDate"); }
			set { setValue<DateTime>("QuotaDate", value); }
		}

		[Data(PhysicalName = "SalesQuota")]
		public decimal SalesQuota
		{
			get { return getValue<decimal>("SalesQuota"); }
			set { setValue<decimal>("SalesQuota", value); }
		}

		[Data(PhysicalName = "rowguid")]
		public Guid rowguid
		{
			get { return getValue<Guid>("rowguid"); }
			set { setValue<Guid>("rowguid", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public string ModifiedDate
		{
			get { return getValue<string>("ModifiedDate"); }
			set { setValue<string>("ModifiedDate", value); }
		}
	}

	public class SalesOrderDetail : DataWrapper, IDataWrapper, IStructuredData
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, namePrefix);
		}

		[Data(AutoIdentity = true, IsAccessKey = true)]
		public int SalesOrderDetailID
		{
			get { return getValue<int>("SalesOrderDetailID"); }
			set { setValue<int>("SalesOrderDetailID", value); }
		}

		[Data(Size = 25, IsNullable = true)]
		public string CarrierTrackingNumber
		{
			get { return getValue<string>("CarrierTrackingNumber"); }
			set { setValue<string>("CarrierTrackingNumber", value); }
		}

		[Data]
		public short OrderQty
		{
			get { return getValue<short>("OrderQty"); }
			set { setValue<short>("OrderQty", value); }
		}

		[Data]
		public decimal UnitPrice
		{
			get { return getValue<decimal>("UnitPrice"); }
			set { setValue<decimal>("UnitPrice", value); }
		}

		[Data]
		public decimal UnitPriceDiscount
		{
			get { return getValue<decimal>("UnitPriceDiscount"); }
			set { setValue<decimal>("UnitPriceDiscount", value); }
		}

		[Data]
		public double LineTotal
		{
			get { return getValue<double>("LineTotal"); }
			set { setValue<double>("LineTotal", value); }
		}

		[Data]
		public Guid rowguid
		{
			get { return getValue<Guid>("rowguid"); }
			set { setValue<Guid>("rowguid", value); }
		}

		[Data]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}
	}

	[DataDefinition(PhysicalName = "SalesPerson", PhysicalSchema = "Sales")]
	public class SalesPerson : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string SalesPersonID = "SalesPersonID";
			public const string SalesQuota = "SalesQuota";
			public const string Bonus = "Bonus";
			public const string CommissionPct = "CommissionPct";
			public const string SalesYTD = "SalesYTD";
			public const string SalesLastYear = "SalesLastYear";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
			public const string QuotaHistory = "QuotaHistory";
			public const string MaxSales = "MaxSales";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

			QuotaHistory = new MultiValueField<SalesPersonQuotaHistory>(this, buildNamePrefix("QuotaHistory"));
		}

		[Data(AutoIdentity = true, PhysicalName = "SalesPersonID", IsPrimaryKey = true)]
		public int SalesPersonID
		{
			get { return getValue<int>("SalesPersonID"); }
			set { setValue<int>("SalesPersonID", value); }
		}

		[Data(IsNullable = true, PhysicalName = "SalesQuota")]
		public decimal? SalesQuota
		{
			get { return getValue<decimal?>("SalesQuota"); }
			set { setValue<decimal?>("SalesQuota", value); }
		}

		[Data(PhysicalName = "Bonus")]
		public decimal Bonus
		{
			get { return getValue<decimal>("Bonus"); }
			set { setValue<decimal>("Bonus", value); }
		}

		[Data(PhysicalName = "CommissionPct")]
		public decimal CommissionPct
		{
			get { return getValue<decimal>("CommissionPct"); }
			set { setValue<decimal>("CommissionPct", value); }
		}

		[Data(PhysicalName = "SalesYTD")]
		public decimal SalesYTD
		{
			get { return getValue<decimal>("SalesYTD"); }
			set { setValue<decimal>("SalesYTD", value); }
		}

		[Data(PhysicalName = "SalesLastYear")]
		public decimal SalesLastYear
		{
			get { return getValue<decimal>("SalesLastYear"); }
			set { setValue<decimal>("SalesLastYear", value); }
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

		[Data(PhysicalName = "SalesPersonQuotaHistory", FkNames = "SalesPersonID")]
		public MultiValueField<SalesPersonQuotaHistory> QuotaHistory;

		[Data(MustPersist = false, IsNullable = true, Expression = "Max(Child(SalesPerson_SalesPerson_QuotaHistory).SalesQuota)")]
		public int? MaxSales
		{
			get { return getValue<int?>("MaxSales"); }
		}

	}

	[DataDefinition(PhysicalName = "SalesOrderHeader", PhysicalSchema = "Sales")]
	public class SalesOrderHeader : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string SalesOrderID = "SalesOrderID";
			public const string RevisionNumber = "RevisionNumber";
			public const string OrderDate = "OrderDate";
			public const string DueDate = "DueDate";
			public const string ShipDate = "ShipDate";
			public const string Status = "Status";
			public const string OnlineOrderFlag = "OnlineOrderFlag";
			public const string SalesOrderNumber = "SalesOrderNumber";
			public const string PurchaseOrderNumber = "PurchaseOrderNumber";
			public const string AccountNumber = "AccountNumber";
			public const string CreditCardApprovalCode = "CreditCardApprovalCode";
			public const string SubTotal = "SubTotal";
			public const string TaxAmt = "TaxAmt";
			public const string Freight = "Freight";
			public const string TotalDue = "TotalDue";
			public const string Comment = "Comment";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
			public const string SalesOrderDetails = "SalesOrderDetails";
			public const string NbReason = "NbReason";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

			SalesOrderDetails = new MultiValueField<SalesOrderDetail>(this, buildNamePrefix("SalesOrderDetails"));
		}

		[Data(AutoIdentity = true, PhysicalName = "SalesOrderID", IsPrimaryKey = true)]
		public int SalesOrderID
		{
			get { return getValue<int>("SalesOrderID"); }
			set { setValue<int>("SalesOrderID", value); }
		}

		[Data(PhysicalName = "RevisionNumber")]
		public byte RevisionNumber
		{
			get { return getValue<byte>("RevisionNumber"); }
			set { setValue<byte>("RevisionNumber", value); }
		}

		[Data(PhysicalName = "OrderDate")]
		public DateTime OrderDate
		{
			get { return getValue<DateTime>("OrderDate"); }
			set { setValue<DateTime>("OrderDate", value); }
		}

		[Data(PhysicalName = "DueDate")]
		public DateTime DueDate
		{
			get { return getValue<DateTime>("DueDate"); }
			set { setValue<DateTime>("DueDate", value); }
		}

		[Data(IsNullable = true, PhysicalName = "ShipDate")]
		public DateTime? ShipDate
		{
			get { return getValue<DateTime?>("ShipDate"); }
			set { setValue<DateTime?>("ShipDate", value); }
		}

		[Data(PhysicalName = "Status")]
		public byte Status
		{
			get { return getValue<byte>("Status"); }
			set { setValue<byte>("Status", value); }
		}

		[Data(PhysicalName = "OnlineOrderFlag")]
		public bool OnlineOrderFlag
		{
			get { return getValue<bool>("OnlineOrderFlag"); }
			set { setValue<bool>("OnlineOrderFlag", value); }
		}

		[Data(Size = 25, PhysicalName = "SalesOrderNumber")]
		public string SalesOrderNumber
		{
			get { return getValue<string>("SalesOrderNumber"); }
			set { setValue<string>("SalesOrderNumber", value); }
		}

		[Data(Size = 25, IsNullable = true, PhysicalName = "PurchaseOrderNumber")]
		public string PurchaseOrderNumber
		{
			get { return getValue<string>("PurchaseOrderNumber"); }
			set { setValue<string>("PurchaseOrderNumber", value); }
		}

		[Data(Size = 15, IsNullable = true, PhysicalName = "AccountNumber")]
		public string AccountNumber
		{
			get { return getValue<string>("AccountNumber"); }
			set { setValue<string>("AccountNumber", value); }
		}

		[Data(Size = 15, IsNullable = true, PhysicalName = "CreditCardApprovalCode")]
		public string CreditCardApprovalCode
		{
			get { return getValue<string>("CreditCardApprovalCode"); }
			set { setValue<string>("CreditCardApprovalCode", value); }
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

		[Data(Size = 128, IsNullable = true, PhysicalName = "Comment")]
		public string Comment
		{
			get { return getValue<string>("Comment"); }
			set { setValue<string>("Comment", value); }
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

		[Data(PhysicalName = "SalesOrderDetail", FkNames = "SalesOrderID")]
		public MultiValueField<SalesOrderDetail> SalesOrderDetails;

		[Data(MustPersist = false)]
		public int NbReason
		{
			get { return getValue<int>("NbReason"); }
			set { setValue<int>("NbReason", value); }
		}

	}

	[DataDefinition(PhysicalName = "Customer", PhysicalSchema = "Sales")]
	public class Customer : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string CustomerID = "CustomerID";
			public const string AccountNumber = "AccountNumber";
			public const string CustomerType = "CustomerType";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "CustomerID", IsPrimaryKey = true)]
		public int CustomerID
		{
			get { return getValue<int>("CustomerID"); }
			set { setValue<int>("CustomerID", value); }
		}

		[Data(Size = 10, PhysicalName = "AccountNumber")]
		public string AccountNumber
		{
			get { return getValue<string>("AccountNumber"); }
			set { setValue<string>("AccountNumber", value); }
		}

		[Data(Size = 1, PhysicalName = "CustomerType")]
		public string CustomerType
		{
			get { return getValue<string>("CustomerType"); }
			set { setValue<string>("CustomerType", value); }
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

	[DataDefinition(PhysicalName = "SalesTerritory", PhysicalSchema = "Sales")]
	public class SalesTerritory : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string TerritoryID = "TerritoryID";
			public const string Name = "Name";
			public const string CountryRegionCode = "CountryRegionCode";
			public const string Group = "Group";
			public const string SalesYTD = "SalesYTD";
			public const string SalesLastYear = "SalesLastYear";
			public const string CostYTD = "CostYTD";
			public const string CostLastYear = "CostLastYear";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "TerritoryID", IsPrimaryKey = true)]
		public int TerritoryID
		{
			get { return getValue<int>("TerritoryID"); }
			set { setValue<int>("TerritoryID", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(Size = 3, PhysicalName = "CountryRegionCode")]
		public string CountryRegionCode
		{
			get { return getValue<string>("CountryRegionCode"); }
			set { setValue<string>("CountryRegionCode", value); }
		}

		[Data(Size = 50, PhysicalName = "Group")]
		public string Group
		{
			get { return getValue<string>("Group"); }
			set { setValue<string>("Group", value); }
		}

		[Data(PhysicalName = "SalesYTD")]
		public decimal SalesYTD
		{
			get { return getValue<decimal>("SalesYTD"); }
			set { setValue<decimal>("SalesYTD", value); }
		}

		[Data(PhysicalName = "SalesLastYear")]
		public decimal SalesLastYear
		{
			get { return getValue<decimal>("SalesLastYear"); }
			set { setValue<decimal>("SalesLastYear", value); }
		}

		[Data(PhysicalName = "CostYTD")]
		public decimal CostYTD
		{
			get { return getValue<decimal>("CostYTD"); }
			set { setValue<decimal>("CostYTD", value); }
		}

		[Data(PhysicalName = "CostLastYear")]
		public decimal CostLastYear
		{
			get { return getValue<decimal>("CostLastYear"); }
			set { setValue<decimal>("CostLastYear", value); }
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

	[DataDefinition(PhysicalName = "CurrencyRate", PhysicalSchema = "Sales")]
	public class CurrencyRate : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string CurrencyRateID = "CurrencyRateID";
			public const string CurrencyRateDate = "CurrencyRateDate";
			public const string AverageRate = "AverageRate";
			public const string EndOfDayRate = "EndOfDayRate";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "CurrencyRateID", IsPrimaryKey = true)]
		public int CurrencyRateID
		{
			get { return getValue<int>("CurrencyRateID"); }
			set { setValue<int>("CurrencyRateID", value); }
		}

		[Data(PhysicalName = "CurrencyRateDate")]
		public DateTime CurrencyRateDate
		{
			get { return getValue<DateTime>("CurrencyRateDate"); }
			set { setValue<DateTime>("CurrencyRateDate", value); }
		}

		[Data(PhysicalName = "AverageRate")]
		public decimal AverageRate
		{
			get { return getValue<decimal>("AverageRate"); }
			set { setValue<decimal>("AverageRate", value); }
		}

		[Data(PhysicalName = "EndOfDayRate")]
		public decimal EndOfDayRate
		{
			get { return getValue<decimal>("EndOfDayRate"); }
			set { setValue<decimal>("EndOfDayRate", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "SalesReason", PhysicalSchema = "Sales")]
	public class SalesReason : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string SalesReasonID = "SalesReasonID";
			public const string Name = "Name";
			public const string ReasonType = "ReasonType";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "SalesReasonID", IsPrimaryKey = true)]
		public int SalesReasonID
		{
			get { return getValue<int>("SalesReasonID"); }
			set { setValue<int>("SalesReasonID", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(Size = 50, PhysicalName = "ReasonType")]
		public string ReasonType
		{
			get { return getValue<string>("ReasonType"); }
			set { setValue<string>("ReasonType", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "Store", PhysicalSchema = "Sales")]
	public class Store : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string CustomerID = "CustomerID";
			public const string Name = "Name";
			public const string Demographics = "Demographics";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "CustomerID", IsPrimaryKey = true)]
		public int CustomerID
		{
			get { return getValue<int>("CustomerID"); }
			set { setValue<int>("CustomerID", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(Size = -1, IsNullable = true, PhysicalName = "Demographics")]
		public string Demographics
		{
			get { return getValue<string>("Demographics"); }
			set { setValue<string>("Demographics", value); }
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

	[DataDefinition(PhysicalName = "Currency", PhysicalSchema = "Sales")]
	public class Currency : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string CurrencyCode = "CurrencyCode";
			public const string Name = "Name";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(Size = 3, PhysicalName = "CurrencyCode", IsPrimaryKey = true)]
		public string CurrencyCode
		{
			get { return getValue<string>("CurrencyCode"); }
			set { setValue<string>("CurrencyCode", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "Individual", PhysicalSchema = "Sales")]
	public class Individual : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string CustomerID = "CustomerID";
			public const string Demographics = "Demographics";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "CustomerID", IsPrimaryKey = true)]
		public int CustomerID
		{
			get { return getValue<int>("CustomerID"); }
			set { setValue<int>("CustomerID", value); }
		}

		[Data(Size = -1, IsNullable = true, PhysicalName = "Demographics")]
		public string Demographics
		{
			get { return getValue<string>("Demographics"); }
			set { setValue<string>("Demographics", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "CreditCard", PhysicalSchema = "Sales")]
	public class CreditCard : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string CreditCardID = "CreditCardID";
			public const string CardType = "CardType";
			public const string CardNumber = "CardNumber";
			public const string ExpMonth = "ExpMonth";
			public const string ExpYear = "ExpYear";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "CreditCardID", IsPrimaryKey = true)]
		public int CreditCardID
		{
			get { return getValue<int>("CreditCardID"); }
			set { setValue<int>("CreditCardID", value); }
		}

		[Data(Size = 50, PhysicalName = "CardType")]
		public string CardType
		{
			get { return getValue<string>("CardType"); }
			set { setValue<string>("CardType", value); }
		}

		[Data(Size = 25, PhysicalName = "CardNumber")]
		public string CardNumber
		{
			get { return getValue<string>("CardNumber"); }
			set { setValue<string>("CardNumber", value); }
		}

		[Data(PhysicalName = "ExpMonth")]
		public byte ExpMonth
		{
			get { return getValue<byte>("ExpMonth"); }
			set { setValue<byte>("ExpMonth", value); }
		}

		[Data(PhysicalName = "ExpYear")]
		public short ExpYear
		{
			get { return getValue<short>("ExpYear"); }
			set { setValue<short>("ExpYear", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "SalesTaxRate", PhysicalSchema = "Sales")]
	public class SalesTaxRate : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string SalesTaxRateID = "SalesTaxRateID";
			public const string TaxType = "TaxType";
			public const string TaxRate = "TaxRate";
			public const string Name = "Name";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "SalesTaxRateID", IsPrimaryKey = true)]
		public int SalesTaxRateID
		{
			get { return getValue<int>("SalesTaxRateID"); }
			set { setValue<int>("SalesTaxRateID", value); }
		}

		[Data(PhysicalName = "TaxType")]
		public byte TaxType
		{
			get { return getValue<byte>("TaxType"); }
			set { setValue<byte>("TaxType", value); }
		}

		[Data(PhysicalName = "TaxRate")]
		public decimal TaxRate
		{
			get { return getValue<decimal>("TaxRate"); }
			set { setValue<decimal>("TaxRate", value); }
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

	}

	[DataDefinition(PhysicalName = "ShoppingCartItem", PhysicalSchema = "Sales")]
	public class ShoppingCartItem : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string ShoppingCartItemID = "ShoppingCartItemID";
			public const string ShoppingCartID = "ShoppingCartID";
			public const string Quantity = "Quantity";
			public const string DateCreated = "DateCreated";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "ShoppingCartItemID", IsPrimaryKey = true)]
		public int ShoppingCartItemID
		{
			get { return getValue<int>("ShoppingCartItemID"); }
			set { setValue<int>("ShoppingCartItemID", value); }
		}

		[Data(Size = 50, PhysicalName = "ShoppingCartID")]
		public string ShoppingCartID
		{
			get { return getValue<string>("ShoppingCartID"); }
			set { setValue<string>("ShoppingCartID", value); }
		}

		[Data(PhysicalName = "Quantity")]
		public int Quantity
		{
			get { return getValue<int>("Quantity"); }
			set { setValue<int>("Quantity", value); }
		}

		[Data(PhysicalName = "DateCreated")]
		public DateTime DateCreated
		{
			get { return getValue<DateTime>("DateCreated"); }
			set { setValue<DateTime>("DateCreated", value); }
		}

		[Data(PhysicalName = "ModifiedDate")]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "SpecialOffer", PhysicalSchema = "Sales")]
	public class SpecialOffer : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string SpecialOfferID = "SpecialOfferID";
			public const string Description = "Description";
			public const string DiscountPct = "DiscountPct";
			public const string Type = "Type";
			public const string Category = "Category";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string MinQty = "MinQty";
			public const string MaxQty = "MaxQty";
			public const string rowguid = "rowguid";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "SpecialOfferID", IsPrimaryKey = true)]
		public int SpecialOfferID
		{
			get { return getValue<int>("SpecialOfferID"); }
			set { setValue<int>("SpecialOfferID", value); }
		}

		[Data(Size = 255, PhysicalName = "Description")]
		public string Description
		{
			get { return getValue<string>("Description"); }
			set { setValue<string>("Description", value); }
		}

		[Data(PhysicalName = "DiscountPct")]
		public decimal DiscountPct
		{
			get { return getValue<decimal>("DiscountPct"); }
			set { setValue<decimal>("DiscountPct", value); }
		}

		[Data(Size = 50, PhysicalName = "Type")]
		public string Type
		{
			get { return getValue<string>("Type"); }
			set { setValue<string>("Type", value); }
		}

		[Data(Size = 50, PhysicalName = "Category")]
		public string Category
		{
			get { return getValue<string>("Category"); }
			set { setValue<string>("Category", value); }
		}

		[Data(PhysicalName = "StartDate")]
		public DateTime StartDate
		{
			get { return getValue<DateTime>("StartDate"); }
			set { setValue<DateTime>("StartDate", value); }
		}

		[Data(PhysicalName = "EndDate")]
		public DateTime EndDate
		{
			get { return getValue<DateTime>("EndDate"); }
			set { setValue<DateTime>("EndDate", value); }
		}

		[Data(PhysicalName = "MinQty")]
		public int MinQty
		{
			get { return getValue<int>("MinQty"); }
			set { setValue<int>("MinQty", value); }
		}

		[Data(IsNullable = true, PhysicalName = "MaxQty")]
		public int? MaxQty
		{
			get { return getValue<int?>("MaxQty"); }
			set { setValue<int?>("MaxQty", value); }
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

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class SalesOrderHeaderSalesPerson : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesOrderHeader), Role = typeof(SalesOrderHeader), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity SalesOrderHeader;

		[RelationEnd(Type = typeof(SalesPerson), Role = typeof(SalesPerson), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "SalesPersonID")]
		public IEntity SalesPerson;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class SalesOrderHeaderCustomer : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesOrderHeader), Role = typeof(SalesOrderHeader), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity SalesOrderHeader;

		[RelationEnd(Type = typeof(Customer), Role = typeof(Customer), Multiplicity = Multiplicity.One, FkNames = "CustomerID")]
		public IEntity Customer;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class SalesOrderHeaderSalesTerritory : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesOrderHeader), Role = typeof(SalesOrderHeader), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity SalesOrderHeader;

		[RelationEnd(Type = typeof(SalesTerritory), Role = typeof(SalesTerritory), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "TerritoryID")]
		public IEntity SalesTerritory;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class SalesOrderHeaderCurrencyRate : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesOrderHeader), Role = typeof(SalesOrderHeader), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity SalesOrderHeader;

		[RelationEnd(Type = typeof(CurrencyRate), Role = typeof(CurrencyRate), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "CurrencyRateID")]
		public IEntity CurrencyRate;

	}

	[DataDefinition(PhysicalName = "SalesOrderHeaderSalesReason", PhysicalSchema = "Sales")]
	public class SalesOrderHeaderSalesReason : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesOrderHeader), Role = typeof(SalesOrderHeader), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "SalesOrderID")]
		public IEntity SalesOrderHeader;

		[RelationEnd(Type = typeof(SalesReason), Role = typeof(SalesReason), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "SalesReasonID")]
		public IEntity SalesReason;

	}

	[DataDefinition(PhysicalName = "StoreContact", PhysicalSchema = "Sales")]
	public class StoreContact : DataWrapper, IDataWrapper, IRelation
	{
		public static partial class Fields
		{
			public const string ContactTypeID = "ContactTypeID";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[RelationEnd(Type = typeof(Store), Role = typeof(Store), Multiplicity = Multiplicity.OneOrMany, FkNames = "CustomerID")]
		public IEntity Store;

		[RelationEnd(Type = typeof(AdventureWorks.Person.Contact), Role = typeof(AdventureWorks.Person.Contact), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "ContactID")]
		public IEntity Contact;

		[Data(DefaultValue = 11, PhysicalName = "ContactTypeID")]
		public AdventureWorks.Person.ContactType ContactTypeID
		{
			get { return getValue<AdventureWorks.Person.ContactType>("ContactTypeID"); }
			set { setValue<AdventureWorks.Person.ContactType>("ContactTypeID", value); }
		}

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class StoreCustomer : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Store), Role = typeof(Store), Multiplicity = Multiplicity.ZeroOrOne)]
		public IEntity Store;

		[RelationEnd(Type = typeof(Customer), Role = typeof(Customer), Multiplicity = Multiplicity.One, FkNames = "CustomerID")]
		public IEntity Customer;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class StoreSalesPerson : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Store), Role = typeof(Store), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity Store;

		[RelationEnd(Type = typeof(SalesPerson), Role = typeof(SalesPerson), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "SalesPersonID")]
		public IEntity SalesPerson;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class SalesPersonSalesTerritory : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesPerson), Role = typeof(SalesPerson), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity SalesPerson;

		[RelationEnd(Type = typeof(SalesTerritory), Role = typeof(SalesTerritory), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "TerritoryID")]
		public IEntity SalesTerritory;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class ToCurrencyRate : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(CurrencyRate), Role = typeof(CurrencyRate), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity CurrencyRate;

		[RelationEnd(Type = typeof(Currency), Role = typeof(Currency), Multiplicity = Multiplicity.One, FkNames = "ToCurrencyCode")]
		public IEntity Currency;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class CustomerSalesTerritory : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Customer), Role = typeof(Customer), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity Customer;

		[RelationEnd(Type = typeof(SalesTerritory), Role = typeof(SalesTerritory), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "TerritoryID")]
		public IEntity SalesTerritory;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class IndividualCustomer : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Individual), Role = typeof(Individual), Multiplicity = Multiplicity.ZeroOrOne)]
		public IEntity Individual;

		[RelationEnd(Type = typeof(Customer), Role = typeof(Customer), Multiplicity = Multiplicity.One, FkNames = "CustomerID")]
		public IEntity Customer;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class FromCurrencyRate : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(CurrencyRate), Role = typeof(CurrencyRate), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity CurrencyRate;

		[RelationEnd(Type = typeof(Currency), Role = typeof(Currency), Multiplicity = Multiplicity.One, FkNames = "FromCurrencyCode")]
		public IEntity Currency;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class SalesOrderHeaderCreditCard : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesOrderHeader), Role = typeof(SalesOrderHeader), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity SalesOrderHeader;

		[RelationEnd(Type = typeof(CreditCard), Role = typeof(CreditCard), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "CreditCardID")]
		public IEntity CreditCard;

	}

	[DataDefinition(PhysicalName = "CustomerAddress", PhysicalSchema = "Sales")]
	public class CustomerAddress : DataWrapper, IDataWrapper, IRelation
	{
		public static partial class Fields
		{
			public const string AddressType = "AddressType";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[RelationEnd(Type = typeof(Customer), Role = typeof(Customer), Multiplicity = Multiplicity.One, FkNames = "CustomerID")]
		public IEntity Customer;

		[RelationEnd(Type = typeof(AdventureWorks.Person.Address), Role = typeof(AdventureWorks.Person.Address), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "AddressID")]
		public IEntity Address;

		[Data(DefaultValue = 1, PhysicalName = "AddressTypeID")]
		public AdventureWorks.Person.AddressType AddressType
		{
			get { return getValue<AdventureWorks.Person.AddressType>("AddressType"); }
			set { setValue<AdventureWorks.Person.AddressType>("AddressType", value); }
		}

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class ContactSalesPerson : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(SalesPerson), Role = typeof(SalesPerson), Multiplicity = Multiplicity.ZeroOrOne)]
		public IEntity SalesPerson;

		[RelationEnd(Type = typeof(AdventureWorks.Person.Contact), Role = typeof(AdventureWorks.Person.Contact), Multiplicity = Multiplicity.One, FkNames = "SalesPersonID")]
		public IEntity Contact;

	}

}


  
