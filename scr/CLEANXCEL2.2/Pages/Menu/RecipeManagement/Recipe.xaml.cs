using CLEANXCEL2._2.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CLEANXCEL2._2.Pages.Menu.RecipeManagement
{
    /// <summary>
    /// Interaction logic for Recipe.xaml
    /// </summary>
    public partial class Recipe : Page
    {
        public Recipe()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            foreach (string processname in Functions.SQL.Query.ExecuteSingleQuery("select process_id.name from process_id where process_id.status = '1'", "name"))
            {
                Button button = new Button()
                {
                    Name = Regex.Replace(processname, @"[^0-9a-zA-Z]+", "_"),
                    ToolTip = processname,
                    Content = "Process ID: " + (ProcessList.Children.Count + 1),
                    Style = (Style)FindResource("BStandardSelection")
                };
                button.Click += SubProcessName_Click;

                ProcessList.Children.Add(button); //(ProcessList.Items.Count + 1) + "\t" + 
            }
            foreach (string processname in Functions.SQL.Query.ExecuteSingleQuery("select recipe_id.name from recipe_id where recipe_id.status = '1'", "name"))
                CBRecipeName.Items.Add(processname);
        }

        private void SubProcessName_Click(object sender, RoutedEventArgs e)
        {
            string processname = (sender as Button).ToolTip.ToString();
            Functions.SQL.Query query = new Functions.SQL.Query();

            #region check process have US                     
            try
            {
                bool processHaveUS = FilterUSInProcess(processname);

                if (!processHaveUS)
                {
                    ProcessDuration.Children.Add(new RecipeTableWithoutUS() { TitleWithoutUS = processname, ProcessTime = ProcessTime(processname) });
                }
                else
                {
                    ProcessDuration.Children.Add(new RecipeTable() { Title = processname, ProcessTime = ProcessTime(processname) });
                }
            }
            catch { }

            #endregion
        }

        private int ProcessTime(string title)
        {
            List<int> list = new List<int>();
            foreach (string processconditions in Functions.SQL.Query.ExecuteSingleQuery("select process.conditions from process right join process_id on process_id.id = process.process_name where process_id.status = 1 and process_id.name='" + title + "'", "conditions"))
            {
                list.AddRange(processconditions.Split(':', '~').Select(x => Convert.ToInt32(x)));
            }

            return (list.Max() - list.Min());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Globals.currentPage = "Recipe";
            Globals.POPUP_URL = "../Pages/Window/WindowsMessageBox.xaml";
            Globals.POPUP_REQUEST("51", "52", Window.WindowsMessageBox.State.Confirmation);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Globals.currentPage = "Recipe_Delete";
            Globals.POPUP_URL = "../Pages/Window/WindowsMessageBox.xaml";
            Globals.POPUP_REQUEST("51", "52", Window.WindowsMessageBox.State.Confirmation);
        }

        public void TriggerProcess()
        {
            Globals.currentPage = null;
            Globals.POPUP_URL = "Pages/Window/WindowsMessageBox.xaml";

            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Parameters.AddWithValue("@recipe_name", CBRecipeName.Text.ToString());

            if (!Functions.SQL.Query.ExecuteCheckQuery("select * from recipe_id where recipe_id.name=@recipe_name and status = '1'", mySqlCommand))
            {
                UploadProcess();
            }
            else // Stay on the page
            {
                //Globals.currentPage = "RecipeOverwrite";

                //query.ExecuteNonQuery("update recipe set recipe.status = '0' where recipe.recipe_name = '" + CBRecipeName.Text.ToString() + "'", mySqlCommand);

                Globals.POPUP_REQUEST("51", "41", Window.WindowsMessageBox.State.Ok);
            }
        }

        public void TriggerDeleteProcess()
        {
            Globals.currentPage = null;
            Globals.POPUP_URL = "Pages/Window/WindowsMessageBox.xaml";

            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Parameters.AddWithValue("@recipe_name", CBRecipeName.Text.ToString());

            if (Functions.SQL.Query.ExecuteCheckQuery("select * from recipe_id where recipe_id.name=@recipe_name and status = '1'", mySqlCommand))
            {
                // Reset Recipe Status
                Functions.SQL.Query.ExecuteNonQuery("update recipe_id set recipe_id.status = '0' where recipe_id.name=@recipe_name", mySqlCommand);

                // Reset Part Status
                Functions.SQL.Query.ExecuteNonQuery("update part_id " +
                                                    "inner join part on part.part_name = part_id.id " +
                                                    "inner join recipe_id on recipe_id.id = part.recipe_name " +
                                                    "set part_id.status = '0' " +
                                                    "where recipe_id.name = @recipe_name", mySqlCommand);

                // Clear Recipe
                CBRecipeName.Items.Clear();

                // Populate Recipe
                foreach (string processname in Functions.SQL.Query.ExecuteSingleQuery("select recipe_id.name from recipe_id where recipe_id.status = '1'", "name"))
                    CBRecipeName.Items.Add(processname);
            }
            else // Stay on the page
            {
                Globals.POPUP_REQUEST("51", "81", Window.WindowsMessageBox.State.Ok);
            }
        }

        public void TriggerOverwriteProcess()
        {
            UploadProcess();
        }

        public void UploadProcess()
        {
            Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLRecipeId(CBRecipeName.Text.ToString());

            //bool haveUS = FilterUSInRecipe(CBRecipeName.SelectedItem.ToString());

            foreach (UIElement children in ProcessDuration.Children)
            {
                #region check process have US
                try
                {
                    RecipeTable standardTable = (RecipeTable)children;
                    standardTable.ProcessName = CBRecipeName.Text.ToString();
                    ((RecipeTable)children).Encode();
                }
                catch
                {
                    RecipeTableWithoutUS standardTable = (RecipeTableWithoutUS)children;
                    standardTable.ProcessName = CBRecipeName.Text.ToString();
                    ((RecipeTableWithoutUS)children).Encode();
                }
                    
                            

                #endregion
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
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("105,3,29,30");

                TBProcessNameTitle.Text = hashtable[29].ToString() + ((MainWindow.language == "mandarin" || MainWindow.language == "japanese") ? "" : " ") + hashtable[3].ToString();
                TBRecipeNameTitle.Text = hashtable[30].ToString() + ((MainWindow.language == "mandarin" || MainWindow.language == "japanese") ? "" : " ") + hashtable[3].ToString();
                Save.Content = hashtable[105].ToString();
            }
            catch { }
        }

        private void CBRecipesName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBRecipeName.SelectedIndex != -1)
            {
                try
                {
                    ProcessDuration.Children.Clear();

                    #region checking recipe have US                  
                    
                    List<List<string>> list = Functions.SQL.Query.ExecuteMultiQuery("select * from (select recipe.id, recipe.process_name, recipe.process_time, recipe.parameters from recipe right join recipe_id on recipe_id.id = recipe.recipe_name where recipe_id.name = '" + CBRecipeName.SelectedItem.ToString() + "') as recipe left join process_id on process_id.id = recipe.process_name order by recipe.id asc", new string[] { "name", "process_time", "parameters" });
                    for (int i = 0; i < list[0].Count(); i++)
                    {
                        string[] parameters = list[2][i].Split(new char[] { '~', '=' });

                        if (!string.IsNullOrEmpty(parameters[1]) && !string.IsNullOrEmpty(parameters[3]))
                        {
                            if(parameters[1] == "0" && parameters[3] == "0")
                            {
                                ProcessDuration.Children.Add(new RecipeTableWithoutUS()
                                {
                                    ProcessTime = Convert.ToDouble(list[1][i]),                                   
                                    TitleWithoutUS = list[0][i]
                                });
                            }
                            else
                            {
                                ProcessDuration.Children.Add(new RecipeTable()
                                {
                                    ProcessTime = Convert.ToDouble(list[1][i]),
                                    Power = Convert.ToDouble(parameters[1]),
                                    Frequency = Convert.ToDouble(parameters[3]),
                                    Title = list[0][i]
                                });
                            }
                        }                      
                    }

                    #endregion
                }
                catch { }
            }
        }

        #region fitler US in recipe name
        private bool FilterUSInRecipe(string recipe_name)
        {
            bool result = false;

            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();

                mySqlCommand.Parameters.AddWithValue("@recipe_name", recipe_name);

                string query = string.Format(@"                  
                    select * from sub_process as sp

                    inner join sub_process_id as spid on spid.id = sp.sub_process_name
                    inner join process as p on p.sub_process_name = spid.id
                    inner join process_id as pid on pid.id = p.process_name
                    inner join recipe as r on r.process_name = pid.id
                    inner join recipe_id as rid on rid.id = r.recipe_name

                    where sp.equipment_name = 'US' and rid.name = @recipe_name
                ");

                if (!string.IsNullOrEmpty(query))
                    result = Functions.SQL.Query.ExecuteCheckQuery(query, mySqlCommand);
            }
            catch
            {

            }

            return result;
        }

        private bool FilterUSInProcess(string process_name)
        {
            bool result = false;

            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();

                mySqlCommand.Parameters.AddWithValue("@process_name", process_name);

                string query = string.Format(@"                  
                    select * from sub_process as sp

                    inner join sub_process_id as spid on spid.id = sp.sub_process_name
                    inner join process as p on p.sub_process_name = spid.id
                    inner join process_id as pid on pid.id = p.process_name                   

                    where sp.equipment_name = 'US' and pid.name = @process_name
                ");

                if (!string.IsNullOrEmpty(query))
                    result = Functions.SQL.Query.ExecuteCheckQuery(query, mySqlCommand);
            }
            catch
            {

            }

            return result;
        }
        #endregion
    }
}
