using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.UserControls
{
    /// <summary>
    /// Interaction logic for Schematics.xaml
    /// </summary>
    public partial class Schematics : UserControl
    {
        private bool leftClamp = false, rightClamp = false;
        private bool leftUnclamp = false, rightUnclamp = false;

        private List<IO> schematics_map = new List<IO>();
        private static AdsStream adsDataStream;
        private static BinaryReader binRead;
        private TcAdsClient adsClient = new TcAdsClient();
        private static int[] hconnect;

        public Schematics()
        {
            InitializeComponent();

            schematics_map = InitiateMapping();
        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                adsDataStream = new AdsStream(schematics_map.Count());
                hconnect = new int[schematics_map.Count()];

                binRead = new BinaryReader(adsDataStream, Encoding.ASCII);

                adsClient.Connect(Globals.AMSNetID, Globals.AMSPort);

                for (int i = 0; i < schematics_map.Count() - 2; i++)
                {
                    hconnect[i] = adsClient.AddDeviceNotification(schematics_map[i].input, adsDataStream, i, 1, AdsTransMode.OnChange, 50, 0, null);
                }

                adsClient.AdsNotification += new AdsNotificationEventHandler(StatusOnChange);
            }
            catch
            {

            }
        }

        private void StatusOnChange(object sender, AdsNotificationEventArgs e)
        {
            for (int i = 0; i < schematics_map.Count(); i++)
            {
                if (e.NotificationHandle == hconnect[i])
                {
                    try
                    {
                        switch (schematics_map[i].input)
                        {
                            case ".X102_08":
                                leftClamp = binRead.ReadBoolean();
                                schematics_map[i].button.IsChecked = TriggerWhenTrue((leftUnclamp && rightUnclamp));
                                break;
                            case ".X102_10":
                                rightClamp = binRead.ReadBoolean();
                                schematics_map[i].button.IsChecked = TriggerWhenTrue((leftUnclamp && rightUnclamp));
                                break;
                            case ".X102_09":
                                leftUnclamp = binRead.ReadBoolean();
                                schematics_map[i].button.IsChecked = !TriggerWhenTrue((leftUnclamp && rightUnclamp));
                                break;
                            case ".X102_11":
                                rightUnclamp = binRead.ReadBoolean();
                                schematics_map[i].button.IsChecked = !TriggerWhenTrue((leftUnclamp && rightUnclamp));
                                break;
                            case ".X101_03":
                                schematics_map[i].button.IsChecked = !TriggerWhenTrue(binRead.ReadBoolean());
                                break;
                            default:
                                schematics_map[i].button.IsChecked = TriggerWhenTrue(binRead.ReadBoolean());
                                break;
                        }
                    }
                    catch { }
                }
            }
        }

        private bool? TriggerWhenTrue(bool status)
        {
            if (status)
                return true;
            else
                return null;
        }

        private void EquipmentTrigger_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;

            switch (toggleButton.Name)
            {
                case "Door":
                case "Clamp":
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, schematics_map.Where(x => x.button == toggleButton).First().output, (toggleButton.IsChecked).ToString(), "bool");
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, schematics_map.Where(x => x.button == toggleButton).ElementAt(1).output, (!toggleButton.IsChecked).ToString(), "bool");
                    break;
                case "Circulation":
                case "SolventTopUp":
                    break;

                default:
                    Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, schematics_map.Where(x => x.button == toggleButton).First().output, toggleButton.IsChecked.ToString(), "bool");
                    break;
            }

            toggleButton.IsChecked = null;
        }

        public List<IO> InitiateMapping()
        {
            List<IO> Mapping = new List<IO>();

            // Equipments
            Mapping.Add(new IO { tag = "CH1", button = CH1, input = ".Y100_06", output = ".WY10006_15", bypass = "." });
            Mapping.Add(new IO { tag = "P1", button = P1, input = ".Y100_10", output = ".WY10010_15", bypass = ".DE10010" });
            Mapping.Add(new IO { tag = "PV1", button = PV1, input = ".Y100_09", output = ".WY10009_15", bypass = ".DE10009" });
            Mapping.Add(new IO { tag = "US1", button = US, input = ".Y100_08", output = ".WY10008_15", bypass = ".DE10008" });
            Mapping.Add(new IO { tag = "H1", button = H1, input = ".Y100_11", output = ".WY10011_15", bypass = ".DE10011" });
            Mapping.Add(new IO { tag = "H2", button = H2, input = ".Y100_12", output = ".WY10012_15", bypass = ".DE10012" });
            Mapping.Add(new IO { tag = "H3", button = H3, input = ".Y100_13", output = ".WY10013_15", bypass = ".DE10013" });

            // Air Valve
            Mapping.Add(new IO { tag = "AV1_1", button = AV1_1, input = ".Y101_00", output = ".WY10100_15", bypass = ".DE10100" });
            Mapping.Add(new IO { tag = "AV1_2", button = AV1_2, input = ".Y101_01", output = ".WY10101_15", bypass = ".DE10102" });
            Mapping.Add(new IO { tag = "AV1_3", button = AV1_3, input = ".Y101_02", output = ".WY10102_15", bypass = ".DE10103" });
            Mapping.Add(new IO { tag = "AV1_4", button = AV1_4, input = ".Y101_03", output = ".WY10103_15", bypass = ".DE10104" });
            Mapping.Add(new IO { tag = "AV1_5", button = AV1_5, input = ".Y101_04", output = ".WY10104_15", bypass = ".DE10105" });
            Mapping.Add(new IO { tag = "AV1_6", button = AV1_6, input = ".Y101_05", output = ".WY10105_15", bypass = ".DE10106" });
            Mapping.Add(new IO { tag = "AV1_7", button = AV1_7, input = ".Y101_06", output = ".WY10106_15", bypass = ".DE10107" });
            Mapping.Add(new IO { tag = "AV1_8", button = AV1_8, input = ".Y101_07", output = ".WY10107_15", bypass = ".DE10108" });
            Mapping.Add(new IO { tag = "AV1_9", button = AV1_9, input = ".Y101_08", output = ".WY10108_15", bypass = ".DE10109" });
            Mapping.Add(new IO { tag = "AV1_10", button = AV1_10, input = ".Y101_09", output = ".WY10109_15", bypass = ".DE10110" });
            Mapping.Add(new IO { tag = "AV1_11", button = AV1_11, input = ".Y101_10", output = ".WY10110_15", bypass = ".DE10111" });
            Mapping.Add(new IO { tag = "AV1_12", button = AV1_12, input = ".Y101_11", output = ".WY10111_15", bypass = ".DE10112" });
            Mapping.Add(new IO { tag = "AV1_13", button = AV1_13, input = ".Y101_12", output = ".WY10112_15", bypass = ".DE10113" });
            //Mapping.Add(new IO { tag = "AV1_14", button = AV1_14, input = ".Y101_13", output = ".WY10113_15", bypass = ".DE10114" });
            Mapping.Add(new IO { tag = "AV1_15", button = AV1_15, input = ".Y101_14", output = ".WY10114_15", bypass = ".DE10115" });
            Mapping.Add(new IO { tag = "AV1_16", button = AV1_16, input = ".Y101_15", output = ".WY10115_15", bypass = ".DE10115" });
            Mapping.Add(new IO { tag = "AV1_17", button = AV1_17, input = ".Y102_00", output = ".WY10200_15", bypass = ".DE10200" });

            #region new added
            Mapping.Add(new IO { tag = "AV1_19", button = AV1_19, input = ".Y103_00", output = ".WY10300_15", bypass = ".DE10300" });
            Mapping.Add(new IO { tag = "AV1_22", button = AV1_22, input = ".Y103_01", output = ".WY10301_15", bypass = ".DE10302" });
            Mapping.Add(new IO { tag = "AV1_23", button = AV1_23, input = ".Y103_02", output = ".WY10302_15", bypass = ".DE10303" });
            Mapping.Add(new IO { tag = "AV1_24", button = AV1_24, input = ".Y103_03", output = ".WY10303_15", bypass = ".DE10304" });
            Mapping.Add(new IO { tag = "AV1_25", button = AV1_25, input = ".Y103_04", output = ".WY10304_15", bypass = ".DE10305" });
            Mapping.Add(new IO { tag = "AV1_26", button = AV1_26, input = ".Y103_05", output = ".WY10305_15", bypass = ".DE10306" });
            #endregion

            // Solenoid Valve
            Mapping.Add(new IO { tag = "SV1_1", button = SV1_1, input = ".Y102_01", output = ".WY10201_15", bypass = ".DE10202" });
            Mapping.Add(new IO { tag = "SV1_2", button = SV1_2, input = ".", output = ".", bypass = ".DE10204" });
            Mapping.Add(new IO { tag = "SV1_3", button = SV1_3, input = ".", output = ".", bypass = ".DE10205" });

            // Gateway
            Mapping.Add(new IO { tag = "Door Close", button = Door, input = ".X101_04", output = ".WY10014_15", bypass = ".DE10206" }); //Open = .WY10015_15 (.X101_03), Close = .WY10014_15 (.X101_04)
            Mapping.Add(new IO { tag = "Door Open", button = Door, input = ".X101_03", output = ".WY10015_15", bypass = ".DE10207" }); //Open = .WY10015_15 (.X101_03), Close = .WY10014_15 (.X101_04)

            // Others
            Mapping.Add(new IO { tag = "Clamp Left", button = Clamp, input = ".X102_08", output = ".WY10206_15", bypass = "." }); //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
            Mapping.Add(new IO { tag = "Unclamp Left", button = Clamp, input = ".X102_09", output = ".WY10207_15", bypass = "." }); //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
            Mapping.Add(new IO { tag = "Clamp Right", button = Clamp, input = ".X102_10", output = ".WY10206_15", bypass = "." }); //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
            Mapping.Add(new IO { tag = "Unclamp Right", button = Clamp, input = ".X102_11", output = ".WY10207_15", bypass = "." }); //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
            //Mapping.Add(new IO { tag = "CH1", button = Circulation, input = ".wPumpCircFunction", output = ".wPumpCircFunction", bypass = "." });
            //Mapping.Add(new IO { tag = "CH1", button = SolventTopUp, input = ".wSolventTopupFunction.15", output = ".wSolventTopupFunction.15", bypass = "." });

            return Mapping;
        }

        public struct IO
        {
            public string tag;
            public ToggleButton button;
            public string output;
            public string input;
            public string bypass;
        }
    }
}
