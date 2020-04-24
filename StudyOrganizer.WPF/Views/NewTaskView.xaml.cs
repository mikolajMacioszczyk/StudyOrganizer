using System;
using System.Windows;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.WPF.Views
{
    public partial class NewTaskView : Window
    {
        private SchoolTask _createdTask;
        public NewTaskView()
        {
            InitializeComponent();
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
                _createdTask = new SchoolTask(TitleText.Text,DescriptionText.Text,GetIsAwarded(), GetDateTime());
                Close();
            }
            catch (LackOfDataException exception)
            {
                MessageBox.Show(exception.Message,"Lack of data", MessageBoxButton.OK,MessageBoxImage.Error);
            }
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