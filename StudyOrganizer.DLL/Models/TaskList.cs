using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudyOrganizer.DLL.Annotations;

namespace StudyOrganizer.DLL.Models
{
    [Serializable]
    public class SchoolTaskList
    {
        public ObservableCollection<SchoolTask> Realized { get; set; }
        public ObservableCollection<SchoolTask> Actual { get; set; }
        public ObservableCollection<SchoolTask> Planned { get; set; }

        public SchoolTaskList(ObservableCollection<SchoolTask> realized, ObservableCollection<SchoolTask> actual, ObservableCollection<SchoolTask> planned)
        {
            Planned = planned;
            Actual = actual;
            Realized = realized;
        }

        public SchoolTaskList()
        {
            Planned = new ObservableCollection<SchoolTask>(){ new SchoolTask("Asembler AK","Nauczyć się używać funkcji skoku i opanować delay-slot",
                true,DateTime.Now)};
            Actual  = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("Analiza Cwiczenia","Przygotować się z szeregów i funkcji wielu zmiennych", 
                    true, DateTime.Now.Add(TimeSpan.FromDays(10))), 
                new SchoolTask("Analiza Cwiczenia","Przygotować się z szeregów i funkcji wielu zmiennych", 
                    false, DateTime.Now.Add(TimeSpan.FromDays(10))),
                new SchoolTask("Analiza Cwiczenia","Przygotować się z szeregów i funkcji wielu zmiennych", 
                    false, DateTime.Now.Add(TimeSpan.FromDays(10))),
                new SchoolTask("Analiza Cwiczenia","Przygotować się z szeregów i funkcji wielu zmiennych", 
                    false, DateTime.Now.Add(TimeSpan.FromDays(10))),
                new SchoolTask("Analiza Cwiczenia","Przygotować się z szeregów i funkcji wielu zmiennych", 
                    false, DateTime.Now.Add(TimeSpan.FromDays(10))),
                new SchoolTask("Analiza Cwiczenia","Przygotować się z szeregów i funkcji wielu zmiennych", 
                    false, DateTime.Now.Add(TimeSpan.FromDays(10)))
            };
            Realized  = new ObservableCollection<SchoolTask>(){ new SchoolTask("AiSD Lista 4","Przygotować algorytmy sortujące wraz z odpowiednim GUI",
                true,DateTime.Now.Subtract(TimeSpan.FromDays(5)))};
        }
    }

    [Serializable]
    public class SchoolTask : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private bool _isAwarded;
        public bool IsAwarded
        {
            get => _isAwarded;
            set
            {
                if (value != _isAwarded)
                {
                    _isAwarded = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Deadline { get; set; }

        public SchoolTask(string title, string description, bool isAwarded, DateTime deadline)
        {
            Title = title;
            Description = description;
            IsAwarded = isAwarded;
            Deadline = deadline;
        }
    
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}