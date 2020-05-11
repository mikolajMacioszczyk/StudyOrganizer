using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.UserControls;
using StudyOrganizer.WPF.ViewModels;
using StudyOrganizer.WPF.Views;

namespace StudyOrganizer.WPF.Pages
{
    public partial class SubjectsPage : Page
    {
        public readonly MenuViewModel _model;
        private List<WrapPanel> DayPanels;
        private List<List<SubjectTemplate>> SubjectsPerDay = new List<List<SubjectTemplate>>() {
            new List<SubjectTemplate>(),
            new List<SubjectTemplate>(),
            new List<SubjectTemplate>(),
            new List<SubjectTemplate>(),
            new List<SubjectTemplate>(),
            new List<SubjectTemplate>(),
            new List<SubjectTemplate>()
        };
        public SubjectsPage(MenuViewModel model)
        {
            _model = model;
            model.OnListChanged += SubjectAdded;
            DataContext = _model;
            InitializeComponent();
            DayPanels = new List<WrapPanel>() {
                Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday
            };
            AssignSubjects();
        }

        private void AssignSubjects()
        {
            foreach (var subject in _model.User.Subjects)
            {
                AddToPanelAtTheBegin(subject);
            }

            for (int i = 0; i < DayPanels.Count; i++)
            {
                SortSubjectsListByIndex(i);
                UpdatePanel(i);
            }
        }

        private void SubjectAdded(Subject subject)
        {
            switch (subject.Day)
            {
                case DayOfWeek.Monday:
                    AddNewSubjectToAccordingPanel(0,subject);
                    break;
                case DayOfWeek.Tuesday:
                    AddNewSubjectToAccordingPanel(1,subject);
                    break;
                case DayOfWeek.Wednesday:
                    AddNewSubjectToAccordingPanel(2,subject);
                    break;
                case DayOfWeek.Thursday:
                    AddNewSubjectToAccordingPanel(3,subject);
                    break;
                case DayOfWeek.Friday:
                    AddNewSubjectToAccordingPanel(4,subject);
                    break;
                case DayOfWeek.Saturday:
                    AddNewSubjectToAccordingPanel(5,subject);
                    break;
                case DayOfWeek.Sunday:
                    AddNewSubjectToAccordingPanel(6,subject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddNewSubjectToAccordingPanel(int panelIndex, Subject newSubject)
        {
            DayPanels[panelIndex].Children.Clear();
            var toAdd = new SubjectTemplate(new SubjectTemplateModel(newSubject, _model.ColorMode));
            toAdd.OnDelete += SubjectDeletedHandler;
            toAdd.OnEdite += EditSubjectHandler;
            SubjectsPerDay[panelIndex].Add(toAdd);
            
            SortSubjectsListByIndex(panelIndex);

            UpdatePanel(panelIndex);
        }

        private void SortSubjectsListByIndex(int index)
        {
            SubjectsPerDay[index].Sort((x,y) => 
                x.Model.ThisSubject.Hour > y.Model.ThisSubject.Hour ? 1 :
                x.Model.ThisSubject.Hour < y.Model.ThisSubject.Hour ? -1 : 0);
        }

        private void UpdatePanel(int panelIndex)
        {
            foreach (var subject in SubjectsPerDay[panelIndex])
            {
                DayPanels[panelIndex].Children.Add(subject);
            }
        }

        private void SubjectsPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            _model.IsNewSubjectPanelVisible = false;
        }

        private void AddToPanelAtTheBegin(Subject subject)
        {
            var toAdd = new SubjectTemplate(new SubjectTemplateModel(subject, _model.ColorMode));
            toAdd.OnDelete += SubjectDeletedHandler;
            toAdd.OnEdite += EditSubjectHandler;
            switch (subject.Day)
            {
                case DayOfWeek.Monday:
                    SubjectsPerDay[0].Add(toAdd);
                    break;
                case DayOfWeek.Tuesday:
                    SubjectsPerDay[1].Add(toAdd);
                    break;
                case DayOfWeek.Wednesday:
                    SubjectsPerDay[2].Add(toAdd);
                    break;
                case DayOfWeek.Thursday:
                    SubjectsPerDay[3].Add(toAdd);
                    break;
                case DayOfWeek.Friday:
                    SubjectsPerDay[4].Add(toAdd);
                    break;
                case DayOfWeek.Saturday:
                    SubjectsPerDay[5].Add(toAdd);
                    break;
                case DayOfWeek.Sunday:
                    SubjectsPerDay[6].Add(toAdd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SubjectDeletedHandler(Subject subject)
        {
            switch (subject.Day)
            {
                case DayOfWeek.Monday:
                    DeleteAndUpdateList(0,subject);
                    break;
                case DayOfWeek.Tuesday:
                    DeleteAndUpdateList(1,subject);
                    break;
                case DayOfWeek.Wednesday:
                    DeleteAndUpdateList(2,subject);
                    break;
                case DayOfWeek.Thursday:
                    DeleteAndUpdateList(3,subject);
                    break;
                case DayOfWeek.Friday:
                    DeleteAndUpdateList(4,subject);
                    break;
                case DayOfWeek.Saturday:
                    DeleteAndUpdateList(5,subject);
                    break;
                case DayOfWeek.Sunday:
                    DeleteAndUpdateList(6,subject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DeleteAndUpdateList(int panelIndex, Subject subject)
        {    
            _model.User.Subjects.Remove(subject);
            DayPanels[panelIndex].Children.Clear();
            SubjectsPerDay[panelIndex] = SubjectsPerDay[panelIndex].Where(x => x.Model.ThisSubject != subject).ToList();
            UpdatePanel(panelIndex);
            new ConnectionToDb().DeleteSubject(subject.Id);
        }

        private void EditSubjectHandler(Subject subject)
        {
            NewSubjectView view = new NewSubjectView(new NewSubjectViewModel(_model.ColorMode, _model.User.UserId));
            Subject subjectInput = view.ShowAndGetSubject();
            if (subjectInput != null)
            {
                SubjectDeletedHandler(subject);
                _model.User.Subjects.Add(subjectInput);
                SubjectAdded(subjectInput);
            }
        }
    }
}