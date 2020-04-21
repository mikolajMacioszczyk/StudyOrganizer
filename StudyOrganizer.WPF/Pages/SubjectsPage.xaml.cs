using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.UserControls;
using StudyOrganizer.WPF.ViewModels;
using StudyOrganizer.WPF.Views;
using DayOfWeek = StudyOrganizer.DLL.Models.DayOfWeek;

namespace StudyOrganizer.WPF.Pages
{
    public partial class SubjectsPage : Page
    {
        public readonly MenuViewModel _model;
        public SubjectsPage(MenuViewModel model)
        {
            _model = model;
            DataContext = _model;
            InitializeComponent();
            AssignSubjects();
        }

        private void AssignSubjects()
        {
            foreach (var subject in _model.User.Subjects)
            {
                AddToPanel(subject);
            }
        }

        private void SubjectsPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            _model.IsNewSubjectPanelVisible = false;
        }
        
        private void AddNewSubject_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        
        private void AddNewSubject_OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            NewSubjectView subjectView = new NewSubjectView();
            var subjectInput = subjectView.ShowAndGetSubject();
            if (subjectInput != null)
            {
                _model.User.Subjects.Add(subjectInput);
            }
            
        }

        private void AddToPanel(Subject subject)
        {
            switch (subject.WeeklyDate.Day)
            {
                case DayOfWeek.Poniedziałek:
                    Monday.Children.Add(new SubjectTemplate(subject));
                    break;
                case DayOfWeek.Wtorek:
                    Tuesday.Children.Add(new SubjectTemplate(subject));
                    break;
                case DayOfWeek.Środa:
                    Wednesday.Children.Add(new SubjectTemplate(subject));
                    break;
                case DayOfWeek.Czwartek:
                    Thursday.Children.Add(new SubjectTemplate(subject));
                    break;
                case DayOfWeek.Piątek:
                    Friday.Children.Add(new SubjectTemplate(subject));
                    break;
                case DayOfWeek.Sobota:
                    Saturday.Children.Add(new SubjectTemplate(subject));
                    break;
                case DayOfWeek.Niedziela:
                    Sunday.Children.Add(new SubjectTemplate(subject));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}