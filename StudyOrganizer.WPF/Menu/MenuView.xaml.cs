using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.Subjects;
using StudyOrganizer.WPF.Task;

namespace StudyOrganizer.WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        private readonly User _user;
        public MenuView(User user)
        {
            _user = user;
            DataContext = user;
            ApplicationCommands.Close.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));
            InitializeComponent();
        }

        private void Exit_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Exit_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            UserDataBase.SaveUser(LoginView.FILE, _user);
            Application.Current.Shutdown(1);
        }
        
        private void TaskCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TaskCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            new TaskView(_user).Show();
            this.Close();
        }
        
        private void HomeCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }
        
        private void Subjects_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Subjects_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            new SubjectsView(_user).Show();
            this.Close();
        }
    }
}