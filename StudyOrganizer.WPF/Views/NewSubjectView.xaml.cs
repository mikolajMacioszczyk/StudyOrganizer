using System;
using System.Linq;
using System.Windows;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ViewModels;
using DayOfWeek = StudyOrganizer.DLL.Models.DayOfWeek;

namespace StudyOrganizer.WPF.Views
{
    public partial class NewSubjectView : Window
    {
        private NewSubjectViewModel _model;
        public NewSubjectView(NewSubjectViewModel model)
        {
            InitializeComponent();
            _model = model;
            DataContext = _model;
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
                
                string name = SubjectName.Text;
                SubjectTypes type = (SubjectTypes) Enum.Parse(typeof(SubjectTypes), TypeComboBox.SelectedItem.ToString());
                DayOfWeek day = (DayOfWeek) Enum.Parse(typeof(DayOfWeek), DayOfWeekComboBox.SelectedItem.ToString());
                int hour = (int) HourSlider.Value;
                _model._returnedSubject = Subject.GetBuilder().Type(type).WithName(name)
                    .DayAndHour(new WeeklyDate(day, hour)).GetSubject();
                Close();
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message,"Lack Of Information",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

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