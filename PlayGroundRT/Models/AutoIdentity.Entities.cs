
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace AutoIdentity
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string TableId = "TableId";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}


	[DataDefinition]
	public class TableId : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string TableName = "TableName";
			public const string NextId = "NextId";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(IsPrimaryKey = true)]
		public string TableName
		{
			get { return getValue<string>("TableName"); }
			set { setValue<string>("TableName", value); }
		}

		[Data]
		public int NextId
		{
			get { return getValue<int>("NextId"); }
			set { setValue<int>("NextId", value); }
		}

	}

}


  
