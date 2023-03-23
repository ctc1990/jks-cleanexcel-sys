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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Pages.Menu.CurrentStatus
{
    /// <summary>
    /// Interaction logic for Schematics.xaml
    /// </summary>
    public partial class Schematics : Page
    {
        private bool leftClamp = false, rightClamp = false, leftUnclamp = false, rightUnclamp = false;
        List<Mapping> mapping = new List<Mapping>();
        private static AdsStream adsDataStream;
        private static BinaryReader binRead;
        private static bool[] bitMap;
        private TcAdsClient adsClient = new TcAdsClient();
        private static int[] hconnect;

        private static AdsStream adsDataStreamProcessname = new AdsStream(109);
        private static AdsBinaryReader binReadProcessname = new AdsBinaryReader(adsDataStreamProcessname);

        public Schematics()
        {
            InitializeComponent();

            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitiateMapping();
        }

        private void Frame_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            FrameSensor.Source = new Uri("../Maintenance/Sensor.xaml", UriKind.RelativeOrAbsolute);
            if (Globals.CONNECTION)
            {
                try
                {
                    adsDataStream = new AdsStream(mapping.Count());
                    hconnect = new int[mapping.Count()];
                    bitMap = new bool[mapping.Count()];

                    binRead = new BinaryReader(adsDataStream, Encoding.ASCII);

                    adsClient.Connect(Globals.AMSNetID, Globals.AMSPort);
                    for (int i = 0; i < mapping.Count(); i++)
                        hconnect[i] = adsClient.AddDeviceNotification(mapping[i].input, adsDataStream, i, 1, AdsTransMode.OnChange, 50, 0, null);

                    adsClient.AdsNotification += new AdsNotificationEventHandler(StatusOnChange);
                }
                catch
                {

                }
            }
        }

        private void StatusOnChange(object sender, AdsNotificationEventArgs e)
        {
            #region show process name
            //if (e.NotificationHandle == hconnect[1])
            //{
            //    string value = binReadProcessname.ReadPlcAnsiString(99);
            //    //ProcessName.Text = string.Format(value);
            //}
            #endregion

            for (int i = 0; i < mapping.Count(); i++)
            {
                if (e.NotificationHandle == hconnect[i])
                {
                    switch (mapping[i].input)
                    {
                        case ".X102_08":
                            leftClamp = binRead.ReadBoolean();
                            ((ToggleButton)mapping[i].button).IsChecked = QuadState2Conditions(leftClamp, rightClamp, leftUnclamp, rightUnclamp);
                            break;
                        case ".X102_10":
                            rightClamp = binRead.ReadBoolean();
                            ((ToggleButton)mapping[i].button).IsChecked = QuadState2Conditions(leftClamp, rightClamp, leftUnclamp, rightUnclamp);
                            break;
                        case ".X102_09":
                            leftUnclamp = binRead.ReadBoolean();
                            ((ToggleButton)mapping[i].button).IsChecked = QuadState2Conditions(leftClamp, rightClamp, leftUnclamp, rightUnclamp);
                            break;
                        case ".X102_11":
                            rightUnclamp = binRead.ReadBoolean();
                            ((ToggleButton)mapping[i].button).IsChecked = QuadState2Conditions(leftClamp, rightClamp, leftUnclamp, rightUnclamp);
                            break;
                        case ".X101_03":
                            if (binRead.ReadBoolean())
                                ((ToggleButton)mapping[i].button).IsChecked = false;
                            break;
                        case ".X101_04":
                            if (binRead.ReadBoolean())
                                ((ToggleButton)mapping[i].button).IsChecked = true;
                            break;
                        default:
                            ((ToggleButton)mapping[i].button).IsChecked = binRead.ReadBoolean();
                            break;
                    }
                }
            }
        }

        private static bool? QuadState2Conditions(bool condition1, bool condition2, bool condition3, bool condition4)
        {
            if (condition1 && condition2)
                return true;
            else if (condition3 && condition4)
                return false;
            else
                return null;
        }

        struct Mapping
        {
            public UIElement button;
            public string output;
            public string input;
        }

        private void InitiateMapping()
        {
            mapping = new List<Mapping>()
            {
                new Mapping { button = CH1, input = ".Y100_06", output = ".WY10006_15" },
                new Mapping { button = AV1_1, input = ".Y101_00", output = ".WY10100_15" },
                new Mapping { button = AV1_2, input = ".Y101_01", output = ".WY10101_15" },
                new Mapping { button = AV1_3, input = ".Y101_02", output = ".WY10102_15" },
                new Mapping { button = AV1_4, input = ".Y101_03", output = ".WY10103_15" },
                new Mapping { button = AV1_5, input = ".Y101_04", output = ".WY10104_15" },
                new Mapping { button = AV1_6, input = ".Y101_05", output = ".WY10105_15" },
                new Mapping { button = AV1_7, input = ".Y101_06", output = ".WY10106_15" },
                new Mapping { button = AV1_8, input = ".Y101_07", output = ".WY10107_15" },
                new Mapping { button = AV1_9, input = ".Y101_08", output = ".WY10108_15" },
                new Mapping { button = AV1_10, input = ".Y101_09", output = ".WY10109_15" },
                new Mapping { button = AV1_11, input = ".Y101_10", output = ".WY10110_15" },
                new Mapping { button = AV1_12, input = ".Y101_11", output = ".WY10111_15" },
                new Mapping { button = AV1_13, input = ".Y101_12", output = ".WY10112_15" },
                ////new Mapping { button = AV1_14, input = ".Y101_13", output = ".WY10113_15" },
                new Mapping { button = AV1_15, input = ".Y101_14", output = ".WY10114_15" },
                new Mapping { button = AV1_16, input = ".Y101_15", output = ".WY10115_15" },
                new Mapping { button = AV1_17, input = ".Y102_00", output = ".WY10200_15" },
                new Mapping { button = P1, input = ".Y100_10", output = ".WY10010_15" },
                new Mapping { button = US, input = ".Y100_08", output = ".WY10008_15" },
                ////new Mapping { button = H1, input = ".Y100_11", output = ".WY10011_15" },
                ////new Mapping { button = H2, input = ".Y100_12", output = ".WY10012_15" },
                new Mapping { button = H3, input = ".Y100_13", output = ".WY10013_15" },
                new Mapping { button = SV1_1, input = ".Y102_01", output = ".WY10201_15" },
                //new Mapping { button = SV1_2, input = ".", output = "." },
                //new Mapping { button = SV1_3, input = ".", output = "." },
                new Mapping { button = PV1, input = ".Y100_09", output = ".WY10009_15" },
                new Mapping { button = Clamp, input = ".X102_08", output = ".WY10206_15" }, //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
                new Mapping { button = Clamp, input = ".X102_10", output = ".WY10206_15" }, //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
                new Mapping { button = Clamp, input = ".X102_09", output = ".WY10207_15" }, //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
                new Mapping { button = Clamp, input = ".X102_11", output = ".WY10207_15" }, //Clamp = .WY10206_15 (.X102_08, .X102_10), Unclamp = .WY10207_15 (.X102_09, .X102_11)
                new Mapping { button = Door, input = ".X101_03", output = ".WY10015_15" }, //Open = .WY10015_15 (.X101_03), Close = .WY10014_15 (.X101_04)
                new Mapping { button = Door, input = ".X101_04", output = ".WY10014_15" }, //Open = .WY10015_15 (.X101_03), Close = .WY10014_15 (.X101_04)
                new Mapping { button = Lamp, input = ".Y102_13", output = ".WY10213_15" },

                #region valve added
                new Mapping { button = AV1_19, input = ".Y103_00", output = ".WY10300_15" },
                new Mapping { button = AV1_22, input = ".Y103_01", output = ".WY10301_15" },
                new Mapping { button = AV1_23, input = ".Y103_02", output = ".WY10302_15" },
                new Mapping { button = AV1_24, input = ".Y103_03", output = ".WY10303_15" },
                new Mapping { button = AV1_25, input = ".Y103_04", output = ".WY10304_15" },
                new Mapping { button = AV1_26, input = ".Y103_05", output = ".WY10305_15" }
                #endregion
            };
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
