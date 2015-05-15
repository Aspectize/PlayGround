
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace PlayGround
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string Session = "Session";
			public const string NextId = "NextId";
		}

		public static partial class EnumsInBase
		{
			public const string EnumInBaseSample = "EnumInBaseSample";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}


	[DataDefinition]
	public class EnumInBaseSample : DataWrapper, IDataWrapper, IDatabaseEnum
	{
		public static partial class Fields
		{
			public const string Id = "Id";
			public const string Name = "Name";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(IsPrimaryKey = true, DefaultValue = "")]
		public string Id
		{
			get { return getValue<string>("Id"); }
			set { setValue<string>("Id", value); }
		}

		[Data(DefaultValue = "")]
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

	[DataDefinition]
	public class Session : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string Id = "Id";
			public const string CirculatingId = "CirculatingId";
			public const string Html = "Html";
			public const string css = "css";
			public const string js = "js";
			public const string Bindings = "Bindings";
			public const string Persist = "Persist";
			public const string HtmlControlInfoJSON = "HtmlControlInfoJSON";
			public const string JSView = "JSView";
			public const string JSLibrary = "JSLibrary";
			public const string Log = "Log";
			public const string DisplayCSS = "DisplayCSS";
			public const string DisplayHTML = "DisplayHTML";
			public const string DisplayJS = "DisplayJS";
			public const string DisplayBindings = "DisplayBindings";
			public const string HtmlWidth = "HtmlWidth";
			public const string CSSWidth = "CSSWidth";
			public const string JsWidth = "JsWidth";
			public const string BindingsHeight = "BindingsHeight";
			public const string TopPanelHeight = "TopPanelHeight";
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

		[Data(DefaultValue = "", MustPersist = false)]
		public string CirculatingId
		{
			get { return getValue<string>("CirculatingId"); }
			set { setValue<string>("CirculatingId", value); }
		}

		[Data(DefaultValue = "")]
		public string Html
		{
			get { return getValue<string>("Html"); }
			set { setValue<string>("Html", value); }
		}

		[Data(DefaultValue = "")]
		public string css
		{
			get { return getValue<string>("css"); }
			set { setValue<string>("css", value); }
		}

		[Data(DefaultValue = "")]
		public string js
		{
			get { return getValue<string>("js"); }
			set { setValue<string>("js", value); }
		}

		[Data(DefaultValue = "")]
		public string Bindings
		{
			get { return getValue<string>("Bindings"); }
			set { setValue<string>("Bindings", value); }
		}

		[Data(DefaultValue = false)]
		public bool Persist
		{
			get { return getValue<bool>("Persist"); }
			set { setValue<bool>("Persist", value); }
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

		[Data(DefaultValue = "")]
		public string Log
		{
			get { return getValue<string>("Log"); }
			set { setValue<string>("Log", value); }
		}

		[Data(DefaultValue = true)]
		public bool DisplayCSS
		{
			get { return getValue<bool>("DisplayCSS"); }
			set { setValue<bool>("DisplayCSS", value); }
		}

		[Data(DefaultValue = true)]
		public bool DisplayHTML
		{
			get { return getValue<bool>("DisplayHTML"); }
			set { setValue<bool>("DisplayHTML", value); }
		}

		[Data(DefaultValue = true)]
		public bool DisplayJS
		{
			get { return getValue<bool>("DisplayJS"); }
			set { setValue<bool>("DisplayJS", value); }
		}

		[Data(DefaultValue = true)]
		public bool DisplayBindings
		{
			get { return getValue<bool>("DisplayBindings"); }
			set { setValue<bool>("DisplayBindings", value); }
		}

		[Data]
		public decimal HtmlWidth
		{
			get { return getValue<decimal>("HtmlWidth"); }
			set { setValue<decimal>("HtmlWidth", value); }
		}

		[Data]
		public decimal CSSWidth
		{
			get { return getValue<decimal>("CSSWidth"); }
			set { setValue<decimal>("CSSWidth", value); }
		}

		[Data]
		public decimal JsWidth
		{
			get { return getValue<decimal>("JsWidth"); }
			set { setValue<decimal>("JsWidth", value); }
		}

		[Data]
		public decimal BindingsHeight
		{
			get { return getValue<decimal>("BindingsHeight"); }
			set { setValue<decimal>("BindingsHeight", value); }
		}

		[Data]
		public decimal TopPanelHeight
		{
			get { return getValue<decimal>("TopPanelHeight"); }
			set { setValue<decimal>("TopPanelHeight", value); }
		}

	}

	[DataDefinition]
	public class NextId : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string Id = "Id";
			public const string Version = "Version";
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

		[Data]
		public int Version
		{
			get { return getValue<int>("Version"); }
			set { setValue<int>("Version", value); }
		}

	}

}


  
