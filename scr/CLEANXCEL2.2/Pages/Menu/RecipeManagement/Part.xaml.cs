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
    /// Interaction logic for Part.xaml
    /// </summary>
    public partial class Part : Page
    {
        List<List<string>> part = new List<List<string>>();
        
        public Part()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            // Load Part Name
            part = Functions.RecipeManagement.RecipeStructure.DataManagement.LoadPartName();
            for (int i = 0; i< part[0].Count(); i++)
            {
                Button button = new Button()
                {
                    Name = Regex.Replace(part[1][i], @"[^0-9a-zA-Z]+", "_"),
                    ToolTip = part[1][i],
                    Content = "Part ID: " + part[0][i],
                    Style = (Style)FindResource("BStandardSelection")
                };
                button.Click += PartName_Click;

                PartList.Children.Add(button); //(SubProcessList.Items.Count + 1) + "\t" + 
            }

            // Load Recipe Name
            List<List<string>> recipes = Functions.RecipeManagement.RecipeStructure.DataManagement.LoadRecipeName();

            for (int i = 0; i < recipes[0].Count(); i++)
                CBRecipeID.Items.Add(recipes[0][i] + " - " + recipes[1][i]);
        }

        private void PartName_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = part[1].FindIndex(x => x.Contains(button.ToolTip.ToString()));

            TBPartName.Text = part[1][index];
            TBDescription.Text = part[2][index];
            CBRecipeID.SelectedItem = part[3][index] + " - " + part[4][index];
            TBBatchNo.Text = part[5][index];
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Globals.currentPage = "Part";
            Globals.POPUP_URL = "../Pages/Window/WindowsMessageBox.xaml";
            Globals.POPUP_REQUEST("51", "52", Window.WindowsMessageBox.State.Confirmation);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Globals.currentPage = "Part_Delete";
            Globals.POPUP_URL = "../Pages/Window/WindowsMessageBox.xaml";
            Globals.POPUP_REQUEST("51", "52", Window.WindowsMessageBox.State.Confirmation);
        }

        public void TriggerProcess()
        {
            Globals.currentPage = null;
            Globals.POPUP_URL = "Pages/Window/WindowsMessageBox.xaml";

            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Parameters.AddWithValue("@part_name", TBPartName.Text.ToString());

            string recipeID = CBRecipeID.SelectedItem.ToString().Trim();
            recipeID = recipeID.Substring(0, recipeID.IndexOf('-') - 1);

            if (!Functions.SQL.Query.ExecuteCheckQuery("select * from part_id where part_id.name=@part_name and status = '1'", mySqlCommand))
            {
                Functions.RecipeManagement.RecipeStructure.DataManagement.RegisterPartName(TBPartName.Text, TBDescription.Text, recipeID, TBBatchNo.Text);
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

            mySqlCommand.Parameters.AddWithValue("@part_name", TBPartName.Text.ToString());

            string recipeID = CBRecipeID.SelectedItem.ToString().Trim();
            recipeID = recipeID.Substring(0, recipeID.IndexOf('-') - 1);

            if (Functions.SQL.Query.ExecuteCheckQuery("select * from part_id where part_id.name=@part_name and status = '1'", mySqlCommand))
            {
                Functions.SQL.Query.ExecuteNonQuery("update part_id set part_id.status = '0' where part_id.name=@part_name", mySqlCommand);

                // Clear Part Name
                PartList.Children.Clear();

                // Load Part Name
                part = Functions.RecipeManagement.RecipeStructure.DataManagement.LoadPartName();
                for (int i = 0; i < part[0].Count(); i++)
                {
                    Button button = new Button()
                    {
                        Name = Regex.Replace(part[1][i], @"[^0-9a-zA-Z]+", "_"),
                        ToolTip = part[1][i],
                        Content = "Part ID: " + part[0][i],
                        Style = (Style)FindResource("BStandardSelection")
                    };
                    button.Click += PartName_Click;

                    PartList.Children.Add(button); //(SubProcessList.Items.Count + 1) + "\t" + 
                }
            }
            else // Stay on the page
            {
                Globals.POPUP_REQUEST("51", "81", Window.WindowsMessageBox.State.Ok);
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
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("3,30,83,105,131,171,173");

                TBRegisteredPart.Text = hashtable[173].ToString();
                TBPartNameTitle.Text = hashtable[131].ToString() + ((MainWindow.language == "mandarin" || MainWindow.language == "japanese") ? "" : " ") + hashtable[3].ToString();
                TBDescriptionTitle.Text = hashtable[83].ToString();
                TBRecipeIDTitle.Text = hashtable[30].ToString() + " ID";
                TBBatchNoTitle.Text = hashtable[171].ToString();
                Save.Content = hashtable[105].ToString();
            }
            catch (NullReferenceException) { }
        }
    }
}
