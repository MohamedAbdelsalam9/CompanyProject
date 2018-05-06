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
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		Controller controller;
		public LoginWindow()
		{
			InitializeComponent();
			controller = new Controller();
		}

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			if (controller.connectionStatus == false)
			{
				MessageBox.Show("Couldn't connect to the database", "Connection Error");
				this.Close();
			}
			Login();
		}

		//try login given the written username and password
		private async void Login()
		{
			string username = UsernameBox.Text;
			string password = PasswordBox.Password;
			int loginResult = await controller.CheckUser(username, password);
			if (loginResult == 1)
				SignSucceeded(); 
			else
				SignFailed();
		}

		//If sign in failed
		private void SignFailed()
		{
			SignInProblemLabel.Visibility = Visibility.Visible;
		}

		//if sign in succeeded
		private void SignSucceeded()
		{
			MainWindowAdmin mainWindow = new MainWindowAdmin();
			mainWindow.Show();
			this.Close();
		}
	}
}
