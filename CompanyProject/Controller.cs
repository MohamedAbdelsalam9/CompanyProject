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
			return await db_manager.ExecuteNonQueryAsync(query);
		}

		private async Task<object> GetUser(string username, string password)
		{
			string query = "SELECT username, password FROM AppUser WHERE (username = '" + username + "' AND password ='" + password + "');";
			return await db_manager.ExecuteScalarAsync(query);
		}

		public async Task<int> CheckUsername(string username)
		{
			string query = "SELECT username FROM AppUser WHERE (username = '" + username + "');";
			var result = await db_manager.ExecuteScalarAsync(query);

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


		///SQL Aggregate and Complicated Functions
		
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
			return await db_manager.ExecuteScalarAsync(query);
		}
		
	}
}
