using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            int panelIndex = DayPanelIndex(subject.Day);
            AddNewSubjectToAccordingPanel(panelIndex,subject);
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
            DayPanels[panelIndex].Children.Clear();
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
<<<<<<< HEAD
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
=======
            int panelId = DayPanelIndex(subject.Day);
            SubjectsPerDay[panelId].Add(toAdd);
>>>>>>> developer
        }

        private void SubjectDeletedHandler(Subject subject)
        {
            int panelIndex = DayPanelIndex(subject.Day);
            Delete(panelIndex,subject);
            UpdatePanel(panelIndex);
        }
    
        private void Delete(int panelIndex, Subject subject)
        {    
            _model.User.Subjects.Remove(subject);
            SubjectsPerDay[panelIndex] = SubjectsPerDay[panelIndex].Where(x => x.Model.ThisSubject != subject).ToList();
<<<<<<< HEAD
            if (SubjectsPerDay[panelIndex].Count == 0)
            {
                StackPanels[panelIndex].Visibility = Visibility.Collapsed;
            }
            UpdatePanel(panelIndex);
=======
            new UpdatesTransmitter(new ConnectionToDb()).DeleteSubject(subject);
>>>>>>> developer
        }
        
        private void EditSubjectHandler(Subject subject)
        {
            NewSubjectView view = new NewSubjectView(new NewSubjectViewModel(_model.ColorMode, _model.User.UserId));
            Subject subjectInput = view.ShowAndGetSubject();
            if (subjectInput != null)
            {
                SubjectDeletedHandler(subject);
                SubjectAdded(subjectInput);
                _model.User.Subjects.Add(subject);
            }
        }

        private int DayPanelIndex(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 6;
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null);
            }
        }
    }
}