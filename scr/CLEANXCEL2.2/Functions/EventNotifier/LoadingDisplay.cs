using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CLEANXCEL2._2.Functions.EventNotifier
{
    class LoadingDisplay
    {
        public static void Run(LoadingActions actions)
        {
            Storyboard SB = new Storyboard();
            Pages.Menu.Index.AppWindow.MainMenu.IsEnabled = false;
            Pages.Menu.Index.AppWindow.MenuContainer.Visibility = Visibility.Visible;
            Pages.Menu.Index.AppWindow.Loading.Visibility = Visibility.Visible;
            Canvas.SetZIndex(Pages.Menu.Index.AppWindow.MenuContainer, 153);
            SB = (Storyboard)Pages.Menu.Index.AppWindow.FindResource("ShowLoading");
            SB.Begin();

            switch (actions)
            {
                case LoadingActions.Loading:
                    Pages.Menu.Index.AppWindow.LoadingDesc.Text = Functions.SQL.Query.ExecuteLanguageQuery("129")[129].ToString();
                    break;
                case LoadingActions.Calibrating:
                    Pages.Menu.Index.AppWindow.LoadingDesc.Text = Functions.SQL.Query.ExecuteLanguageQuery("128")[128].ToString();
                    break;
                case LoadingActions.Analysing:
                    Pages.Menu.Index.AppWindow.LoadingDesc.Text = Functions.SQL.Query.ExecuteLanguageQuery("130")[130].ToString();
                    break;
            }
        }

        public static void Stop(LoadingActions actions)
        {
            Storyboard SB = new Storyboard();

            switch (actions)
            {
                case LoadingActions.Loading:
                case LoadingActions.Calibrating:
                case LoadingActions.Analysing:
                    SB = (Storyboard)Pages.Menu.Index.AppWindow.FindResource("HideLoading");
                    SB.Begin();
                    break;
            }

            Canvas.SetZIndex(Pages.Menu.Index.AppWindow.MenuContainer, 151);
            Pages.Menu.Index.AppWindow.MenuContainer.Visibility = Visibility.Hidden;
            Pages.Menu.Index.AppWindow.Loading.Visibility = Visibility.Hidden;
            Pages.Menu.Index.AppWindow.MainMenu.IsEnabled = true;
        }

        public enum LoadingActions
        {
            Loading,
            Calibrating,
            Analysing
        }
    }
}
