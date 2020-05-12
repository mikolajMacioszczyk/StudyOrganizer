using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.WPF.Views
{
    public partial class NewTaskView : Window
    {
        private SchoolTask _createdTask;
        private ConnectionToDb _dbConnection;
        private int _schoolTaskId;
        public NewTaskView(int schoolTaskId)
        {
            _dbConnection = new ConnectionToDb();
            _schoolTaskId = schoolTaskId;
            InitializeComponent();
            GroupComboBox.ItemsSource = Enum.GetValues(typeof(TaskGroup));
            GroupComboBox.SelectedIndex = 0;
        }

        public SchoolTask OpenNewTaskView()
        {
            base.ShowDialog();
            return _createdTask;
        }


        private void Dismiss_OnClick(object sender, RoutedEventArgs e)
        {
            _createdTask = null;
            Close();
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateInputs();
                LoadTaskToDB();
                _createdTask = GetSchoolTaskFromDB(TitleText.Text);
                Close();
            }
            catch (LackOfDataException exception)
            {
                MessageBox.Show(exception.Message,"Lack of data", MessageBoxButton.OK,MessageBoxImage.Error);
            }    
        }

        private void LoadTaskToDB()
        {
            string tittle = TitleText.Text;
            string description = DescriptionText.Text;
            bool isAwarded = GetIsAwarded();
            DateTime deadline = GetDateTime();
            TaskGroup group = (TaskGroup) Enum.Parse(typeof(TaskGroup), GroupComboBox.SelectedItem.ToString());
            _dbConnection.InsertSchoolTask(_schoolTaskId,tittle,description, group, isAwarded, deadline );
        }

        private SchoolTask GetSchoolTaskFromDB(string title)
        {
            return _dbConnection.GetSchoolTask(TitleText.Text);
        }
        
        private void ValidateInputs()
        {
            if (string.IsNullOrEmpty(TitleText.Text))
            {
                throw new LackOfDataException("Wpisz tytuł zadania.");
            }

            if (string.IsNullOrEmpty(DescriptionText.Text))
            { 
                throw new LackOfDataException("Opis zadania nie może być pusty.");
            }
        }

        private bool GetIsAwarded()
        {
            if (IsAwardedCheckBox.IsChecked == null)
            {
                return false;
            }
            return (bool) IsAwardedCheckBox.IsChecked;
        }

        private DateTime GetDateTime()
        {
            var date = Calendar.SelectedDate;
            if (date==null)
            {
                return DateTime.Now;
            }
            return (DateTime) date;
        }
    }
}