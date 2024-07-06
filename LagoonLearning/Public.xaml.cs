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
using System.Collections.ObjectModel;

namespace LagoonLearning
{
    /// <summary>
    /// Interaction logic for Public.xaml
    /// </summary>
    public partial class Public : Window
    {
        Data de = new Data();
        DAO dao = new DAO();
        SqlDataReader dr;
        

        public Public()
        {
            InitializeComponent();
            Loaded += Public_Loaded;
        }
        //page load function
        private void Public_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateListView();
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
        private void MenuShare_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Share sh = new Share();
            sh.Show();
        }
        private void MenuTest_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Test te = new Test();
            te.Show();
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

        public void PopulateListView()
        {
            ObservableCollection<PublicCard> pubstacks = new ObservableCollection<PublicCard>();
            flashcardPublicListView.ItemsSource = pubstacks; //populates listview with the variables added in while loop

            //This needs to select all flashcards from the public table and display them
            SqlCommand cmd = de.OpenCon().CreateCommand();
            cmd.CommandText = "uspGetPublicStacks";
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();

            while (dr.Read()) //adds the stack and username to the listbox for display
            {
                string stk = dr["PStack"].ToString();
                string user = dr["Username"].ToString();

                pubstacks.Add(new PublicCard { Title = stk, Author = user});
            }
            de.CloseCon();
        }

        private void importStackBtn_Click(object sender, RoutedEventArgs e) //import function to grab the flashcard for your own study
        {
            try
            {
                string user = SessionManager.CurrentUser;

                if (flashcardPublicListView.SelectedItem is PublicCard selectedFlash)
                {
                    string stack = selectedFlash.Title;
                    string author = selectedFlash.Author;

                    if (flashcardPublicListView.SelectedItem != null & selectedFlash.Author != user) //if variables are not null
                    {
                        Data de = new Data();
                        de.ImportStack(user, stack, author); //use method with above variables to import the stack to the current user's flashcard library

                        MessageBox.Show($"Stack from {author} successfully shared!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else //tell user to select stacks that they have not created to prevent repeat entries
                    {
                        MessageBox.Show("Error. Please select a stack you have not created", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
