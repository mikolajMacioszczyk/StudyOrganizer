using System;
using System.Collections.Generic;
using System.Linq;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ColorStyle;
using DayOfWeek = StudyOrganizer.DLL.Models.DayOfWeek;

namespace StudyOrganizer.WPF.ViewModels
{
    public class NewSubjectViewModel
    {
        public Subject _returnedSubject { get; set; }
        public ColorMode ColorStyle { get; set; }
        public IEnumerable<DayOfWeek> DayOfWeekValues { get; set; }
        public IEnumerable<SubjectTypes> SubjectTypesValues { get; set; }

        public NewSubjectViewModel(ColorMode colorStyle)
        {
            ColorStyle = colorStyle;
            DayOfWeekValues = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            SubjectTypesValues = Enum.GetValues(typeof(SubjectTypes)).Cast<SubjectTypes>();
        }
    }
}