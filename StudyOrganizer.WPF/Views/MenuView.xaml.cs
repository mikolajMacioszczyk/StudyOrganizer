using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.Pages;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        private readonly MenuViewModel _model;
        private List<Page> _pages;
        private int _pageIndex;
        public MenuView(User user)
        {
            _model = new MenuViewModel(user);
            DataContext = _model;
            _pages = new List<Page>()
            {
                new HomePage(_model),
                new SubjectsPage(_model),
                new TaskPage(_model),
                new AccountPage(_model)
            };
            _pageIndex = 0;
            ApplicationCommands.Close.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));
            InitializeComponent();
            MainFrame.Navigate(_pages[0]);
        }

        private void Exit_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Exit_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            UserDataBase.SaveUser(LoginView.FILE, _model.User);
            Application.Current.Shutdown(1);
        }
        
        private void TaskCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _pageIndex != 2;
        }

        private void TaskCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _pageIndex = 2;
            MainFrame.Navigate(_pages[_pageIndex]);
            _model.IsNewTaskPanelVisible = true;
        }
        
        private void HomeCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _pageIndex != 0;
        }
        
        private void HomeCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _pageIndex = 0;
            MainFrame.Navigate(_pages[_pageIndex]);
        }
        
        private void Subjects_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _pageIndex != 1;
        }

        private void Subjects_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _pageIndex = 1;
            MainFrame.Navigate(_pages[_pageIndex]);
            _model.IsNewSubjectPanelVisible = true;
        }

        private void AddNewTask_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        
        private void AddNewTask_OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            NewTaskView newTaskView = new NewTaskView();
            SchoolTask userNewTask = newTaskView.OpenNewTaskView();
            if (userNewTask != null)
            {
                _model.User.TaskList.Planned.Add(userNewTask);
            }
        }

        private void Account_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _pageIndex != 3;
        }

        private void Account_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _pageIndex = 3;
            MainFrame.Navigate(_pages[_pageIndex]);
        }

    }
}