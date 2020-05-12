using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StudyOrganizer.DLL.Annotations;
using StudyOrganizer.DLL.DataBase;

namespace StudyOrganizer.DLL.Models
{
    [Serializable]
    public class User : INotifyPropertyChanged
    {
        public int UserId { get; private set; }
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
        public ObservableCollection<Subject> Subjects { get; set; }

        public User(int id,string name, string study, in int semester, string login, string password)
        {
            UserId = id;
            Name = name;
            Study = study;
            Semester = semester;
            Password = password;
            Login = login;
            LoadLists();
        }

        private void LoadLists()
        {
            ConnectionToDb dbConnection = new ConnectionToDb();
            TaskList = dbConnection.GetTaskList(UserId);
            Subjects = dbConnection.GetSubjectList(UserId);
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
