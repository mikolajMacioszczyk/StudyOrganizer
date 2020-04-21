using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StudyOrganizer.DLL.Annotations;

namespace StudyOrganizer.WPF.ViewModels
{
    public class ColorMode : INotifyPropertyChanged
    {
        private SolidColorBrush _selected;
        public static SolidColorBrush FirstColor { get; set; } = Brushes.Firebrick;
        public static SolidColorBrush SecondColor { get; set; } = (SolidColorBrush)(new BrushConverter().ConvertFrom("#cce6fc"));
        public static SolidColorBrush ThirdColor { get; set; } = Brushes.CornflowerBlue;
        
        public SolidColorBrush Selected
        {
            get => _selected;
            set
            {
                if (value != _selected)
                {
                    _selected = value;
                    OnPropertyChanged();                    
                }
            }
        }

        public ColorMode()
        {
            Selected = FirstColor;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}