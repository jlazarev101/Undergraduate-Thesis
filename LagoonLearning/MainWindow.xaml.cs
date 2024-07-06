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
using System.Data.SqlClient;
using System.Data;
using DAL;
using BIZ;


namespace LagoonLearning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        HashPass hp = new HashPass();
        Data de = new Data();
        DAO dao = new DAO();
        string user, pass;
        string login = string.Empty;

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration rp = new Registration();
            rp.Show();
        }


        //login code that checks if the login details correspond with the entries in the table by calling an external method stored in the DATA class.
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool loginSuccessful;
            user = txtUserLog.Text;
            pass = hp.Passhash(txtPassLog.Password);
            loginSuccessful = de.GetUser(user, pass);

            try
            {
                if (loginSuccessful == true)
                {
                    MessageBox.Show($"Welcome Back, {user}", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    Index ix = new Index();
                    ix.Show();
                    SessionManager.Login(user);
                    this.Close();
                    txtUserLog.Clear();
                    txtPassLog.Clear();

                }
                else
                {
                    MessageBox.Show("Invalid Credentials. Try Again or Register a new Account.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtUserLog.Clear();
                    txtPassLog.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error. Please try again: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        











    }
}
