using System.Diagnostics.CodeAnalysis;
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

        public UpdatesTransmitter(User user, ConnectionToDb dbConnection)
        {
            _user = user;
            _dbConnection = dbConnection;
        }

        public void UpdateLogin(string login)
        {
            if (!string.IsNullOrEmpty(login) && _dbConnection.IsLoginFree(login))
            {
                _dbConnection.UpdateUser(_user._userId, login, _user.Password, _user.Name, _user.Semester, _user.Study);
                _user.Login = login;
            }
        }

        public void UpdatePassword(string password)
        {
            if (!_user.Password.Equals(password) && Validator.PasswordValidation(password))
            {
                _dbConnection.UpdateUser(_user._userId, _user.Login, password, _user.Name, _user.Semester, _user.Study);
                _user.Password = password;
            }
        }
        
        public void UpdateName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _dbConnection.UpdateUser(_user._userId, _user.Login, _user.Password, name, _user.Semester, _user.Study);
                _user.Name = name;
            }
        }
        
        public void UpdateSemester(int semester)
        {
            if (_user.Semester != semester && semester>0)
            {
                _dbConnection.UpdateUser(_user._userId, _user.Login, _user.Password, _user. Name, semester, _user.Study);
                _user.Semester = semester;   
            }
        }
        
        public void UpdateStudy(string study)
        {
            if (!string.IsNullOrEmpty(study) && !study.Equals(_user.Study))
            {
                _dbConnection.UpdateUser(_user._userId, _user.Login, _user.Password, _user. Name, _user.Semester, study);
            }
        }
    }
}