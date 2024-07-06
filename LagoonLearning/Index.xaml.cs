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
using System.Timers;
using System.Diagnostics;
using System.Windows.Navigation;

namespace LagoonLearning
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        static DateTime targetTime;
        static Timer backtimer;
        
        public Index()
        {
            InitializeComponent();
            //Create backend Dispatch timer and setting the text to 00:00:00
            TimerTxt.Text = ("00:00:00");
            backtimer = new Timer(1000);
            backtimer.Elapsed += Timer_Elapsed;
            PauseBtn.Visibility = Visibility.Visible; //displays correct play/pause state
            PlayBtn.Visibility = Visibility.Collapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //This was written for testing to check the timer was running as the timer is only a backend function for testing
            Debug.WriteLine("tick");
            UpdateText();
        }

        private void UpdateText()
        {
            //Function that will connect the backend timer to the frontend label
            Application.Current.Dispatcher.Invoke(() =>
            {
                var ts = targetTime - DateTime.Now;

                //if statement to display minutes on the label if requested minutes is more than 0
                if (ts.TotalSeconds > 0)
                {
                    TimerTxt.Text = ts.ToString(@"hh\:mm\:ss");
                }
                else
                {
                    backtimer.Stop();
                    TimerTxt.Text = "00:00:00";
                    MessageBox.Show("Timer Done! Take a Break!", "Timer Notice", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

            });
        }
        private int desiredMinutes = 0; //default minute counter is 0

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //default sets it to 0 and updates text to show no minutes on the timer
                desiredMinutes = 0;
                targetTime = DateTime.Now.AddMinutes(desiredMinutes);
                TimerTxt.Text = "00:00:00";
                backtimer.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occurred: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private TimeSpan RemainingTime;

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Pause button that will stop front end timer and backend simultaneously
                if (backtimer.Enabled)
                {
                    backtimer.Stop();
                    RemainingTime = targetTime - DateTime.Now;
                    PauseBtn.Visibility = Visibility.Collapsed; //show correct button based on if timer is on or off
                    PlayBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    targetTime = DateTime.Now + RemainingTime;
                    backtimer.Start();
                    UpdateText(); //updates text timer in unison with backend timer
                    PlayBtn.Visibility = Visibility.Collapsed; //show correct button based on if timer is on or off
                    PauseBtn.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occurred: {ex} Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //all menu items that lead to the different app pages
        private void MenuLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void MenuStudy_Click(object sender, RoutedEventArgs e)
        {
            Study st = new Study();
            st.Show();
        }

        private void MenuCreate_Click(object sender, RoutedEventArgs e)
        {
            Create cr = new Create();
            cr.Show();
        }

        private void MenuShare_Click(object sender, RoutedEventArgs e)
        {
            Share sh = new Share();
            sh.Show();
        }
        private void MenuTest_Click(object sender, RoutedEventArgs e)
        {
            Test te = new Test();
            te.Show();
        }
        private void MenuPub_Click(object sender, RoutedEventArgs e)
        {
            Public pu = new Public();
            pu.Show();
        }
        private void MenuStats_Click(object sender, RoutedEventArgs e)
        {
            Stats st = new Stats();
            st.Show();
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void GoToCreatePage(object sender, MouseButtonEventArgs e)
        {
            Create cr = new Create();
            cr.Show();
        }
        private void GoToStudyPage(object sender, MouseButtonEventArgs e)
        {
            Study st = new Study();
            st.Show();
        }
        private void GoToSharePage(object sender, MouseButtonEventArgs e)
        {
            Share sh = new Share();
            sh.Show();
        }
        private void GoToPublicPage(object sender, MouseButtonEventArgs e)
        {
            Public pu = new Public();
            pu.Show();
        }
        private void GoToTestPage(object sender, MouseButtonEventArgs e)
        {
            Test te = new Test();
            te.Show();
        }
        
        private void StartButton_Click(object sender, RoutedEventArgs e) //start button that starts the front end timer and backend timer
        {
            if (MinuteTxt.Text == string.Empty && desiredMinutes == 0)
            {
                MessageBox.Show("Please submit a number of minutes. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                targetTime = DateTime.Now.AddMinutes(desiredMinutes);
                backtimer.Start();
                UpdateText();
            }
            
        }


        private void SubmitButton_Click(object sender, RoutedEventArgs e) //on event of submitting minutes for countdown
        {
            //declaration of variables
            string input = MinuteTxt.Text;
            int newMinutes = 0;
            int hours, minutes;

            //code that trys to parse inputted minutes into the timer, if not it returns an error
            try
            {
                if (int.TryParse(input, out newMinutes)) //tries input to check whether it is a number
                {
                    if (backtimer.Enabled) //stops timer if it's on
                    {
                        backtimer.Stop();
                    }

                    desiredMinutes = newMinutes;
                    targetTime = DateTime.Now.AddMinutes(newMinutes); //adds the minutes entered into the timer

                    //this if-else statement changes minutes if they are over 60 to an hourly display format
                    if (newMinutes >= 60)
                    {
                        hours = newMinutes / 60;
                        minutes = newMinutes % 60;
                        TimerTxt.Text = $"{hours:00}:{minutes:00}:00";
                    }
                    else
                    {
                        TimerTxt.Text = $"00:{newMinutes:00}:00";
                    }
                    MinuteTxt.Clear(); //clears input box
                }
                else //result if input value is not a number
                {
                    MessageBox.Show("Inputted Value is not an accepted number. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    MinuteTxt.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error has occurred: {ex} Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
