﻿using System;
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

namespace CLEANXCEL2._2.UserControls
{
    /// <summary>
    /// Interaction logic for ConditionButton.xaml
    /// </summary>
    public partial class ConditionButton : UserControl
    {
        public event EventHandler Checked;

        public ConditionButton()
        {
            InitializeComponent();
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(ConditionButton), new PropertyMetadata(""));
        
        public string EquipmentName
        {
            get { return (string)GetValue(EquipmentNameProperty); }
            set { SetValue(EquipmentNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EquipmentName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EquipmentNameProperty =
            DependencyProperty.Register("EquipmentName", typeof(string), typeof(ConditionButton), new PropertyMetadata(""));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ConditionButton), new PropertyMetadata(""));

        public Visibility StartCondEnable
        {
            set { start.Visibility = value; }
        }

        public Visibility StopCondEnable
        {
            set { stop.Visibility = value; }
        }

        public bool IsChecked
        {
            get { return (bool)RBConditionButton.IsChecked; }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (Checked != null)
            {
                Checked(this, e);
            }
        }
    }
}
