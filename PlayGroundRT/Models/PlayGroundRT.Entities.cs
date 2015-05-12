
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace PLayGroundRT
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string Session = "Session";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}


	[DataDefinition]
	public class Session : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string Id = "Id";
			public const string HtmlControlInfoJSON = "HtmlControlInfoJSON";
			public const string JSView = "JSView";
			public const string JSLibrary = "JSLibrary";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(IsPrimaryKey = true)]
		public string Id
		{
			get { return getValue<string>("Id"); }
			set { setValue<string>("Id", value); }
		}

		[Data(DefaultValue = "")]
		public string HtmlControlInfoJSON
		{
			get { return getValue<string>("HtmlControlInfoJSON"); }
			set { setValue<string>("HtmlControlInfoJSON", value); }
		}

		[Data(DefaultValue = "")]
		public string JSView
		{
			get { return getValue<string>("JSView"); }
			set { setValue<string>("JSView", value); }
		}

		[Data(DefaultValue = "")]
		public string JSLibrary
		{
			get { return getValue<string>("JSLibrary"); }
			set { setValue<string>("JSLibrary", value); }
		}

	}

}


  
