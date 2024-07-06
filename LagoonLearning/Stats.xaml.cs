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
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        Data de = new Data();
        DAO dao = new DAO();
        SqlDataReader dr;

        public Stats()
        {
            InitializeComponent();
            Loaded += Stats_Loaded;
        }
        private void GoToBack(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void Stats_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
             //acts on page load
        }
        private void PopulateListView() //method to populate listview
        {
            ObservableCollection<ScoreEntry> scores = new ObservableCollection<ScoreEntry>();
            StatsListView.ItemsSource = scores; //populates listview with the variables added in while loop

            string user = SessionManager.CurrentUser; //uses the currently logged in user

            SqlCommand cmd = de.OpenCon().CreateCommand(); //use stored procedure with the user parameter
            cmd.CommandText = "uspGetScoreByUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user", user);
            dr = cmd.ExecuteReader(); //executes this command

            while (dr.Read()) //while loop to add all relevant rows into the listbox
            {
                string stk = dr["Stack"].ToString();
                string cans = dr["RightAnswer"].ToString();
                string tot = dr["Total"].ToString();
                string dat = Convert.ToDateTime(dr["Date"]).ToString("d"); //returns date only as the date without time for cleaner look
                string score = $"{cans}/{tot}"; //puts correct answers out of total giving a "score"

                scores.Add(new ScoreEntry { Stack = stk, Score = score, Date = dat }); //assigns the values to the data binding of the columns in the listview
            }
            de.CloseCon();

        }


    }
}
