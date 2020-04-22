using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using StudyOrganizer.DLL.Annotations;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ColorStyle;
using StudyOrganizer.WPF.UserControls;

namespace StudyOrganizer.WPF.ViewModels
{
    public class SingleTaskTemplateModel : DependencyObject
    {
        public ObservableCollection<SchoolTask> ThisTaskList { get; set; }
        public SchoolTaskListType Type { get; set; }
        public ColorMode ColorMode { get; set; }

        public SingleTaskTemplateModel(ObservableCollection<SchoolTask> thisTaskList,ColorMode colorStyle, SchoolTaskListType type)
        {
            ThisTaskList = thisTaskList;
            ColorMode = colorStyle;
            Type = type;
        }
    }
}