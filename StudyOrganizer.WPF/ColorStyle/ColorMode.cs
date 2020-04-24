using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using StudyOrganizer.DLL.Annotations;

namespace StudyOrganizer.WPF.ColorStyle
{
    public class ColorMode : INotifyPropertyChanged
    {
        private static BrushConverter _converter = new BrushConverter();
        private ColorGroup _selected;
        public SolidColorBrush FirstGroupCard { get; }
        public SolidColorBrush SecondGroupCard { get; }
        public SolidColorBrush ThirdGroupCard { get; }

        public static ColorGroup FirstGroup = new ColorGroup(
            (SolidColorBrush) _converter.ConvertFromString("#c5e3f0"),
            (SolidColorBrush) _converter.ConvertFromString("#a9c5d1"),
            (SolidColorBrush) _converter.ConvertFromString("#d7d9db"));
        public static ColorGroup SecondGroup = new ColorGroup(
            (SolidColorBrush) _converter.ConvertFromString("#fae7be"),
            (SolidColorBrush) _converter.ConvertFromString("#f7d999"),
            (SolidColorBrush) _converter.ConvertFromString("#dedad3"));
        public static ColorGroup ThirdGroup = new ColorGroup(
            (SolidColorBrush) _converter.ConvertFromString("#ebd5d6"),
            (SolidColorBrush) _converter.ConvertFromString("#ffccce"),
            (SolidColorBrush) _converter.ConvertFromString("#e0d7d8"));

        public ColorGroup Selected
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
            FirstGroupCard = FirstGroup.MainColor;
            SecondGroupCard = SecondGroup.MainColor;
            ThirdGroupCard = ThirdGroup.MainColor;
            Selected = FirstGroup;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}