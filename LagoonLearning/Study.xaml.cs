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
    /// Interaction logic for Study.xaml
    /// </summary>
    public partial class Study : Window
    {
        
        Data de = new Data();
        DAO dao = new DAO();
        SqlDataReader dr;

        public Study()
        {
            InitializeComponent();

            Loaded += Study_Loaded;

        }
        //loads listbox on page load  
        private void Study_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateListBox();
        }

        //taskbar functions
        private void MenuHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void MenuPub_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Public pu = new Public();
            pu.Show();
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

        //this function populates the listbox
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
                flashcardSetListBox.Items.Add(stk);
            }
            de.CloseCon();
        }

        //changes the small numbers underneath the flashcards to show you your progress
        public void ChangeNumbering()
        {
            int total, current;

            total = qaList.Count;
            current = currentIndex + 1;

            progressText.Text = $"{current}/{total}";

        }

        //list declaration for flashcard display.
        List<Tuple<string, string>> qaList = new List<Tuple<string, string>>();

        //this function adds the question and answer together as a tuple, then adds the tuple to a list with all other flashcards of the smae stack. 
        //the flashcards are selected based on the user who made them and the stack selected in the listbox
        public List<Tuple<string, string>> DisplayStack(string user, string stack)
        {
            qaList.Clear();
            SqlCommand cmd = de.OpenCon().CreateCommand();
            cmd.CommandText = "uspDisplayFlash";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@sk", stack);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string question = dr["Question"].ToString();
                string answer = dr["Answer"].ToString();

                qaList.Add(new Tuple<string, string>(question, answer));
            }
            de.CloseCon();

            return qaList;
        }
        //declaring indexing for the stack. this will aid rotation of the stack and keep the cards together.
        private int currentIndex = 0;

        //the load button displays 
        private void loadStackBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string stack = flashcardSetListBox.SelectedItem.ToString();
                string creator = SessionManager.CurrentUser;
                var qaList = DisplayStack(creator, stack);

                if (flashcardSetListBox.SelectedItem != null)
                {
                    currentIndex = 0;
                    flashcardTxt.Text = qaList[currentIndex].Item1;
                    showingAnswer = false;
                    stackTitle.Text = stack.ToString();
                    ChangeNumbering();
                }
                else
                {
                    MessageBox.Show("Please select a flashcard stack first.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flashcard stack: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private bool showingAnswer = false;
        //action of clicking on the card to flip over
        private void flashcardTxt_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //checking if there is cards in the deck. 
            if (qaList.Count == 0) 
                return;
            try
            {
                //toggle between showing the question and the answer
                if (showingAnswer)
                {
                    //if currently showing the answer, switch to the question
                    flashcardTxt.FontStyle = FontStyles.Normal;
                    flashcardTxt.FontWeight = FontWeights.Bold;
                    flashcardTxt.Text = qaList[currentIndex].Item1;
                    showingAnswer = false; // Update flag to indicate the question is now being displayed
                }
                else
                {
                    //if currently showing the question, switch to the answer
                    flashcardTxt.FontStyle = FontStyles.Italic;
                    flashcardTxt.FontWeight = FontWeights.Light;
                    flashcardTxt.Text = qaList[currentIndex].Item2;
                    showingAnswer = true; // Update flag to indicate the answer is now being displayed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occured: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void PreviousFlashcardButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //action on previous button click. if the index goes to below zero on the click, it reverts the index to the count of flashcards -1 to account for
                // indexing starting at 0
                if (currentIndex > 0)
                {
                    currentIndex--; //Move to previous index
                    flashcardTxt.FontStyle = FontStyles.Normal;
                    flashcardTxt.FontWeight = FontWeights.Bold;
                    flashcardTxt.Text = qaList[currentIndex].Item1;
                    showingAnswer = false;
                }
                else
                {
                    currentIndex = qaList.Count - 1;
                    flashcardTxt.Text = qaList[currentIndex].Item1;
                }
                ChangeNumbering();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occured: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void NextFlashcardButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //action on next button click. if the index is at most one underneath the max, it will increment,
                //else, it will revert to 0, or the first flashcard in the deck 
                if (currentIndex + 1 < qaList.Count)
                {
                    currentIndex++; // Move to the next index
                    flashcardTxt.FontStyle = FontStyles.Normal;
                    flashcardTxt.FontWeight = FontWeights.Bold;
                    flashcardTxt.Text = qaList[currentIndex].Item1;
                    showingAnswer = false;
                }
                else
                {
                    currentIndex = 0;
                    flashcardTxt.Text = qaList[currentIndex].Item1;
                }
                ChangeNumbering();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occured: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }

        private void deleteStackBtn_Click(object sender, RoutedEventArgs e)
        {
            //this will call all flashcards under the user and stack selected and will delete them from the table
            try
            {
                string stack = flashcardSetListBox.SelectedItem.ToString();
                string creator = SessionManager.CurrentUser;

                if (flashcardSetListBox.SelectedItem != null)
                {

                    flashcardSetListBox.Items.Remove(flashcardSetListBox.SelectedItem);
                    Data de = new Data();
                    de.DeleteStack(creator, stack);

                    MessageBox.Show("Flashcard Stack deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    flashcardTxt.Text = "";
                }
                else
                {
                    MessageBox.Show("Please select a flashcard stack to delete.", "Selection Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Invalid entry. Please check details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
