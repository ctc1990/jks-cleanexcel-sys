﻿using System;
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
using CLEANXCEL2._2.Functions.EventNotifier;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Pages.Menu.Bypass
{
    /// <summary>
    /// Interaction logic for SchematicsActive.xaml
    /// </summary>
    public partial class SchematicsActive : Page
    {
        ADSEventNotification aDSEventNotification = new ADSEventNotification();
        List<Mapping> mapping = new List<Mapping>();
        private TcAdsClient adsClient = new TcAdsClient();

        public SchematicsActive()
        {
            InitializeComponent();
        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                adsClient.Connect(Globals.AMSNetID, Globals.AMSPort);

                List<Functions.EventNotifier.ADSEventNotification.VariableInfo> variableInfo = new List<Functions.EventNotifier.ADSEventNotification.VariableInfo>();
                try
                {
                    for (int i = 0; i < mapping.Count() - 2; i++)
                    {
                        variableInfo.Add(new Functions.EventNotifier.ADSEventNotification.VariableInfo() { variableName = mapping[i].input, variableType = "bool" });
                    }

                    aDSEventNotification.ADSEventGenerateNotif(adsClient, variableInfo);
                    aDSEventNotification.ActionChange += ADSEventNotification_ActionChange;
                }
                catch
                { }
            }
        }

        private void ADSEventNotification_ActionChange()
        {
            mapping[aDSEventNotification.index].button.IsChecked = Convert.ToBoolean(aDSEventNotification.response);
        }

        private void EquipmentTrigger_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.CONNECTION)
            {
                ToggleButton toggleButton = (ToggleButton)sender;

                switch (toggleButton.IsChecked)
                {
                    case true:
                        //toggleButton.IsChecked = true;
                        Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, mapping.Where(x => x.button == toggleButton).First().output, (toggleButton.IsChecked).ToString(), "bool");
                        //CardCollection.Children.Add(LoadCard(toggleButton.Name.Replace('_', '-')));
                        break;
                    case null:
                        //toggleButton.IsChecked = false;
                        //Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, mapping.Where(x => x.button == toggleButton).First().output, "true", "bool");
                        break;
                    case false:
                        //toggleButton.IsChecked = null;
                        Functions.ADS.ADS_ReadWrite.ADS_WriteValue(adsClient, mapping.Where(x => x.button == toggleButton).First().output, (toggleButton.IsChecked).ToString(), "bool");
                        break;
                }
            }
        }

        //private static UserControls.Card LoadCard(string label)
        //{
        //    return new UserControls.Card { Label = label };
        //}

        private void InitiateMapping()
        {
            //mapping.Add(new Mapping { button = CH1, input = ".Y100_06", output = ".WY10006_15" });
            mapping.Add(new Mapping { button = AV1_1, input = ".DE10100", output = ".DE10100" });
            mapping.Add(new Mapping { button = AV1_2, input = ".DE10102", output = ".DE10102" });
            mapping.Add(new Mapping { button = AV1_3, input = ".DE10103", output = ".DE10103" });
            mapping.Add(new Mapping { button = AV1_4, input = ".DE10104", output = ".DE10104" });
            mapping.Add(new Mapping { button = AV1_5, input = ".DE10105", output = ".DE10105" });
            mapping.Add(new Mapping { button = AV1_6, input = ".DE10106", output = ".DE10106" });
            mapping.Add(new Mapping { button = AV1_7, input = ".DE10107", output = ".DE10107" });
            mapping.Add(new Mapping { button = AV1_8, input = ".DE10108", output = ".DE10108" });
            mapping.Add(new Mapping { button = AV1_9, input = ".DE10109", output = ".DE10109" });
            mapping.Add(new Mapping { button = AV1_10, input = ".DE10110", output = ".DE10110" });
            mapping.Add(new Mapping { button = AV1_11, input = ".DE10111", output = ".DE10111" });
            mapping.Add(new Mapping { button = AV1_12, input = ".DE10112", output = ".DE10112" });
            mapping.Add(new Mapping { button = AV1_13, input = ".DE10113", output = ".DE10113" });
            //mapping.Add(new Mapping { button = AV1_14, input = ".DE10114", output = ".DE10114" });
            mapping.Add(new Mapping { button = AV1_15, input = ".DE10115", output = ".DE10115" });
            mapping.Add(new Mapping { button = AV1_16, input = ".DE10115", output = ".DE10115" });
            mapping.Add(new Mapping { button = AV1_17, input = ".DE10200", output = ".DE10200" });
            mapping.Add(new Mapping { button = P1, input = ".DE10010", output = ".DE10010" });
            mapping.Add(new Mapping { button = PV1, input = ".DE10009", output = ".DE10009" });
            mapping.Add(new Mapping { button = US, input = ".DE10008", output = ".DE10008" });
            mapping.Add(new Mapping { button = H1, input = ".DE10011", output = ".DE10011" });
            mapping.Add(new Mapping { button = H2, input = ".DE10012", output = ".DE10012" });
            mapping.Add(new Mapping { button = H3, input = ".DE10013", output = ".DE10013" });
            mapping.Add(new Mapping { button = SV1_1, input = ".DE10202", output = ".DE10202" });
            mapping.Add(new Mapping { button = Door, input = ".DE10206", output = ".DE10206" }); // Clamp = DE10206, Unclamp = DE10207

            #region new added
            //mapping.Add(new Mapping { button = AV1_19, input = ".DE10300", output = ".DE10300" }); //valve not in deactivated
            //mapping.Add(new Mapping { button = AV1_22, input = ".DE10301", output = ".DE10301" });
            //mapping.Add(new Mapping { button = AV1_23, input = ".DE10302", output = ".DE10302" });
            //mapping.Add(new Mapping { button = AV1_24, input = ".DE10303", output = ".DE10303" });
            //mapping.Add(new Mapping { button = AV1_25, input = ".DE10304", output = ".DE10304" });
            //mapping.Add(new Mapping { button = AV1_26, input = ".DE10305", output = ".DE10305" });
            #endregion
        }

        struct Mapping
        {
            public ToggleButton button;
            public string output;
            public string input;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitiateMapping();
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
