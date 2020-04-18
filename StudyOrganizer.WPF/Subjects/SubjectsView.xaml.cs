using System.Windows;
using System.Windows.Input;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.Task;
using StudyOrganizer.WPF.Views;

namespace StudyOrganizer.WPF.Subjects
{
    public partial class SubjectsView : Window
    {
        private readonly User _user;

        public SubjectsView(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void Exit_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Exit_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            UserDataBase.SaveUser(LoginView.FILE, _user);
            Application.Current.Shutdown();
        }

        private void HomeCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        
        private void HomeCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            new MenuView(_user).Show();
            this.Close();
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

        private void Subjects_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }
    }
}