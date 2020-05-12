using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.DLL.DataBase
{
    public class UpdatesTransmitter
    {
        private readonly User _user;
        private ConnectionToDb _dbConnection;

        public UpdatesTransmitter([NotNull]User user)
        {
            _user = user;
            _dbConnection = new ConnectionToDb();
        }

        public UpdatesTransmitter([NotNull]User user, [NotNull] ConnectionToDb dbConnection)
        {
            _user = user;
            _dbConnection = dbConnection;
        }
        
        public UpdatesTransmitter([NotNull] ConnectionToDb dbConnection)
        {
            _dbConnection = new ConnectionToDb();
        }

        public bool IsLoginFree(string login)
        {
            return _dbConnection.IsLoginFree(login);
        }

        public void UpdateLogin(string login)
        {
            if (!string.IsNullOrEmpty(login) && _dbConnection.IsLoginFree(login))
            {
                _dbConnection.UpdateUser(_user.UserId, login, _user.Password, _user.Name, _user.Semester, _user.Study);
                _user.Login = login;
            }
        }

        public void UpdatePassword(string password)
        {
            if (!_user.Password.Equals(password) && Validator.PasswordValidation(password))
            {
                _dbConnection.UpdateUser(_user.UserId, _user.Login, password, _user.Name, _user.Semester, _user.Study);
                _user.Password = password;
            }
        }
        
        public void UpdateName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _dbConnection.UpdateUser(_user.UserId, _user.Login, _user.Password, name, _user.Semester, _user.Study);
                _user.Name = name;
            }
        }
        
        public void UpdateSemester(int semester)
        {
            if (_user.Semester != semester && semester>0)
            {
                _dbConnection.UpdateUser(_user.UserId, _user.Login, _user.Password, _user. Name, semester, _user.Study);
                _user.Semester = semester;   
            }
        }
        
        public void UpdateStudy(string study)
        {
            if (!string.IsNullOrEmpty(study) && !study.Equals(_user.Study))
            {
                _dbConnection.UpdateUser(_user.UserId, _user.Login, _user.Password, _user. Name, _user.Semester, study);
            }
        }

        public void DeleteTask([NotNull]SchoolTask task)
        {
            if (task == null)
            {
                return;
            }
            _dbConnection.DeleteSchoolTask(task.Id);
        }
        
        public void UpdateTaskTittle(SchoolTask task, string title)
        {    
            if (!string.IsNullOrEmpty(title))
            {
                try
                {
                    _dbConnection.UpdateSchoolTask(task.Id, task.TaskListId,title, task.Description, task.TaskGroup, task.IsAwarded,task.Deadline);
                    task.Title = title;
                }
                catch (MySqlException ignore) { }
            }
        }
        
        public void UpdateTaskDescription(SchoolTask task, string description)
        {    
            if (!string.IsNullOrEmpty(description))
            {
                _dbConnection.UpdateSchoolTask(task.Id, task.TaskListId,task.Title, description, task.TaskGroup, task.IsAwarded,task.Deadline);
                task.Description = description;
            }
        }

        public void UpdateTaskGroup(SchoolTask task, TaskGroup taskGroup)
        {    
            _dbConnection.UpdateSchoolTask(task.Id, task.TaskListId,task.Title, task.Description, taskGroup, task.IsAwarded,task.Deadline);
            task.TaskGroup = taskGroup;
        }
        
        public void UpdateTaskAward(SchoolTask task, bool isAwarded)
        {    
            _dbConnection.UpdateSchoolTask(task.Id, task.TaskListId,task.Title, task.Description, task.TaskGroup, isAwarded,task.Deadline);
            task.IsAwarded = isAwarded;
        }
        
        public void UpdateTaskDeadline(SchoolTask task, DateTime deadline)
        {    
            _dbConnection.UpdateSchoolTask(task.Id, task.TaskListId,task.Title, task.Description, task.TaskGroup, task.IsAwarded,deadline);
            task.Deadline = deadline;
        }

        public void DeleteSubject(Subject toDelete)
        {
            if (toDelete == null)
            {
                return;
            }
            _dbConnection.DeleteSubject(toDelete.Id);
        }

        public void UpdateSubjectName(Subject subject, string name)
        {
            if (!string.IsNullOrEmpty(name) && subject != null)
            {
                try
                {
                    _dbConnection.UpdateSubjectEqualityData(subject.Id, name, subject.Type);
                    subject.Name = name;
                }
                catch (MySqlException ignore) { }
            }
        }
        
        public void UpdateSubjectType(Subject subject, SubjectTypes type)
        {
            if (subject != null)
            {
                _dbConnection.UpdateSubjectEqualityData(subject.Id, subject.Name, type);
                subject.Type = type;
            }
        }
        
        public void UpdateSubjectDay(Subject subject, DayOfWeek day)
        {
            if (subject != null)
            {
                _dbConnection.UpdateSubject(subject.Id, subject.SubjectListId, subject.Name, subject.Type, day.ToString(), subject.Hour);
                subject.Day = day;
            }
        }
        
        public void UpdateSubjectHour(Subject subject, int hour)
        {
            if (subject != null)
            {
                _dbConnection.UpdateSubject(subject.Id, subject.SubjectListId, subject.Name, subject.Type, subject.Day.ToString(), hour);
                subject.Hour = hour;
            }
        }
    }
}