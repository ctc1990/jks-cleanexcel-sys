﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CLEANXCEL2._2.Pages.Menu.Alarm
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        public static Index AppWindow;
        public Index()
        {
            InitializeComponent();
            AppWindow = this;
        }

        private void RBCurrent_Checked(object sender, RoutedEventArgs e)
        {
            if (MENUPAGE_URL != "CurrentAlarm.xaml")
            {
                MENUPAGE_URL = "CurrentAlarm.xaml";
                HidePage();
            }
        }

        private void RBHistory_Checked(object sender, RoutedEventArgs e)
        {
            if (MENUPAGE_URL != "HistoryAlarm.xaml")
            {
                MENUPAGE_URL = "HistoryAlarm.xaml";
                HidePage();
            }
        }

        public static String MENUPAGE_URL = null; // Modifiable

        public static void MENUPAGE_REQUEST()
        {
            if (MENUPAGE_URL != null)
            {
                AppWindow.FrameLocalContainer.Source = new Uri(MENUPAGE_URL, UriKind.RelativeOrAbsolute);
                Storyboard SB = (Storyboard)AppWindow.FindResource("ShowFrame");
                SB.Begin();
                Console.WriteLine("MENUPAGE_REQUEST : Completed.");
            }
            else
            {
                Console.WriteLine("MENUPAGE_REQUEST : MENUPAGE_URL is not defined.");
            }
        }

        private void FrameLocalContainer_Loaded(object sender, RoutedEventArgs e)
        {
            MENUPAGE_URL = "CurrentAlarm.xaml";
            RBCurrent.IsChecked = true;
            MENUPAGE_REQUEST();
        }

        public void HidePage()
        {
            Storyboard SB = (Storyboard)FindResource("HideFrame");
            SB.Begin();
        }

        private void LoadPage(object sender, EventArgs e)
        {
            MENUPAGE_REQUEST();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            try
            {
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("85,95,99,100");

                PageTitle.Text = hashtable[85].ToString();
                PageDescription.Text = hashtable[95].ToString();
                RBCurrent.Content = hashtable[99].ToString();
                RBHistory.Content = hashtable[100].ToString();
            }
            catch
            { }
        }
    }
}
