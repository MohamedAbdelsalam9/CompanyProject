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
    public partial class MainWindowClient : Window
    {
		/// General Function
		Controller controller = new Controller();

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


		public MainWindowClient()
        {
            InitializeComponent();
		}

		//Generate Columns
		private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Tabs.SelectedIndex == 0) //Services
			{
				ServicesGrid.Visibility = Visibility.Visible;
				PartsGrid.Visibility = Visibility.Collapsed;
				LoadServices();
			}
			else if (Tabs.SelectedIndex == 1) //Parts
			{
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
			if (e.ChangeCommited && e.TableName.Contains("Service"))
				LoadServices();
			else if (e.ChangeCommited && e.TableName.Contains("Part"))
				LoadParts();
		}


		///Admin, Employees and Clients (with some exceptions)
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

		private void RefreshSerButton_Click(object sender, RoutedEventArgs e)
		{
			LoadServices();
		}


		///Admin, Employees and Clients (with some exceptions)
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
			Parts_Datatypes = Parts_Datatypes.Take(2).ToList();
			partsSelectedCells = partsSelectedCells.Take(2).ToArray();
		}

		private void RefreshPaButton_Click(object sender, RoutedEventArgs e)
		{
			LoadParts();
		}

		private async void CurrentStockButton_Click(object sender, RoutedEventArgs e)
		{
			DataTable SerEmpCliStats = await controller.GetStock();
			StatisticsWindow statisticsWindow = new StatisticsWindow(SerEmpCliStats);
			statisticsWindow.Show();
		}
	}
}
