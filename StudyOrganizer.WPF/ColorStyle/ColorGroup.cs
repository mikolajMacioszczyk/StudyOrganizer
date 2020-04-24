using System.Windows.Media;

namespace StudyOrganizer.WPF.ColorStyle
{
    public class ColorGroup
    {
        public SolidColorBrush MainColor { get; }
        public SolidColorBrush SecondColor { get; }
        public SolidColorBrush BackColor { get; }
            
        public ColorGroup(SolidColorBrush mainColor, SolidColorBrush secondColor, SolidColorBrush backColor)
        {
            MainColor = mainColor;
            BackColor = backColor;
            SecondColor = secondColor;
        }
    }
}