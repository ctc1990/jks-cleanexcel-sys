﻿using CLEANXCEL2._2.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
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

namespace CLEANXCEL2._2.Pages.Menu.RecipeManagement
{
    /// <summary>
    /// Interaction logic for SubProcess.xaml
    /// </summary>
    public partial class SubProcess : Page
    {
        public static SubProcess AppWindow;
        public struct Conditions
        {
            public string tag;
            public string variable_name;
            public ConditionPanel conditionPanel;
        }

        List<Mapping> mapping = new List<Mapping>();
        List<Conditions> equipmentCondition = new List<Conditions>();

        public SubProcess()
        {
            InitializeComponent();
            AppWindow = this;
        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            InitiateMapping();
        }

        private void EquipmentTrigger_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;
            if (toggleButton.IsChecked == true)
            {
                ConditionButton a = new ConditionButton();
                ConditionButton conditionButton = new ConditionButton
                {
                    Name = mapping.Where(x => x.button == toggleButton).First().output,
                    Label = toggleButton.Name,
                    EquipmentName = toggleButton.Tag.ToString(),
                    Description = "Some Equipments",
                    VerticalAlignment = VerticalAlignment.Top,
                };

                conditionButton.Checked += RadioButton_Checked;
                RegisterName(mapping.Where(x => x.button == toggleButton).First().output, conditionButton);
                EquipmentStackPanel.Children.Add(conditionButton);
                equipmentCondition.Add(new Conditions { tag = conditionButton.Label.ToString(), variable_name = conditionButton.Name, conditionPanel = new ConditionPanel() });
            }
            else
            {
                if (((ConditionButton)FindName(mapping.Where(x => x.button == toggleButton).First().output)).IsChecked == true)
                    ConditionScrollViewer.Content = null;
                EquipmentStackPanel.Children.Remove((UIElement)FindName(mapping.Where(x => x.button == toggleButton).First().output));
                equipmentCondition.RemoveAll(x => x.variable_name == mapping.Where(y => y.button == toggleButton).First().output);
                UnregisterName(mapping.Where(x => x.button == toggleButton).First().output);
            }
        }

        private void RadioButton_Checked(object sender, EventArgs e)
        {
            ConditionButton radioButton = sender as ConditionButton;
            ConditionScrollViewer.Content = (equipmentCondition.Where(x => x.variable_name == radioButton.Name).First()).conditionPanel;
        }

        struct Mapping
        {
            public ToggleButton button;
            public string output;
        }

        private void InitiateMapping()
        {
            //mapping.Add(new Mapping { button = CH1, output = "" });
            mapping.Add(new Mapping { button = AV1_1, output = "bOut1ActValve1" });
            mapping.Add(new Mapping { button = AV1_2, output = "bOut2ActValve2" });
            mapping.Add(new Mapping { button = AV1_3, output = "bOut3ActValve3" });
            mapping.Add(new Mapping { button = AV1_4, output = "bOut4ActValve4" });
            mapping.Add(new Mapping { button = AV1_5, output = "bOut5ActValve5" });
            mapping.Add(new Mapping { button = AV1_6, output = "bOut6ActValve6" });
            mapping.Add(new Mapping { button = AV1_7, output = "bOut7ActValve7" });
            mapping.Add(new Mapping { button = AV1_8, output = "bOut8ActValve8" });
            mapping.Add(new Mapping { button = AV1_9, output = "bOut9ActValve9" });
            mapping.Add(new Mapping { button = AV1_10, output = "bOut10ActValve10" });
            mapping.Add(new Mapping { button = AV1_11, output = "bOut11ActValve11" });
            mapping.Add(new Mapping { button = AV1_12, output = "bOut12ActValve12" });
            mapping.Add(new Mapping { button = AV1_13, output = "bOut13ActValve13" });
            //mapping.Add(new Mapping { button = AV1_14, output = "bOut14ActValve14" });
            mapping.Add(new Mapping { button = AV1_15, output = "bOut15ActValve15" });
            mapping.Add(new Mapping { button = AV1_16, output = "bOut16ActValve16" });
            mapping.Add(new Mapping { button = AV1_17, output = "bOut17ActValve17" });
            mapping.Add(new Mapping { button = P1, output = "bOut8EnSubTankPump" });
            mapping.Add(new Mapping { button = US, output = "bOut3EnProTankBottomUltrasonicA" });
            mapping.Add(new Mapping { button = H1, output = "bOut2EnProTankHeater" });
            mapping.Add(new Mapping { button = H2, output = "bOut9EnSubTankHeater" });
            mapping.Add(new Mapping { button = H3, output = "WY10013" });
            mapping.Add(new Mapping { button = SV1_1, output = "bOut18ActValve18" });
            //mapping.Add(new Mapping { button = SV1_2, output = "bOut20ActValve20" });
            //mapping.Add(new Mapping { button = SV1_3, output = "bOut18ActValve18" });
            mapping.Add(new Mapping { button = PV1, output = "bOut11EnVacuumPump" });
            //iOut3ProTankBtmUsAPwrPercent
            //iOut8ProTankBtmUsAkHz

            #region new added 20022020
            mapping.Add(new Mapping { button = AV1_19, output = "bOut19ActValve19" });
            mapping.Add(new Mapping { button = AV1_22, output = "bOut22ActValve22" });
            mapping.Add(new Mapping { button = AV1_23, output = "bOut23ActValve23" });
            mapping.Add(new Mapping { button = AV1_24, output = "bOut24ActValve24" });
            mapping.Add(new Mapping { button = AV1_25, output = "bOut25ActValve25" });
            mapping.Add(new Mapping { button = AV1_26, output = "bOut26ActValve26" });
            #endregion
        }

        public struct SubProcess_ValueSetting
        {
            public int iOut1ProTankPumpLPM;
            public int iOut2ProTankPumpHz;
            public int iOut3ProTankBtmUsAPwrPercent;
            public int iOut4ProTankBtmUsBPwrPercent;
            public int iOut5ProTankBtmUsCPwrPercent;
            public int iOut6ProTankSideUsAPwrPercent;
            public int iOut7ProTankSideUsBPwrPercent;
            public int iOut8ProTankBtmUsAkHz;
            public int iOut9ProTankBtmUsBkHz;
            public int iOut10ProTankBtmUsCkHz;
            public int iOut11ProTankSideUsAkHz;
            public int iOut12ProTankSideUsBkHz;
            public int iOut13ProTankBlowerHz;
            public float rOut14ProTankChangeTemperatureDegC;
            public float rOut15SubTankChangeTemperatureDegC;
            public int iOut16SubTankPumpLPM;
            public int iOut17SubTankPumpHz;
            public int iOut18SlowPullVelocity;
            public int iOut19SlowPullDelayTime;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Globals.currentPage = "SubProcess";
            Globals.POPUP_URL = "../Pages/Window/WindowsMessageBox.xaml";
            Globals.POPUP_REQUEST("51", "52", Window.WindowsMessageBox.State.Confirmation);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Globals.currentPage = "SubProcess_Delete";
            Globals.POPUP_URL = "../Pages/Window/WindowsMessageBox.xaml";
            Globals.POPUP_REQUEST("51", "52", Window.WindowsMessageBox.State.Confirmation);
        }

        public void TriggerProcess()
        {
            Globals.currentPage = null;
            Globals.POPUP_URL = "Pages/Window/WindowsMessageBox.xaml";

            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Parameters.AddWithValue("@sub_process_name", CBSubProcessName.Text.ToString());

            if (!Functions.SQL.Query.ExecuteCheckQuery("select * from sub_process_id where sub_process_id.name=@sub_process_name and status = '1'", mySqlCommand))
            {
                UploadSubProcess();
                CBSubProcessName.SelectedIndex = -1;
            }
            else // Stay on the page
            {
                //Globals.currentPage = "SubProcessOverwrite";

                //query.ExecuteNonQuery("update sub_process set sub_process.status = '0' where sub_process.sub_process_name = '" + CBSubProcessName.Text.ToString() + "'", mySqlCommand);

                Globals.POPUP_REQUEST("51", "41", Window.WindowsMessageBox.State.Ok);
            }
        }

        public void TriggerDeleteProcess()
        {
            Globals.currentPage = null;
            Globals.POPUP_URL = "Pages/Window/WindowsMessageBox.xaml";

            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Parameters.AddWithValue("@sub_process_name", CBSubProcessName.Text.ToString());

            if (Functions.SQL.Query.ExecuteCheckQuery("select * from sub_process_id where sub_process_id.name=@sub_process_name and status = '1'", mySqlCommand))
            {
                // Reset Sub-Process Status
                Functions.SQL.Query.ExecuteNonQuery("update sub_process_id set sub_process_id.status = '0' where sub_process_id.name=@sub_process_name", mySqlCommand);

                // Reset Process Status
                Functions.SQL.Query.ExecuteNonQuery("update process_id " +
                                                    "inner join process on process.process_name = process_id.id " +
                                                    "inner join sub_process_id on sub_process_id.id = process.sub_process_name " +
                                                    "set process_id.status = '0' " +
                                                    "where sub_process_id.name = @sub_process_name", mySqlCommand);

                // Reset Recipe Status
                Functions.SQL.Query.ExecuteNonQuery("update recipe_id " +
                                                    "inner join recipe on recipe.recipe_name = recipe_id.id " +
                                                    "inner join process_id on process_id.id = recipe.process_name " +
                                                    "inner join process on process.process_name = process_id.id " +
                                                    "inner join sub_process_id on sub_process_id.id = process.sub_process_name " +
                                                    "set recipe_id.status = '0' " +
                                                    "where sub_process_id.name = @sub_process_name", mySqlCommand);

                // Reset Part Status
                Functions.SQL.Query.ExecuteNonQuery("update part_id " +
                                                    "inner join part on part.part_name = part_id.id " +
                                                    "inner join recipe_id on recipe_id.id = part.recipe_name " +
                                                    "inner join recipe on recipe.recipe_name = recipe_id.id " +
                                                    "inner join process_id on process_id.id = recipe.process_name " +
                                                    "inner join process on process.process_name = process_id.id " +
                                                    "inner join sub_process_id on sub_process_id.id = process.sub_process_name " +
                                                    "set part_id.status = '0' " +
                                                    "where sub_process_id.name = @sub_process_name", mySqlCommand);

                // Clear Sub-Process
                CBSubProcessName.Items.Clear();

                // Populate Sub-Process
                foreach (string subprocessname in Functions.SQL.Query.ExecuteSingleQuery("select sub_process_id.name from sub_process_id where (sub_process_id.status = '1' and sub_process_id.name not LIKE '%TEMPLATE%')", "name"))
                {
                    if (!subprocessname.ToLower().Contains("avx"))
                        CBSubProcessName.Items.Add(subprocessname);
                }

                CBSubProcessName.SelectedIndex = -1;
            }
            else // Stay on the page
            {
                //Globals.currentPage = "SubProcessOverwrite";

                //query.ExecuteNonQuery("update sub_process set sub_process.status = '0' where sub_process.sub_process_name = '" + CBSubProcessName.Text.ToString() + "'", mySqlCommand);

                Globals.POPUP_REQUEST("51", "81", Window.WindowsMessageBox.State.Ok);
            }
        }

        public void TriggerOverwriteProcess()
        {
            UploadSubProcess();
        }

        private void UploadSubProcess()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcessId(CBSubProcessName.Text.ToString());
            foreach (Conditions equipment in equipmentCondition)
            {
                if (!equipment.conditionPanel.Encode(equipment.variable_name).StartConditions.Any() && !equipment.conditionPanel.Encode(equipment.variable_name).StopConditions.Any())
                {
                    Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcess(equipment.tag, equipment.conditionPanel.Encode(equipment.variable_name).Name, "");
                }
                foreach (string StartCondition in equipment.conditionPanel.Encode(equipment.variable_name).StartConditions)
                {
                    Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcess(equipment.tag, equipment.conditionPanel.Encode(equipment.variable_name).Name, StartCondition);
                }
                foreach (string StopCondition in equipment.conditionPanel.Encode(equipment.variable_name).StopConditions)
                {
                    Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLSubProcess(equipment.tag, equipment.conditionPanel.Encode(equipment.variable_name).Name, StopCondition);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            try
            {
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("105,3,28");

                TBSubProcessNameTitle.Text = hashtable[28].ToString() + ((MainWindow.language == "mandarin" || MainWindow.language == "japanese") ? "" : " ") + hashtable[3].ToString();
                Save.Content = hashtable[105].ToString();
            }
            catch { }
        }

        private void Initialize()
        {
            foreach (string subprocessname in Functions.SQL.Query.ExecuteSingleQuery("select sub_process_id.name from sub_process_id where (sub_process_id.status = '1' and sub_process_id.name not LIKE '%TEMPLATE%')", "name"))
            {
                if (!subprocessname.ToLower().Contains("avx"))
                    CBSubProcessName.Items.Add(subprocessname);
            }
        }

        private void CBSubProcessName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            for (int i = 0; i < mapping.Count(); i++)
            {
                try
                {
                    mapping[i].button.IsChecked = false;
                    EquipmentTrigger_Click(mapping[i].button, null);
                }
                catch (NullReferenceException) { }
            }

            if (CBSubProcessName.SelectedIndex != -1)
            {
                string SubProcessName = CBSubProcessName.SelectedItem.ToString();
                try
                {
                    foreach (string equipmentstate in Functions.SQL.Query.ExecuteSingleQuery(
                        "select distinct sub_process.equipment_state from sub_process right join sub_process_id on " +
                        "sub_process_id.id = sub_process.sub_process_name where " +
                        "(sub_process_id.status = '1' and sub_process_id.name = '" + SubProcessName + "')", "equipment_state"))
                    {
                        ToggleButton toggleButton = mapping.Where(x => equipmentstate.Contains(x.output)).First().button;
                        toggleButton.IsChecked = true;

                        EquipmentTrigger_Click(toggleButton, null);
                        foreach (string condition in Functions.SQL.Query.ExecuteSingleQuery(
                            "select sub_process.conditions from sub_process left join sub_process_id on sub_process_id.id = sub_process.sub_process_name" +
                            " where (sub_process_id.name = '" + SubProcessName + "' and sub_process.equipment_state = '" + equipmentstate + "')", "conditions"))
                        {
                            if (condition != "")
                            {
                                Conditions conditions = equipmentCondition.Where(x => x.tag == toggleButton.Name).First();
                                if (condition.Contains("="))
                                {
                                    string[] info = condition.Split('=');
                                    conditions.conditionPanel.Decode(info[0], "=", info[1]);
                                    ConditionState(info[0], (ConditionButton)FindName(conditions.variable_name));
                                }
                                else if (condition.Contains(">"))
                                {
                                    string[] info = condition.Split('>');
                                    conditions.conditionPanel.Decode(info[0], ">", info[1]);
                                    ConditionState(info[0], (ConditionButton)FindName(conditions.variable_name));
                                }
                                else if (condition.Contains("<"))
                                {
                                    string[] info = condition.Split('<');
                                    conditions.conditionPanel.Decode(info[0], "<", info[1]);
                                    ConditionState(info[0], (ConditionButton)FindName(conditions.variable_name));
                                }
                            }
                        }
                    }
                }
                catch (NullReferenceException) { }
            }
        }

        private static void ConditionState(string variable, ConditionButton conditionButton)
        {
            if (variable.Contains("St"))
                conditionButton.StartCondEnable = Visibility.Visible;
            else
                conditionButton.StopCondEnable = Visibility.Visible;
        }
    }
}
