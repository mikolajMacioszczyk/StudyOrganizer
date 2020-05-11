using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
            Planned = planned.Sort();
            Actual = actual.Sort();
            Realized = realized.Sort();
        }

        public SchoolTaskList()
        {
            Planned = new ObservableCollection<SchoolTask>();
            Actual = new ObservableCollection<SchoolTask>();
            Realized = new ObservableCollection<SchoolTask>();
        }
        
        public void ListChanged(SchoolTaskListType type)
        {
            switch (type)
            {
                case SchoolTaskListType.Realized:
                    Realized.Sort();
                    break;
                case SchoolTaskListType.Actual:
                    Actual.Sort();
                    break;
                case SchoolTaskListType.Goals:
                    Planned.Sort();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum TaskGroup
    {
        Planned, Actual, Realized
    }

    [Serializable]
    public class SchoolTask : INotifyPropertyChanged
    {
        public int Id { get; private set; }
        public int TaskListId { get; set; }
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
        
        public TaskGroup TaskGroup { get; set; }
        public DateTime Deadline { get; set; }

        public SchoolTask(int id, int taskListId, string title, string description, bool isAwarded,TaskGroup taskGroup, DateTime deadline)
        {
            Id = id;
            TaskListId = taskListId;
            TaskGroup = taskGroup;    
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

        protected bool Equals(SchoolTask other)
        {
            return _isAwarded == other._isAwarded && Title == other.Title && Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SchoolTask) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_isAwarded, Title, Description, Deadline);
        }
    }
    
    public enum SchoolTaskListType
    {
        Realized,
        Actual,
        Goals
    }
    
    public static class Sorter
    {
        private static void Swap(ref ObservableCollection<SchoolTask> list, int index)
        {
            SchoolTask term = list[index];
            list[index] = list[index+1];
            list[index+1] = term;
        }
        public static ObservableCollection<SchoolTask> Sort(this ObservableCollection<SchoolTask> toSort)
        {
            Comparer<SchoolTask> byAwardComparer = new SchoolTaskAwardComparer();
            Comparer<SchoolTask> byDeadlineComparer = new SchoolTaskDateComparer();
            for (int i = 0; i < toSort.Count-1; i++)
            {
                bool isSorted = true;
                for (int j = 0; j < toSort.Count-1; j++)
                {
                    int byAwardResult = byAwardComparer.Compare(toSort[j], toSort[j + 1]);
                    if (byAwardResult == 1 || byAwardResult == 0 && byDeadlineComparer.Compare(toSort[j],toSort[j+1]) == 1)
                    {
                        Swap(ref toSort,j);
                        isSorted = false;
                    }
                }
                if (isSorted)
                {
                    break;
                }
            }

            return toSort;
        }
    }    

    class SchoolTaskAwardComparer : Comparer<SchoolTask>
    {
        public override int Compare(SchoolTask x, SchoolTask y)
        {
            if (!x.IsAwarded && y.IsAwarded)
            {
                return 1;
            }

            if (x.IsAwarded && !y.IsAwarded)
            {
                return -1;
            }
            return 0;
        }
    }
    class SchoolTaskDateComparer : Comparer<SchoolTask>
    {
        public override int Compare(SchoolTask x, SchoolTask y)
        {
            if (x.Deadline.Year > y.Deadline.Year)
            {
                return 1;
            }
            if (x.Deadline.Year < y.Deadline.Year)
            {
                return -1;
            }
            if (x.Deadline.Month > y.Deadline.Month)
            {
                return 1;
            }
            if (x.Deadline.Month < y.Deadline.Month)
            {
                return -1;
            }
            if (x.Deadline.Day > y.Deadline.Day)
            {
                return 1;
            }
            if (x.Deadline.Day < y.Deadline.Day)
            {
                return -1;
            }
            return 0;
        }
    }
}