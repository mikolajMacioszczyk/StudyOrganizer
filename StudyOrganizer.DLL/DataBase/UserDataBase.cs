
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.DLL.DataBase
{
    public static class UserDataBase
    {
        public static bool IsUserRegistered(string file, string login, string password)
        {
            if (!File.Exists(file))
            {
                return false;
            }

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            using (StreamReader reader = new StreamReader(file))
            {
                string[] whole = File.ReadAllLines(file);
                string logCoded = Coder.CodeString(login);
                for (int i = 0; i < whole.Length; i++)
                {
                    if (whole[i].Equals(logCoded))
                    {
                        if (Coder.CodeString(password).Equals(whole[++i]))
                        {
                            return true;
                        }
                        return false;
                    }
                    i++;
                }
                return false;
            }
        }

        public static User GetUser(string file, string login, string password)
        {
            if (!IsUserRegistered(file,login,password))
            {
                throw new UserNotInDatabaseException("User not in database");
            }

            return GetUserFromFile(login + file);
        }

        public static User GetUserFromFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException();
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(file, FileMode.Open,FileAccess.Read, FileShare.Read);
            Object deserialized = formatter.Deserialize(stream);
            stream.Close();
            return (User) deserialized;
        }

        public static void SaveUser(string file, User user)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(user.Login+file, FileMode.Create,FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, user);
            stream.Close();
        }
        
        public static bool IsLoginFree(string file, string login)
        {
            if (!File.Exists(file))
            {
                return true;
            }
            string[] wholeText = File.ReadAllLines(file);
            var coded = Coder.CodeString(login);
            return !wholeText.Contains(coded);
        }

        public static void UpdateLogin(string file, string newLogin, string oldLogin)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("Database don't exist");
            }
            if (string.IsNullOrEmpty(newLogin))
            {
                throw new InvalidInputException("Login mustn't be empty");
            }
            string[] before = File.ReadAllLines(file);
            string codedOld = Coder.CodeString(oldLogin);
            string codedNew = Coder.CodeString(newLogin);
            bool isUpdated = false;
            string[] after = before.Select(log =>
            {
                if (codedOld.Equals(log))
                {
                    isUpdated = true;
                    return codedNew;
                }
                if (codedNew.Equals(log))
                {
                    throw new InvalidInputException();
                }
                return log;
            }).ToArray();
            if (!isUpdated)
            {
                throw new InvalidInputException();
            }

            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (var userDates in after)
                {
                    writer.WriteLine(userDates);
                }
            }
            User witchingChange = GetUserFromFile(oldLogin + file);
            witchingChange.Login = newLogin;
            SaveUser(newLogin+file, witchingChange);
            File.Delete(oldLogin+file);
        }

        public static void RegisterUser(string file, string login, string password,string name, string study, int semester)
        {
            User newUser = new User(name,study,semester,login);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(login+file, FileMode.Create,FileAccess.Write, FileShare.None);
            formatter.Serialize(stream,newUser);
            stream.Close();

            using (StreamWriter writer = File.AppendText(file))
            {
                writer.WriteLine(Coder.CodeString(login));
                writer.WriteLine(Coder.CodeString(password));
            }
        }

        public static void ClearFile(string filePath)
        {
            File.Delete(filePath);
            File.Delete("Data"+filePath);
        }
    }
}
