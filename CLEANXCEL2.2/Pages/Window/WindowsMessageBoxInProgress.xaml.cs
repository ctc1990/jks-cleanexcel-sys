using MySql.Data.MySqlClient;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CLEANXCEL2._2.Pages.Window
{
    /// <summary>
    /// Interaction logic for WindowsMessageBox.xaml
    /// </summary>
    public partial class WindowsMessageBoxInProgress : Page
    {
        private static string s_caption;
        private static string s_message;
        private static State s_state;
        public static WindowsMessageBoxInProgress AppWindow;

        public WindowsMessageBoxInProgress(string caption, string message, State state)
        {
            InitializeComponent();
            AppWindow = this;
            
            s_caption = caption;
            s_message = message;
            s_state = state;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            Globals.POPDOWN_REQUEST();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Globals.currentPage = "";
            Globals.passingParameters = false;
            Globals.POPDOWN_REQUEST();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            try
            {
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery(s_caption + "," + s_message + ",81,82");

                switch (s_state)
                {
                    case State.Confirmation:
                        ButtonYes.Visibility = Visibility.Visible;
                        ButtonOK.Visibility = Visibility.Hidden;                       
                        ButtonYes.Content = hashtable[81].ToString();
                        
                        break;
                    case State.Ok:
                        ButtonYes.Visibility = Visibility.Hidden;
                        ButtonOK.Visibility = Visibility.Visible;                       
                        ButtonOK.Content = hashtable[81].ToString();
                        break;
                }

                Caption.Text = hashtable[Convert.ToInt32(s_caption)].ToString();
                Message.Text = hashtable[Convert.ToInt32(s_message)].ToString();
            }
            catch { }
        }

        

        public enum State
        {
            Confirmation,
            Ok
        }
    }
}
