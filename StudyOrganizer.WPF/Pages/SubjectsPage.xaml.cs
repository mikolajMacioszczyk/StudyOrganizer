using System;
using System.Collections.Generic;
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
            switch (subject.WeeklyDate.Day)
            {
                case DayOfWeek.Poniedziałek:
                    AddNewSubjectToAccordingPanel(0,subject);
                    break;
                case DayOfWeek.Wtorek:
                    AddNewSubjectToAccordingPanel(1,subject);
                    break;
                case DayOfWeek.Środa:
                    AddNewSubjectToAccordingPanel(2,subject);
                    break;
                case DayOfWeek.Czwartek:
                    AddNewSubjectToAccordingPanel(3,subject);
                    break;
                case DayOfWeek.Piątek:
                    AddNewSubjectToAccordingPanel(4,subject);
                    break;
                case DayOfWeek.Sobota:
                    AddNewSubjectToAccordingPanel(5,subject);
                    break;
                case DayOfWeek.Niedziela:
                    AddNewSubjectToAccordingPanel(6,subject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddNewSubjectToAccordingPanel(int panelIndex, Subject newSubject)
        {
            DayPanels[panelIndex].Children.Clear();
            SubjectsPerDay[panelIndex].Add(new SubjectTemplate(new SubjectTemplateModel(newSubject,_model.ColorMode)));
            
            SortSubjectsListByIndex(panelIndex);

            UpdatePanel(panelIndex);
        }

        private void SortSubjectsListByIndex(int index)
        {
            SubjectsPerDay[index].Sort((x,y) => 
                x.Model.ThisSubject.WeeklyDate.Hour > y.Model.ThisSubject.WeeklyDate.Hour ? 1 :
                x.Model.ThisSubject.WeeklyDate.Hour < y.Model.ThisSubject.WeeklyDate.Hour ? -1 : 0);
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
            switch (subject.WeeklyDate.Day)
            {
                case DayOfWeek.Poniedziałek:
                    SubjectsPerDay[0].Add(new SubjectTemplate(new SubjectTemplateModel(subject,_model.ColorMode)));
                    break;
                case DayOfWeek.Wtorek:
                    SubjectsPerDay[1].Add(new SubjectTemplate(new SubjectTemplateModel(subject,_model.ColorMode)));
                    break;
                case DayOfWeek.Środa:
                    SubjectsPerDay[2].Add(new SubjectTemplate(new SubjectTemplateModel(subject,_model.ColorMode)));
                    break;
                case DayOfWeek.Czwartek:
                    SubjectsPerDay[3].Add(new SubjectTemplate(new SubjectTemplateModel(subject,_model.ColorMode)));
                    break;
                case DayOfWeek.Piątek:
                    SubjectsPerDay[4].Add(new SubjectTemplate(new SubjectTemplateModel(subject,_model.ColorMode)));
                    break;
                case DayOfWeek.Sobota:
                    SubjectsPerDay[5].Add(new SubjectTemplate(new SubjectTemplateModel(subject,_model.ColorMode)));
                    break;
                case DayOfWeek.Niedziela:
                    SubjectsPerDay[6].Add(new SubjectTemplate(new SubjectTemplateModel(subject,_model.ColorMode)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}