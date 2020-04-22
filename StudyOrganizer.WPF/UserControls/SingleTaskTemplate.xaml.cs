using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.UserControls
{
    public enum SchoolTaskListType
    {
        Realized,
        Actual,
        Goals
    }
    
    public partial class SingleTaskTemplate : UserControl
    {
        public SingleTaskTemplateModel Model { get; set; }
        public SchoolTaskList SchoolTaskList { get; set; }
        public SingleTaskTemplate()
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

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno chcesz usunąć zadanie?", "Info Box", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DeleteFromListDataContextList(sender);
            }
        }
        
        private void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            SchoolTask schoolTask = DeleteFromListDataContextList(sender);
            
            SchoolTaskList.Realized.Add(schoolTask);
        }
        
        private void MoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (Model.Type)
            {
                case SchoolTaskListType.Realized:
                    SchoolTaskList.Actual.Add(DeleteFromListDataContextList(sender));
                    break;
                case SchoolTaskListType.Actual:
                    SchoolTaskList.Planned.Add(DeleteFromListDataContextList(sender));
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


            switch (Model.Type)
            {
                case SchoolTaskListType.Realized:
                    SchoolTaskList.Realized.Remove(task);
                    Control.ItemsSource = SchoolTaskList.Realized;
                    break;
                case SchoolTaskListType.Actual:
                    SchoolTaskList.Actual.Remove(task);
                    Control.ItemsSource = SchoolTaskList.Actual;
                    break;
                case SchoolTaskListType.Goals:
                    SchoolTaskList.Planned.Remove(task);
                    Control.ItemsSource = SchoolTaskList.Planned;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return task;
        }
    }
}