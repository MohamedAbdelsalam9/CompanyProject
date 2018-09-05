using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;


namespace CompanyProject
{
	public class Controller
	{
		DBManager db_manager;
		public bool connectionStatus;
		public string errorMessage;

		public Controller()
		{
			db_manager = new DBManager();
			if (db_manager.connectionStatus)
				this.connectionStatus = true;
			else
				this.connectionStatus = false;
			this.errorMessage = "";
		}

		
		///Normal SQL Queries (using async for better functionality)
		//Add an Employee
		public async Task<int> AddEmployee(int EID, string address, string position, int phone, string evaluation, DateTime DOB, string sex, 
			string email, DateTime start_date, string name, int E_Dnum, DateTime E_D_start_date, int E_Bnum)
		{
			string query = "INSERT INTO Employee (EID, address, position, phone, evaluation, DOB, sex, email, start_date, name, E_Dnum, " +
					"E_D_start_date, E_Bnum)" +
					"Values ('" + EID + "','" + address + "','" + position + "','" + phone + "','" + evaluation + "','" + DOB.ToString("yyyy-MM-dd") + 
					"','" + sex + "','" + email + "','" + start_date.ToString("yyyy-MM-dd") + "','" + name + "','" + E_Dnum + 
					"','" + E_D_start_date.ToString("yyyy-MM-dd") + "','" + E_Bnum + "');";
			Console.WriteLine(query);
			return await db_manager.ExecuteNonQueryAsync(query);
		}

		private async Task<object> GetUser(string username, string password)
		{
			string query = "SELECT username, password FROM AppUser WHERE (username = '" + username + "' AND password ='";
			Console.WriteLine(query + "..");
			query += password + "');";
			return await db_manager.ExecuteScalarAsync(query);
		}

		public async Task<string> GetRole(string username, string password)
		{
			string query = "SELECT role FROM AppUser WHERE (username = '" + username + "' AND password ='";
			Console.WriteLine(query + "..");
			query += password + "');";
			return (await db_manager.ExecuteScalarAsync(query)).ToString();
		}

		public async Task<int> CheckUsername(string username)
		{
			string query = "SELECT username FROM AppUser WHERE (username = '" + username + "');";
			var result = await db_manager.ExecuteScalarAsync(query);
			Console.WriteLine(query);
			if (result != null) //no username like that is present
				return 1;
			else
				return 0;
		}

		//Insertion of Data into Lists to fill tables
		//Get all Employees, Clients, Project, whatever
		public async Task<DataTable> GetAllTable(string tableName)
		{
			string query = "SELECT * FROM " + tableName + ";";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//update an item
		public async Task<int> UpdateItem(Util.ModifiedItem modifiedItem)
		{
			int changesCount = 0;
			string query = "UPDATE " + modifiedItem.TableName + " SET ";
			//SET deleted columns to null
			foreach (string header in modifiedItem.DeletedHeaders)
			{
				query += header + " = null ";
				changesCount++;
				if (changesCount < modifiedItem.ChangesCount)
					query += ", ";
			}
			//SET modified columns to their values
			for (int i = 0; i < modifiedItem.ChangedHeaders.Count; i++)
			{
				query += modifiedItem.ChangedHeaders[i] + " = '" + modifiedItem.ChangedValues[i] + "' ";
				changesCount++;
				if (changesCount < modifiedItem.ChangesCount)
					query += ", ";
			}

			//use the primary keys to identify the columns
			query += "WHERE ";
			for (int i = 0; i < modifiedItem.PrimaryKeysHeaders.Count; i++)
			{
				query += modifiedItem.PrimaryKeysHeaders[i] + " = '" + modifiedItem.PrimaryKeysValues[i] + "'";
				if ((i+1) < modifiedItem.PrimaryKeysHeaders.Count)
					query += " AND ";
			}
			query += ";";

			Console.WriteLine(query);
			int result = await db_manager.ExecuteNonQueryAsync(query);
			this.errorMessage = db_manager.errorMessage;
			return result;
		}

		//Delete an item (a row)
		public async Task<int> DeleteItem(Util.ModifiedItem deletedItem)
		{
			string query = "DELETE FROM " + deletedItem.TableName + " WHERE ";
			//use the primary keys to identify the columns
			for (int i = 0; i < deletedItem.PrimaryKeysHeaders.Count; i++)
			{
				query += deletedItem.PrimaryKeysHeaders[i] + " = '" + deletedItem.PrimaryKeysValues[i] + "'";
				if ((i + 1) < deletedItem.PrimaryKeysHeaders.Count)
					query += " AND ";
			}
			query += ";";

			Console.WriteLine(query);
			int result = await db_manager.ExecuteNonQueryAsync(query);
			this.errorMessage = db_manager.errorMessage;
			return result;
		}

		//add an item
		public async Task<int> AddItem(Util.ModifiedItem addedItem)
		{
			int changesCount = 0;
			string query = "INSERT INTO " + addedItem.TableName + " (";

			for (int i = 0; i < addedItem.ChangedHeaders.Count; i++)
			{
				query += addedItem.ChangedHeaders[i];
				changesCount++;
				if (changesCount < addedItem.ChangesCount)
					query += ", ";
			}

			query += ") VALUES (";
			changesCount = 0;
			for (int i = 0; i < addedItem.ChangedHeaders.Count; i++)
			{
				query += "'" + addedItem.ChangedValues[i] + "'";
				changesCount++;
				if (changesCount < addedItem.ChangesCount)
					query += ", ";
			}
			query += ");";

			Console.WriteLine(query);
			int result = await db_manager.ExecuteNonQueryAsync(query);
			this.errorMessage = db_manager.errorMessage;
			return result;
		}

		public async Task<DataTable> GetEmpSubordinates(string Super_EID)
		{
			string query;
			query = "SELECT EID, first_name AS FirstName, last_name AS LastName, email AS Email" +
				", position AS Position, salary AS Salary, Super_EID AS SupervisorEID ";
			query += "FROM Employee WHERE Super_EID = '" + Super_EID + "';";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}


		///SQL Aggregate and Complicated Functions
		//Get the service name, the employer name and the client name working together in a project, with some other details (like the start date of work, etc)
		public async Task<DataTable> GetSerEmpCli()
		{
			string query;
			query = "SELECT  S.name AS ServiceName, concat(E.first_name,' ',E.last_name) AS EmployeeName, " +
				"concat(C.first_name,' ',C.last_name) AS ClientName, C.company AS CompanyName, P.status AS Status" +
				", P.startdate AS StartDate, P.end_date AS EndDate, P.price AS Price ";
			query += "FROM ((Employee E JOIN Project P ON E.EID=P.P_EID) JOIN Service S ON P.P_SID=S.SID) " +
				"JOIN Client C ON C.CID=P.P_CID;";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}
		
		//Get Employes with no Projects
		public async Task<DataTable> GetEmpWithoutProj()
		{
			string query;
			query = "SELECT  EID, first_name AS FirstName, last_name AS LastName, email AS Email" +
				", position AS Position, salary AS Salary ";
			query += "FROM Employee E ";
			query += "WHERE NOT EXISTS (SELECT P_EID FROM Project WHERE E.EID=P_EID);";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get Services with no Clients
		public async Task<DataTable> GetSerWithoutCli()
		{
			string query;
			query = "SELECT  S.SID, S.name AS ServiceName, PreviousPurchases, PreviousProjects ";
			query += "FROM Service S LEFT JOIN (SELECT P_SID, COUNT(*) AS PreviousProjects, " +
				"SUM(price) AS PreviousPurchases FROM Project WHERE end_date<GETDATE() " +
				"GROUP BY P_SID) AS R1 ON SID=R1.P_SID ";
			query += "WHERE NOT EXISTS(SELECT P_SID FROM Project WHERE S.SID = P_SID " +
				"AND (end_date>=GETDATE() OR end_date IS NULL)) ";
			query += "ORDER BY PreviousProjects DESC;";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get Employees with most Projects
		public async Task<DataTable> GetEmpsWithMostProj()
		{
			string query;
			query = "SELECT  EID, first_name AS FirstName, last_name AS LastName, email AS Email" +
				", position AS Position, salary AS Salary, ProjectsCount ";
			query += "FROM Employee E JOIN (SELECT P_EID, COUNT(*) AS ProjectsCount FROM Project " +
				"GROUP BY P_EID) AS R ON E.EID=R.P_EID ";
			query += "ORDER BY ProjectsCount DESC;";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get Services with most Clients
		public async Task<DataTable> GetSerWithMostCli()
		{
			string query;
			query = "SELECT  S.SID, S.name AS ServiceName, R1.purchases AS TotalPurchases, TotalProjects, CurrentProjects ";
			query += "FROM Service S JOIN (SELECT P_SID, COUNT(*) AS TotalProjects, SUM(price) AS purchases " +
				"FROM Project GROUP BY P_SID) AS R1 ON SID=R1.P_SID ";
			query += "LEFT JOIN (SELECT P2.P_SID, COUNT(*) AS CurrentProjects FROM Project P2 " +
				"WHERE EXISTS (SELECT P2.end_date WHERE P2.end_date>GETDATE() OR P2.end_date IS NULL) GROUP BY P2.P_SID) AS R2 ON SID=R2.P_SID ";
			query += "ORDER BY TotalProjects DESC;";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get the Count, Average, Max, Min and Sum for Salary (Employer)  Grouped by Department
		public async Task<DataTable> GetSalStatisticsDep()
		{
			string query;
			query = "SELECT D.Dnum AS Department_Number, D.name AS Department_Name, COUNT(*) AS Employees_Count" +
				", SUM(E.salary) AS Salaries_Sum, MIN(E.salary) AS Minimum_Salary, MAX(E.salary) AS Maximum_Salary" +
				", AVG(E.salary) AS Average_Salary ";
			query += "FROM Employee E, Department D WHERE E.E_Dnum = D.Dnum GROUP BY D.Dnum, D.name;";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get the Count, Average, Max, Min and Sum for a certain column (Generic)
		public async Task<DataTable> GetTableStatistics(string tableName, string columnName)
		{
			string query;
			query = "SELECT ";
			query += "COUNT(*) AS Count, SUM(" + columnName + ") AS Sum, MIN(" + columnName + ") AS Min" +
				", MAX(" + columnName + ") AS Max, AVG(" + columnName + ") AS Average ";
			query += "FROM " + tableName + ";";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get the Count, Average, Max, Min and Sum for Salary (Employer)  Grouped by Branches
		public async Task<DataTable> GetSalStatisticsBr()
		{
			string query;
			query = "SELECT B.Bnum AS Branch_Number, B.name AS Branch_Name, COUNT(*) AS Employees_Count, SUM(E.salary) AS Salaries_Sum, MIN(E.salary) AS Minimum_Salary, MAX(E.salary) AS Maximum_Salary, AVG(E.salary) AS Average_Salary ";
			query += "FROM Employee E, Branch B WHERE E.E_Bnum = B.Bnum GROUP BY B.Bnum, B.name;";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get the Count, Average, Max, Min and Sum for tables Grouped by a value (Generic)
		public async Task<DataTable> GetTableStatisticsGrouped(List<string> groupByColumns, string tableName, string columnName)
		{
			string query;
			query = "SELECT ";
			for (int i = 0; i < groupByColumns.Count; i++)
			{
				query += groupByColumns[i] + " ";
				if ((i+1) < groupByColumns.Count)
					query += groupByColumns[i] + ", ";
			}
			query += ", COUNT(*) AS Count, SUM(" + columnName + ") AS Sum, MIN(" + columnName + ") AS Min" +
				", MAX(" + columnName + ") AS Max, AVG(" + columnName + ") AS Average ";
			query += "FROM " + tableName + "GROUP BY ";
			for (int i = 0; i < groupByColumns.Count; i++)
			{
				query += groupByColumns[i] + " ";
				if ((i + 1) < groupByColumns.Count)
					query += groupByColumns[i] + ", ";
			}
			query += ";";

			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get the Stock Parts with Warehouses adresses
		public async Task<DataTable> GetStock()
		{
			string query;
			query = "SELECT  ST_partid AS PartID, WID as WarehouseID, address AS Address, quantity AS AvailableStock ";
			query += "FROM Stock, Warehouse ";
			query += "WHERE WID=ST_WID ";
			query += "ORDER BY ST_partid ASC, WID ASC;";
			Console.WriteLine(query);
			return await db_manager.ExecuteReaderAsync(query);
		}

		//Get max value 
		public async Task<object> GetMinMax(string tablename, string header, string minmax = "max")
		{
			minmax = minmax.ToLower();
			string query;
			if (minmax == "max")
				query = "SELECT MAX(" + header + ") FROM " + tablename + ";";
			else if (minmax == "min")
				query = "SELECT MIN(" + header + ") FROM " + tablename + ";";
			else
				return -1;

			Console.WriteLine(query);
			return await db_manager.ExecuteScalarAsync(query);
		}

		///Other Functions
		//Check username and password are true
		public async Task<int> CheckUser(string username, string password)
		{
			object result = await GetUser(username, password);
			if (!(result != null))
				return 0;
			else if (result.Equals(0))
				return 0;
			else
				return 1;
		}

		//Get next autoincrement value
		public async Task<object> GetNextIncrement(string tablename)
		{
			string query = "SELECT IDENT_CURRENT('" + tablename + "') + 1;";
			Console.WriteLine(query);
			return await db_manager.ExecuteScalarAsync(query);
		}
		
	}
}
