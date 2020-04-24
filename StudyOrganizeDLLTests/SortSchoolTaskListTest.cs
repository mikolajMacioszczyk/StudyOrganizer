using System;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizeDLLTests
{
    [TestFixture]
    public class SortSchoolTaskListTest
    {

        [Test]
        public void SortEmptyList()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>().Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>();
            Assert.True(toSort.SequenceEqual(other));
        }

        [Test]
        public void SequenceEqualTest()
        {
            ObservableCollection<SchoolTask> first = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now),
                new SchoolTask("Second", "describtion", true, DateTime.Now),
            };
            ObservableCollection<SchoolTask> second = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now),
                new SchoolTask("Second", "describtion", true, DateTime.Now),
            };
            Assert.True(first.SequenceEqual(second));
    }
        
        [Test]
        public void SortAlreadySortedList()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now),
                new SchoolTask("Second", "describtion", true, DateTime.Now),
                new SchoolTask("Third", "describtion", true, DateTime.Now),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now),
                new SchoolTask("Sixth", "describtion", true, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now),
                new SchoolTask("Second", "describtion", true, DateTime.Now),
                new SchoolTask("Third", "describtion", true, DateTime.Now),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now),
                new SchoolTask("Sixth", "describtion", true, DateTime.Now),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListWithFirstAndSecondElementAtFinalPositions()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First","describtion",true,DateTime.Now),
                new SchoolTask("Second","describtion",false,DateTime.Now),
                new SchoolTask("Third","describtion",false,DateTime.Now),
                new SchoolTask("Fourth","describtion",true,DateTime.Now),
                new SchoolTask("Fifth","describtion",true,DateTime.Now),
                new SchoolTask("Sixth","describtion",false,DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now),
                new SchoolTask("Second", "describtion", false, DateTime.Now),
                new SchoolTask("Third", "describtion", false, DateTime.Now),
                new SchoolTask("Sixth", "describtion", false, DateTime.Now),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTest1()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", false, DateTime.Now),
                new SchoolTask("Second", "describtion", true, DateTime.Now),
                new SchoolTask("Third", "describtion", true, DateTime.Now),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now),
                new SchoolTask("Sixth", "describtion", false, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("Second", "describtion", true, DateTime.Now),
                new SchoolTask("Third", "describtion", true, DateTime.Now),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now),
                new SchoolTask("First", "describtion", false, DateTime.Now),
                new SchoolTask("Sixth", "describtion", false, DateTime.Now),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTest2()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now),
                new SchoolTask("Second", "describtion", false, DateTime.Now),
                new SchoolTask("Third", "describtion", false, DateTime.Now),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now),
                new SchoolTask("Sixth", "describtion", false, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now),
                new SchoolTask("Second", "describtion", false, DateTime.Now),
                new SchoolTask("Third", "describtion", false, DateTime.Now),
                new SchoolTask("Sixth", "describtion", false, DateTime.Now),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTestWithDatetime()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", true, DateTime.Now.Add(TimeSpan.FromDays(2))),
                new SchoolTask("Second", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(3))),
                new SchoolTask("Third", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask("Fourth", "describtion", true, DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now.Add(TimeSpan.FromDays(5))),
                new SchoolTask("Sixth", "describtion", false, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("Fourth", "describtion", true, DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask("First", "describtion", true, DateTime.Now.Add(TimeSpan.FromDays(2))),
                new SchoolTask("Fifth", "describtion", true, DateTime.Now.Add(TimeSpan.FromDays(5))),
                new SchoolTask("Sixth", "describtion", false, DateTime.Now),
                new SchoolTask("Third", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask("Second", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(3))),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTestWithDatetime2()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("First", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(32)))),
                new SchoolTask("Second", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(31)))),
                new SchoolTask("Third", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(1))),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask("Third", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask("Second", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(31)))),
                new SchoolTask("First", "describtion", false, DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(32)))),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
    }
}