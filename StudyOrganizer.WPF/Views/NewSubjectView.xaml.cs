using System;
using System.Windows;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Views
{
    public partial class NewSubjectView : Window
    {
        private NewSubjectViewModel _model;
        private ConnectionToDb dbConnection;
        public NewSubjectView(NewSubjectViewModel model)
        {
            InitializeComponent();
            _model = model;
            DataContext = _model;
            dbConnection = new ConnectionToDb();
        }

        public Subject ShowAndGetSubject()
        {
            base.ShowDialog();
            return _model._returnedSubject;
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message,"Lack Of Information",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            try
            {
                LoadSubjectToDB();
                _model._returnedSubject = GetSubjectFromDB();
                Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSubjectToDB()
        {
            SubjectTypes type = (SubjectTypes) Enum.Parse(typeof(SubjectTypes), TypeComboBox.SelectedItem.ToString());
            string day = DayOfWeekComboBox.SelectedItem.ToString();
            string name = SubjectName.Text;
            int hour = (int) HourSlider.Value;
            dbConnection.InsertSubject(_model.SubjectListId, name, type, day, hour);
        }

        private Subject GetSubjectFromDB()
        {
            return dbConnection.GetSubject(SubjectName.Text);
;        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(SubjectName.Text))
            {
                throw new InvalidInputException("Nazwa przedmiotu nie może być pusta");
            }
        }

        private void Dismiss_OnClick(object sender, RoutedEventArgs e)
        {
            _model._returnedSubject = null;
            Close();
        }
    }
}