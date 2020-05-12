using System;
using System.Collections.Generic;
using System.Text;

namespace StudyOrganizer.DLL.Models
{
    [Serializable]
    public class Subject
    {
        public int Id { get; private set; }
        public int SubjectListId { get; private set; }
        public string Name { get; set; }
        public SubjectTypes Type { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }

        private Subject() { }

        public override string ToString()    
        {
            return $"{Name}, {nameof(Type)}: {Type}, {Day} {Hour}";
        }

        public Subject(int id, int subjectListId, string name, SubjectTypes type, DayOfWeek day, int hour)
        {
            Id = id;
            SubjectListId = subjectListId;
            Name = name;    
            Type = type;
            Day = day;
            Hour = hour;
        }
    }

    public enum SubjectTypes
    {
        Wyklad, Cwiczenia, Lablatoria, Semianaria, Projekt
    }
}
