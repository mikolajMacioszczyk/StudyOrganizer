using System;
using System.Windows;
using System.Windows.Controls;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Views
{
    /// <summary>
    ///     Logika interakcji dla klasy SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        private ConnectionToDb dbConnection;
        public SignUpView()
        {
            InitializeComponent();
            dbConnection = new ConnectionToDb();
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SubmitValidation();
            }
            catch (InvalidInputException exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            new LoginView().Show();
            Close();
        }

        public void SubmitValidation()
        {
            SignUpHelper helper = (SignUpHelper) Panel.DataContext;
            Validator.PasswordValidation(helper.Password);
            if (!dbConnection.IsLoginFree(helper.Login))
                throw new InvalidInputException("Login already used");
            dbConnection.InsertUser(helper.Login, helper.Password, helper.Name, helper.Semester, helper.Study);
        }

        private void Password_OnLostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox box = (PasswordBox) sender;
            SignUpHelper helper = (SignUpHelper) Panel.DataContext;
            helper.Password = box.Password;
        }
    }
}