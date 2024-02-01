using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwinCAT.Ads;


namespace CLEANXCEL2._2.Pages.Menu.Operation
{
    /// <summary>
    /// Interaction logic for Operation.xaml
    /// </summary>
    public partial class Operation : Page
    {
        public static Operation AppWindow;
        int TotalProcessTime = 1;
        private TcAdsClient adsClient = new TcAdsClient();
        private static AdsStream adsDataStream = new AdsStream(109);
        private static AdsBinaryReader binRead = new AdsBinaryReader(adsDataStream);
        private static int[] hconnect = new int[7];
        private bool AutoPrepInProgress = false;

        public Operation()
        {
            InitializeComponent();
            AppWindow = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
            LoadLanguage();

            if (Globals.CONNECTION)
            {
                try
                {

                    adsClient.Connect(Globals.AMSNetID, Globals.AMSPort);

                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bAutoModeOnPB", "true", "bool");
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bMainOnOff", "true", "bool");

                    try
                    {
                        ((RadioButton)FindName(Globals.currentRecipe)).IsChecked = true;
                    }
                    catch { }
                    
                    hconnect[0] = adsClient.AddDeviceNotification(".ARDsStnSeqProcessCtrl[1].Out_iCurrentProcessTime", adsDataStream, 0, 4, AdsTransMode.OnChange, 50, 0, null);
                    hconnect[1] = adsClient.AddDeviceNotification(".DSHmiStationDisplayInfo[1].sStationSubDescription", adsDataStream, 4, 100, AdsTransMode.OnChange, 50, 0, null);
                    hconnect[2] = adsClient.AddDeviceNotification(".bAutoPreparationPb", adsDataStream, 104, 1, AdsTransMode.OnChange, 50, 0, null);
                    hconnect[3] = adsClient.AddDeviceNotification(".bAutoPreparationDone", adsDataStream, 105, 1, AdsTransMode.OnChange, 50, 0, null);
                    hconnect[4] = adsClient.AddDeviceNotification(".bStn1DoorOpenCompleted", adsDataStream, 106, 1, AdsTransMode.OnChange, 50, 0, null);
                    hconnect[5] = adsClient.AddDeviceNotification(".bStn1DoorCloseCompleted", adsDataStream, 107, 1, AdsTransMode.OnChange, 50, 0, null);
                    hconnect[6] = adsClient.AddDeviceNotification(".bBasketCfmEn", adsDataStream, 108, 1, AdsTransMode.OnChange, 50, 0, null);

                    adsClient.AdsNotification += new AdsNotificationEventHandler(StatusOnChange);
                }
                catch
                {

                }
            }
        }

        private void StatusOnChange(object sender, AdsNotificationEventArgs e)
        {
            try
            {
                if (e.NotificationHandle == hconnect[0])
                {
                    int value = binRead.ReadInt16();
                    myrad.Value = Convert.ToDouble(((double)value / TotalProcessTime) * 100);
                    myrad.ToolTip = TotalProcessTime - value;
                }
                else if (e.NotificationHandle == hconnect[1])
                {
                    string value = binRead.ReadPlcAnsiString(99);
                    //Console.WriteLine("Description : " + value);
                    myrad.Tag = string.Format(value);
                    ProcessName.Text = string.Format(value);
                    Globals.processGlobalName = ProcessName.Text;                 
                }
                else if (e.NotificationHandle == hconnect[2])
                {
                    AutoPrepInProgress = binRead.ReadBoolean();

                    if (!AutoPrepInProgress)
                        AutoPreparation.IsChecked = false;
                }
                else if (e.NotificationHandle == hconnect[3])
                {
                    bool AutoPrepCompleted = binRead.ReadBoolean();

                    if (AutoPrepInProgress && !AutoPrepCompleted)
                        AutoPreparation.IsChecked = null;
                    else if (AutoPrepInProgress && AutoPrepCompleted)
                        AutoPreparation.IsChecked = true;
                }
                else if (e.NotificationHandle == hconnect[4])
                {
                    Door.IsChecked = !binRead.ReadBoolean();
                }
                else if (e.NotificationHandle == hconnect[5])
                {
                    Door.IsChecked = binRead.ReadBoolean();
                }
                else if (e.NotificationHandle == hconnect[6])
                {
                    bool BasketConfirm = binRead.ReadBoolean();

                    Start.IsChecked = BasketConfirm;
                    if (BasketConfirm)
                    {
                        StartEllipse.Fill = (Brush)FindResource("RedPinkColor");
                    }
                    else
                    {
                        StartEllipse.Fill = (Brush)FindResource("CWhite");
                    }
                }
            }
            catch
            {

            }
        }

        private void Initialize()
        {
            //LoadPartName();
            LoadRecipeName();
        }

        private void LoadRecipeName()
        {
            List<List<string>> recipes = Functions.SQL.Query.ExecuteMultiQuery("select * from recipe_id where status = 1", new string[] { "id", "name" });
            for (int i = 0; i<recipes[0].Count();i++)
            {
                int TotalRecipeTime = Convert.ToInt32((Functions.SQL.Query.ExecuteSingleQuery(
                    "select sum(recipe.process_time) as total_time from recipe right join " +
                    "recipe_id on recipe_id.id = recipe.recipe_name where recipe_id.id  = '" + recipes[0][i] + "'", "total_time"))[0]);
                RadioButton radioButton = new RadioButton()
                {
                    Name = "RECIPES" + recipes[0][i],
                    Tag = recipes[0][i],
                    ToolTip = recipes[1][i],
                    Content = TotalRecipeTime.ToString(),
                    Style = (Style)FindResource("RBStandardSelection"),
                    GroupName = "recipes"
                };
                RegisterName(radioButton.Name, radioButton);
                radioButton.Checked += recipes_Checked;
                RecipeStackPanel.Children.Add(radioButton);

                if (radioButton.Tag.ToString() == Globals.currentRecipe)
                    radioButton.IsChecked = true;
            }
        }

        private void LoadPartName()
        {
            RadioButton selected = new RadioButton();
            List<List<string>> parts = Functions.RecipeManagement.RecipeStructure.DataManagement.LoadPartName();
            for(int i = 0; i < parts[0].Count(); i++)
            {
                RadioButton radioButton = new RadioButton()
                {
                    Name = "PARTS_" + parts[0][i].Replace(" ", "_"),// + "_" + parts[3][i].Replace(" ", "_"),
                    Tag = parts[0][i],
                    ToolTip = parts[1][i],
                    Content = parts[2][i],
                    Style = (Style)FindResource("RBStandardSelection"),
                    GroupName = "parts"
                };
                RegisterName(radioButton.Name, radioButton);
                radioButton.Checked += parts_Checked;
                PartStackPanel.Children.Add(radioButton);

                if (radioButton.Tag.ToString() == Globals.currentPart)
                    radioButton.IsChecked = true;
            }
            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            myrad.Value = 0;
        }

        private void SystemStatus_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                try
                {
                    CheckBox checkbox = (CheckBox)sender;

                    switch (checkbox.Name)
                    {
                        case "AutoPreparation":
                            Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bAutoPreparationPb", checkbox.IsChecked.ToString(), "bool");
                            checkbox.IsChecked = null;
                            break;
                        case "Door":
                            switch (checkbox.IsChecked)
                            {
                                case true:
                                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bMapDoorOpen", (!checkbox.IsChecked).ToString(), "bool");
                                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bMapDoorClose", checkbox.IsChecked.ToString(), "bool");
                                    checkbox.IsChecked = null;
                                    break;
                                case false:
                                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bMapDoorClose", checkbox.IsChecked.ToString(), "bool");
                                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bMapDoorOpen", (!checkbox.IsChecked).ToString(), "bool");
                                    checkbox.IsChecked = null;
                                    break;
                            }
                            break;
                    }
                }
                catch { }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //string recipe = Globals.currentRecipe.Replace('_', ' ');
            //Functions.RecipeManagement.BlackBox.RecipeModule.UploadRecipe(adsClient, recipe, TotalProcessTime);

            Functions.EventNotifier.LoadingDisplay.Run(Functions.EventNotifier.LoadingDisplay.LoadingActions.Loading);
            ToggleButton toggleButton = (ToggleButton)sender;

            System.ComponentModel.BackgroundWorker RunOperation = new System.ComponentModel.BackgroundWorker();
            RunOperation.DoWork += RunOperation_DoWork;
            RunOperation.RunWorkerCompleted += RunOperation_RunWorkerCompleted;

            RunOperation.RunWorkerAsync(toggleButton.IsChecked);
        }

        private void RunOperation_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                bool argument = Convert.ToBoolean(e.Argument);
                string errorMessage = "";
                try
                {
                    Globals.InitiatingRecipe = true;
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, ".bAutoStartPB", argument.ToString(), "bool");
                    if (argument == true)
                    {
                        string recipe = Globals.currentRecipe.Replace('_', ' ');
                        Functions.RecipeManagement.RecipeStructure.StatusManagement.POST_History_Recipe();
                        Functions.RecipeManagement.RecipeStructure.StandardCommand.EmptyRecipe(adsClient);
                        Functions.RecipeManagement.BlackBox.RecipeModule.UploadRecipe(adsClient, recipe, TotalProcessTime, out errorMessage);

                        if(!string.IsNullOrEmpty(errorMessage))
                        {
                            Functions.RecipeManagement.RecipeStructure.StandardCommand.StopProcess(adsClient);
                            Globals.POPUP_REQUEST("50", errorMessage, Window.WindowsMessageBox.State.Error);
                        }

                        Functions.RecipeManagement.RecipeStructure.StandardCommand.StartRecipeNumber(adsClient, 1);
                    }
                    else
                    {
                        Functions.RecipeManagement.RecipeStructure.StandardCommand.StopProcess(adsClient);
                    }

                }
                catch(Exception ex) {
                }
            }
        }

        private void RunOperation_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Functions.EventNotifier.LoadingDisplay.Stop(Functions.EventNotifier.LoadingDisplay.LoadingActions.Loading);

            switch (LogInterval.SelectedIndex)
            {
                case 0:
                    Globals.LoggingHistoryStatusDefault = true;
                    break;
                case 1:
                    Globals.LoggingHistoryStatusDefault = false;
                    Pages.Menu.Index.AppWindow.RunIntervalLogging(1);
                    break;
                case 2:
                    Globals.LoggingHistoryStatusDefault = false;
                    Pages.Menu.Index.AppWindow.RunIntervalLogging(2);
                    break;
                case 3:
                    Globals.LoggingHistoryStatusDefault = false;
                    Pages.Menu.Index.AppWindow.RunIntervalLogging(5);
                    break;
                case 4:
                    Globals.LoggingHistoryStatusDefault = false;
                    Pages.Menu.Index.AppWindow.RunIntervalLogging(10);
                    break;
            }
        }

        private void recipes_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radioButton = (RadioButton)sender;
                TotalProcessTime = Convert.ToInt32(((RadioButton)sender).Content);

                //RecipeName.Text = radioButton.ToolTip.ToString();
                myrad.ToolTip = radioButton.Content.ToString();

                //Globals.currentRecipe = myrad.Tag.ToString();
                Globals.currentRecipe = radioButton.Tag.ToString();
            }
            catch { }
        }

        private void parts_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radioButton = (RadioButton)sender;
                string selectedpart = radioButton.Name;

                Globals.currentPart = radioButton.Tag.ToString();

                ((RadioButton)FindName("RECIPES" + selectedpart.Substring(selectedpart.IndexOf('_') + 1))).IsChecked = true;
            }
            catch { }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                adsClient.Disconnect();
                adsClient.Dispose();
            }
        }

        private void LoadLanguage()
        {
            try
            {
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("30,114,115,131,186");

                TBPartTitle.Text = hashtable[131].ToString();
                TBRecipeTitle.Text = hashtable[30].ToString();
                myrad.Tag = hashtable[115].ToString();
                AutoPreparation.Content = hashtable[114].ToString();
                Door.Content = hashtable[186].ToString();
            }
            catch (NullReferenceException) { }
        }
    }
}
