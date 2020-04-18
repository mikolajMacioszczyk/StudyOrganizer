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
            var subject = Subject.GetBuilder().WithName("Fiza").WeeklyDuration(2).Type(SubjectTypes.Wyklad)
                .WithName("AiSD").WeeklyDuration(3).GetSubject();

            var expected = "AiSD, Type: Wyklad, 3 hours in week";
            var result = subject.ToString();

            Assert.AreEqual(expected,result);
        }
    }
}