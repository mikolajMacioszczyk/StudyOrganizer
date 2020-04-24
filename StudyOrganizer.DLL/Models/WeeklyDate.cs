using System;

namespace StudyOrganizer.DLL.Models
{
    [Serializable]
    public class WeeklyDate
    {
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }

        public WeeklyDate(DayOfWeek day, int hour)
        {
            Day = day;
            Hour = hour;
        }

        public override string ToString()
        {
            return $"{nameof(Day)}: {Day}, {nameof(Hour)}: {Hour}";
        }
    }

    public enum DayOfWeek
    {
        Poniedziałek, Wtorek, Środa, Czwartek, Piątek, Sobota, Niedziela
    }
}