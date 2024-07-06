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
using System.Data.SqlClient;
using System.Data;
using DAL;
using BIZ;

namespace LagoonLearning
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        //back button function
        private void GoToBack(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HashPass hp = new HashPass();

            //checks if the boxes are empty
            if (txtFn.Text == "" ||txtSn.Text == "" || txtEmail.Text == "" || txtPhone.Text == "" || txtUser.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Please do not leave Blanks!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                try
                { 
                    //reads textboxes and assigns them as variables. Hashes the password using the hash class
                    string username = txtUser.Text;
                    string pass = txtPass.Text;
                    string Password = hp.Passhash(txtPass.Text);
                    string firstname = txtFn.Text;
                    string surname = txtSn.Text;
                    string Email = txtEmail.Text;
                    string Phone = txtPhone.Text;



                    //adds variables to the different sql tables

                    LoginDetails ld = new LoginDetails(username, Password, firstname);
                    Person p = new Person(firstname, surname, Email, Phone);



                    Data de = new Data();
                    de.RegistrationALL(firstname, surname, Email, Phone, username, Password);
                    MessageBox.Show("User Registered!", "User Created", MessageBoxButton.OK, MessageBoxImage.Information);

                    //clears form

                    txtFn.Clear();
                    txtSn.Clear();
                    txtEmail.Clear();
                    txtPhone.Clear();
                    txtUser.Clear();
                    txtPass.Clear();
                }
                catch (Exception ex )
                {
                    MessageBox.Show($"Invalid entry. Please check your entries: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
