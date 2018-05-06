using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject
{
	public class RefreshDataEventArgs : EventArgs
	{
		private string tableName;
		private bool changeCommited;

		//Raise this event of a change has been made to one of the tables (specifying which table), to refresh
		//it in the gui
		public RefreshDataEventArgs(string tableName, bool changeCommited = true)
		{
			this.tableName = tableName;
			this.changeCommited = changeCommited;
		}

		public string TableName
		{
			get { return tableName; }
		}

		public bool ChangeCommited
		{
			get { return changeCommited; }
		}

		public delegate void RefreshDataEventHandler(object sender, RefreshDataEventArgs e);
	}
}
