using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject
{
	public static class Constants
	{


		public static string ConnectionString => "Data Source=Ashraf\\SQLEXPRESS;Initial Catalog=CompanyProjectData2;Integrated Security=True;";
		public static string[] unmodifiableIDs = {"EID", "CID"}; //IDs that even admins won't be allowed to modify (they are already autoincremental and automatically generated)
		public static string[] tablesWithUsers = { "Employee", "Client" }; //tables that have users associated with 

		public class Tablename_PKs
		{
			string tablename;
			List<string> primarykeys;
			public string TableName
			{
				get { return tablename; }
				set { tablename = value; }
			}
			public List<string> PrimaryKeys
			{
				get { return primarykeys; }
				set { primarykeys = value; }
			}
		}

		public static List<Tablename_PKs> tablenames_PKs;

		static Constants()
		{
			tablenames_PKs = new List<Tablename_PKs>();

			//add the table names and their primaery keys to the tablenames_PKs list
			Tablename_PKs Appuser = new Tablename_PKs
			{
				TableName = "Appuser",
				PrimaryKeys = new List<string> { "username" }
			};
			tablenames_PKs.Add(Appuser);

			Tablename_PKs Asset = new Tablename_PKs
			{
				TableName = "Asset",
				PrimaryKeys = new List<string> { "A_Partid" }
			};
			tablenames_PKs.Add(Asset);

			Tablename_PKs Branch = new Tablename_PKs
			{
				TableName = "Branch",
				PrimaryKeys = new List<string> { "Bnum" }
			};
			tablenames_PKs.Add(Branch);

			Tablename_PKs Client = new Tablename_PKs
			{
				TableName = "Client",
				PrimaryKeys = new List<string> { "CID" }
			};
			tablenames_PKs.Add(Client);

			Tablename_PKs Department = new Tablename_PKs
			{
				TableName = "Department",
				PrimaryKeys = new List<string> { "Dnum" }
			};
			tablenames_PKs.Add(Department);

			Tablename_PKs Employee = new Tablename_PKs
			{
				TableName = "Employee",
				PrimaryKeys = new List<string> { "EID" }
			};
			tablenames_PKs.Add(Employee);

			Tablename_PKs Part = new Tablename_PKs
			{
				TableName = "Part",
				PrimaryKeys = new List<string> { "PartID" }
			};
			tablenames_PKs.Add(Part);

			Tablename_PKs Project = new Tablename_PKs
			{
				TableName = "Project",
				PrimaryKeys = new List<string> { "P_EID", "P_SID", "P_CID" }
			};
			tablenames_PKs.Add(Project);

			Tablename_PKs Service = new Tablename_PKs
			{
				TableName = "Service",
				PrimaryKeys = new List<string> { "SID" }
			};
			tablenames_PKs.Add(Service);

			Tablename_PKs Stock = new Tablename_PKs
			{
				TableName = "Stock",
				PrimaryKeys = new List<string> { "ST_partid", "ST_WID" }
			};
			tablenames_PKs.Add(Stock);

			Tablename_PKs Supplier = new Tablename_PKs
			{
				TableName = "Supplier",
				PrimaryKeys = new List<string> { "SupID" }
			};
			tablenames_PKs.Add(Supplier);

			Tablename_PKs Supply = new Tablename_PKs
			{
				TableName = "Supply",
				PrimaryKeys = new List<string> { "S_partID", "Sup_PID" }
			};
			tablenames_PKs.Add(Supply);

			Tablename_PKs Tool = new Tablename_PKs
			{
				TableName = "Tool",
				PrimaryKeys = new List<string> { "T_partID" }
			};
			tablenames_PKs.Add(Tool);

			Tablename_PKs Warehouse = new Tablename_PKs
			{
				TableName = "Warehouse",
				PrimaryKeys = new List<string> { "WID" }
			};
			tablenames_PKs.Add(Warehouse);
		}
	}
}
