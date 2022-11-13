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

namespace CLEANXCEL2._2.Pages.Menu.Operation
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
            MENUPAGE_URL = "Operation.xaml";
            RBOperation.IsChecked = true;
            MENUPAGE_REQUEST();
        }

        public void HidePage()
        {
            Storyboard SB = (Storyboard)FindResource("HideFrame");
            SB.Begin();
        }

        private void RBOperation_Checked(object sender, RoutedEventArgs e)
        {
            if (MENUPAGE_URL != "Operation.xaml")
            {
                MENUPAGE_URL = "Operation.xaml";
                HidePage();
            }
        }

        private void RBSchematics_Checked(object sender, RoutedEventArgs e)
        {

            if (MENUPAGE_URL != "../CurrentStatus/Schematics.xaml")
            {
                MENUPAGE_URL = "../CurrentStatus/Schematics.xaml";
                HidePage();
            }
        }

        //private void RBSensor_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (MENUPAGE_URL != "../Maintenance/Sensor.xaml")
        //    {
        //        MENUPAGE_URL = "../Maintenance/Sensor.xaml";
        //        HidePage();
        //    }
        //}

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
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("35,93,103,104");

                PageTitle.Text = hashtable[35].ToString();
                PageDescription.Text = hashtable[93].ToString();
                RBOperation.Content = hashtable[35].ToString();
                RBSchematics.Content = hashtable[103].ToString();
                //RBSensor.Content = hashtable[104].ToString();
            }
            catch { }
        }
    }
}
