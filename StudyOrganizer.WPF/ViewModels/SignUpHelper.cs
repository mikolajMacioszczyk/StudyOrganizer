using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Exceptions;
using StudyOrganizer.WPF.Annotations;
using StudyOrganizer.WPF.Views;

namespace StudyOrganizer.WPF.ViewModels
{
    public class SignUpHelper : INotifyPropertyChanged
    {
        private string _login;
        private string _name;

        private string _password;

        private int _semester;

        private string _study;

        private Visibility _submitVisibility;

        public SignUpHelper()
        {
            VerifyVisibility();
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Study
        {
            get => _study;
            set
            {
                if (_study != value)
                {
                    _study = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Semester
        {
            get => _semester;
            set
            {
                if (_semester != value)
                {
                    _semester = value;
                    OnPropertyChanged();
                }
            }
        }

        public Visibility SubmitVisibility
        {
            get => _submitVisibility;
            set
            {
                if (value != _submitVisibility)
                {
                    _submitVisibility = value;
                    OnSubmitChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void VerifyVisibility()
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Study) && Semester != null)
                SubmitVisibility = Visibility.Visible;
            else
                SubmitVisibility = Visibility.Collapsed;
        }

        /*public void SubmitValidation()
        {
            Validator.PasswordValidation(Password);
            if (!UserDataBase.IsLoginFree(LoginView.FILE, Login))
                throw new InvalidInputException("Login already used");

            UserDataBase.RegisterUser(LoginView.FILE, Login, Password, Name, Study, (int) Semester);
        }*/

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            VerifyVisibility();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnSubmitChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}