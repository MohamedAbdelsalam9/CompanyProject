using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject
{
	public class Controller
	{
		DBManager db_manager;

		public Controller()
		{
			db_manager = new DBManager();
		}

		public async Task<int> AddEmployee(int EID, string address, string Position, int phone, string evaluation, DateTime DOB, string sex, 
			string email, DateTime start_date, string name, int E_Dnum, DateTime E_D_start_date, int E_Bnum)
		{
			string query = "INSERT INTO Employee (EID, address, Position, phone, evaluation, DOB, sex, email, start_date, name, E_Dnum, " +
					"E_D_start_date, E_Bnum, password)" +
					"Values ('" + EID + "','" + address + "','" + Position + "','" + phone + "','" + evaluation + "','" + DOB.ToString("yyyy-MM-dd") + 
					"','" + sex + "','" + email + "','" + start_date.ToString("yyyy-MM-dd") + "','" + name + "','" + E_Dnum + 
					"','" + E_D_start_date.ToString("yyyy-MM-dd") + "','" + E_Bnum + "');";
			return await db_manager.ExecuteNonQueryAsync(query);
		}

		private async Task<object> GetUser(string username, string password)
		{
			string query = "SELECT username, password FROM Users WHERE (username = '" + username + "' AND password ='" + password + "');";
			return await db_manager.ExecuteScalarAsync(query);
		}

		public async Task<int> CheckUser(string username, string password)
		{
			object result = await GetUser(username, password);
			if (result != null)
				return 1;
			else
				return 0;
		}
	}
}
