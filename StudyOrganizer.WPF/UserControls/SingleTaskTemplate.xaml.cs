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
        public SchoolTaskList ThisSchoolTaskList { get; set; }
        public SchoolTaskListType Type { get; set; }
        public SolidColorBrush ColorStyle { get; set; }
        
        public SingleTaskTemplate()
        {   
            InitializeComponent();
        }

        public void AddColor(SolidColorBrush color)
        {
            ColorStyle = color;
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
            
            ThisSchoolTaskList.Realized.Add(schoolTask);
        }
        
        private void MoveButton_OnClick(object sender, RoutedEventArgs e)
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