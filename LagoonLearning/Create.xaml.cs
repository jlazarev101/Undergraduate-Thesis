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

namespace LagoonLearning
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            ToggleListBox();
        }
        //this will disable buttons if there are no flashcard entries
        private void ToggleListBox()
        {
            removeSelectedFlashcardBtn.IsEnabled = flashcardsStackListBox.Items.Count > 0;
            saveFlashcardSetBtn.IsEnabled = flashcardsStackListBox.Items.Count > 0;
        }
        //sends user to different pages based on selected menu items
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
            this.Hide();
        }

        //this will add the flashcard to the listbox to be viewed and assembled
        private void AddFlashcardToSetBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(questTxt.Text) || string.IsNullOrWhiteSpace(answerTxt.Text)) //checks if there is blanks
                {
                    MessageBox.Show("Please do not leave blanks!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Flashcard flash = new Flashcard(questTxt.Text, answerTxt.Text,setNameText.Text,SessionManager.CurrentUser); //adds the flashcard with the user attribute
                flashcardsStackListBox.Items.Add(flash);

                questTxt.Clear();
                answerTxt.Clear();

                ToggleListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occured: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //this will remove the selected flashcard from the list
        private void RemoveSelectedFlashcardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int clickedpos = flashcardsStackListBox.SelectedIndex;
                flashcardsStackListBox.Items.RemoveAt(clickedpos);

                //to find a new list box index to select
                clickedpos = (clickedpos == 0 && flashcardsStackListBox.Items.Count >= 1) ? clickedpos : --clickedpos;
                if (clickedpos >= 0)
                {
                    flashcardsStackListBox.SelectedItem = flashcardsStackListBox.Items.GetItemAt(clickedpos);
                }
                questTxt.Clear();
                answerTxt.Clear();

                ToggleListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occured: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //this will save all the flashcards in the listbox as one row each. It will take each listbox entry in turn and add them one by one
        private void SaveFlashcardSetBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string question = questTxt.Text;
                string answer = answerTxt.Text;
                string stack = setNameText.Text;
                string creator = SessionManager.CurrentUser;

                foreach(Flashcard flashcard in flashcardsStackListBox.Items)
                {
                    Data de = new Data();
                    de.FlashcardCreate(flashcard.Question, flashcard.Answer, flashcard.Stack, flashcard.Creator);
                }

                MessageBox.Show("Flashcard Stack saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                questTxt.Clear();
                answerTxt.Clear();
                setNameText.Clear();
                flashcardsStackListBox.Items.Clear();

            }
            catch (Exception)
            {
                MessageBox.Show("Invalid entry. Please check details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //this will, on selection, bring up previous created flashcards to be viewed in their entirety
        private void flashcardSetList_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = flashcardsStackListBox.SelectedItem as Flashcard;
            if (selectedItem != null)
            {
                questTxt.Text = selectedItem.Question;
                answerTxt.Text = selectedItem.Answer; 
            }
        }
        //method to clear the text fields when clicked
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            questTxt.Clear();
            answerTxt.Clear();
        }
    }
}
