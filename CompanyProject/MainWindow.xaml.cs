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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyProject
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Controller controller = new Controller();
		public MainWindow()
		{
			InitializeComponent();
		}
		
		public async Task<int> AddEmplyees()
		{
			DateTime Date1 = new DateTime(1994, 07, 16);
			return await controller.AddEmployee(10, "asdasd", "Admin", 12432423, "Good", Date1, "Male",
				"sdasd@sdasd", Date1, "Mohamed", 1, Date1, 1);
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			int x = await AddEmplyees();
			label1.Content = x.ToString();
		}
	}
}
