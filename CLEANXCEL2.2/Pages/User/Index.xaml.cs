using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CLEANXCEL2._2.Pages.User
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>

    public partial class Index : Page
    {
        // string LOCAL_PAGE = "Login.xaml";
        public static Index AppWindow;
        public Index()
        {
            InitializeComponent();
            AppWindow = this;
            Console.WriteLine("Pages/User/Index Started");
        }

        private void FrameContainerLoaded(object sender, RoutedEventArgs e)
        {
            RBSignIn.IsChecked = true;
        }

        private void LoadPage(object sender, EventArgs e)
        {
            Globals.SUBPAGE_REQUEST();
            //Storyboard SB = (Storyboard)FindResource("ShowFrame");
            //SB.Begin();
            Globals.SUBPAGE_URL = "";
        }

        public void HidePage()
        {
            Storyboard SB = (Storyboard)FindResource("HideFrame");
            SB.Begin();
        }

        private void RBSignIn_Checked(object sender, RoutedEventArgs e)
        {
            if (Globals.SUBPAGE_URL != "Login.xaml")
            {
                Globals.SUBPAGE_URL = "Login.xaml";
                HidePage();
            }
        }

        private void RBRegistration_Checked(object sender, RoutedEventArgs e)
        {
            if (Globals.SUBPAGE_URL != "Register.xaml")
            {
                Globals.SUBPAGE_URL = "Register.xaml";
                HidePage();
            }
        }

        private void RBPassword_Checked(object sender, RoutedEventArgs e)
        {
            if (Globals.SUBPAGE_URL != "Reset.xaml")
            {
                Globals.SUBPAGE_URL = "Reset.xaml";
                HidePage();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            try
            {
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("1,49,55");

                RBSignIn.Content = hashtable[1].ToString();
                RBRegistration.Content = hashtable[55].ToString();
                RBPassword.Content = hashtable[49].ToString();
            }
            catch { }
        }

        private void CbxMainLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(((ComboBoxItem)CbxMainLanguage.SelectedItem).Content.ToString())
            {
                case "English":
                    MainWindow.language = "english";
                    break;
                case "中文 (简体)":
                    MainWindow.language = "mandarin";
                    break;
                case "Français":
                    MainWindow.language = "english";//"french";
                    break;
                case "Deutsche":
                    MainWindow.language = "english";//"german";
                    break;
                case "日本語":
                    MainWindow.language = "english";//"japanese";
                    break;
            }
            FrameLocalContainer.Refresh();
            LoadLanguage();
        }
    }
}
