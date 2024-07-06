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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        Data de = new Data();
        DAO dao = new DAO();
        SqlDataReader dr;

        public Test()
        {
            InitializeComponent();
            Loaded += Test_Loaded; //Populate listbox on load
        }
        private void Test_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateListBox(); //Function that actions on load
        }
        public void PopulateListBox() //Function that populates ListBox
        {
            string user = SessionManager.CurrentUser; //uses the currently logged in user
            SqlCommand cmd = de.OpenCon().CreateCommand(); //use stored procedure with the user parameter
            cmd.CommandText = "uspGetStackByUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", user);
            dr = cmd.ExecuteReader(); //executes this command

            while (dr.Read()) //while loop to add all relevant rows into the listbox
            {
                string stk = dr["Stack"].ToString();
                flashcardTestListBox.Items.Add(stk);
            }
            de.CloseCon();
        }
        public void ChangeNumbering() //method that changes the progress numbers beneath the flashcards
        {
            int total = 0; //sets variables to 0 so they can be called in more scopes
            int current = 0;
            if (wrongList.Any()) //checks if there is anything in the second list
            {
                total = qaList.Count + wrongList.Count; //if there is, the total is the count of both lists
                if (!isShowingwrongList) //then to find the current card is based on what position it is in, ie first or second list
                {
                    current = currentIndex + 1; //adds one as index starts at 0
                    stackTitle.FontFamily = new FontFamily("Impact"); //changes/keeps the font to impact
                }
                else //if it is showing the second list
                {
                    current = currentIndex + 1 + qaList.Count; //current = total of first list + wherever it is in the second
                    stackTitle.Text = "Revision Time"; //sets title and style of title when user is revising
                    stackTitle.FontFamily = new FontFamily("MV Boli");
                }
            }
            else //if there is no second list, the total is only predicated on the first list
            {
                total = qaList.Count;
                current = currentIndex + 1;
            }
            progressText.Text = $"{current}/{total}"; //displays the current card out of the total

        }
        
        public void ResetScreen() //method to reset the textblocks on the page when the test is over
        {
            progressText.Text = "0/0";
            stackTitle.Text = "Test Yourself!";
            stackTitle.FontFamily = new FontFamily("Impact");
            flashcardTxt.Text = "";
        }

        List<Tuple<string, string>> qaList = new List<Tuple<string, string>>();
        private int currentIndex = 0; //positioning index of the flashcards so the cards can be moved through

        //this function adds the question and answer together as a tuple, then adds the tuple to a list with all other flashcards of the smae stack. 
        //the flashcards are selected based on the user who made them and the stack selected in the listbox
        public List<Tuple<string, string>> DisplayStack(string user, string stack) //method to display selected stack based on the one selected
        {
            qaList.Clear(); //clears the list if this method is called several times while app is being used ie they test multiple times
            SqlCommand cmd = de.OpenCon().CreateCommand(); //calls the stored procedure to display the flashcards based on the user and stack selected
            cmd.CommandText = "uspDisplayFlash";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@sk", stack);
            dr = cmd.ExecuteReader();

            while (dr.Read()) //for each entry, it adds the question and answer as a paired tuple so they can be switched back and forth for viewing
            {
                string question = dr["Question"].ToString();
                string answer = dr["Answer"].ToString();

                qaList.Add(new Tuple<string, string>(question, answer));
            }
            de.CloseCon();

            return qaList;
        }
        private void MenuHome_Click(object sender, RoutedEventArgs e) //menu item to go to home screen
        {
            this.Close();
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e) //menu item to shut down app
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void MenuStat_Click(object sender, RoutedEventArgs e) //menu item to open stats
        {
            Stats st = new Stats();
            st.Show();
        }
        private void GoToBack(object sender, RoutedEventArgs e) //menu item to go to back
        {
            this.Close();
        }

        private bool showingAnswer = false; //flag to know what side of the card is shown

        private void testStackBtn_Click(object sender, RoutedEventArgs e) //button event when stack is selected to be tested
        {
            try
            {
                string stack = flashcardTestListBox.SelectedItem.ToString(); //selected listbox item for stack
                string creator = SessionManager.CurrentUser; //current session user for user
                var qaList = DisplayStack(creator, stack); //calls add method from above with the variables grabbed in 2 lines above
                ResetScreen();
                if (flashcardTestListBox.SelectedItem != null) //if you have selected something, this will action
                {
                    currentIndex = 0; //sets index at beginning
                    wrongq = 0; //no wrong answers yet so set to 0
                    flashcardTxt.Text = qaList[currentIndex].Item1; //shows first items question
                    showingAnswer = false; //sets bools to the default positions
                    isShowingwrongList = false;
                    wrongList.Clear(); //clears the wrong list so only wrong ones of that run can be added
                    stackTitle.Text = stack.ToString(); // changes title of stack to the header for clarity
                    ChangeNumbering(); //calls the method to change the numbers
                }
                else //catch if user doesnt select a stack
                {
                    MessageBox.Show("Please select a flashcard stack first.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flashcard stack: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        List<Tuple<string, string>> wrongList = new List<Tuple<string, string>>(); //declaration of wronglist variable
        public int wrongq = 0; //declaration of wrong question counter
        public bool isShowingwrongList = false;  //bool that decides which list is being shown

        private void TestCorrect(object sender, RoutedEventArgs e) //button event when user gets the flashcard correct
        {
            try
            {
                int correctans = qaList.Count - wrongq; //declared the correct answer as total minus the wrong questions
                //the following deals with the different cases when this button is pressed
                if (!isShowingwrongList && currentIndex + 1 < qaList.Count) //if the first list is showing, and the current index is not at the end
                {
                    currentIndex++; //increments index
                    flashcardTxt.Text = qaList[currentIndex].Item1; //resets the textbox to the new index
                }
                else if (!isShowingwrongList && currentIndex + 1 == qaList.Count) //first list is showing and it's the end of qaList
                {
                    if (wrongList.Any()) //check if wrongList has any items
                    {
                        isShowingwrongList = true; //if it does, switch to displaying wrongList
                        currentIndex = 0; //reset currentIndex for wrongList
                        flashcardTxt.Text = wrongList[currentIndex].Item1; //display the first item from wrongList
                    }
                    else
                    {
                        //if there was no wrong answers and the end of the list is reached, it gives your score and an option to restart
                        de.AddScore(SessionManager.CurrentUser, flashcardTestListBox.SelectedItem.ToString(), correctans, qaList.Count, DateTime.Now);
                        MessageBoxResult result = MessageBox.Show($"Test complete. Your score is {correctans}/{qaList.Count}. Do you want to restart", "Info", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (result == MessageBoxResult.Yes)
                        {
                            testStackBtn_Click(sender, e); //if yes, calls test button event
                        }
                        else if (result == MessageBoxResult.No)
                        {
                            ResetScreen(); //if no, resets page
                            return;
                        }
                    }
                }
                else if (isShowingwrongList && currentIndex + 1 < wrongList.Count) //if wronglist is showing and it is not at the end
                {
                    currentIndex++; //increment index
                    flashcardTxt.Text = wrongList[currentIndex].Item1; //displays new index
                }
                else if (isShowingwrongList && currentIndex + 1 == wrongList.Count) //wronglist is showing and it is the end of wrongList
                {
                    //all items in wrongList have been displayed, test is complete, display results and offer restart
                    de.AddScore(SessionManager.CurrentUser, flashcardTestListBox.SelectedItem.ToString(), correctans, qaList.Count, DateTime.Now);
                    MessageBoxResult result = MessageBox.Show($"Test complete. Your score is {correctans}/{qaList.Count}. Do you want to restart", "Info", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        testStackBtn_Click(sender, e); //if yes, calls test button event
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        ResetScreen(); //if no, resets page
                        return;
                    }
                }

                ChangeNumbering(); //whatever the scenario, numbers must be changed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void TestIncorrect(object sender, RoutedEventArgs e) //if someone gets a wrong answer
        {
            try
            {
                int correctans = qaList.Count - wrongq; //correct answer = total count of list minus wrong questions
                //the following deals with the different cases when this button is pressed
                if (!isShowingwrongList && currentIndex + 1 < qaList.Count) //not showing wronglist, and not at the end of the first list
                {
                    wrongList.Add(new Tuple<string, string>(qaList[currentIndex].Item1, qaList[currentIndex].Item2)); //adds current flashcard to wrong list for revision
                    currentIndex++; //increments index
                    flashcardTxt.Text = qaList[currentIndex].Item1; //display new flashcard
                    wrongq++; //increments wrong question counter
                    
                }
                else if (!isShowingwrongList && currentIndex + 1 == qaList.Count) //if not showing wronglist and at the end of the qaList
                {
                    wrongList.Add(new Tuple<string, string>(qaList[currentIndex].Item1, qaList[currentIndex].Item2)); //adds current flashcard to wrong list for revision
                    wrongq++; //increments wrong question counter
                    isShowingwrongList = true; //switch to displaying wrongList
                    currentIndex = 0; //reset currentIndex for wrongList
                    flashcardTxt.Text = wrongList[currentIndex].Item1; //display the first item from wrongList
                    
                }
                else if (isShowingwrongList && currentIndex + 1 < wrongList.Count) // Still items in wrongList to display
                {
                    currentIndex++; //increments index
                    flashcardTxt.Text = wrongList[currentIndex].Item1; //display new flashcard
                }
                else if (isShowingwrongList && currentIndex + 1 == wrongList.Count) //showing wronglist and at the end of wrongList
                {
                    // All items in wrongList have been displayed, test is complete
                    de.AddScore(SessionManager.CurrentUser, flashcardTestListBox.SelectedItem.ToString() ,correctans, qaList.Count, DateTime.Now);
                    MessageBoxResult result = MessageBox.Show($"Test complete. Your score is {correctans}/{qaList.Count}. Do you want to restart", "Info", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        testStackBtn_Click(sender, e); //calls test button event
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        ResetScreen(); //resets screen
                        return;
                    }
                }

                ChangeNumbering(); //changes number beneath flashcards
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void flashcardTxt_MouseUp(object sender, RoutedEventArgs e) //clicking flashcard to show the opposing question/answer
        {
            if (qaList.Count == 0) //if there is no list, this method doesn't do anything
                return;
            try
            {
                //determine the source list based on whether wrongList is being shown
                var sourceList = isShowingwrongList ? wrongList : qaList;

                //toggle the display state between question and answer
                if (showingAnswer)
                {
                    //switch to showing the question with wanted style
                    flashcardTxt.FontStyle = FontStyles.Normal;
                    flashcardTxt.FontWeight = FontWeights.Bold;
                    flashcardTxt.Text = sourceList[currentIndex].Item1; // Show question
                }
                else
                {
                    //switch to showing the answer
                    flashcardTxt.FontStyle = FontStyles.Italic;
                    flashcardTxt.FontWeight = FontWeights.Light;
                    flashcardTxt.Text = sourceList[currentIndex].Item2; // Show answer
                }

                showingAnswer = !showingAnswer; //toggle the showingAnswer flag to opposite

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occured: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
