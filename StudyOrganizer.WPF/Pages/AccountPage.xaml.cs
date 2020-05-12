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
        private UpdatesTransmitter _transmitter;
        public AccountPage(MenuViewModel model)
        {
            Model = model;
            DataContext = model;
            _transmitter = new UpdatesTransmitter(model.User);
            InitializeComponent();
        }

        private void Name_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrEmpty(box.Text))
            {
                box.Text = Model.User.Name;
                WarningOutput.Text = "Pole imienia nie może być puste";
                WarningOutput.Foreground = Brushes.Red;
            }
            else
            {
                _transmitter.UpdateName(box.Text);
                WarningOutput.Text = "Zapisano";
                WarningOutput.Foreground = Brushes.Khaki;
            }
        }
        
        private void Login_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrEmpty(box.Text))
            {
                box.Text = Model.User.Login;
                WarningOutput.Text = "Pole loginu nie może być puste";
                WarningOutput.Foreground = Brushes.Red;
            }
            else
            {   
                if (_transmitter.IsLoginFree(box.Text))
                {
                    _transmitter.UpdateLogin(box.Text);
                    WarningOutput.Text = "Zapisano";
                    WarningOutput.Foreground = Brushes.Khaki;
                }
                else if (!Model.User.Login.Equals(box.Text))
                {
                    box.Text = Model.User.Login;
                    WarningOutput.Text = "Login zajęty.";
                    WarningOutput.Foreground = Brushes.Red;
                }
            }
        }
        
        private void Password_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            try
            {
                Validator.PasswordValidation(box.Text);
                _transmitter.UpdatePassword(box.Text);
                WarningOutput.Text = "Zapisano";
                WarningOutput.Foreground = Brushes.Khaki;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                box.Text = Model.User.Password;
                WarningOutput.Text = "Hasło powinno składać się z 8 znaków zawierających \nwielką literę i małą literę lub znaki specjalne";
                WarningOutput.Foreground = Brushes.Red;
            }
        }
        
        private void Study_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrEmpty(box.Text))
            {
                box.Text = Model.User.Study;
                WarningOutput.Text = "Pole study nie może byc puste.";
                WarningOutput.Foreground = Brushes.Red;
            }
            else
            {
                _transmitter.UpdateStudy(box.Text);
                WarningOutput.Text = "Zapisano";
                WarningOutput.Foreground = Brushes.Khaki;
            }
        }
        
        private void Semester_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            try
            {
                int value = Convert.ToInt32(box.Text);
                if (value < 0)
                {
                    box.Text = Model.User.Semester.ToString();
                    WarningOutput.Text = "Semestr musi być liczbą nieujemną.";
                    WarningOutput.Foreground = Brushes.Red;
                }
                else
                {
                    _transmitter.UpdateSemester(value);
                    WarningOutput.Text = "Zapisano";
                    WarningOutput.Foreground = Brushes.Khaki;
                }
            }
            catch (FormatException ignore)
            {
                box.Text = Model.User.Semester.ToString();
                WarningOutput.Text = "Podana wartość musi być liczbą.";
                WarningOutput.Foreground = Brushes.Red;
            }
        }
    }
}