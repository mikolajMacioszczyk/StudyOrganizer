using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using MySql.Data.MySqlClient;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.DLL.DataBase
{
    public class ConnectionToDb
    {
        private MySqlConnection _connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public ConnectionToDb()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "study_organizer_db";
            uid = "Miko";
            password = "Pa55word";
            string connectionString = "SERVER="+server+";"+"DATABASE="+database+";UID="+uid+";PASSWORD="+password+";";
            
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

        public void InsertSubject(int subjectListId, string subjectName, SubjectTypes type, DateTime dateTime)
        {
            char subjectType = DetermineType(type);
            string query =
                $"INSERT INTO subject (subject_list_id, name, subject_type, date) VALUES ('{subjectListId}', '{subjectName}', '{subjectType}', '{dateTime}')";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query,_connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
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
        
        public void InsertSchoolTask(int schoolTaskId, string title, string description,TaskGroup group, bool isAwarded, DateTime deadline)
        {
            char taskGroup = DetermineGroup(group);
            string query =
                $"INSERT INTO school_task (task_list_id, tittle, description, is_awarded, task_group, deadline) VALUES ('{schoolTaskId}', '{title}', '{description}', '{isAwarded}', '{taskGroup}', '{deadline}')";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query,_connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
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

        public bool IsLoginFree(string login)
        {
            string query = $"Select count(*) from user where login = '{login}'";
            bool toReturn = true;
            if (this.OpenConnection() == true)
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
            string taskListQuery = $"INSERT INTO task_list () VALUES ()";
            string subjectListQuery = $"INSERT INTO subject_list () VALUES ()";
            if (this.OpenConnection() == true)
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
            string query = $"UPDATE school_task SET tittle = '{title}', task_list_id = '{taskListId}', description = '{description}', task_group = '{taskGrop}', is_awarded = '{isAwarded}', deadline='{deadline}'" +
                           $"WHERE id = {schoolTaskId}";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public void UpdateSubject(int subjectId, int subjectLstId, string name, SubjectTypes type, DateTime time)
        {
            char subjectType =DetermineType(type);
            string query = $"UPDATE subject SET subject_list_id = '{subjectLstId}', name = '{name}', subject_type = '{subjectType}', date = '{time}' " +
                           $"WHERE id = {subjectId}";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public void UpdateUser(int userId, string login, string password, string name, int semester, string study)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string passwordHash = GetMD5Hash(md5Hash, password);
                string query = $"UPDATE user SET login = '{login}', password = '{passwordHash}', name = '{name}', semester = '{semester}', study = '{study}' " +
                               $"WHERE id = {userId}";
                if (this.OpenConnection() == true)
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
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void DeleteSchoolTask(int taskId)
        {
            string query = $"DELETE FROM school_id WHERE id = {taskId}";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        
        public void DeleteUser(int userId)
        {
            string query = $"DELETE FROM user WHERE id = {userId}";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
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

        public int Count()
        {
            throw new NotImplementedException();

        }

        public void Backup()
        {
            throw new NotImplementedException();

        }

        public void Restore()
        {
            throw new NotImplementedException();
        }
    }
}