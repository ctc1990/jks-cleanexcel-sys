using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CLEANXCEL2._2.UserControls
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class RecipeTableWithoutUS : UserControl
    {
        public string ProcessName;
        Hashtable hashtable;

        public RecipeTableWithoutUS()
        {
            InitializeComponent();
            LoadLanguage();
        }

        public string TitleWithoutUS
        {
            get { return (string)GetValue(SetTitleProperty); }
            set { SetValue(SetTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SetTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetTitleProperty =
            DependencyProperty.Register("TitleWithoutUS", typeof(string), typeof(RecipeTableWithoutUS), new PropertyMetadata(new PropertyChangedCallback(OnSetTitleChanged)));


        private static void OnSetTitleChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            RecipeTableWithoutUS recipeTableWithoutUS = d as RecipeTableWithoutUS;
            recipeTableWithoutUS.OnSetTitleChanged(e);
        }

        private void OnSetTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            titleWithoutUS.Text = e.NewValue.ToString();
        }

        public double ProcessTime
        {
            get { return (double)GetValue(ProcessTimeProperty); }
            set { SetValue(ProcessTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProcessTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProcessTimeProperty =
            DependencyProperty.Register("ProcessTime", typeof(double), typeof(RecipeTableWithoutUS), new PropertyMetadata((double)0));
                  
        public void Encode()
        {
            Functions.RecipeManagement.BlackBox.RecipeModule.UploadSQLRecipe(TitleWithoutUS, ProcessTime.ToString(), "0", "0");
        }

        private void Protect_DeleteButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            (this.Parent as StackPanel).Children.Remove(this);
        }

        private void LoadLanguage()
        {
            try
            {
                hashtable = Functions.SQL.Query.ExecuteLanguageQuery("135,136,950");               
            }
            catch (NullReferenceException) { }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!object.Equals(storage, value))
            {
                storage = value;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
