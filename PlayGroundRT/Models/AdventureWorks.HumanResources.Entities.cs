
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

using Aspectize.Core;

[assembly:AspectizeDALAssemblyAttribute]

namespace AdventureWorks.HumanResources
{
	public static partial class SchemaNames
	{
		public static partial class Entities
		{
			public const string Employee = "Employee";
			public const string Department = "Department";
			public const string JobCandidate = "JobCandidate";
			public const string EmployeeStat = "EmployeeStat";
		}

		public static partial class Relations
		{
			public const string EmployeeManager = "EmployeeManager";
			public const string JobCandidateEmployee = "JobCandidateEmployee";
			public const string EmployeeDepartment = "EmployeeDepartment";
			public const string EmployeeAddress = "EmployeeAddress";
		}

		public static partial class EnumsInBase
		{
			public const string Shift = "Shift";
		}
	}

	[SchemaNamespace]
	public class DomainProvider : INamespace
	{
		public string Name { get { return GetType().Namespace; } }
		public static string DomainName { get { return new DomainProvider().Name; } }
	}

	namespace Roles
	{
		public interface Manager { }
	}

	[DataDefinition(PhysicalName = "Shift", PhysicalSchema = "HumanResources")]
	public class Shift : DataWrapper, IDataWrapper, IDatabaseEnum
	{
		public static partial class Fields
		{
			public const string ShiftID = "ShiftID";
			public const string Name = "Name";
			public const string StartTime = "StartTime";
			public const string EndTime = "EndTime";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(IsPrimaryKey = true, AutoIdentity = true)]
		public byte ShiftID
		{
			get { return getValue<byte>("ShiftID"); }
			set { setValue<byte>("ShiftID", value); }
		}

		[Data(Size = 50)]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data]
		public DateTime StartTime
		{
			get { return getValue<DateTime>("StartTime"); }
			set { setValue<DateTime>("StartTime", value); }
		}

		[Data]
		public DateTime EndTime
		{
			get { return getValue<DateTime>("EndTime"); }
			set { setValue<DateTime>("EndTime", value); }
		}

		public static Type GetUnderlyingType()
		{
			return typeof(Byte);
		}
	}

	public class EmployeePayHistory : DataWrapper, IDataWrapper, IStructuredData
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, namePrefix);
		}

		[Data(IsAccessKey = true)]
		public DateTime RateChangeDate
		{
			get { return getValue<DateTime>("RateChangeDate"); }
			set { setValue<DateTime>("RateChangeDate", value); }
		}

		[Data]
		public decimal Rate
		{
			get { return getValue<decimal>("Rate"); }
			set { setValue<decimal>("Rate", value); }
		}

		[Data]
		public byte PayFrequency
		{
			get { return getValue<byte>("PayFrequency"); }
			set { setValue<byte>("PayFrequency", value); }
		}
	}

	[DataDefinition(PhysicalName = "Employee", PhysicalSchema = "HumanResources")]
	public class Employee : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string EmployeeID = "EmployeeID";
			public const string NationalIDNumber = "NationalIDNumber";
			public const string LoginID = "LoginID";
			public const string Title = "Title";
			public const string BirthDate = "BirthDate";
			public const string MaritalStatus = "MaritalStatus";
			public const string Gender = "Gender";
			public const string HireDate = "HireDate";
			public const string SalariedFlag = "SalariedFlag";
			public const string VacationHours = "VacationHours";
			public const string SickLeaveHours = "SickLeaveHours";
			public const string CurrentFlag = "CurrentFlag";
			public const string PayHistory = "PayHistory";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

			PayHistory = new MultiValueField<EmployeePayHistory>(this, buildNamePrefix("PayHistory"));
		}

		[Data(AutoIdentity = true, PhysicalName = "EmployeeID", IsPrimaryKey = true)]
		public int EmployeeID
		{
			get { return getValue<int>("EmployeeID"); }
			set { setValue<int>("EmployeeID", value); }
		}

		[Data(Size = 15, IsNullable = true, PhysicalName = "NationalIDNumber")]
		public string NationalIDNumber
		{
			get { return getValue<string>("NationalIDNumber"); }
			set { setValue<string>("NationalIDNumber", value); }
		}

		[Data(Size = 256, PhysicalName = "LoginID")]
		public string LoginID
		{
			get { return getValue<string>("LoginID"); }
			set { setValue<string>("LoginID", value); }
		}

		[Data(Size = 50, PhysicalName = "Title")]
		public string Title
		{
			get { return getValue<string>("Title"); }
			set { setValue<string>("Title", value); }
		}

		[Data(PhysicalName = "BirthDate")]
		public Date BirthDate
		{
			get { return getValue<Date>("BirthDate"); }
			set { setValue<Date>("BirthDate", value); }
		}

		[Data(Size = 1, IsNullable = true, PhysicalName = "MaritalStatus")]
		public string MaritalStatus
		{
			get { return getValue<string>("MaritalStatus"); }
			set { setValue<string>("MaritalStatus", value); }
		}

		[Data(Size = 1, IsNullable = true, PhysicalName = "Gender")]
		public string Gender
		{
			get { return getValue<string>("Gender"); }
			set { setValue<string>("Gender", value); }
		}

		[Data(IsNullable = true, PhysicalName = "HireDate")]
		public DateTime? HireDate
		{
			get { return getValue<DateTime?>("HireDate"); }
			set { setValue<DateTime?>("HireDate", value); }
		}

		[Data(IsNullable = true, PhysicalName = "SalariedFlag")]
		public bool? SalariedFlag
		{
			get { return getValue<bool?>("SalariedFlag"); }
			set { setValue<bool?>("SalariedFlag", value); }
		}

		[Data(IsNullable = true, PhysicalName = "VacationHours")]
		public short? VacationHours
		{
			get { return getValue<short?>("VacationHours"); }
			set { setValue<short?>("VacationHours", value); }
		}

		[Data(IsNullable = true, PhysicalName = "SickLeaveHours")]
		public short? SickLeaveHours
		{
			get { return getValue<short?>("SickLeaveHours"); }
			set { setValue<short?>("SickLeaveHours", value); }
		}

		[Data(IsNullable = true, PhysicalName = "CurrentFlag")]
		public bool? CurrentFlag
		{
			get { return getValue<bool?>("CurrentFlag"); }
			set { setValue<bool?>("CurrentFlag", value); }
		}

		[Data(PhysicalName = "EmployeePayHistory", FkNames = "EmployeeID")]
		public MultiValueField<EmployeePayHistory> PayHistory;

	}

	[DataDefinition(PhysicalName = "Department", PhysicalSchema = "HumanResources")]
	public class Department : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string DepartmentID = "DepartmentID";
			public const string Name = "Name";
			public const string GroupName = "GroupName";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "DepartmentID", IsPrimaryKey = true)]
		public short DepartmentID
		{
			get { return getValue<short>("DepartmentID"); }
			set { setValue<short>("DepartmentID", value); }
		}

		[Data(Size = 50, PhysicalName = "Name")]
		public string Name
		{
			get { return getValue<string>("Name"); }
			set { setValue<string>("Name", value); }
		}

		[Data(Size = 50, PhysicalName = "GroupName")]
		public string GroupName
		{
			get { return getValue<string>("GroupName"); }
			set { setValue<string>("GroupName", value); }
		}

	}

	[DataDefinition(PhysicalName = "JobCandidate", PhysicalSchema = "HumanResources")]
	public class JobCandidate : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string JobCandidateID = "JobCandidateID";
			public const string JobResume = "JobResume";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(AutoIdentity = true, PhysicalName = "JobCandidateID", IsPrimaryKey = true)]
		public int JobCandidateID
		{
			get { return getValue<int>("JobCandidateID"); }
			set { setValue<int>("JobCandidateID", value); }
		}

		[Data(Size = -1, IsNullable = true, PhysicalName = "Resume")]
		public string JobResume
		{
			get { return getValue<string>("JobResume"); }
			set { setValue<string>("JobResume", value); }
		}

	}

	[DataDefinition(MustPersist = false)]
	public class EmployeeStat : Entity, IDataWrapper
	{
		public static partial class Fields
		{
			public const string Id = "Id";
			public const string YearMin = "YearMin";
			public const string YearMax = "YearMax";
			public const string NbMale = "NbMale";
			public const string NbFemale = "NbFemale";
			public const string Decade = "Decade";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[Data(IsPrimaryKey=true)]
		public Guid Id
		{
			get { return getValue<Guid>("Id"); }
			set { setValue<Guid>("Id", value); }
		}

		[Data]
		public int YearMin
		{
			get { return getValue<int>("YearMin"); }
			set { setValue<int>("YearMin", value); }
		}

		[Data]
		public int YearMax
		{
			get { return getValue<int>("YearMax"); }
			set { setValue<int>("YearMax", value); }
		}

		[Data]
		public int NbMale
		{
			get { return getValue<int>("NbMale"); }
			set { setValue<int>("NbMale", value); }
		}

		[Data]
		public int NbFemale
		{
			get { return getValue<int>("NbFemale"); }
			set { setValue<int>("NbFemale", value); }
		}

		[Data(Expression = "YearMin + ' - ' + YearMax")]
		public string Decade
		{
			get { return getValue<string>("Decade"); }
		}

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class EmployeeManager : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(Employee), Role = typeof(Employee), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity Employee;

		[RelationEnd(Type = typeof(Employee), Role = typeof(Roles.Manager), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "ManagerID", Container = true)]
		public IEntity Manager;

	}

	[DataDefinition]
	[RelationPersistenceMode(SeparateTable = false)]
	public class JobCandidateEmployee : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(JobCandidate), Role = typeof(JobCandidate), Multiplicity = Multiplicity.ZeroOrMany)]
		public IEntity JobCandidate;

		[RelationEnd(Type = typeof(Employee), Role = typeof(Employee), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "EmployeeID")]
		public IEntity Employee;

	}

	[DataDefinition(PhysicalName = "EmployeeDepartmentHistory", PhysicalSchema = "HumanResources")]
	public class EmployeeDepartment : DataWrapper, IDataWrapper, IRelation
	{
		public static partial class Fields
		{
			public const string ShiftID = "ShiftID";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
		}

		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);

		}

		[RelationEnd(Type = typeof(Employee), Role = typeof(Employee), Multiplicity = Multiplicity.ZeroOrMany, FkNames = "EmployeeID")]
		public IEntity Employee;

		[RelationEnd(Type = typeof(Department), Role = typeof(Department), Multiplicity = Multiplicity.One, FkNames = "DepartmentID")]
		public IEntity Department;

		[Data(DefaultValue = 1, PhysicalName = "ShiftID", Unique = true)]
		public Shift ShiftID
		{
			get { return getValue<Shift>("ShiftID"); }
			set { setValue<Shift>("ShiftID", value); }
		}

		[Data(Unique = true)]
		public DateTime StartDate
		{
			get { return getValue<DateTime>("StartDate"); }
			set { setValue<DateTime>("StartDate", value); }
		}

		[Data(IsNullable = true)]
		public DateTime? EndDate
		{
			get { return getValue<DateTime?>("EndDate"); }
			set { setValue<DateTime?>("EndDate", value); }
		}

	}

	[DataDefinition(PhysicalName = "EmployeeAddress", PhysicalSchema = "HumanResources")]
	public class EmployeeAddress : DataWrapper, IDataWrapper, IRelation
	{
		void IDataWrapper.InitData(DataRow data, string namePrefix)
		{
			base.InitData(data, null);
		}

		[RelationEnd(Type = typeof(AdventureWorks.Person.Address), Role = typeof(AdventureWorks.Person.Address), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "AddressID")]
		public IEntity Address;

		[RelationEnd(Type = typeof(Employee), Role = typeof(Employee), Multiplicity = Multiplicity.ZeroOrOne, FkNames = "EmployeeID")]
		public IEntity Employee;

	}

}


  
