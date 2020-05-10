using System;
using System.IO;
using System.Windows;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Views
{
    /// <summary>
    ///     Logika interakcji dla klasy ShellView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public static readonly string FILE = "MyUsers.txt";
        private ConnectionToDb dbConnection;
        public LoginView()
        {
            InitializeComponent();
            dbConnection = new ConnectionToDb();
        }

        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SignInValidation(Login.Text, Password.Password);
                User user = dbConnection.GetUser(Login.Text, Password.Password);
                new MenuView(user).Show();
                Close();
            }
            catch (InvalidInputException exception)
            {
                Intro.Text = exception.Message;
            }
            catch (UserNotInDatabaseException exception)
            {
                Intro.Text = exception.Message;
            }
        }
        
        private void SignInValidation(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new InvalidInputException("All fields must be filled out!");
        }

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            new SignUpView().Show();
            Close();
        }
    }
}