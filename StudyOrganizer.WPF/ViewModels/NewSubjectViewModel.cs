using System;
using System.Collections.Generic;
using System.Linq;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ColorStyle;

namespace StudyOrganizer.WPF.ViewModels
{
    public class NewSubjectViewModel
    {
        public int SubjectListId { get; }
        public Subject _returnedSubject { get; set; }
        public ColorMode ColorStyle { get; set; }
        public IEnumerable<DayOfWeek> DayOfWeekValues { get; set; }
        public IEnumerable<SubjectTypes> SubjectTypesValues { get; set; }

        public NewSubjectViewModel(ColorMode colorStyle, int subjectListId)
        {
            ColorStyle = colorStyle;
            SubjectListId = subjectListId;
            DayOfWeekValues = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            SubjectTypesValues = Enum.GetValues(typeof(SubjectTypes)).Cast<SubjectTypes>();
        }
    }
}