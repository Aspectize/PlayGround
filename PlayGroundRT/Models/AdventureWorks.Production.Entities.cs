
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace AdventureWorks.Production
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string Product = "Product";
			public const string Subcategory = "Subcategory";
			public const string Category = "Category";
			public const string ProductPhoto = "ProductPhoto";
		}

		public static partial class Relations
		{
			public const string CategorySubcategory = "CategorySubcategory";
			public const string ProductSubcategory = "ProductSubcategory";
			public const string ProductProductPhoto = "ProductProductPhoto";
		}

		public static partial class EnumsInBase
		{
			public const string UnitMeasure = "UnitMeasure";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}


	[DataDefinition(MustPersist = false)]
	public enum EnumGender
	{
		[Description("H")]
		H,
		[Description("L")]
		L,
		[Description("M")]
		M
	}

	[DataDefinition(PhysicalName = "UnitMeasure", PhysicalSchema = "Production")]
	public class UnitMeasure : DataWrapper, IDataWrapper, IDatabaseEnum
	{
		public static partial class Fields
		{
			public const string UnitMeasureCode = "UnitMeasureCode";
			public const string Name = "Name";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(Size = 3, IsPrimaryKey = true)]
		public string UnitMeasureCode
		{
			get { return getValue<string>("UnitMeasureCode"); }
			set { setValue<string>("UnitMeasureCode", value); }
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

	[DataDefinition(PhysicalName = "Product", PhysicalSchema = "Production")]
	public class Product : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string ProductID = "ProductID";
			public const string Name = "Name";
			public const string ProductNumber = "ProductNumber";
			public const string SafetyStockLevel = "SafetyStockLevel";
			public const string Size = "Size";
			public const string SizeUnitMeasureCode = "SizeUnitMeasureCode";
			public const string SellStartDate = "SellStartDate";
			public const string SellEndDate = "SellEndDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[Data(AutoIdentity = true, PhysicalName = "ProductID", IsPrimaryKey = true)]
		public int ProductID
		{
			get { return getValue<int>("ProductID"); }
			set { setValue<int>("ProductID", value); }
		}

		[Data(Size = 50, DefaultValue = "", PhysicalName = "Name", Unique = true)]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(Size = 25, PhysicalName = "ProductNumber", RegexPattern = @"(?#Invalid ProductNumber)^\s*\S.*$")]
		public string ProductNumber
		{
			get { return getValue<string>("ProductNumber"); }
			set { setValue<string>("ProductNumber", value); }
		}

		[Data(PhysicalName = "SafetyStockLevel")]
		public short SafetyStockLevel
		{
			get { return getValue<short>("SafetyStockLevel"); }
			set { setValue<short>("SafetyStockLevel", value); }
		}

		[Data(Size = 5, IsNullable = true, PhysicalName = "Size")]
		public string Size
		{
			get { return getValue<string>("Size"); }
			set { setValue<string>("Size", value); }
		}

		[Data(IsNullable = true, PhysicalName = "SizeUnitMeasureCode")]
		public UnitMeasure SizeUnitMeasureCode
		{
			get { return getValue<UnitMeasure>("SizeUnitMeasureCode"); }
			set { setValue<UnitMeasure>("SizeUnitMeasureCode", value); }
		}

		[Data(PhysicalName = "SellStartDate")]
		public DateTime SellStartDate
		{
			get { return getValue<DateTime>("SellStartDate"); }
			set { setValue<DateTime>("SellStartDate", value); }
		}

		[Data(IsNullable = true, PhysicalName = "SellEndDate")]
		public DateTime? SellEndDate
		{
			get { return getValue<DateTime?>("SellEndDate"); }
			set { setValue<DateTime?>("SellEndDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "ProductSubcategory", PhysicalSchema = "Production")]
	public class Subcategory : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string SubcategoryID = "SubcategoryID";
			public const string Name = "Name";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "ProductSubcategoryID", IsPrimaryKey = true)]
		public int SubcategoryID
		{
			get { return getValue<int>("SubcategoryID"); }
			set { setValue<int>("SubcategoryID", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

	}

	[DataDefinition(PhysicalName = "ProductCategory", PhysicalSchema = "Production")]
	public class Category : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string CategoryID = "CategoryID";
			public const string Name = "Name";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "ProductCategoryID", IsPrimaryKey = true)]
		public int CategoryID
		{
			get { return getValue<int>("CategoryID"); }
			set { setValue<int>("CategoryID", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

	}

	[DataDefinition(PhysicalName = "ProductPhoto", PhysicalSchema = "Production")]
	public class ProductPhoto : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string ProductPhotoID = "ProductPhotoID";
			public const string ThumbNailPhoto = "ThumbNailPhoto";
			public const string ThumbnailPhotoFileName = "ThumbnailPhotoFileName";
			public const string LargePhoto = "LargePhoto";
			public const string LargePhotoFileName = "LargePhotoFileName";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "ProductPhotoID", IsPrimaryKey = true)]
		public int ProductPhotoID
		{
			get { return getValue<int>("ProductPhotoID"); }
			set { setValue<int>("ProductPhotoID", value); }
		}

		[Data(Size = -1, PhysicalName = "ThumbNailPhoto")]
		public byte[] ThumbNailPhoto
		{
			get { return getValue<byte[]>("ThumbNailPhoto"); }
			set { setValue<byte[]>("ThumbNailPhoto", value); }
		}

		[Data(Size = 50, IsNullable = true, PhysicalName = "ThumbnailPhotoFileName")]
		public string ThumbnailPhotoFileName
		{
			get { return getValue<string>("ThumbnailPhotoFileName"); }
			set { setValue<string>("ThumbnailPhotoFileName", value); }
		}

		[Data(Size = -1, PhysicalName = "LargePhoto")]
		public byte[] LargePhoto
		{
			get { return getValue<byte[]>("LargePhoto"); }
			set { setValue<byte[]>("LargePhoto", value); }
		}

		[Data(Size = 50, IsNullable = true, PhysicalName = "LargePhotoFileName")]
		public string LargePhotoFileName
		{
			get { return getValue<string>("LargePhotoFileName"); }
			set { setValue<string>("LargePhotoFileName", value); }
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
	public class CategorySubcategory : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Subcategory), Role = typeof(Subcategory), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity Subcategory;

		[RelationEnd(Type = typeof(Category), Role = typeof(Category), Multiplicity = Multiplicity.One, FkNames = "ProductCategoryID", Container = true)]
		public IEntity Category;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class ProductSubcategory : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Product), Role = typeof(Product), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity Product;

		[RelationEnd(Type = typeof(Subcategory), Role = typeof(Subcategory), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "ProductSubcategoryID", Container = true)]
		public IEntity Subcategory;

	}

	[DataDefinition(PhysicalName = "ProductProductPhoto", PhysicalSchema = "Production")]
	public class ProductProductPhoto : DataWrapper, IDataWrapper, IRelation
	{
		public static partial class Fields
		{
			public const string Primary = "Primary";
			public const string ModifiedDate = "ModifiedDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Product), Role = typeof(Product), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "ProductID")]
		public IEntity Product;

		[RelationEnd(Type = typeof(ProductPhoto), Role = typeof(ProductPhoto), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "ProductPhotoID")]
		public IEntity ProductPhoto;

		[Data]
		public bool Primary
		{
			get { return getValue<bool>("Primary"); }
			set { setValue<bool>("Primary", value); }
		}

		[Data]
		public DateTime ModifiedDate
		{
			get { return getValue<DateTime>("ModifiedDate"); }
			set { setValue<DateTime>("ModifiedDate", value); }
		}

	}

}


  
