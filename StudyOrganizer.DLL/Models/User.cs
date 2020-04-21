using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StudyOrganizer.DLL.Annotations;

namespace StudyOrganizer.DLL.Models
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        private string _name;
        private string _study;
        private int _semester;
        private string _login;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Study
        {
            get => _study;
            set
            {
                _study = value; 
                OnPropertyChanged();
            }
        }
        public int Semester
        {
            get => _semester;
            set
            {
                _semester = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public SchoolTaskList TaskList { get; set; }
        public List<Subject> Subjects { get; set; }

        public User(string name, string study, int semester, string login, List<Subject> subjects, SchoolTaskList task)
        {
            Name = name;
            Study = study;
            Semester = semester;
            Login = login;
            TaskList = task;
            Subjects = subjects;
        }

        public User(string name, string study, in int semester, string login)
        {
            Name = name;
            Study = study;
            Semester = semester;
            Login = login;
            Subjects = new List<Subject>
            {
                Subject.GetBuilder().Type(SubjectTypes.Cwiczenia).WithName("AK").DayAndHour(new WeeklyDate(DayOfWeek.Poniedziałek,10)).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Wyklad).WithName("AiSD").DayAndHour(new WeeklyDate(DayOfWeek.Wtorek,10)).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Lablatoria).WithName("Fizyka").DayAndHour(new WeeklyDate(DayOfWeek.Poniedziałek,10)).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Wyklad).WithName("Fizyka").DayAndHour(new WeeklyDate(DayOfWeek.Środa,10)).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Lablatoria).WithName("AiSK").DayAndHour(new WeeklyDate(DayOfWeek.Czwartek,10)).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Cwiczenia).WithName("Dyskretna").DayAndHour(new WeeklyDate(DayOfWeek.Sobota,10)).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Cwiczenia).WithName("Analiza").DayAndHour(new WeeklyDate(DayOfWeek.Piątek,10)).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Lablatoria).WithName("SO").DayAndHour(new WeeklyDate(DayOfWeek.Czwartek,10)).GetSubject()
            };
            TaskList = new SchoolTaskList();
        }

        protected bool Equals(User other)
        {
            return Name == other.Name && Study == other.Study && Semester == other.Semester && Login == other.Login;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Study, Semester, Login);
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
