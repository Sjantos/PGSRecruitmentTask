using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PGSRecruitmentTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersonalDataCollector capturedData;
        bool[] tabsDataCorrect;

        public MainWindow()
        {
            InitializeComponent();
            capturedData = new PersonalDataCollector();
            //States of tabs, false turn true, when data in this tab is correct
            tabsDataCorrect = new bool[] { false, false, false, false };
        }

        /// <summary>
        /// Go to previous tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrevious_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() => mainTabControl.SelectedIndex = mainTabControl.SelectedIndex - 1));
        }

        /// <summary>
        /// Go to next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() => mainTabControl.SelectedIndex = mainTabControl.SelectedIndex + 1));
        }

        /// <summary>
        /// Check what tab is visible, hide or show buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstNameTab.IsSelected)
            {
                buttonPrevious.Visibility = Visibility.Hidden;
                enableOrDisableTab(0);
            }
            else if (LastNameTab.IsSelected)
            {
                buttonPrevious.Visibility = Visibility.Visible;
                enableOrDisableTab(1);
            }
            else if (AddressTab.IsSelected)
                enableOrDisableTab(2);
            else if (PhoneNumberTab.IsSelected)
                enableOrDisableTab(3);
            else if (SummaryTab.IsSelected)
            {
                //When summary tab is reached, block all tabs and hide buttons
                labelSummary.Content = capturedData.ToString();
                buttonPrevious.Visibility = Visibility.Hidden;
                buttonNext.Visibility = Visibility.Hidden;
                for (int i = 0; i < mainTabControl.SelectedIndex; i++)
                    (mainTabControl.Items[i] as TabItem).IsEnabled = false;
            }
        }

        /// <summary>
        /// Check if first name is correct and enable or disable next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textboxFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (capturedData.TakeName(textboxFirstName.Text))
            {
                enableNextStep();
                tabsDataCorrect[0] = true;
            }
            else
            {
                tabsDataCorrect[0] = false;
                disableNextStep("Błędne dane");
            }
        }

        /// <summary>
        /// Check if last name is correct and enable or disable next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textboxLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (capturedData.TakeLastName(textboxLastName.Text))
            {
                enableNextStep();
                tabsDataCorrect[1] = true;
            }
            else
            {
                tabsDataCorrect[1] = false;
                disableNextStep("Błędne dane");
            }
        }

        /// <summary>
        /// Check if all address data is correct and enable or disable next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addressTextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            string[] input = new string[] { textboxStreet.Text, textboxHouseNumber.Text, textboxFlatNumber.Text, textboxCity.Text, textboxZipCode.Text };
            if (capturedData.TakeAddress(input))
            {
                enableNextStep();
                tabsDataCorrect[2] = true;
            }
            else
            {
                tabsDataCorrect[2] = false;
                disableNextStep("Błędne dane");
            }
        }

        /// <summary>
        /// Check if phone number is correct and enable or disable next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textboxPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (capturedData.TakePhoneNumber(textboxPhoneNumber.Text))
            {
                enableNextStep();
                tabsDataCorrect[3] = true;
            }
            else
            {
                tabsDataCorrect[3] = false;
                disableNextStep("Błędne dane");
            }
        }

        /// <summary>
        /// Disable next tab and button Next (incorrect data)
        /// </summary>
        /// <param name="text"></param>
        private void disableNextStep(string text)
        {
            buttonNext.Content = text;
            buttonNext.IsEnabled = false;
            TabItem nextTabItem = mainTabControl.Items[mainTabControl.SelectedIndex + 1] as TabItem;
            nextTabItem.IsEnabled = false;
        }

        /// <summary>
        /// Enable next tab and button Next
        /// </summary>
        private void enableNextStep()
        {
            buttonNext.Content = "Dalej";
            buttonNext.IsEnabled = true;
            TabItem nextTabItem = mainTabControl.Items[mainTabControl.SelectedIndex + 1] as TabItem;
            nextTabItem.IsEnabled = true;
        }

        /// <summary>
        /// Set next tab to enable if data is correct
        /// </summary>
        /// <param name="index"></param>
        private void enableOrDisableTab(int index)
        {
            if (tabsDataCorrect[index])
                enableNextStep();
            else
                disableNextStep("Błędne dane");
        }

        /// <summary>
        /// Handles enter press to go to the next tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                Dispatcher.BeginInvoke((Action)(() => mainTabControl.SelectedIndex = mainTabControl.SelectedIndex + 1));
        }
    }
}
