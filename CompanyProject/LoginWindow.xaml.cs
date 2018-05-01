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

		private async void Login()
		{
			string username = UsernameBox.Text;
			string password = PasswordBox.Text;
			int loginResult = await controller.CheckUser(username, password);
			if (loginResult == 1)
				SignSucceeded(); ///
			else
				SignFailed();
		}

		private void SignFailed()
		{
			SignInProblemLabel.Visibility = Visibility.Visible;
		}

		private void SignSucceeded() ///
		{
			SignInProblemLabel.Visibility = Visibility.Visible;
			SignInProblemLabel.Content = "Sign in succeeded";
		}

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			Login();
		}

		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			////Goto_Registration_Page();
		}
	}
}
