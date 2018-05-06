using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompanyProject
{
	/// <summary>
	/// Interaction logic for EditWindow.xaml
	/// </summary>
	public partial class EditWindow : Window
	{
		Constants.Tablename_PKs tablename_PKs;
		List<Util.Header_Datatype> Headers_Datatypes;
		string[] oldItemData;
		List<TextBox> textboxes;
		Util.ModifiedItem modifiedItem;
		Controller controller;

		public EditWindow(Constants.Tablename_PKs tablename_PKs, List<Util.Header_Datatype> Headers_Datatypes, string[] ItemData)
		{
			InitializeComponent();
			this.tablename_PKs = tablename_PKs;
			this.Headers_Datatypes = Headers_Datatypes;
			this.oldItemData = ItemData;
			this.textboxes = new List<TextBox>();

			controller = new Controller();

			modifiedItem = new Util.ModifiedItem
			{
				TableName = tablename_PKs.TableName,
				ChangesCount = 0,
				PrimaryKeysHeaders = tablename_PKs.PrimaryKeys,
				PrimaryKeysValues = new List<string>(new string[tablename_PKs.PrimaryKeys.Count]),
				DeletedHeaders = new List<string>(),
				ChangedHeaders = new List<string>(),
				ChangedValues = new List<string>()
			};

			//get primary keys values
			modifiedItem = Util.GetPKValues(modifiedItem, Headers_Datatypes, oldItemData);

			Add_Labels_Textboxes();
		}


		private void Add_Labels_Textboxes()
		{
			for (int i = 0; i < Headers_Datatypes.Count; i++)
			{
				string Header = Headers_Datatypes[i].Header;
				Type datatype = Headers_Datatypes[i].Datatype;

				Grid newGrid = new Grid
				{
					HorizontalAlignment = HorizontalAlignment.Stretch,
					Height = 30,
					Name = Header + "Grid"
				};
				this.SP.Children.Add(newGrid);


				Label newLabel = new Label {
					Name = Header + "Label",
					Content = Header,
					HorizontalAlignment = HorizontalAlignment.Left,
					VerticalAlignment = VerticalAlignment.Center
				};
				newGrid.Children.Add(newLabel);
				if (Header.ToLower() == "sex")
					newLabel.Content = Header + " (M or F)";
				else if (datatype.Equals(typeof(System.DateTime)))
					newLabel.Content = Header + " (yyyy-MM-dd)";

				TextBox newText = new TextBox
				{
					Name = Header + "Textbox",
					HorizontalAlignment = HorizontalAlignment.Right,
					VerticalAlignment = VerticalAlignment.Center,
					Width = 120
				};

				try { newText.Text = oldItemData[i];}
				catch {;}
				this.textboxes.Add(newText);
				newGrid.Children.Add(newText);

				//if this is an unmodifiable ID (IDs admins are not allowed to change), make the textbox unmodifiable)
				foreach (string str in Constants.unmodifiableIDs)
				{
					if (str.Contains(Header))
					{
						newText.IsReadOnly = true;
						newText.Background = Brushes.LightGray;
					}
				}
			}
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private async void CommitButton_Click(object sender, RoutedEventArgs e)
		{
			modifiedItem.ChangesCount = 0;
			modifiedItem.DeletedHeaders = new List<string>();
			modifiedItem.ChangedHeaders = new List<string>();
			modifiedItem.ChangedValues = new List<string>();

			if (controller.connectionStatus == false)
			{
				MessageBox.Show("Couldn't connect to the database", "Connection Error");
				this.Close();
			}

			//check data 
			for (int i = 0; i < Headers_Datatypes.Count; i++)
			{
				string Header = Headers_Datatypes[i].Header;
				string newtext = this.textboxes[i].Text;

				//if the data in this textbox changed, save it
				if (!(newtext.Equals(oldItemData[i])))
				{
					var datatype = Headers_Datatypes[i].Datatype;
					modifiedItem.ChangesCount += 1;

					//if the change is empty, then it is a delete not an update
					if (newtext.Equals(""))
					{
						//if the primary key is getting deleted
						if (modifiedItem.PrimaryKeysHeaders.Contains(Header))
						{
							MessageBox.Show(Header + " is a primary key, so it can't be null");
							return;
						}
						else
							modifiedItem.DeletedHeaders.Add(Header);
					}

					//if it is an update not a delete
					else
					{
						//Confirm that the changed text matches the intended datatype
						if (datatype.Equals(typeof(System.DateTime)))
						{
						//check that the datetime is in the format yyyy-MM-dd
							try
							{
								modifiedItem.ChangedValues.Add(DateTime.ParseExact(newtext, "yyyy-MM-dd", null).ToString());
								modifiedItem.ChangedHeaders.Add(Header);
							}
							catch
							{
								MessageBox.Show(Header + " must be of format \"yyyy-MM-dd\"");
								return;
							}
						}
						else
						{
							try
							{
								modifiedItem.ChangedValues.Add(Convert.ChangeType(newtext, datatype).ToString());
								modifiedItem.ChangedHeaders.Add(Header);
							}
							catch
							{
								MessageBox.Show(Header + " must be of type " + datatype.ToString().Split('.')[1], "Datatype Error");
								return;
							}
						}	
					}
				}
			}

			int success = 0;
			if (modifiedItem.ChangesCount > 0)
			{
				success = await controller.UpdateItem(modifiedItem);
				if (success == 0)
				{
					//print sql error in a message box)
					MessageBox.Show(controller.errorMessage, "Database Error");
					return;
				}
			}
				
			//raise an event to update the employee in the main database
			OnRefreshTable(new RefreshDataEventArgs(tablename_PKs.TableName, true));
			this.Close();
		}

		//For raising an event that a change has been committed and data in the Main Window needs to be updated
		public event RefreshDataEventArgs.RefreshDataEventHandler RefreshTable;
		protected virtual void OnRefreshTable(RefreshDataEventArgs e)
		{
			RefreshTable?.Invoke(this, e);
		}
	}
}
