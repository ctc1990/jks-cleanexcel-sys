using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;



namespace CLEANXCEL2._2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public class Globals
    {
        public static readonly String AMSNetID = "5.50.81.78.1.1";        // System AMS Net ID
        //public static readonly String AMSNetID = "";        // System AMS Net ID
        public static readonly int AMSPort = 801;      // System Port
        public static string processGlobalName = "";

        public static readonly String Key = "7189";         // Key for the system
        // public const Int32 BUFFER_SIZE = 10; // Unmodifiable
        // public static readonly String CODE_PREFIX = "US-"; // Unmodifiable
        
        public static bool CONNECTION = false; // Modifiable
        public static String PAGE_URL = null; // Modifiable
        public static String MENU_URL = null; // Modifiable
        public static String SUBPAGE_URL = null; // Modifiable
        // public static String MENUPAGE_URL = null; // Modifiable
        public static String POPUP_URL = null; // Modifiable
        public static Int32 MENU_SELECTION = 0;

        public static string currentPage;   // Current Active Page
        public static object passingParameters; // Current Passing Parameters

        public static string currentRecipe = "";    // Current Selected Recipe
        public static string currentPart = "";      // Current Selected Part
        public static bool InitiatingRecipe = false;// Initiating Recipe
        public static bool LoggingHistoryStatusDefault = true;

        public static string AuthenticationLevel = "11111111111111";



        public static void PAGE_REQUEST()
        {
            if (PAGE_URL != null)
            {
                CLEANXCEL2._2.MainWindow.AppWindow.FrameContainer.Source = new Uri(PAGE_URL, UriKind.RelativeOrAbsolute);
                Console.WriteLine("PAGE_REQUEST : Completed.");
            }
            else
            {
                Console.WriteLine("PAGE_REQUEST : PAGE_URL is not defined.");
            }
        }

        public static void MENU_REQUEST()
        {
            if (MENU_URL != null)
            {
                CLEANXCEL2._2.Pages.Menu.Index.AppWindow.FrameContainer.Source = new Uri(MENU_URL, UriKind.RelativeOrAbsolute);
                Storyboard SB = (Storyboard)CLEANXCEL2._2.Pages.Menu.Index.AppWindow.FindResource("ShowFrame");
                SB.Begin();
                Console.WriteLine("MENU_REQUEST : Completed.");
            }
            else
            {
                Console.WriteLine("MENU_REQUEST : MENU_URL is not defined.");
            }
        }

        public static void SUBPAGE_REQUEST()
        {
            if (SUBPAGE_URL != null)
            {
                // CLEANXCEL2._2.MainWindow.AppWindow.FrameContainer.Source = new Uri(SUBPAGE_URL, UriKind.RelativeOrAbsolute);
                CLEANXCEL2._2.Pages.User.Index.AppWindow.FrameLocalContainer.Source = new Uri(SUBPAGE_URL, UriKind.RelativeOrAbsolute);
                Storyboard SB = (Storyboard)CLEANXCEL2._2.Pages.User.Index.AppWindow.FindResource("ShowFrame");
                SB.Begin();
                Console.WriteLine("SUBPAGE_REQUEST : Completed.");
            }
            else
            {
                Console.WriteLine("SUBPAGE_REQUEST : SUBPAGE_URL is not defined.");
            }
        }

        public static void POPUP_REQUEST(string caption, string message, Pages.Window.WindowsMessageBox.State state)
        {
            if (POPUP_URL != null)
            {
                //MainWindow.AppWindow.PopupContainer.Source = new Uri(POPUP_URL, UriKind.RelativeOrAbsolute);

                MainWindow.AppWindow.PopupContainer.Navigate(new Pages.Window.WindowsMessageBox(caption, message, state));
                Storyboard SB = (Storyboard)MainWindow.AppWindow.FindResource("MyPopUp");
                SB.Begin();
            }
            else
            {
                Console.WriteLine("POPUP_REQUEST : POPUP_URL is not defined.");
            }
        }

        public static void POPUP_REQUESTInProgress(string caption, string message, Pages.Window.WindowsMessageBoxInProgress.State state)
        {
            if (POPUP_URL != null)
            {
                //MainWindow.AppWindow.PopupContainer.Source = new Uri(POPUP_URL, UriKind.RelativeOrAbsolute);

                MainWindow.AppWindow.PopupContainer.Navigate(new Pages.Window.WindowsMessageBoxInProgress(caption, message, state));
                Storyboard SB = (Storyboard)MainWindow.AppWindow.FindResource("MyPopUp");
                SB.Begin();
            }
            else
            {
                Console.WriteLine("POPUP_REQUEST : POPUP_URL is not defined.");
            }
        }

        public static void POPDOWN_REQUEST()
        {
            if (PAGE_URL != null)
            {
                Storyboard SB = (Storyboard)MainWindow.AppWindow.FindResource("MyPopDown");
                SB.Begin();
            }
            else
            {
                Console.WriteLine("POPDOWN_REQUEST : POPDOWN_URL is not defined.");
            }
        }
    }

    public partial class App : Application
    {

    }
}
