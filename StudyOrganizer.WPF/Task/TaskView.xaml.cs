using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.Subjects;
using StudyOrganizer.WPF.Views;

namespace StudyOrganizer.WPF.Task
{
    public partial class TaskView : Window
    {
        private readonly User _user;

        public TaskView(User user)
        {
            _user = user;
            InitializeComponent();
            ActualItemsControl.DataContext = user.TaskList.Actual;
            ActualItemsControl.ThisSchoolTaskList = user.TaskList;
            
            RealizedItemsControl.DataContext = user.TaskList.Realized;
            RealizedItemsControl.ThisSchoolTaskList = _user.TaskList;
            
            GoalsItemsControl.DataContext = user.TaskList.Planned;
            GoalsItemsControl.ThisSchoolTaskList = user.TaskList;
        }

        private void Close_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            UserDataBase.SaveUser(LoginView.FILE, _user);
            Application.Current.Shutdown(1);
        }
        
        private void Home_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Home_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            new MenuView(_user).Show();
            this.Close();
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
        
        private void Tasks_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}