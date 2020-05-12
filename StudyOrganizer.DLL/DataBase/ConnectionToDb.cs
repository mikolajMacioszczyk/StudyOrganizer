using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.DLL.DataBase
{
    public class ConnectionToDb
    {
        private MySqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;
        private string _password;

        public ConnectionToDb()
        {
            Initialize();
        }

        private void Initialize()
        {
            _server = "localhost";
            _database = "study_organizer_db";
            _uid = "Miko";
            _password = "Pa55word";
            string connectionString = "SERVER="+_server+";"+"DATABASE="+_database+";UID="+_uid+";PASSWORD="+_password+";";
            
            _connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                if (e.Number == 0)
                { 
                    Console.WriteLine("Cannot connect to server.");
                }
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return false;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void InsertSubject(int subjectListId, string subjectName, SubjectTypes type, string day, int hour)
        {
            if (string.IsNullOrEmpty(subjectName) || string.IsNullOrEmpty(day))
            {
                throw new InvalidInputException("Name and day cannot be empty");
            }
            char subjectType = DetermineType(type);
            string canUpdateQuery =
                $"SELECT COUNT(*) FROM subject WHERE name = '{subjectName}' and subject_type = '{subjectType}'";
            string query =
                $"INSERT INTO subject (subject_list_id, name, subject_type, day, hour) VALUES ('{subjectListId}', '{subjectName}', '{subjectType.ToString()}', '{day}', '{hour}')";
            if (this.OpenConnection())
            {
                MySqlCommand canExecuteCommand = new MySqlCommand(canUpdateQuery,_connection);
                MySqlCommand cmd = new MySqlCommand(query,_connection);
                int amount = int.Parse(canExecuteCommand.ExecuteScalar() + "");
                if (amount==0)
                {
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                else
                {
                    this.CloseConnection();
                    throw new InvalidOperationException("Subject already in database");
                }
            }
        }
        private char DetermineType(SubjectTypes type)
        {
            switch (type)
            {
                case SubjectTypes.Wyklad:
                    return 'W';
                case SubjectTypes.Cwiczenia:
                    return 'C';
                case SubjectTypes.Lablatoria:
                    return 'L';
                case SubjectTypes.Semianaria:
                    return 'S';
                case SubjectTypes.Projekt:
                    return 'P';
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private SubjectTypes DetermineType(char type)
        {
            switch (type)
            {
                case 'W':
                    return SubjectTypes.Wyklad;
                case 'C':
                    return SubjectTypes.Cwiczenia;
                case 'L':
                    return SubjectTypes.Lablatoria;
                case 'S':
                    return SubjectTypes.Semianaria;
                case 'P':
                    return SubjectTypes.Projekt;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }    
        
        public void InsertSchoolTask(int schoolTaskId, string title, string description,TaskGroup group, bool isAwarded, DateTime deadline)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                throw new InvalidInputException("Title and description cannot be empty");
            }
            char taskGroup = DetermineGroup(group);
            byte boolValue = isAwarded == true ? (byte) 1 : (byte) 0;    
            var sqlFormattedDate = deadline.Date.ToString("yyyy-MM-dd HH:mm:ss");
            string query =
                $"INSERT INTO school_task (task_list_id, tittle, description, is_awarded, task_group, deadline) VALUES ('{schoolTaskId}', '{title}', '{description}', '{boolValue}', '{taskGroup}', '{sqlFormattedDate}')";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query,_connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    this.CloseConnection();
                }
            }
        }

        private char DetermineGroup(TaskGroup type)
        {
            switch (type)
            {
                case TaskGroup.Planned:
                    return 'P';
                case TaskGroup.Actual:
                    return 'A';
                case TaskGroup.Realized:
                    return 'R';
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private TaskGroup DetermineGroup(char c)
        {
            switch (c)
            {
                case 'P':
                    return TaskGroup.Planned;
                case 'A':
                    return TaskGroup.Actual;
                case 'R':
                    return TaskGroup.Realized;
                default:
                    throw new ArgumentOutOfRangeException(nameof(c), c, null);
            }
        }

        public bool IsLoginFree(string login)
        {
            string query = $"Select count(*) from user where login = '{login}'";
            bool toReturn = true;
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query,_connection);
                int userWithTheSameLoginCount = int.Parse(cmd.ExecuteScalar() + "");
                if (userWithTheSameLoginCount > 0)
                {
                    toReturn = false;
                }
                this.CloseConnection();
            }
            return toReturn;
        }

        public void InsertUser(string login, string password, string name, int semester, string study)
        {
            if (string.IsNullOrEmpty(login)||string.IsNullOrEmpty(password)||string.IsNullOrEmpty(name))
            {
                throw new InvalidInputException("Login, Password and Name cannot be Empty");
            }
            string newUserQuery;
            using (MD5 md5Hash = MD5.Create())
            {
                string passwordHash = GetMD5Hash(md5Hash, password);
                newUserQuery =
                    $"INSERT INTO user (login, password, name, semester, study) VALUES ('{login}', '{passwordHash}', '{name}', '{semester}', '{study}')";
            }
            string taskListQuery = "INSERT INTO task_list () VALUES ()";
            string subjectListQuery = "INSERT INTO subject_list () VALUES ()";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(newUserQuery,_connection);
                MySqlCommand cmd2 = new MySqlCommand(taskListQuery,_connection);
                MySqlCommand cmd3 = new MySqlCommand(subjectListQuery,_connection);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        private string GetMD5Hash(MD5 md5Hasher, string input)
        {
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", data.Select(x => x.ToString("x2")));
        }

        public void UpdateSchoolTask(int schoolTaskId, int taskListId, string title, string description, TaskGroup group, bool isAwarded, DateTime deadline)
        {
            char taskGrop = DetermineGroup(group);
            byte boolValue = (byte) (isAwarded == true ? 1 : 0);
            var sqlFormattedDate = deadline.Date.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $"UPDATE school_task SET tittle = '{title}', task_list_id = '{taskListId}', description = '{description}', task_group = '{taskGrop}', is_awarded = '{boolValue}', deadline='{sqlFormattedDate}'" +
                           $"WHERE id = {schoolTaskId}";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    this.CloseConnection();
                }
            }
        }
        
        public void UpdateSubject(int subjectId, int subjectLstId, string name, SubjectTypes type, string day, int hour)
        {
            char subjectType =DetermineType(type);
            string canUpdateQuery =
                $"SELECT COUNT(*) FROM subject WHERE name = '{name}' and subject_type = '{subjectType}'";
            string query = $"UPDATE subject SET subject_list_id = '{subjectLstId}', name = '{name}', subject_type = '{subjectType}', day = '{day}', hour = '{hour}' " +
                           $"WHERE id = {subjectId}";
            if (this.OpenConnection())
            {
                MySqlCommand canExecuteCommand = new MySqlCommand(canUpdateQuery, _connection);
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                int amount = int.Parse(canExecuteCommand.ExecuteScalar()+"");
                if (amount==1)
                {
                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
        }

        public void UpdateSubjectEqualityData(int subjectId, string name, SubjectTypes type)
        {    
            char subjectType =DetermineType(type);
            string canUpdateQuery =
                $"SELECT COUNT(*) FROM subject WHERE name = '{name}' and subject_type = '{subjectType}'";
            string query = $"UPDATE subject SET name = '{name}', subject_type = '{subjectType}' WHERE id = {subjectId}";
            if (this.OpenConnection())
            {
                MySqlCommand canExecuteCommand = new MySqlCommand(canUpdateQuery, _connection);
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                int amount = int.Parse(canExecuteCommand.ExecuteScalar()+"");
                if (amount==0)
                {
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                else
                {
                    this.CloseConnection();
                    throw new InvalidOperationException("Subject in database");
                    throw new InvalidOperationException("Subject in database");
                }
            }
        }
        
        public void UpdateUser(int userId, string login, string password, string name, int semester, string study)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string passwordHash = GetMD5Hash(md5Hash, password);
                string query = $"UPDATE user SET login = '{login}', password = '{passwordHash}', name = '{name}', semester = '{semester}', study = '{study}' " +
                               $"WHERE id = {userId}";
                if (this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, _connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
            }
        }

        public void DeleteSubject(int subjectId)
        {
            string query = $"DELETE FROM subject WHERE id = {subjectId}";
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void DeleteSchoolTask(int taskId)
        {
            string query = $"DELETE FROM school_task WHERE id = {taskId}";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public void DeleteUser(int userId)
        {
            string deleteUserQuery = $"DELETE FROM user WHERE id = {userId}";
            string deleteTasksQuery = $"DELETE FROM school_task WHERE task_list_id = {userId}";
            string deleteSubjectsQuery = $"DELETE FROM subject WHERE subject_list_id = {userId}";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(deleteUserQuery, _connection);
                MySqlCommand cmd2 = new MySqlCommand(deleteTasksQuery, _connection);
                MySqlCommand cmd3 = new MySqlCommand(deleteSubjectsQuery, _connection);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public User GetUser(string login, string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string passwordHash = GetMD5Hash(md5Hash, password);
                string isInBaseQuery = $"SELECT Count(*) FROM user where login = '{login}' and password = '{passwordHash}'";
                string query = $"SELECT * FROM user where login = '{login}' and password = '{passwordHash}'";
                if (this.OpenConnection() ==  true)
                {
                    MySqlCommand isInBaseCmd = new MySqlCommand(isInBaseQuery, _connection);
                    int count = int.Parse(isInBaseCmd.ExecuteScalar() + "");
                    if (count==0)
                    {
                        this.CloseConnection();
                        throw new UserNotInDatabaseException("User not in database");
                    }
                    MySqlCommand cmd = new MySqlCommand(query, _connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    int id = dataReader.GetInt32("id");
                    string name = dataReader.GetString("name");
                    string study = dataReader.GetString("study");
                    int semester = dataReader.GetInt32("semester");
                    User readed = new User(id, name, study, (semester), login, password);
                    this.CloseConnection();
                    return readed;    
                }
            }
            this.CloseConnection();
            throw new Exception("Something goes wrong");
        }
        
        public Subject GetSubject(string name)    
        {
            string query = $"SELECT * FROM subject where name = '{name}'";
            if (this.OpenConnection() ==  true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                if (dataReader.HasRows)
                {
                    int id = dataReader.GetInt32("id");
                    int subjectListId = dataReader.GetInt32("subject_list_id");
                    SubjectTypes type = DetermineType(dataReader.GetChar("subject_type"));
                    int hour = dataReader.GetInt32("hour");
                    DayOfWeek day = (DayOfWeek) Enum.Parse(typeof(DayOfWeek), dataReader.GetString("day"));
                    this.CloseConnection();
                    return new Subject(id, subjectListId, name, type, day, hour);
                }
                this.CloseConnection();
            }
            throw new InvalidOperationException("Something goes wrong");
        }
        
        public SchoolTask GetSchoolTask(string tittle)    
        {
            string query = $"SELECT * FROM school_task where tittle = '{tittle}'";
            if (this.OpenConnection() ==  true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                int id = dataReader.GetInt32("id");
                int taskListId = dataReader.GetInt32("task_list_id");
                string description = dataReader.GetString("description");
                bool isAwarded = dataReader.GetBoolean("is_awarded");
                TaskGroup group = DetermineGroup(dataReader.GetChar("task_group"));
                DateTime deadline = dataReader.GetDateTime("deadline");
                SchoolTask readed = new SchoolTask(id, taskListId, tittle, description,isAwarded, group, deadline);
                this.CloseConnection();
                return readed;    
            }
            this.CloseConnection();
            throw new Exception("Something goes wrong");
        }

        public SchoolTaskList GetTaskList(int userId)
        {
            string query = $"SELECT * FROM school_task where task_list_id = '{userId}'";
            if (this.OpenConnection() ==  true)
            {
                List<SchoolTask> wholeTasks = new List<SchoolTask>();
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32("id");
                    int taskListId = dataReader.GetInt32("task_list_id");
                    string tittle = dataReader.GetString("tittle");
                    string description = dataReader.GetString("description");
                    bool isAwarded = dataReader.GetBoolean("is_awarded");
                    TaskGroup group = DetermineGroup(dataReader.GetChar("task_group"));
                    DateTime deadline = dataReader.GetDateTime("deadline");
                    wholeTasks.Add(new SchoolTask(id, taskListId, tittle, description, isAwarded, group, deadline));
                }   
                ObservableCollection<SchoolTask> realized = new ObservableCollection<SchoolTask>();
                ObservableCollection<SchoolTask> actual = new ObservableCollection<SchoolTask>();
                ObservableCollection<SchoolTask> planned = new ObservableCollection<SchoolTask>();
                foreach (var schoolTask in wholeTasks)
                {
                    switch (schoolTask.TaskGroup)
                    {
                        case TaskGroup.Planned:
                            planned.Add(schoolTask);
                            break;
                        case TaskGroup.Actual:
                            actual.Add(schoolTask);
                            break;
                        case TaskGroup.Realized:
                            realized.Add(schoolTask);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                this.CloseConnection();
                return new SchoolTaskList(realized, actual, planned);
            }
            this.CloseConnection();
            throw new Exception("Something goes wrong");
        }
        
        public ObservableCollection<Subject> GetSubjectList(int userId)
        {
            string query = $"SELECT * FROM subject where subject_list_id = '{userId}'";
            if (this.OpenConnection() ==  true)
            {
                ObservableCollection<Subject> output = new ObservableCollection<Subject>();
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    int id = dataReader.GetInt32("id");
                    int subjectListId = dataReader.GetInt32("subject_list_id");
                    string name = dataReader.GetString("name");
                    SubjectTypes type = DetermineType(dataReader.GetChar("subject_type"));
                    DayOfWeek day = (DayOfWeek) Enum.Parse(typeof(DayOfWeek), dataReader.GetString("day"));
                    int hour = dataReader.GetInt32("hour");
                    output.Add(new Subject(id, subjectListId, name, type, day,hour));
                }   
                this.CloseConnection();
                return output;
            }
            this.CloseConnection();
            throw new Exception("Something goes wrong");
        }
    }
}