using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Sensor.xaml
    /// </summary>
    public partial class Sensor : Page
    {
        private static AdsStream adsDataStream;
        private static BinaryReader binRead;
        private TcAdsClient adsClient = new TcAdsClient();
        private static int[] hconnect;
        Hashtable hashtable = new Hashtable();

        private bool[] VacuumTankLevel = new bool[] { false };
        private bool[] ProcessChamberLevel = new bool[] { false, false };
        private bool[] StorageTank1Level = new bool[] { false, false, false, false };
        private bool[] StorageTank2Level = new bool[] { false };
        private bool[] DistillationTankLevel = new bool[] { false, false, false, false };

        private static int[] separator = new int[] { 13, 17 };

        public Sensor()
        {
            InitializeComponent();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                try
                {
                    adsDataStream = new AdsStream(InputVariable.Count() * 4);
                    binRead = new BinaryReader(adsDataStream, Encoding.ASCII);
                    hconnect = new int[InputVariable.Count()];

                    adsClient.Connect(Globals.AMSNetID, Globals.AMSPort);

                    for (int i = 0; i < InputVariable.Length; i++)
                    {
                        if (i < separator[0])
                            hconnect[i] = adsClient.AddDeviceNotification(InputVariable[i], adsDataStream, i * 4, 4, AdsTransMode.OnChange, 50, 0, null);
                        else if (i<separator[1])
                            hconnect[i] = adsClient.AddDeviceNotification(InputVariable[i], adsDataStream, i * 2, 2, AdsTransMode.OnChange, 50, 0, null);
                        else
                            hconnect[i] = adsClient.AddDeviceNotification(InputVariable[i], adsDataStream, i * 2, 1, AdsTransMode.OnChange, 50, 0, null);
                    }

                    adsClient.AdsNotification += new AdsNotificationEventHandler(StatusOnChange);
                }
                catch
                {

                }
            }
        }

        private void Load()
        {
            DigitalDisplay1.EqName = "Process Chamber";
            DigitalDisplay1.MeasurementName = "Temperature";
            DigitalDisplay1.Unit = "Celsius";
            DigitalDisplay1.LogicName = "Level";
            DigitalDisplay1.Logic = "";
            DigitalDisplay1.ValueSet = 50.00;
            DigitalDisplay1.ActualValue = 49.67;

            DigitalDisplay2.EqName = "Chiller";
            DigitalDisplay2.MeasurementName = "Temperature";
            DigitalDisplay2.Unit = "Celsius";
            DigitalDisplay2.ValueSet = 0.00;
            DigitalDisplay2.ActualValue = 0.00;

            DigitalDisplay3.EqName = "Sub-Tank 1";
            DigitalDisplay3.MeasurementName = "Temperature";
            DigitalDisplay3.Unit = "Celsius";
            DigitalDisplay3.LogicName = "Level";
            DigitalDisplay3.Logic = "";
            DigitalDisplay3.ValueSet = 50.00;
            DigitalDisplay3.ActualValue = 49.50;

            DigitalDisplay4.EqName = "Sub-Tank 2";
            DigitalDisplay4.MeasurementName = "Temperature";
            DigitalDisplay4.Unit = "Celsius";
            DigitalDisplay4.LogicName = "Level";
            DigitalDisplay4.Logic = "";
            DigitalDisplay4.ValueSet = 50.00;
            DigitalDisplay4.ActualValue = 49.16;

            DigitalDisplay5.EqName = "Distillation Tank";
            DigitalDisplay5.Location = "Top";
            DigitalDisplay5.MeasurementName = "Temperature";
            DigitalDisplay5.Unit = "Celsius";
            DigitalDisplay5.LogicName = "Level";
            DigitalDisplay5.Logic = "";
            DigitalDisplay5.ValueSet = 50.00;
            DigitalDisplay5.ActualValue = 49.78;

            DigitalDisplay6.EqName = "Distillation Tank";
            DigitalDisplay6.Location = "Bottom";
            DigitalDisplay6.MeasurementName = "Temperature";
            DigitalDisplay6.Unit = "Celsius";
            DigitalDisplay6.LogicName = "Level";
            DigitalDisplay6.Logic = "";
            DigitalDisplay6.ValueSet = 50.00;
            DigitalDisplay6.ActualValue = 49.67;

            DigitalDisplay7.EqName = "Ultrasonic";
            DigitalDisplay7.MeasurementName = "Power";
            DigitalDisplay7.Unit = "Percentage";
            DigitalDisplay7.ValueSet = 0.00;
            DigitalDisplay7.ActualValue = 0.00;

            DigitalDisplay8.EqName = "Ultrasonic";
            DigitalDisplay8.MeasurementName = "Frequency";
            DigitalDisplay8.Unit = "kHz";
            DigitalDisplay8.ValueSet = 0.00;
            DigitalDisplay8.ActualValue = 0.00;

            DigitalDisplay9.EqName = "Vacuum Pump";
            DigitalDisplay9.MeasurementName = "Pressure";
            DigitalDisplay9.Unit = "kPa";
            DigitalDisplay9.LogicName = "Level";
            DigitalDisplay9.Logic = "Intermediate";
            DigitalDisplay9.ValueSet = 0.00;
            DigitalDisplay9.ActualValue = 0.00;
        }

        private void StatusOnChange(object sender, AdsNotificationEventArgs e)
        {
            try
            {
                for (int i = 0; i < InputVariable.Count(); i++)
                {

                    if (e.NotificationHandle == hconnect[i])
                    {
                        if (i < separator[0])
                        {
                            double value = binRead.ReadSingle();
                            DecodeValue(i, value);
                        }
                        else if (i< separator[1])
                        {
                            int value = binRead.ReadInt16();
                            DecodeValue(i, value);
                        }
                        else
                        {
                            bool value = binRead.ReadBoolean();
                            DecodeBoolean(i, value);
                        }
                    }
                }
            }
            catch { }
        }

        private void DecodeValue(int index, double status)
        {
            switch (index)
            {
                case 0:
                    DigitalDisplay1.ActualValue = status;   // .ARrStnTempPV[1]
                    break;
                case 1:
                    DigitalDisplay1.ValueSet = status;      // .ARrStnTempSV[1]
                    break;
                case 2:
                    DigitalDisplay2.ActualValue = status;   // .ARrStnTempPV[2]
                    break;
                case 3:
                    DigitalDisplay2.ValueSet = status;      // .ARrStnTempSV[2]
                    break;
                case 4:
                    DigitalDisplay3.ActualValue = status;   // .ARrStnTempPV[3]
                    break;
                case 5:
                    DigitalDisplay3.ValueSet = status;      // .ARrStnTempSV[3]
                    break;
                case 6:
                    DigitalDisplay4.ActualValue = status;   // .ARrStnTempPV[5]
                    break;
                case 7:
                    DigitalDisplay4.ValueSet = status;      // .ARrStnTempSV[5]
                    break;
                case 8:
                    DigitalDisplay5.ActualValue = status;   // .ARrStnTempPV[4]
                    break;
                case 9:
                    DigitalDisplay5.ValueSet = status;      // .ARrStnTempSV[4]
                    break;
                case 10:
                    DigitalDisplay6.ActualValue = status;   // .ARrStnTempPV[6]
                    break;
                case 11:
                    DigitalDisplay6.ValueSet = status;   // .ARrStnTempSV[6]
                    break;
                case 12:
                    DigitalDisplay9.ActualValue = status;   // .ARrStnVacuumPV[1]
                    break;
                case 13:
                    DigitalDisplay7.ActualValue = status;   // .ARiStnUSSideAPv[1]
                    break;
                case 14:
                    DigitalDisplay7.ValueSet = status;      // .ARiStnManualUSBtmAsv[1]
                    break;
                case 15:
                    DigitalDisplay8.ActualValue = status;   // .DSWeberEnclosure[1].DSWeberGenerator[1].IN_iFreqSetPoint
                    break;
                case 16:
                    DigitalDisplay8.ValueSet = status;   // .DSWeberEnclosure[1].DSWeberGenerator[1].DSWeberMulFreqSwitching.iFreqOutput_TS
                    break;
            }
        }

        private void DecodeBoolean(int index, bool status)
        {
            switch (index)
            {
                case 18:
                    ProcessChamberLevel[1] = status;
                    IndicateLevel2(ProcessChamberLevel, DigitalDisplay1);
                    //DigitalDisplay1.Logic = hashtable[159].ToString();      // Reg
                    break;
                case 19:
                    ProcessChamberLevel[0] = status;
                    IndicateLevel2(ProcessChamberLevel, DigitalDisplay1);
                    //DigitalDisplay1.Logic = hashtable[161].ToString();      // Empty
                    break;
                case 22:
                    StorageTank1Level[0] = status;
                    IndicateLevel4(StorageTank1Level, DigitalDisplay3);
                    //DigitalDisplay3.Logic = hashtable[157].ToString();      // Min
                    break;
                case 23:
                    StorageTank1Level[1] = status;
                    IndicateLevel4(StorageTank1Level, DigitalDisplay3);
                    //DigitalDisplay3.Logic = hashtable[159].ToString();      // Top
                    break;
                case 24:
                    StorageTank1Level[2] = status;
                    IndicateLevel4(StorageTank1Level, DigitalDisplay3);
                    //DigitalDisplay3.Logic = hashtable[158].ToString();      // Reg
                    break;
                case 25:
                    StorageTank1Level[3] = !status;
                    IndicateLevel4(StorageTank1Level, DigitalDisplay3);
                    //DigitalDisplay3.Logic = hashtable[160].ToString();      // Max
                    break;
                case 26:
                    StorageTank2Level[0] = status;
                    IndicateLowerLevel1(StorageTank2Level, DigitalDisplay4);
                    //DigitalDisplay4.Logic = hashtable[157].ToString();      // Min
                    break;
                case 28:
                    DistillationTankLevel[0] = status;
                    IndicateLevel4(DistillationTankLevel, DigitalDisplay6);
                    //DigitalDisplay6.Logic = hashtable[157].ToString();      // Min
                    break;
                case 29:
                    DistillationTankLevel[1] = status;
                    IndicateLevel4(DistillationTankLevel, DigitalDisplay6);
                    //DigitalDisplay6.Logic = hashtable[159].ToString();      // Top
                    break;
                case 30:
                    DistillationTankLevel[2] = status;
                    IndicateLevel4(DistillationTankLevel, DigitalDisplay6);
                    //DigitalDisplay6.Logic = hashtable[158].ToString();      // Reg
                    break;
                case 31:
                    DistillationTankLevel[3] = !status;
                    IndicateLevel4(DistillationTankLevel, DigitalDisplay6);
                    //DigitalDisplay6.Logic = hashtable[160].ToString();      // Max
                    break;
                case 32:
                    VacuumTankLevel[0] = !status;
                    IndicateHigherLevel1(VacuumTankLevel, DigitalDisplay9);
                    //DigitalDisplay9.Logic = hashtable[160].ToString();      // Max
                    break;
            }
            //if (status)
            //{
            //    switch (index)
            //    {
            //        case 17:
            //            // Process Tank Foam
            //            break;
            //        case 20:
            //            // Process Tank Shuttle Top
            //            break;
            //        case 21:
            //            // Process Tank Shuttle Bottom
            //            break;
            //        case 27:
            //            // Water Separator Level
            //            break;
            //    }
            //}
        }

        private void IndicateLowerLevel1(bool[] state, UIElement uiElement)
        {
            UserControls.DigitalDisplay digitalDisplay = (UserControls.DigitalDisplay)uiElement;

            if (state[0])
            {
                digitalDisplay.Logic = hashtable[158].ToString();
            }
            else
                digitalDisplay.Logic = hashtable[161].ToString();
        }

        private void IndicateHigherLevel1(bool[] state, UIElement uiElement)
        {
            UserControls.DigitalDisplay digitalDisplay = (UserControls.DigitalDisplay)uiElement;

            if (state[0])
            {
                digitalDisplay.Logic = hashtable[160].ToString();
            }
            else
                digitalDisplay.Logic = hashtable[158].ToString();
        }

        private void IndicateLevel2(bool[] state, UIElement uiElement)
        {
            UserControls.DigitalDisplay digitalDisplay = (UserControls.DigitalDisplay)uiElement;

            if (state[0])
            {
                if (state[1])
                    digitalDisplay.Logic = hashtable[158].ToString();
                else
                    digitalDisplay.Logic = hashtable[157].ToString();
            }
            else
                digitalDisplay.Logic = hashtable[161].ToString();
        }

        private void IndicateLevel4(bool[] state, UIElement uiElement)
        {
            UserControls.DigitalDisplay digitalDisplay = (UserControls.DigitalDisplay)uiElement;

            if (state[0])
            {
                if (state[1])
                {
                    if (state[2])
                    {
                        if (state[3])
                            digitalDisplay.Logic = hashtable[160].ToString();
                        else
                            digitalDisplay.Logic = hashtable[158].ToString();
                    }
                    else
                        digitalDisplay.Logic = hashtable[159].ToString();
                }
                else
                    digitalDisplay.Logic = hashtable[159].ToString();
                    //digitalDisplay.Logic = hashtable[157].ToString();
            }
            else
                digitalDisplay.Logic = hashtable[157].ToString();
                //digitalDisplay.Logic = hashtable[161].ToString();

        }

        private string[] InputVariable = new string[]
        {
            // Process Chamber
            ".ARrStnTempPV[1]",                                                                     // 0
            ".ARrStnTempSV[1]",                                                                     // 1

            // Chiller 
            ".ARrStnTempPV[6]",                                                                     // 2
            ".ARrStnTempHALsv[6]",                                                                  // 3
            //".ARrStnTempSV[6]",

            // Sub-Tank 1
            ".ARrStnTempPV[2]",                                                                     // 4
            ".ARrStnTempSV[2]",                                                                     // 5

            // Sub-Tank 2
            ".ARrStnTempPV[3]",                                                                     // 6
            ".ARrStnTempSV[3]",                                                                     // 7

            // Distillation Tank (Top)
            ".ARrStnTempPV[5]",                                                                     // 8
            ".ARrStnTempSV[5]",                                                                     // 9

            // Distillation Tank (Bottom)
            ".ARrStnTempPV[4]",                                                                     // 10
            ".ARrStnTempSV[4]",                                                                     // 11

            // Vacuum Pump
            ".ARrStnVacuumPV[1]",                                                                   // 12

            // Ultrasonic Power
            ".ARiStnUSSideAPv[1]",                                                                  // 13
            ".ARDsStnSeqProcessCtrl[1].Out_DSStnSeqProOutput.i3ProTankBtmUsAPwrPercent",            // 14

            // Ultrasonic Frequency
            ".DSWeberEnclosure[1].DSWeberGenerator[1].IN_iFreqSetPoint",                            // 15
            ".DSWeberEnclosure[1].DSWeberGenerator[1].DSWeberMulFreqSwitching.iFreqOutput_TS",      // 16

            // Process Tank
            ".X101_00",                                                                             // 17   - Foam
            ".X101_01",                                                                             // 18   - Reg
            ".X101_02",                                                                             // 19   - Fully Drain
            ".X101_03",                                                                             // 20   - Shuttle Top
            ".X101_04",                                                                             // 21   - Shuttle Bot

            // Sub-Tank 1 Level
            ".X101_08",                                                                             // 22   - Min
            ".X101_07",                                                                             // 23   - Top
            ".X101_06",                                                                             // 24   - Reg
            ".X101_05",                                                                             // 25   - Max

            // Sub-Tank 2 Level
            ".X101_09",                                                                             // 26   - Min

            // Water Separator Level
            ".X101_10",                                                                             // 27   - Min

            // Distillation Tank Level
            ".X101_14",                                                                             // 28   - Min
            ".X101_13",                                                                             // 29   - Top
            ".X101_12",                                                                             // 30   - Reg
            ".X101_11",                                                                             // 31   - Max

            // Vacuum Tank Level
            ".X101_15",                                                                             // 32   - Max

            // Manual Ultrasonic Value
            //".ARiStnManualUSBtmAsv[1]",
        };

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
                hashtable = Functions.SQL.Query.ExecuteLanguageQuery("108,109,110,111,112,116,135,136,154,155,156,162,921,923,926");

                // Process Chamber
                DigitalDisplay1.EqName = hashtable[116].ToString();
                DigitalDisplay1.MeasurementName = hashtable[112].ToString();
                DigitalDisplay1.Unit = hashtable[155].ToString();
                DigitalDisplay1.LogicName = hashtable[154].ToString();
                DigitalDisplay1.Logic = "";
                DigitalDisplay1.ValueSet = 20.00;
                DigitalDisplay1.ActualValue = 23.67;

                // Chiller
                DigitalDisplay2.EqName = hashtable[926].ToString();
                DigitalDisplay2.MeasurementName = hashtable[112].ToString();
                DigitalDisplay2.Unit = hashtable[155].ToString();
                DigitalDisplay2.Logic = "";
                DigitalDisplay2.ValueSet = 30.00;
                DigitalDisplay2.ActualValue = 25.40;

                // Sub-Tank 1
                DigitalDisplay3.EqName = hashtable[108].ToString() + " 1";
                DigitalDisplay3.MeasurementName = hashtable[112].ToString();
                DigitalDisplay3.Unit = hashtable[155].ToString();
                DigitalDisplay3.LogicName = hashtable[154].ToString();
                DigitalDisplay3.Logic = "";
                DigitalDisplay3.ValueSet = 20.00;
                DigitalDisplay3.ActualValue = 23.67;

                // Sub-Tank 2
                DigitalDisplay4.EqName = hashtable[108].ToString() + " 2";
                DigitalDisplay4.MeasurementName = hashtable[112].ToString();
                DigitalDisplay4.Unit = hashtable[155].ToString();
                DigitalDisplay4.LogicName = hashtable[154].ToString();
                DigitalDisplay4.Logic = "";
                DigitalDisplay4.ValueSet = 20.00;
                DigitalDisplay4.ActualValue = 24.01;

                // Distillation Tank Top
                DigitalDisplay5.EqName = hashtable[109].ToString();
                DigitalDisplay5.Location = hashtable[110].ToString();
                DigitalDisplay5.MeasurementName = hashtable[112].ToString();
                DigitalDisplay5.Unit = hashtable[155].ToString();
                DigitalDisplay5.LogicName = "";
                DigitalDisplay5.Logic = "";
                DigitalDisplay5.ValueSet = 50.00;
                DigitalDisplay5.ActualValue = 48.54;

                // Distillation Tank Bottom
                DigitalDisplay6.EqName = hashtable[109].ToString();
                DigitalDisplay6.Location = hashtable[111].ToString();
                DigitalDisplay6.MeasurementName = hashtable[112].ToString();
                DigitalDisplay6.Unit = hashtable[155].ToString();
                DigitalDisplay6.LogicName = hashtable[154].ToString();
                DigitalDisplay6.Logic = "";
                DigitalDisplay6.ValueSet = 50.00;
                DigitalDisplay6.ActualValue = 49.67;

                // Ultrasonic Power
                DigitalDisplay7.EqName = hashtable[921].ToString();
                DigitalDisplay7.MeasurementName = hashtable[135].ToString();
                DigitalDisplay7.Unit = hashtable[162].ToString();
                DigitalDisplay7.Logic = "";
                DigitalDisplay7.ValueSet = 0.00;
                DigitalDisplay7.ActualValue = 10.00;

                // Ultrasonic Frequency
                DigitalDisplay8.EqName = hashtable[921].ToString();
                DigitalDisplay8.MeasurementName = hashtable[136].ToString();
                DigitalDisplay8.Unit = "kHz";
                DigitalDisplay8.Logic = "";
                DigitalDisplay8.ValueSet = 0.00;
                DigitalDisplay8.ActualValue = 0.00;

                // Vacuum Pump
                DigitalDisplay9.EqName = hashtable[923].ToString();
                DigitalDisplay9.MeasurementName = hashtable[156].ToString();
                DigitalDisplay9.Unit = "kPa";
                DigitalDisplay9.LogicName = hashtable[154].ToString();
                DigitalDisplay9.Logic = "Regular";
                DigitalDisplay9.ValueSet = 0.00;
                DigitalDisplay9.ActualValue = 0.00;

                hashtable = Functions.SQL.Query.ExecuteLanguageQuery("157,158,159,160,161");
            }
            catch (NullReferenceException) { }
        }
    }
}
