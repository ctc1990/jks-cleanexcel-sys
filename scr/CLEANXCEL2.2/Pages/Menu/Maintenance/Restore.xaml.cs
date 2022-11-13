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
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Pages.Menu.Maintenance
{
    /// <summary>
    /// Interaction logic for Calibration.xaml
    /// </summary>
    public partial class Restore : Page
    {
        TcAdsClient adsClient = new TcAdsClient();

        public Restore()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
            LoadCalibration();

            adsClient.Connect(Globals.AMSNetID, Globals.AMSPort);
        }

        private void CBCalibrate_Click(object sender, RoutedEventArgs e)
        {
            CBCalibrate.IsChecked = null;
            
            //Functions.EventNotifier.LoadingDisplay.Run(Functions.EventNotifier.LoadingDisplay.LoadingActions.Calibrating);

            System.ComponentModel.BackgroundWorker CalibrateVacuumPressureBW = new System.ComponentModel.BackgroundWorker();

            CalibrateVacuumPressureBW.DoWork += CalibrateVacuumPressureBW_DoWork;
            CalibrateVacuumPressureBW.RunWorkerCompleted += CalibrateVacuumPressureBW_RunWorkerCompleted;


            List<double> list = new List<double>() { Convert.ToDouble(LowVacuumLevel.Text), Convert.ToDouble(HighVacuumLevel.Text) };

            CalibrateVacuumPressureBW.RunWorkerAsync(list);
        }

        private void CBPersistentRecovery_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                Functions.MemoryManagement.Machine.POST_MachineMemory(adsClient);
                #region show msg box in prgress
                Globals.POPUP_URL = "Pages/Window/WindowsMessageBoxInProgress.xaml";
                Globals.POPUP_REQUESTInProgress("50", "106", Window.WindowsMessageBoxInProgress.State.Ok);
                #endregion
            }
        }

        private void CBBackupStorage_Click(object sender, RoutedEventArgs e)
        {
            Functions.SQL.Backup.Backup_SQL("C:\\fe01fs.sql");

            #region show msg box in prgress
            Globals.POPUP_URL = "Pages/Window/WindowsMessageBoxInProgress.xaml";
            Globals.POPUP_REQUESTInProgress("50", "106", Window.WindowsMessageBoxInProgress.State.Ok);
            #endregion
        }

        private void CalibrateVacuumPressureBW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                List<double> list = e.Argument as List<double>;
                Functions.RecipeManagement.SubProcessBlock.Calibration.CalibrateVacuumLevel(adsClient, list[0], list[1], ".ANI1_2");
                UpdateCalibrationData("Low Vacuum Level", list[0]);
                UpdateCalibrationData("High Vacuum Level", list[1]);
            }
        }

        private void CalibrateVacuumPressureBW_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Functions.EventNotifier.LoadingDisplay.Stop(Functions.EventNotifier.LoadingDisplay.LoadingActions.Calibrating);

            CBCalibrate.IsChecked = true;
        }

        private static void UpdateCalibrationData(string name, double value)
        {
            string query = "";
            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Parameters.AddWithValue("@name", name);
            mySqlCommand.Parameters.AddWithValue("@value", value);

            if (Functions.SQL.Query.ExecuteCheckQuery("select * from calibration where calibration.name = '" + name + "'", new MySql.Data.MySqlClient.MySqlCommand()))
                query = "update calibration set value = @value where name = @name";
            else
                query = "insert into calibration (name, value) values(@name, @value)";

            Functions.SQL.Query.ExecuteNonQuery(query, mySqlCommand);
        }

        private void LoadCalibration()
        {
            try
            {
                List<List<string>> list = Functions.SQL.Query.ExecuteMultiQuery("select calibration.name, calibration.value from calibration", new string[] { "name", "value" });

                LowVacuumLevel.Text = list[1][list[0].IndexOf("Low Vacuum Level")];
                HighVacuumLevel.Text = list[1][list[0].IndexOf("High Vacuum Level")];
            }
            catch { }
        }

        private void LoadLanguage()
        {
            try
            {
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("63,181,182,183,184");

                LowVacuumLevelCap.Text = hashtable[63].ToString();
                HighVacuumLevelCap.Text = hashtable[184].ToString();
                TBCalibrateTitle.Text = hashtable[181].ToString();
                TBPersistentMemoryTitle.Text = hashtable[182].ToString();
                TBBackupStorageTitle.Text = hashtable[183].ToString();
            }
            catch (NullReferenceException) { }
        }
    }
}
