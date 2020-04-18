
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            if (!File.Exists("Data"+file))
            {
                return false;
            }
            using (StreamReader reader = new StreamReader("Data"+file))
            {
                string[] whole = reader.ReadToEnd().Split(" ");
                for (int i = 0; i < whole.Length; i++)
                {
                    if (Coder.CodeString(login).Equals(whole[i]))
                    {
                        if (Coder.CodeString(password).Equals(whole[++i]))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        i++;
                    }
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
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    if (line.Equals($"LOG: {login}"))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static void RegisterUser(string file, string login, string password,string name, string study, int semester)
        {
            User newUser = new User(name,study,semester,login);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(login+file, FileMode.Create,FileAccess.Write, FileShare.None);
            formatter.Serialize(stream,newUser);
            stream.Close();

            using (StreamWriter writer = File.AppendText("Data"+file))
            {
                writer.Write(Coder.CodeString(login)+" ");
                writer.Write(Coder.CodeString(password)+" ");
            }
        }

        public static void ClearFile(string filePath)
        {
            File.Delete(filePath);
            File.Delete("Data"+filePath);
        }
    }
}
