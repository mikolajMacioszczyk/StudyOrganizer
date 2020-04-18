using System;
using System.Collections.Generic;
using System.Text;

namespace StudyOrganizer.DLL.Models
{
    [Serializable]
    public class Subject
    {
        public string Name { get; set; }
        public SubjectTypes Type { get; set; }
        public int HoursPerWeek { get; set; }

        private Subject()
        {
        }

        public override string ToString()
        {
            return $"{Name}, {nameof(Type)}: {Type}, {HoursPerWeek} hours in week";
        }

        public static Builder GetBuilder() => new Builder();

        public class Builder
        {
            private Subject _currentBuild;
            public Builder()
            {
                _currentBuild = new Subject();
            }
            public Subject GetSubject()
            {
                return _currentBuild;
            }

            public Builder WithName(string name)
            {
                _currentBuild.Name = name;
                return this;
            }

            public Builder Type(SubjectTypes type)
            {
                _currentBuild.Type = type;
                return this;
            }

            public Builder WeeklyDuration(int time)
            {
                _currentBuild.HoursPerWeek = time;
                return this;
            }
        }
    }

    public enum SubjectTypes
    {
        Wyklad, Cwiczenia, Lablatoria, Semianaria, Projekt
    }
}
