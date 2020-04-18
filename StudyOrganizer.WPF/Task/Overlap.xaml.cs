using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.WPF.Task
{
    public enum SchoolTaskListType
    {
        Realized,
        Actual,
        Goals
    }
    public partial class Overlap : UserControl
    {
        public SchoolTaskList ThisSchoolTaskList { get; set; }
        public SchoolTaskListType Type { get; set; }
        public Overlap()
        {
            InitializeComponent();
        }

        private void Award_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Image image = button.Content as Image;
            string[] texts = image.Source.ToString().Split("/");
            string path = texts[texts.Length - 1];
            if (path.Equals("gold_star.png"))
            {
                image.Source = new BitmapImage(new Uri(@"..\blank_star.png", UriKind.Relative));
            }
            else if (path.Equals("blank_star.png"))
            {
                image.Source = new BitmapImage(new Uri(@"..\gold_star.png", UriKind.Relative));
            }
            else
            {
                MessageBox.Show(path);
            }
        }

        internal void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno chcesz usunąć zadanie?", "Info Box", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DeleteFromListDataContextList(sender);
            }
        }
        
        internal void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            SchoolTask schoolTask = DeleteFromListDataContextList(sender);
            
            ThisSchoolTaskList.Realized.Add(schoolTask);
        }
        
        internal void MoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (Type)
            {
                case SchoolTaskListType.Realized:
                    ThisSchoolTaskList.Actual.Add(DeleteFromListDataContextList(sender));
                    break;
                case SchoolTaskListType.Actual:
                    ThisSchoolTaskList.Planned.Add(DeleteFromListDataContextList(sender));
                    break;
                case SchoolTaskListType.Goals:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private SchoolTask DeleteFromListDataContextList(object sender)
        {

            Button button = sender as Button;
            SchoolTask task = button.DataContext as SchoolTask;


            switch (Type)
            {
                case SchoolTaskListType.Realized:
                    ThisSchoolTaskList.Realized.Remove(task);
                    Control.ItemsSource = ThisSchoolTaskList.Realized;
                    break;
                case SchoolTaskListType.Actual:
                    ThisSchoolTaskList.Actual.Remove(task);
                    Control.ItemsSource = ThisSchoolTaskList.Actual;
                    break;
                case SchoolTaskListType.Goals:
                    ThisSchoolTaskList.Planned.Remove(task);
                    Control.ItemsSource = ThisSchoolTaskList.Planned;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return task;
        }
    }
}