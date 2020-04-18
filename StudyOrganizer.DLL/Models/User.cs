using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyOrganizer.DLL.Models
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public string Study { get; set; }
        public int Semester { get; set; }
        public string Login { get; set; }
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
                Subject.GetBuilder().Type(SubjectTypes.Cwiczenia).WithName("AK").WeeklyDuration(2).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Wyklad).WithName("AiSD").WeeklyDuration(2).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Lablatoria).WithName("Fizyka").WeeklyDuration(2).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Wyklad).WithName("Fizyka").WeeklyDuration(2).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Lablatoria).WithName("AiSK").WeeklyDuration(2).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Cwiczenia).WithName("Dyskretna").WeeklyDuration(2).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Cwiczenia).WithName("Analiza").WeeklyDuration(2).GetSubject(),
                Subject.GetBuilder().Type(SubjectTypes.Lablatoria).WithName("SO").WeeklyDuration(2).GetSubject()
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
    }
}
