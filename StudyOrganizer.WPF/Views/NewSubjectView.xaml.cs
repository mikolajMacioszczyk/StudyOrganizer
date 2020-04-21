using System;
using System.Linq;
using System.Windows;
using StudyOrganizer.DLL.Models;
using DayOfWeek = StudyOrganizer.DLL.Models.DayOfWeek;

namespace StudyOrganizer.WPF.Views
{
    public partial class NewSubjectView : Window
    {
        private Subject _returnSubject;
        public NewSubjectView()
        {
            InitializeComponent();
            DayOfWeekComboBox.DataContext = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            TypeComboBox.DataContext = Enum.GetValues(typeof(SubjectTypes)).Cast<SubjectTypes>();
        }

        public Subject ShowAndGetSubject()
        {
            base.ShowDialog();
            return _returnSubject;
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            //validate
            _returnSubject.Name = SubjectName.Text;
            _returnSubject.Type = (SubjectTypes) Enum.Parse(typeof(SubjectTypes), TypeComboBox.SelectedItem.ToString());
            //_returnSubject.WeeklyDate = new WeeklyDate();
            Close();
        }

        private void Dismiss_OnClick(object sender, RoutedEventArgs e)
        {
            _returnSubject = null;
            Close();
        }
    }
}