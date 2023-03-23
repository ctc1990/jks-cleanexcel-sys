﻿using MySql.Data.MySqlClient;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwinCAT.Ads;

namespace CLEANXCEL2._2.Pages.Menu.Alarm
{
    /// <summary>
    /// Interaction logic for HistoryAlarm.xaml
    /// </summary>
    public partial class HistoryAlarm : Page
    {
        private int index = 0;

        public HistoryAlarm()
        {
            InitializeComponent();
        }

        private void AlarmStack_Loaded(object sender, RoutedEventArgs e)
        {
            RenderAlarm("alarm_history.activated_time", "desc");
        }

        private void RenderAlarm(string category, string order)
        {
            Functions.EventNotifier.LoadingDisplay.Run(Functions.EventNotifier.LoadingDisplay.LoadingActions.Loading);

            BackgroundWorker HistoryAlarmBW = new BackgroundWorker();

            HistoryAlarmBW.DoWork += HistoryAlarmBW_DoWork;
            HistoryAlarmBW.RunWorkerCompleted += HistoryAlarmBW_RunWorkerCompleted;

            HistoryAlarmBW.RunWorkerAsync(new AlarmType() { category = category, order = order });
        }

        private void HistoryAlarmBW_DoWork(object sender, DoWorkEventArgs e)
        {
            AlarmType alarmType = (AlarmType)e.Argument;
            
            e.Result = LoadAlarm(alarmType.category, alarmType.order, index);
        }

        private void HistoryAlarmBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<List<string>> alarm_history = e.Result as List<List<string>>;

            AlarmStack.Children.Clear();
            for (int i = 0; i < alarm_history[0].Count(); i++)
            {
                AlarmStack.Children.Add(
                                    new UserControls.AlarmRow
                                    {
                                        Width = Title.ActualWidth,
                                        ID = alarm_history[0][i],
                                        AlarmCode = alarm_history[1][i],
                                        DateTime = alarm_history[3][i],
                                        IO = alarm_history[2][i]
                                    });
            }

            //index += alarm_history[0].Count();

            Functions.EventNotifier.LoadingDisplay.Stop(Functions.EventNotifier.LoadingDisplay.LoadingActions.Loading);
        }

        private List<List<string>> LoadAlarm(string category, string order, int index)
        {
            string aa = "select alarm_history.code, alarm_history.io_code, alarm_history.activated_time, english.terms from alarm_history left join alarm_code on alarm_history.code = alarm_code.code left join english on alarm_code.description = english.id where alarm_history.status = '0' order by alarm_history." + category + " " + order + " limit 50";
            List<List<string>> alarm_history = Functions.SQL.Query.ExecuteMultiQuery(
                "select alarm_history.id, alarm_history.code, alarm_history.io_code, alarm_history.activated_time, english.terms " +
                "from alarm_history left join alarm_code on alarm_history.code = alarm_code.code " +
                "left join english on alarm_code.description = english.id " +
                "where alarm_history.status = '0' " +
                "order by " + category + " " + order + " limit " + index.ToString() + ", 20",
                new string[] { "id", "code", "io_code", "activated_time" });

            return alarm_history;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            try
            {
                Functions.SQL.Query query = new Functions.SQL.Query();

                Hashtable hashtable = Functions.SQL.Query.ExecuteLanguageQuery("42,43,83,84,107");
                
                ID.Content = hashtable[42].ToString();
                Code.Content = hashtable[43].ToString();
                Description.Content = hashtable[83].ToString();
                Datetime.Content = hashtable[84].ToString() + "/" + hashtable[107].ToString();
            }
            catch { }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void ID_Click(object sender, RoutedEventArgs e)
        {
            if (ID.IsChecked==true)
                RenderAlarm("alarm_history.id", "desc");
            else
                RenderAlarm("alarm_history.id", "asc");
        }

        private void Code_Click(object sender, RoutedEventArgs e)
        {
            if (Code.IsChecked == true)
                RenderAlarm("alarm_history.code", "desc");
            else
                RenderAlarm("alarm_history.code", "asc");
        }

        private void Description_Click(object sender, RoutedEventArgs e)
        {
            if (Description.IsChecked == true)
                RenderAlarm(MainWindow.language + ".terms", "desc");
            else
                RenderAlarm(MainWindow.language + ".terms", "asc");
        }

        private void Datetime_Click(object sender, RoutedEventArgs e)
        {
            if (Datetime.IsChecked == true)
                RenderAlarm("alarm_history.activated_time", "desc");
            else
                RenderAlarm("alarm_history.activated_time", "asc");
        }

        private struct AlarmType
        {
            public string category;
            public string order;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalOffset == (sender as ScrollViewer).ScrollableHeight && e.VerticalOffset != 0)
            {
                //Console.WriteLine("End");
                //RenderAlarm("alarm_history.activated_time", "desc");
            }
        }
    }
}
