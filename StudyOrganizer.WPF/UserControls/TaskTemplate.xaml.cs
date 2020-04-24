using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.UserControls
{
    public partial class TaskTemplate : UserControl
    {
        private SingleTaskTemplateModel _model;
        public SingleTaskTemplateModel Model
        {
            get => _model;
            set
            {
                if (value!=_model)
                {
                    _model = value;
                    Control.ItemsSource = _model.ThisTaskList;
                }
            }
        }
        
        public delegate void TaskDeleted(SchoolTask task, SchoolTaskListType type);
        public event TaskDeleted OnDelete;
        public delegate void TaskMoved(SchoolTask task, SchoolTaskListType type);
        public event TaskMoved OnMove;
        public delegate void TaskRealized(SchoolTask task, SchoolTaskListType type);
        public event TaskRealized OnRealize;
        public delegate void TaskAwardChanged(SchoolTaskListType type);
        public event TaskAwardChanged OnAwardChanged;
        public TaskTemplate()
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
            OnAwardChanged?.Invoke(_model.Type);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć zadanie?", "Info Box", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                OnDelete?.Invoke(GetSchoolTaskFromDataContext(sender), Model.Type);
            }
        }
        private void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnRealize?.Invoke(GetSchoolTaskFromDataContext(sender), Model.Type);
        }
        
        private void MoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnMove?.Invoke(GetSchoolTaskFromDataContext(sender), Model.Type);
        }
        private SchoolTask GetSchoolTaskFromDataContext(object sender)
        {
            Button button = sender as Button;
            SchoolTask task = button.DataContext as SchoolTask;
            return task;
        }
    }
}