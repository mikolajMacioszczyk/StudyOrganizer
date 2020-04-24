using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.WPF.ViewModels;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.Views;

namespace StudyOrganizer.WPF.Pages
{
    public partial class AccountPage : Page
    {
        public MenuViewModel Model { get; }
        private string _oldName;
        private string _oldLogin;
        private string _oldStudy;
        private int _oldSemester;
        public AccountPage(MenuViewModel model)
        {
            Model = model;
            DataContext = model;
            InitializeComponent();
        }

        private void Save_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                UpdateContent();
                WarningOutput.Text = "Saved";
                WarningOutput.Foreground = Brushes.Khaki;
            }
            catch (InvalidInputException exception)
            {
                WarningOutput.Text = exception.Message;
                WarningOutput.Foreground = Brushes.Red;
                Model.User.Login = _oldLogin;
            }
            
            if (Model.User.Login == _oldLogin || UserDataBase.IsLoginFree(LoginView.FILE,Model.User.Login)){}
        }

        private void Save_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AccountPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadContent();
        }

        private void LoadContent()
        {
            _oldName = Model.User.Name;
            _oldLogin = Model.User.Login;
            _oldStudy = Model.User.Study;
            _oldSemester = Model.User.Semester;
        }

        private void UpdateContent()
        {
            if (Model.User.Login != _oldLogin)
            {
                if (!UserDataBase.IsLoginFree(LoginView.FILE,Model.User.Login))
                {
                    throw new InvalidInputException("User already in Database.");
                }
                UserDataBase.UpdateLogin(LoginView.FILE,Model.User.Login,_oldLogin);
            }
            LoadContent();
        }
        
        private void AccountPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            Model.User.Name = _oldName;
            Model.User.Login = _oldLogin;
            Model.User.Study = _oldStudy;
            Model.User.Semester = _oldSemester;
        }
    }
}