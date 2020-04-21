using NUnit.Framework;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    public class SubjectTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BuilderTest()
        {
            var subject = Subject.GetBuilder().WithName("Fiza").DayAndHour(new WeeklyDate(DayOfWeek.Czwartek,12)).Type(SubjectTypes.Wyklad)
                .WithName("AiSD").GetSubject();

            var expected = "AiSD, Type: Wyklad, Day: Czwartek, Hour: 12";
            var result = subject.ToString();

            Assert.AreEqual(expected,result);
        }
    }
}