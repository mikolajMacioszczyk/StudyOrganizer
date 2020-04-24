using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private List<StackPanel> StackPanels;
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
            Prepare();
        }

        private void Prepare()
        {
            DayPanels = new List<WrapPanel>() {
                Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday
            };
            StackPanels = new List<StackPanel>()
            {
                MondayPanel,TuesdayPanel,WednesdayPanel,ThursdayPanel,FridayPanel,SaturdayPanel,SundayPanel
            };
            foreach (var panel in StackPanels)
            {
                panel.Visibility = Visibility.Collapsed;
            }
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
                x.Model.ThisSubject.WeeklyDate.Hour > y.Model.ThisSubject.WeeklyDate.Hour ? 1 :
                x.Model.ThisSubject.WeeklyDate.Hour < y.Model.ThisSubject.WeeklyDate.Hour ? -1 : 0);
        }

        private void UpdatePanel(int panelIndex)
        {
            foreach (var subject in SubjectsPerDay[panelIndex])
            {
                DayPanels[panelIndex].Children.Add(subject);
                StackPanels[panelIndex].Visibility = Visibility.Visible;
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
            switch (subject.WeeklyDate.Day)
            {
                case DayOfWeek.Poniedziałek:
                    StackPanels[0].Visibility = Visibility.Visible;
                    SubjectsPerDay[0].Add(toAdd);
                    break;
                case DayOfWeek.Wtorek:
                    StackPanels[1].Visibility = Visibility.Visible;
                    SubjectsPerDay[1].Add(toAdd);
                    break;
                case DayOfWeek.Środa:
                    StackPanels[2].Visibility = Visibility.Visible;
                    SubjectsPerDay[2].Add(toAdd);
                    break;
                case DayOfWeek.Czwartek:
                    StackPanels[3].Visibility = Visibility.Visible;
                    SubjectsPerDay[3].Add(toAdd);
                    break;
                case DayOfWeek.Piątek:
                    StackPanels[4].Visibility = Visibility.Visible;
                    SubjectsPerDay[4].Add(toAdd);
                    break;
                case DayOfWeek.Sobota:
                    StackPanels[5].Visibility = Visibility.Visible;
                    SubjectsPerDay[5].Add(toAdd);
                    break;
                case DayOfWeek.Niedziela:
                    StackPanels[6].Visibility = Visibility.Visible;
                    SubjectsPerDay[6].Add(toAdd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SubjectDeletedHandler(Subject subject)
        {
            switch (subject.WeeklyDate.Day)
            {
                case DayOfWeek.Poniedziałek:
                    DeleteAndUpdateList(0,subject);
                    break;
                case DayOfWeek.Wtorek:
                    DeleteAndUpdateList(1,subject);
                    break;
                case DayOfWeek.Środa:
                    DeleteAndUpdateList(2,subject);
                    break;
                case DayOfWeek.Czwartek:
                    DeleteAndUpdateList(3,subject);
                    break;
                case DayOfWeek.Piątek:
                    DeleteAndUpdateList(4,subject);
                    break;
                case DayOfWeek.Sobota:
                    DeleteAndUpdateList(5,subject);
                    break;
                case DayOfWeek.Niedziela:
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
            if (SubjectsPerDay[panelIndex].Count == 0)
            {
                StackPanels[panelIndex].Visibility = Visibility.Collapsed;
            }
            UpdatePanel(panelIndex);
        }

        private void EditSubjectHandler(Subject subject)
        {
            NewSubjectView view = new NewSubjectView(new NewSubjectViewModel(_model.ColorMode));
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