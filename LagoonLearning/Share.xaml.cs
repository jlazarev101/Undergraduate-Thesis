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
using BIZ;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LagoonLearning
{
    /// <summary>
    /// Interaction logic for Share.xaml
    /// </summary>
    public partial class Share : Window
    {
        Data de = new Data();
        DAO dao = new DAO();
        SqlDataReader dr;

        public Share()
        {
            InitializeComponent();
            Loaded += Share_Loaded;
        }
        //populate listbox on load
        private void Share_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //give menu items functionality
        private void MenuHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuStudy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Study st = new Study();
            st.Show();
        }

        private void MenuCreate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Create cr = new Create();
            cr.Show();
        }
        private void MenuPublic_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Public pb = new Public();
            pb.Show();
        }
        private void MenuStat_Click(object sender, RoutedEventArgs e)
        {
            Stats st = new Stats();
            st.Show();
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void GoToBack(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void MenuTest_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Test te = new Test();
            te.Show();
        }
        private void MenuPub_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Public pu = new Public();
            pu.Show();
        }

        //same method as study page, to populate list box with stacks created by the user
        public void PopulateListBox()
        {
            string user = SessionManager.CurrentUser;
            SqlCommand cmd = de.OpenCon().CreateCommand();
            cmd.CommandText = "uspGetStackByUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", user);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string stk = dr["Stack"].ToString();
                flashcardShareListBox.Items.Add(stk);
            }
            de.CloseCon();
        }

        //button on click shares selected stack to inputted user
        private void shareStackBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string stack = flashcardShareListBox.SelectedItem.ToString();
                string creator = SessionManager.CurrentUser;
                string sharee = shareStackTxt.Text;

                Data de = new Data();
                bool userExists = de.CheckUser(sharee);
                if (userExists == true)
                {
                    de.ShareStack(creator, stack, sharee);
                    MessageBox.Show($"Stack successfully shared to {sharee}!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    shareStackTxt.Clear();
                }
                else
                {
                    MessageBox.Show("Error, User not found. Double check entry", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, User not found: {ex}","Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }



        }

        private void sharePublicBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string stack = flashcardShareListBox.SelectedItem.ToString();
                string creator = SessionManager.CurrentUser;

                Data de = new Data();
                if (flashcardShareListBox.SelectedItem != null)
                {
                    de.PublishStack(creator, stack);
                    MessageBox.Show($"Stack successfully shared to Public!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    shareStackTxt.Clear();
                }
                else
                {
                    MessageBox.Show("Error, Please select stack. Double check entry", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:: {ex}, Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}
