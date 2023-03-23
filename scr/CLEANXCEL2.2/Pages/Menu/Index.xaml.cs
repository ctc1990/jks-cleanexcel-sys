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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using MySql.Data.MySqlClient;
using System.Collections;
using TwinCAT.Ads;
using System.IO;
using CLEANXCEL2._2.Functions.EventNotifier;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace CLEANXCEL2._2.Pages.Menu
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        DispatcherTimer IntervalLogging = new DispatcherTimer();
        private TcAdsClient adsClient = new TcAdsClient();
        AlarmNotification alarmNotification = new AlarmNotification();
        SubProcessNotification subprocessNotification = new SubProcessNotification();

        bool notifier = true;
        bool trigger = false;
        public static Index AppWindow;
        public Index()
        {
            try
            {
                InitializeComponent();
                AppWindow = this;
                
                AlarmWarningText.Text = "Null";
                Functions.EventNotifier.LoadingDisplay.Run(LoadingDisplay.LoadingActions.Loading);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CheckAuthentication()
        {
            MMOperation.IsEnabled = Globals.AuthenticationLevel.Substring(3, 1).Contains("1");
            MMMaintenance.IsEnabled = Globals.AuthenticationLevel.Substring(4, 1).Contains("1");
            MMBypass.IsEnabled = Globals.AuthenticationLevel.Substring(5, 1).Contains("1");
            MMDefaultSettings.IsEnabled = Globals.AuthenticationLevel.Substring(6, 1).Contains("1");
            MMKnowledgeBase.IsEnabled = Globals.AuthenticationLevel.Substring(8, 4).Contains("1");
            MMCurrentStatus.IsEnabled = Globals.AuthenticationLevel.Substring(12, 1).Contains("1");
            MMAnalytics.IsEnabled = Globals.AuthenticationLevel.Substring(13, 1).Contains("1");
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton thisButton = (ToggleButton)sender;
            if (thisButton.IsChecked.Value)
            {
                MenuContainer.Visibility = Visibility.Visible;
                Storyboard SB = (Storyboard)FindResource("ShowMenu");
                SB.Begin();
                trigger = true;
            }
            else
            {
                Storyboard SB = (Storyboard)FindResource("HideMenu");
                SB.Begin();
                trigger = false;
            }
        }

        private void HideMenu()
        {
            if (trigger)
            {
                Storyboard SB = (Storyboard)FindResource("HideMenu");
                SB.Begin();
            }
        }

        public void HidePage()
        {
            Storyboard SB = (Storyboard)FindResource("HideFrame");
            SB.Begin();
        }

        private void MMSelections_Checked(object sender, RoutedEventArgs e)
        {
            string page = "";
            RadioButton radioButton = (RadioButton)sender;

            switch (radioButton.Name)
            {
                case "MMCurrentStatus":
                    page = "CurrentStatus/Index.xaml";
                    break;
                case "MMDefaultSettings":
                    page = "DefaultSettings/Index.xaml";
                    break;
                case "MMKnowledgeBase":
                    page = "UnderConstruction/UnderConstruction.xaml";
                    break;
                case "MMVersions":
                    page = "UnderConstruction/UnderConstruction.xaml";
                    break;
                case "MMRecipeManagement":
                    page = "RecipeManagement/Index.xaml";
                    break;
                case "MMOperation":
                    page = "Operation/Index.xaml";
                    break;
                case "MMMaintenance":
                    page = "Maintenance/Index.xaml";
                    break;
                case "MMAlarm":
                    page = "Alarm/Index.xaml";
                    break;
                case "MMBypass":
                    page = "Bypass/Index.xaml";
                    break;
                case "MMAnalytics":
                    page = "Analytics/Index.xaml";
                    break;
                case "MMCredits":
                    page = "UnderConstruction/UnderConstruction.xaml";
                    break;
                case "MMLogOut":
                    page = "../Pages/User/Index.xaml";
                    break;
            }
            //Console.WriteLine("Pass");

            switch (radioButton.Name)
            {
                case "MMLogOut":
                    HideMenu();
                    Globals.MENU_URL = null;
                    Globals.PAGE_URL = page;
                    Globals.PAGE_REQUEST();
                    break;
                case "MMAlarm":
                    if (AlarmWarningText.Tag.ToString() == "Active")
                    {
                        Storyboard SB = (Storyboard)FindResource("HideAlarmWarning");
                        SB.Begin();
                        AlarmWarningText.Tag = "Inactive";
                    }
                    goto default;
                default:
                    Globals.MENU_URL = page;
                    HidePage();
                    HideMenu();
                    break;
            }
            MainMenu.IsChecked = false;
            MainMenu_Click(MainMenu, null);
        }

        private void FrameLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void FrameContainer_LoadCompleted(object sender, NavigationEventArgs e)
        {

        }

        private void LoadPage(object sender, EventArgs e)
        {
            Globals.MENU_REQUEST();
        }

        private void HideMenuContainer(object sender, EventArgs e)
        {
            MenuContainer.Visibility = Visibility.Hidden;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Hashtable hashtable;
                hashtable = LoadLanguage();

                MMCurrentStatusTitle.Text = hashtable[39].ToString();
                MMCurrentStatusDesc.Text = hashtable[88].ToString();

                MMDefaultSettingsTitle.Text = hashtable[32].ToString();
                MMDefaultSettingsDesc.Text = hashtable[89].ToString();

                MMKnowledgeBaseTitle.Text = hashtable[34].ToString();
                MMKnowledgeBaseDesc.Text = hashtable[90].ToString();

                MMVersionsTitle.Text = hashtable[98].ToString();
                MMVersionsDesc.Text = hashtable[91].ToString();

                MMRecipeManagementTitle.Text = hashtable[27].ToString();
                MMRecipeManagementDesc.Text = hashtable[92].ToString();

                MMOperationTitle.Text = hashtable[35].ToString();
                MMOperationDesc.Text = hashtable[93].ToString();

                MMMaintenanceTitle.Text = hashtable[19].ToString();
                MMMaintenanceDesc.Text = hashtable[94].ToString();

                MMAlarmTitle.Text = hashtable[85].ToString();
                MMAlarmDesc.Text = hashtable[95].ToString();

                MMBypassTitle.Text = hashtable[125].ToString();
                MMBypassDesc.Text = hashtable[126].ToString();

                MMAnalyticsTitle.Text = hashtable[40].ToString();
                MMAnalyticsDesc.Text = hashtable[132].ToString();

                MMCreditsTitle.Text = hashtable[86].ToString();
                MMCreditsDesc.Text = hashtable[96].ToString();

                MMLogOutTitle.Text = hashtable[87].ToString();
                MMLogOutDesc.Text = hashtable[97].ToString();
            }
            catch { }
        }

        private Hashtable LoadLanguage()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            return Functions.SQL.Query.ExecuteLanguageQuery("39,32,34,98,27,35,40,19,85,86,87,88,89,90,91,92,93,94,95,96,97,125,126,132");
        }

        private void InitializeADSNotifier()
        {
            Functions.SQL.Query.ExecuteNonQuery("update alarm_history set status = '0'", new MySqlCommand());
            alarmNotification.ActionChange += AlarmNotification_ActionChange;
            subprocessNotification.ActionIntervalStop += SubprocessNotification_ActionIntervalStop;

            try
            {
                adsClient.Connect(Globals.AMSNetID, Globals.AMSPort);
                alarmNotification.AlarmGenerateNotif(adsClient, ".ARbAlarmOutput");
                subprocessNotification.SubProcessChangeGenerateNotif(adsClient, ".fbMainStationProcess_Stn[1].StnSeqProcessfb.iStnSeqProcessCaseNo");
                Globals.CONNECTION = true;
                MMCurrentStatus.IsChecked = true;
                Functions.EventNotifier.LoadingDisplay.Stop(LoadingDisplay.LoadingActions.Loading);
            }
            catch
            {
                Globals.CONNECTION = false;
                MMCurrentStatus.IsChecked = true;
                Functions.EventNotifier.LoadingDisplay.Stop(LoadingDisplay.LoadingActions.Loading);
                Globals.POPUP_URL = "Pages/Window/WindowsMessageBox.xaml";
                Globals.POPUP_REQUEST("50", "106", Window.WindowsMessageBox.State.Ok);
            }
        }

        private void AlarmNotification_ActionChange()
        {
            //Console.WriteLine("Alarm Started");
            int count = Convert.ToInt32(Functions.SQL.Query.ExecuteSingleQuery("select count(status) as count from alarm_history where status = '1'", "count")[0]);
            switch (count)
            {
                case 0:
                    if (AlarmWarningText.Tag.ToString() == "Active")
                    {
                        Storyboard SB = (Storyboard)FindResource("HideAlarmWarning");
                        SB.Begin();
                        AlarmWarningText.Tag = "Inactive";
                    }
                    break;
                default:
                    AlarmWarningText.Text = count + " Alarm(s) has occured";
                    if (AlarmWarningText.Tag.ToString() == "Inactive")
                    {
                        Storyboard SB = (Storyboard)FindResource("ShowAlarmWarning");
                        SB.Begin();
                        AlarmWarningText.Tag = "Active";
                    }
                    break;
            }
            if (Globals.MENU_URL == "Alarm/Index.xaml")
                FrameContainer.NavigationService.Refresh();
        }

        private void SubprocessNotification_ActionIntervalStop()
        {
            StopIntervalLogging();
        }

        public void RunIntervalLogging(int seconds)
        {
            IntervalLogging.Tick += IntervalLogging_Tick;
            IntervalLogging.Interval = new TimeSpan(0, 0, seconds);
            IntervalLogging.Start();
        }

        private void IntervalLogging_Tick(object sender, EventArgs e)
        {
            //Functions.RecipeManagement.RecipeStructure.StatusManagement.POST_History_Status(adsClient);
        }

        public void StopIntervalLogging()
        {
            IntervalLogging.Stop();

            //Functions.RecipeManagement.RecipeStructure.StatusManagement.POST_History_Status_Ended(adsClient);

            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bAutoStartPB", "false", "bool");
            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bBasketCfmEn", "false", "bool");
        }

        private void HideAlarm_Click(object sender, RoutedEventArgs e)
        {
            Storyboard SB = (Storyboard)FindResource("HideAlarmWarning");
            SB.Begin();
            AlarmWarningText.Tag = "Inactive";
        }

        private void DirecttoAlarm_Click(object sender, RoutedEventArgs e)
        {
            Storyboard SB = (Storyboard)FindResource("HideAlarmWarning");
            SB.Begin();
            AlarmWarningText.Tag = "Inactive";

            MMAlarm.IsChecked = true;
        }

        private void ShowLoading_Completed(object sender, EventArgs e)
        {
            if (notifier)
            {
                CheckAuthentication();
                InitializeADSNotifier();
                notifier = false;
            }
        }

        private void HideLoading_Completed(object sender, EventArgs e)
        {

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                adsClient.Disconnect();
                adsClient.Dispose();
            }
        }
    }
}
