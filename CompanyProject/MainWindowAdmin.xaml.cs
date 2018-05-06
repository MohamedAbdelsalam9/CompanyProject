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
using System.Collections.ObjectModel;
using System.Data;


namespace CompanyProject
{
    /// <summary>
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class MainWindowAdmin : Window
    {
		Controller controller = new Controller();

		DataTable EmployeesList;
		List<Util.Header_Datatype> Employees_Datatypes = new List<Util.Header_Datatype>();
		IList<DataGridCellInfo> employeesSelectedCellsInfo;
		//values of current state of selectedcells
		string[] employeesSelectedCells;

		DataTable ClientsList;
		List<Util.Header_Datatype> Clients_Datatypes = new List<Util.Header_Datatype>();
		IList<DataGridCellInfo> clientsSelectedCellsInfo;
		//values of current state of selectedcells
		string[] clientsSelectedCells;

		DataTable ServicesList;
		List<Util.Header_Datatype> Services_Datatypes = new List<Util.Header_Datatype>();
		IList<DataGridCellInfo> servicesSelectedCellsInfo;
		//values of current state of selectedcells
		string[] servicesSelectedCells;

		DataTable PartsList;
		List<Util.Header_Datatype> Parts_Datatypes = new List<Util.Header_Datatype>();
		IList<DataGridCellInfo> partsSelectedCellsInfo;
		//values of current state of selectedcells
		string[] partsSelectedCells;


		public MainWindowAdmin()
        {
            InitializeComponent();
		}

		//Generate Columns
		private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Tabs.SelectedIndex == 0) //Employees
			{
				EmployeesGrid.Visibility = Visibility.Visible;
				ClientsGrid.Visibility = Visibility.Collapsed;
				ServicesGrid.Visibility = Visibility.Collapsed;
				PartsGrid.Visibility = Visibility.Collapsed;
				LoadEmployees();
			}
			else if (Tabs.SelectedIndex == 1) //Clients
			{
				EmployeesGrid.Visibility = Visibility.Collapsed;
				ClientsGrid.Visibility = Visibility.Visible;
				ServicesGrid.Visibility = Visibility.Collapsed;
				PartsGrid.Visibility = Visibility.Collapsed;
				LoadClients();
			}
			else if (Tabs.SelectedIndex == 2) //Services
			{
				EmployeesGrid.Visibility = Visibility.Collapsed;
				ClientsGrid.Visibility = Visibility.Collapsed;
				ServicesGrid.Visibility = Visibility.Visible;
				PartsGrid.Visibility = Visibility.Collapsed;
				LoadServices();
			}
			else if (Tabs.SelectedIndex == 3) //Parts
			{
				EmployeesGrid.Visibility = Visibility.Collapsed;
				ClientsGrid.Visibility = Visibility.Collapsed;
				ServicesGrid.Visibility = Visibility.Collapsed;
				PartsGrid.Visibility = Visibility.Visible;
				LoadParts();
			}
		}


		///Generic for all tables
		//Open an Edit Window giving them the table name, primary keys, Columns names, data types, and the present selected data (in case of editing)
		private void Edit_Window(List<Util.Header_Datatype> Headers_Datatypes, string[] ItemData, string TableName)
		{
			//get the primary keys of the current table from the Constants class
			Constants.Tablename_PKs tablename_PKs = Util.GetTablePk(TableName);

			EditWindow editwindow = new EditWindow(tablename_PKs, Headers_Datatypes, ItemData);

			// Connects the EmployeeChanged method to the EmployeeChanged event.
			editwindow.RefreshTable += new RefreshDataEventArgs.RefreshDataEventHandler(ItemChanged);

			editwindow.Show();
		}

		private void Add_Window(List<Util.Header_Datatype> Headers_Datatypes, string TableName)
		{
			//get the primary keys of the current table from the Constants class
			Constants.Tablename_PKs tablename_PKs = Util.GetTablePk(TableName);

			AddWindow addwindow = new AddWindow(tablename_PKs, Headers_Datatypes);

			// Connects the EmployeeChanged method to the EmployeeChanged event.
			addwindow.RefreshTable += new RefreshDataEventArgs.RefreshDataEventHandler(ItemChanged);

			addwindow.Show();
		}

		//An event handler to detect if an employee was changed to refresh the data
		private void ItemChanged(object sender, RefreshDataEventArgs e)
		{
			if (e.ChangeCommited && e.TableName.Contains("Employee"))
				LoadEmployees();
			else if (e.ChangeCommited && e.TableName.Contains("Client"))
				LoadClients();
			else if (e.ChangeCommited && e.TableName.Contains("Service"))
				LoadServices();
			else if (e.ChangeCommited && e.TableName.Contains("Part"))
				LoadParts();
		}


		/// Employees specific functions
		//Generate Columns automatically
		private void EmployeesDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			Util.Header_Datatype temp = new Util.Header_Datatype
			{
				Header = e.Column.Header.ToString(),
				Datatype = e.PropertyType
			};
			Employees_Datatypes.Add(temp);

			if (temp.Datatype == typeof(System.DateTime))
				(e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd";
		}

		//Load the Employees List
		private async void LoadEmployees()
		{
			if (controller.connectionStatus == false)
			{
				MessageBox.Show("Couldn't connect to the database", "Connection Error");
				this.Close();
			}
			Employees_Datatypes.Clear();
			EmployeesList = await controller.GetAllTable("Employee");
			EmployeesDataGrid.DataContext = EmployeesList;
		}

		//if a specific row is selected, save it's status to see if the user changes it
		private void EmployeesDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			employeesSelectedCellsInfo = e.AddedCells;
			employeesSelectedCells = new string[employeesSelectedCellsInfo.Count];
			for (int i = 0; i < employeesSelectedCellsInfo.Count; i++)
			{
				try { employeesSelectedCells[i] = (employeesSelectedCellsInfo[i].Column.GetCellContent(employeesSelectedCellsInfo[i].Item)
						as TextBlock).Text; }
				catch { employeesSelectedCells[i] = ""; }
			}
		}

		private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
		{
			Add_Window(Employees_Datatypes, "Employee");
		}

		private void RefreshEmpButton_Click(object sender, RoutedEventArgs e)
		{
			LoadEmployees();
		}

		//Edit Selected Employee
		private void EditEmpButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (EmployeesDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Employee is Selected", "Select an Employee");
				return;
			}

			//Display the Edit window
			Edit_Window(Employees_Datatypes, employeesSelectedCells, "Employee");
		}

		private async void DeleteEmpButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (EmployeesDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Employee is Selected", "Select an Employee");
				return;
			}

			Util.ModifiedItem deletedItem = new Util.ModifiedItem();
			deletedItem.TableName = "Employee";
			deletedItem.PrimaryKeysHeaders = Util.GetTablePk(deletedItem.TableName).PrimaryKeys;
			deletedItem = Util.GetPKValues(deletedItem, Employees_Datatypes, employeesSelectedCells); //get values of primary keys (deletedItem.PrimaryKeysValues)

			if (MessageBox.Show("Are you sure you want to delete the Employee? (Deleting the Employee will also delete the user associated)", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
			{
				await controller.DeleteItem(deletedItem);
				LoadEmployees();
			}
		}


		/// Clients specific functions
		//Generate Columns automatically
		private void ClientsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			Util.Header_Datatype temp = new Util.Header_Datatype
			{
				Header = e.Column.Header.ToString(),
				Datatype = e.PropertyType
			};
			Clients_Datatypes.Add(temp);

			if (temp.Datatype == typeof(System.DateTime))
				(e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd";
		}

		//Load the Clients List
		private async void LoadClients()
		{
			if (controller.connectionStatus == false)
			{
				MessageBox.Show("Couldn't connect to the database", "Connection Error");
				this.Close();
			}
			Clients_Datatypes.Clear();
			ClientsList = await controller.GetAllTable("Client");
			ClientsDataGrid.DataContext = ClientsList;
		}

		//if a specific row is selected, save it's status to see if the user changes it
		private void ClientsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			clientsSelectedCellsInfo = e.AddedCells;
			clientsSelectedCells = new string[clientsSelectedCellsInfo.Count];
			for (int i = 0; i < clientsSelectedCellsInfo.Count; i++)
			{
				try
				{
					clientsSelectedCells[i] = (clientsSelectedCellsInfo[i].Column.GetCellContent(clientsSelectedCellsInfo[i].Item)
					  as TextBlock).Text;
				}
				catch { clientsSelectedCells[i] = ""; }
			}
		}

		private void AddClientButton_Click(object sender, RoutedEventArgs e)
		{
			Add_Window(Clients_Datatypes, "Client");
		}

		private void RefreshCliButton_Click(object sender, RoutedEventArgs e)
		{
			LoadClients();
		}

		//Edit Selected Client
		private void EditCliButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (ClientsDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Client is Selected", "Select a Client");
				return;
			}

			//Display the Edit window
			Edit_Window(Clients_Datatypes, clientsSelectedCells, "Client");
		}

		private async void DeleteCliButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (ClientsDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Client is Selected", "Select an Client");
				return;
			}

			Util.ModifiedItem deletedItem = new Util.ModifiedItem();
			deletedItem.TableName = "Client";
			deletedItem.PrimaryKeysHeaders = Util.GetTablePk(deletedItem.TableName).PrimaryKeys;
			deletedItem = Util.GetPKValues(deletedItem, Clients_Datatypes, clientsSelectedCells); //get values of primary keys (deletedItem.PrimaryKeysValues)

			if (MessageBox.Show("Are you sure you want to delete the Client? (Deleting the Client will also delete the user associated)", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
			{
				await controller.DeleteItem(deletedItem);
				LoadClients();
			}
		}


		/// Services specific functions
		//Generate Columns automatically
		private void ServicesDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			Util.Header_Datatype temp = new Util.Header_Datatype
			{
				Header = e.Column.Header.ToString(),
				Datatype = e.PropertyType
			};
			Services_Datatypes.Add(temp);

			if (temp.Datatype == typeof(System.DateTime))
				(e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd";
		}

		//Load the Services List
		private async void LoadServices()
		{
			if (controller.connectionStatus == false)
			{
				MessageBox.Show("Couldn't connect to the database", "Connection Error");
				this.Close();
			}
			Services_Datatypes.Clear();
			ServicesList = await controller.GetAllTable("Service");
			ServicesDataGrid.DataContext = ServicesList;
		}

		//if a specific row is selected, save it's status to see if the user changes it
		private void ServicesDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			servicesSelectedCellsInfo = e.AddedCells;
			servicesSelectedCells = new string[servicesSelectedCellsInfo.Count];
			for (int i = 0; i < servicesSelectedCellsInfo.Count; i++)
			{
				try
				{
					servicesSelectedCells[i] = (servicesSelectedCellsInfo[i].Column.GetCellContent(servicesSelectedCellsInfo[i].Item)
					  as TextBlock).Text;
				}
				catch { servicesSelectedCells[i] = ""; }
			}
		}

		private void AddServiceButton_Click(object sender, RoutedEventArgs e)
		{
			Add_Window(Services_Datatypes, "Service");
		}

		private void RefreshSerButton_Click(object sender, RoutedEventArgs e)
		{
			LoadServices();
		}

		//Edit Selected Service
		private void EditSerButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (ServicesDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Service is Selected", "Select a Service");
				return;
			}

			//Display the Edit window
			Edit_Window(Services_Datatypes, servicesSelectedCells, "Service");
		}

		private async void DeleteSerButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (ServicesDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Service is Selected", "Select a Service");
				return;
			}

			Util.ModifiedItem deletedItem = new Util.ModifiedItem();
			deletedItem.TableName = "Service";
			deletedItem.PrimaryKeysHeaders = Util.GetTablePk(deletedItem.TableName).PrimaryKeys;
			deletedItem = Util.GetPKValues(deletedItem, Services_Datatypes, servicesSelectedCells); //get values of primary keys (deletedItem.PrimaryKeysValues)

			if (MessageBox.Show("Are you sure you want to delete the Service?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
			{
				await controller.DeleteItem(deletedItem);
				LoadServices();
			}
		}


		/// Parts specific functions
		//Generate Columns automatically
		private void PartsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			Util.Header_Datatype temp = new Util.Header_Datatype
			{
				Header = e.Column.Header.ToString(),
				Datatype = e.PropertyType
			};
			Parts_Datatypes.Add(temp);

			if (temp.Datatype == typeof(System.DateTime))
				(e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd";
		}

		//Load the Parts List
		private async void LoadParts()
		{
			if (controller.connectionStatus == false)
			{
				MessageBox.Show("Couldn't connect to the database", "Connection Error");
				this.Close();
			}
			Parts_Datatypes.Clear();
			PartsList = await controller.GetAllTable("Part");
			PartsDataGrid.DataContext = PartsList;
		}

		//if a specific row is selected, save it's status to see if the user changes it
		private void PartsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			partsSelectedCellsInfo = e.AddedCells;
			partsSelectedCells = new string[partsSelectedCellsInfo.Count];
			for (int i = 0; i < partsSelectedCellsInfo.Count; i++)
			{
				try
				{
					partsSelectedCells[i] = (partsSelectedCellsInfo[i].Column.GetCellContent(partsSelectedCellsInfo[i].Item)
					  as TextBlock).Text;
				}
				catch { partsSelectedCells[i] = ""; }
			}
		}

		private void AddPartButton_Click(object sender, RoutedEventArgs e)
		{
			Add_Window(Parts_Datatypes, "Part");
		}

		private void RefreshPaButton_Click(object sender, RoutedEventArgs e)
		{
			LoadParts();
		}

		//Edit Selected Part
		private void EditPaButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (PartsDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Part is Selected", "Select a Part");
				return;
			}

			//Display the Edit window
			Edit_Window(Parts_Datatypes, partsSelectedCells, "Part");
		}

		private async void DeletePaButton_Click(object sender, RoutedEventArgs e)
		{
			//if no row is selected
			if (PartsDataGrid.SelectedCells.Count == 0)
			{
				MessageBox.Show("No Part is Selected", "Select a Part");
				return;
			}

			Util.ModifiedItem deletedItem = new Util.ModifiedItem();
			deletedItem.TableName = "Part";
			deletedItem.PrimaryKeysHeaders = Util.GetTablePk(deletedItem.TableName).PrimaryKeys;
			deletedItem = Util.GetPKValues(deletedItem, Parts_Datatypes, partsSelectedCells); //get values of primary keys (deletedItem.PrimaryKeysValues)

			if (MessageBox.Show("Are you sure you want to delete the Part?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
			{
				await controller.DeleteItem(deletedItem);
				LoadParts();
			}
		}

		private void EditCliButton_Click_1(object sender, RoutedEventArgs e)
		{

		}
	}
}
