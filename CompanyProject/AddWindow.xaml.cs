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
	public partial class AddWindow : Window
	{
		Constants.Tablename_PKs tablename_PKs;
		List<Util.Header_Datatype> Headers_Datatypes;
		List<TextBox> textboxes;
		Util.ModifiedItem addedItem;
		Util.ModifiedItem addedUser;
		Controller controller;


		public AddWindow(Constants.Tablename_PKs tablename_PKs, List<Util.Header_Datatype> Headers_Datatypes)
		{
			InitializeComponent();
			this.tablename_PKs = tablename_PKs;
			this.Headers_Datatypes = Headers_Datatypes;
			this.textboxes = new List<TextBox>();
			roleComboBox.ItemsSource = new string[]{ "admin", "employee", "client" }; //user types

			controller = new Controller();

			addedItem = new Util.ModifiedItem
			{
				TableName = tablename_PKs.TableName,
				ChangesCount = 0,
				PrimaryKeysHeaders = tablename_PKs.PrimaryKeys,
				PrimaryKeysValues = new List<string>(new string[tablename_PKs.PrimaryKeys.Count]),
				ChangedHeaders = new List<string>(),
				ChangedValues = new List<string>()
			};

			//if the the table has an associated user
			if (Constants.tablesWithUsers.Contains(tablename_PKs.TableName))
				this.NextButton.Visibility = Visibility.Visible;
			else
				this.CommitNoUserButton.Visibility = Visibility.Visible;

			Add_Labels_Textboxes();
		}


		private async void Add_Labels_Textboxes()
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
				if (tablename_PKs.PrimaryKeys.Contains(Header))
					newLabel.Content = Header + "*";

				TextBox newText = new TextBox
				{
					Name = Header + "Textbox",
					HorizontalAlignment = HorizontalAlignment.Right,
					VerticalAlignment = VerticalAlignment.Center,
					Width = 120
				};

				this.textboxes.Add(newText);
				newGrid.Children.Add(newText);

				//if this is an unmodifiable ID (IDs admins are not allowed to change), make the textbox unmodifiable)
				if (Constants.unmodifiableIDs.Contains(Header))
				{
					newText.IsReadOnly = true;
					//get next autoincrement value
					var result = await controller.GetNextIncrement(tablename_PKs.TableName);
					newText.Text = result.ToString();
					newText.Background = Brushes.LightGray;
				}
			}
		}


		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}


		//For raising an event that a change has been committed and data in the Main Window needs to be updated
		public event RefreshDataEventArgs.RefreshDataEventHandler RefreshTable;
		protected virtual void OnRefreshTable(RefreshDataEventArgs e)
		{
			RefreshTable?.Invoke(this, e);
		}


		private void CheckConnection()
		{
			if (controller.connectionStatus == false)
			{
				MessageBox.Show("Couldn't connect to the database", "Connection Error");
				this.Close();
			}
		}


		//check primary keys were not left empty
		private bool CheckPrimaryKeysFilled()
		{
			bool result = true;
			for (int i = 0; i < Headers_Datatypes.Count; i++)
			{
				if (tablename_PKs.PrimaryKeys.Contains(Headers_Datatypes[i].Header))
				{
					if (textboxes[i].Text.Replace(" ","") == "")
					{
						textboxes[i].BorderBrush = Brushes.Red;
						result = false;
					}
				}
			}
			return result;
		}


		//Add item (employee or client) and go to the add user oage
		private async void NextButton_Click(object sender, RoutedEventArgs e)
		{
			CheckConnection();

			addedItem.ChangesCount = 0;
			addedItem.ChangedHeaders = new List<string>();
			addedItem.ChangedValues = new List<string>();

			//check primary keys are not empty
			if (!CheckPrimaryKeysFilled())
				return;

			//check data 
			for (int i = 0; i < Headers_Datatypes.Count; i++)
			{
				string Header = Headers_Datatypes[i].Header;
				string newtext = this.textboxes[i].Text;

				//if this is an unmodifiable ID, pass (it will be added automatically)
				if (Constants.unmodifiableIDs.Contains(Header))
					continue;

				//if data was put in the box
				if (!(newtext.Equals("")))
				{
					var datatype = Headers_Datatypes[i].Datatype;
					addedItem.ChangesCount += 1;
					//Confirm that the changed text matches the intended datatype
					if (datatype.Equals(typeof(System.DateTime)))
					{
						//check that the datetime is in the format yyyy-MM-dd
						try
						{
							addedItem.ChangedValues.Add(DateTime.ParseExact(newtext, "yyyy-MM-dd", null).ToString());
							addedItem.ChangedHeaders.Add(Header);
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
							addedItem.ChangedValues.Add(Convert.ChangeType(newtext, datatype).ToString());
							addedItem.ChangedHeaders.Add(Header);
						}
						catch
						{
							MessageBox.Show(Header + " must be of type " + datatype.ToString().Split('.')[1], "Datatype Error");
							return;
						}
					}
				}
			}


			int success = 0;
			if (addedItem.ChangesCount > 0)
			{
				success = await controller.AddItem(addedItem);
				if (success == 0)
				{
					//print sql error in a message box)
					MessageBox.Show(controller.errorMessage, "Database Error");
					return;
				}
			}
			else
			{
				MessageBox.Show("No data was added", "Add Data");
				return;
			}

			CancelButton.Visibility = Visibility.Hidden;
			NextButton.Visibility = Visibility.Hidden;
			SP.Visibility = Visibility.Hidden;
			CancelUserButton.Visibility = Visibility.Visible;
			CommitWithUserButton.Visibility = Visibility.Visible;
			UserPanel.Visibility = Visibility.Visible;
		}


		private async void CommitNoUserButton_Click(object sender, RoutedEventArgs e)
		{
			CheckConnection();

			addedItem.ChangesCount = 0;
			addedItem.ChangedHeaders = new List<string>();
			addedItem.ChangedValues = new List<string>();

			//check primary keys are not empty
			if (!CheckPrimaryKeysFilled())
				return;

			//check data 
			for (int i = 0; i < Headers_Datatypes.Count; i++)
			{
				string Header = Headers_Datatypes[i].Header;
				string newtext = this.textboxes[i].Text;

				//if this is an unmodifiable ID, pass (it will be added automatically)
				if (Constants.unmodifiableIDs.Contains(Header))
					continue;

					//if data was put in the box
					if (!(newtext.Equals("")))
				{
					var datatype = Headers_Datatypes[i].Datatype;
					addedItem.ChangesCount += 1;
					//Confirm that the changed text matches the intended datatype
					if (datatype.Equals(typeof(System.DateTime)))
					{
						//check that the datetime is in the format yyyy-MM-dd
						try
						{
							addedItem.ChangedValues.Add(DateTime.ParseExact(newtext, "yyyy-MM-dd", null).ToString());
							addedItem.ChangedHeaders.Add(Header);
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
							addedItem.ChangedValues.Add(Convert.ChangeType(newtext, datatype).ToString());
							addedItem.ChangedHeaders.Add(Header);
						}
						catch
						{
							MessageBox.Show(Header + " must be of type " + datatype.ToString().Split('.')[1], "Datatype Error");
							return;
						}
					}
				}
			}

			int success = 0;
			if (addedItem.ChangesCount > 0)
			{
				success = await controller.AddItem(addedItem);
				if (success == 0)
				{
					//print sql error in a message box)
					MessageBox.Show(controller.errorMessage, "Database Error");
					return;
				}
			}
			else
			{
				MessageBox.Show("No data was added", "Add Data");
				return;
			}

			//raise an event to update the employee in the main database
			OnRefreshTable(new RefreshDataEventArgs(tablename_PKs.TableName, true));
			this.Close();
		}


		private async void CommitWithUserButton_Click(object sender, RoutedEventArgs e)
		{
			addedUser = new Util.ModifiedItem
			{
				TableName = "AppUser",
				ChangesCount = 0,
				ChangedHeaders = new List<string>(),
				ChangedValues = new List<string>()
			};

			string username = usernameTextbox.Text;
			if (username.Equals(""))
			{
				MessageBox.Show("Username can't be empty", "Add Username");
				return;
			}
			else if ((await controller.CheckUsername(username)) == 1)
			{
				MessageBox.Show("This username is already used", "Change Username");
				return;
			}

			string password = passwordTextbox.Text;
			if (password.Length < 8)
			{
				MessageBox.Show("Password can't be less than 8 charachters", "Modify Password");
				return;
			}

			var role_ = roleComboBox.SelectedItem;
			if (!(role_ != null))
			{
				MessageBox.Show("Please choose a role for this user", "Choose a role");
				return;
			}
			string role = role_.ToString();
			if (role.Equals("client") && !(tablename_PKs.TableName.Equals("Client")))
			{
				MessageBox.Show("client role is only for clients", "Choose a role");
				return;
			}
			else if (role.Equals("admin") && !(tablename_PKs.TableName.Equals("Employee")))
			{
				MessageBox.Show("admin role is only for employees", "Choose a role");
				return;
			}
			else if (role.Equals("employee") && !(tablename_PKs.TableName.Equals("Employee")))
			{
				MessageBox.Show("employee role is only for employees", "Choose a role");
				return;
			}

			addedUser.ChangedHeaders.Add("username");
			addedUser.ChangedHeaders.Add("password");
			addedUser.ChangedHeaders.Add("role");
			addedUser.ChangedValues.Add(username);
			addedUser.ChangedValues.Add(password);
			addedUser.ChangedValues.Add(role);
			addedUser.ChangesCount += 3;

			if (tablename_PKs.TableName.Equals("Employee"))
			{
				string ID_ = (await controller.GetNextIncrement("Employee")).ToString();
				int ID = Convert.ToInt32(ID_) - 1;

				addedUser.ChangedHeaders.Add("EID");
				addedUser.ChangedValues.Add(ID.ToString());
				addedUser.ChangesCount += 1;
			}
			else if (tablename_PKs.TableName.Equals("Client"))
			{
				string ID_ = (await controller.GetNextIncrement("Client")).ToString();
				int ID = Convert.ToInt32(ID_) - 1;

				addedUser.ChangedHeaders.Add("CID");
				addedUser.ChangedValues.Add(ID.ToString());
				addedUser.ChangesCount += 1;
			}

			int success = 0;
			success = await controller.AddItem(addedUser);
			if (success == 0)
			{
				//print sql error in a message box)
				MessageBox.Show(controller.errorMessage, "Database Error");
				return;
			}

			//raise an event to update the employee in the main database
			OnRefreshTable(new RefreshDataEventArgs(tablename_PKs.TableName, true));
			this.Close();
		}


		private async void CancelUserButton_Click(object sender, RoutedEventArgs e)
		{
			//get the primary key values of the added item, to delete it
			for (int i = 0; i < addedItem.ChangedHeaders.Count; i++)
			{
				for (int j = 0; j < addedItem.PrimaryKeysHeaders.Count; j++)
				{
					if (addedItem.ChangedHeaders[i].Equals(addedItem.PrimaryKeysHeaders[j]))
					{
						addedItem.PrimaryKeysValues[j] = addedItem.ChangedValues[i];
					}
				}
			}
			//if the primary keys contain one autoincremental key, get it
			for (int i = 0; i < addedItem.PrimaryKeysHeaders.Count; i++)
			{
				if (Constants.unmodifiableIDs.Contains(addedItem.PrimaryKeysHeaders[i]))
				{
					string ID_ = (await controller.GetNextIncrement(tablename_PKs.TableName)).ToString();
					int ID = Convert.ToInt32(ID_) - 1;
					addedItem.PrimaryKeysValues[i] = ID.ToString();
				}
			}
			

			//delete the item as no user was added for it and it requires a user
			var result = await controller.DeleteItem(addedItem);
			this.Close();
		}
	}
}
