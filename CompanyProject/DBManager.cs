using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace CompanyProject
{
	public class DBManager
	{
		SqlConnection myConnection;

		public DBManager()
		{
			myConnection = new SqlConnection(CompanyProject.Constants.);
			try
			{
				myConnection.Open();
				Console.WriteLine("The DB connection is opened successfully");
			}
			catch (Exception e)
			{
				Console.WriteLine("The DB connection is failed");
				Console.WriteLine(e.ToString());
			}
		}

		public async Task<int> ExecuteNonQueryAsync(string query)
		{
			try
			{
				SqlCommand myCommand = new SqlCommand(query, myConnection);
				return await myCommand.ExecuteNonQueryAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
		}

		public async Task<DataTable> ExecuteReaderAsync(string query)
		{
			try
			{
				SqlCommand myCommand = new SqlCommand(query, myConnection);
				SqlDataReader reader = await myCommand.ExecuteReaderAsync();
				if (reader.HasRows)
				{
					DataTable dt = new DataTable();
					dt.Load(reader);
					reader.Close();
					return dt;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<object> ExecuteScalar(string query)
		{
			try
			{
				SqlCommand myCommand = new SqlCommand(query, myConnection);
				return await myCommand.ExecuteScalarAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
		}

		public void CloseConnection()
		{
			try
			{
				myConnection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}


	}
}
;