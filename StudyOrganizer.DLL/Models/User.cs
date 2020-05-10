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
        public int _userId { get; private set; }
        private string _name;
        private string _study;
        private int _semester;    
        private string _login;
        private string _password;

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
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public SchoolTaskList TaskList { get; set; }
        public List<Subject> Subjects { get; set; }

        public User(string name, string study, int semester, string login, string password, List<Subject> subjects, SchoolTaskList task)
        {
            Name = name;
            Study = study;
            Semester = semester;
            Login = login;
            Password = password;
            TaskList = task;
            Subjects = subjects;
        }

        public User(int id,string name, string study, in int semester, string login, string password)
        {
            _userId = id;
            Name = name;
            Study = study;
            Semester = semester;
            Password = password;
            Login = login;
            Subjects = new List<Subject>();
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
