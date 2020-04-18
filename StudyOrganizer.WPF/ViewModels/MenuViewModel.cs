using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudyOrganizer.DLL.Annotations;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.WPF.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private bool _isNewTaskPanelVisible;
        public User User { get; }

        public bool IsNewTaskPanelVisible
        {
            get => _isNewTaskPanelVisible;
            set
            {
                if (_isNewTaskPanelVisible != value)
                {       
                    _isNewTaskPanelVisible = value;
                    OnPropertyChanged();                   
                }
            }
        }

        public MenuViewModel(User user)
        {
            User = user;
            IsNewTaskPanelVisible = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}