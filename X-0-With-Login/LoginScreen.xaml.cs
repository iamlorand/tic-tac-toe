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

namespace X_0_With_Login
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string user = "test";
            string pw = "123";
            string usrname = username.Text;

            if ((usrname == user) && (pw == password.Password))
            {
                MessageBox.Show("Logged In");
                MainWindow mainWindow = new MainWindow();
                mainWindow.isLogged = true;
                mainWindow.username = usrname;
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect!");
            }
        }
    }
}
