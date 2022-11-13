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

namespace CLEANXCEL2._2.Pages.Menu.Analytics
{
    /// <summary>
    /// Interaction logic for ValueDiagram.xaml
    /// </summary>
    public partial class ValueDiagram : Page
    {
        String PageUrl = "http://localhost/chartjs/samples/JKSChartR1.php?recipe=";
        Hashtable hashTableTypeList = new Hashtable();

        public ValueDiagram()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
            Functions.SQL.Backup.Modify_StatusBLOBStructure();
            LoadSelection();
        }

        private void LoadSelection()
        {
            // Load Recipe Selection
            List<List<string>> list = Functions.SQL.Query.ExecuteMultiQuery("select * from history_recipe left join recipe_id on recipe_id.id = history_recipe.recipe_name order by history_recipe.id desc", new string[] { "id", "name" });

            for (int i = 0; i < list[0].Count(); i++)
                RecipeSelection.Items.Add(new ComboBoxItem() { Content = list[0][i] + " - " + list[1][i], Tag = list[0][i] });

            RecipeSelection.SelectedIndex = 0;


            //list = Decode_Recipe(Convert.ToInt32(list[0][0]));
            //List<List<string>> parameters = Decode_Parameters(list);

            ////foreach (string value in (parameters[0].Where(x => x.Contains("Set"))).Select(x => x.Substring(0, x.IndexOf(' ', x.IndexOf("Set") - 10) - 1)))
            //foreach (string value in parameters[0].Where(x => x.Contains("Set")).Select(x => x.Replace(" Set", "")))
            //    GraphSelection.Items.Add(hashTableTypeList[value].ToString());

            //GraphSelection.SelectedIndex = 0;
            ////foreach (string value in (parameters[0].Where(x => x.Contains("Set"))).Select(x => x.Replace("Set", "")))
            ////    GraphSelection.Items.Add(value);
        }

        private void Analyse_Click(object sender, RoutedEventArgs e)
        {
            Functions.EventNotifier.LoadingDisplay.Run(Functions.EventNotifier.LoadingDisplay.LoadingActions.Analysing);

            LocalBrowser.Navigate(PageUrl + ((ComboBoxItem)RecipeSelection.SelectedValue).Tag);
            //Dispatcher.Invoke((Action)(() =>
            //{
            //    Render(Convert.ToInt32(((ComboBoxItem)RecipeSelection.SelectedValue).Tag), GraphSelection.SelectedItem.ToString(), TimelineChart);
            //}), DispatcherPriority.Render);

            Functions.EventNotifier.LoadingDisplay.Stop(Functions.EventNotifier.LoadingDisplay.LoadingActions.Analysing);
        }

        private void LoadLanguage()
        {
            try
            {
                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("112,113,135,136,176,187,188,189,190,191,192,193,194,195");

                bAnalyse.Content = hashtable[176].ToString();
                //YAxisLabel.Text = hashtable[179].ToString();
                //XAxisLabel.Text = hashtable[107].ToString() + " (s)";
                //TBSetTitle.Text = hashtable[177].ToString();
                //TBActualTitle.Text = hashtable[178].ToString();

                hashTableTypeList.Add("187", hashtable[187].ToString());
                hashTableTypeList.Add("188", hashtable[188].ToString());
                hashTableTypeList.Add("189", hashtable[189].ToString());
                hashTableTypeList.Add("190", hashtable[190].ToString());
                hashTableTypeList.Add("191", hashtable[191].ToString());
                hashTableTypeList.Add("192", hashtable[192].ToString());
                hashTableTypeList.Add("193", hashtable[193].ToString());
                hashTableTypeList.Add("194", hashtable[194].ToString());
                hashTableTypeList.Add("195", hashtable[195].ToString());

                hashTableTypeList.Add("112", hashtable[112].ToString());
                hashTableTypeList.Add("113", hashtable[113].ToString());
                hashTableTypeList.Add("135", hashtable[135].ToString());
                hashTableTypeList.Add("136", hashtable[136].ToString());
            }
            catch (NullReferenceException) { }
        }
    }
}
