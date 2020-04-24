using System.Windows.Media;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ColorStyle;

namespace StudyOrganizer.WPF.ViewModels
{
    public class SubjectTemplateModel
    {
        public Subject ThisSubject { get; set; }
        public ColorMode ColorMode { get; set; }

        public SubjectTemplateModel(Subject thisSubject, ColorMode colorStyle)
        {
            ThisSubject = thisSubject;
            ColorMode = colorStyle;
        }
    }
}