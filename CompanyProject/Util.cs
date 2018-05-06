using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject
{
    public static class Util
    {
		//save the values changed for the item (employee, client, part, etc), the headers of these values, the headers of the values deleted, and the tablename
		public class ModifiedItem
		{
			string tablename;
			int changesCount;
			List<string> primaryKeysHeaders;
			List<string> primaryKeysValues;
			List<string> deletedHeaders;
			List<string> changedHeaders;
			List<string> changedValues;
			public string TableName
			{
				get { return tablename; }
				set { tablename = value; }
			}
			public int ChangesCount
			{
				get { return changesCount; }
				set { changesCount = value; }
			}
			public List<string> PrimaryKeysHeaders
			{
				get { return primaryKeysHeaders; }
				set { primaryKeysHeaders = value; }
			}
			public List<string> PrimaryKeysValues
			{
				get { return primaryKeysValues; }
				set { primaryKeysValues = value; }
			}
			public List<string> DeletedHeaders
			{
				get { return deletedHeaders; }
				set { deletedHeaders = value; }
			}
			public List<string> ChangedHeaders
			{
				get { return changedHeaders; }
				set { changedHeaders = value; }
			}
			public List<string> ChangedValues
			{
				get { return changedValues; }
				set { changedValues = value; }
			}
		}

		//save the Datatypes of the corresponding column name (header)
		public class Header_Datatype
		{
			string tablename;
			string header;
			Type datatype;
			public string TableName
			{
				get { return tablename; }
				set { tablename = value; }
			}
			public string Header
			{
				get { return header; }
				set { header = value; }
			}
			public Type Datatype
			{
				get { return datatype; }
				set { datatype = value; }
			}
		}

		public static Constants.Tablename_PKs GetTablePk(string TableName)
		{
			for (int i = 0; i < Constants.tablenames_PKs.Count; i++)
			{
				if (Constants.tablenames_PKs[i].TableName.Contains(TableName))
					return Constants.tablenames_PKs[i];
			}
			return null;
		}

		public static ModifiedItem GetPKValues(ModifiedItem modifiedItem,List<Header_Datatype> Headers_Datatypes, string[] ItemValues)
		{
			if (Headers_Datatypes.Count != ItemValues.Length)
				throw new System.ArgumentException("Headers count and values count are not equal", "Headers_Datatypes");

			modifiedItem.PrimaryKeysValues = new List<string>(new string[modifiedItem.PrimaryKeysHeaders.Count]);
			for (int i = 0; i < Headers_Datatypes.Count; i++)
			{
				for (int j = 0; j < modifiedItem.PrimaryKeysHeaders.Count; j++)
				{
					if (modifiedItem.PrimaryKeysHeaders[j].Equals(Headers_Datatypes[i].Header))
						modifiedItem.PrimaryKeysValues[j] = ItemValues[i];
				}
			}
			return modifiedItem;
		}
	}
}
