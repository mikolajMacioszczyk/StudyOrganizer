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
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", true, TaskGroup.Actual, DateTime.Now),
            };
            ObservableCollection<SchoolTask> second = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", true,TaskGroup.Actual, DateTime.Now),
            };
            Assert.True(first.SequenceEqual(second));
    }
        
        [Test]
        public void SortAlreadySortedList()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", true, TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true, TaskGroup.Actual,DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", true, TaskGroup.Actual, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", true, TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true, TaskGroup.Actual,DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", true, TaskGroup.Actual, DateTime.Now),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListWithFirstAndSecondElementAtFinalPositions()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", false, TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true, TaskGroup.Actual,DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", false, TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", false,TaskGroup.Actual, DateTime.Now),

            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", true, TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true, TaskGroup.Actual,DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", false, TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", false,TaskGroup.Actual, DateTime.Now),
            };    
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTest1()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", false,TaskGroup.Actual, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"Second", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"First", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", false,TaskGroup.Actual, DateTime.Now),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTest2()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", false,TaskGroup.Actual, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fourth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Fifth", "describtion", true,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Second", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Sixth", "describtion", false,TaskGroup.Actual, DateTime.Now),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTestWithDatetime()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", true, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(2))),
                new SchoolTask(0,0,"Second", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(3))),
                new SchoolTask(0,0,"Third", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask(0,0,"Fourth", "describtion", true, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask(0,0,"Fifth", "describtion", true, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(5))),
                new SchoolTask(0,0,"Sixth", "describtion", false, TaskGroup.Actual, DateTime.Now),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"Fourth", "describtion", true, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask(0,0,"First", "describtion", true, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(2))),
                new SchoolTask(0,0,"Fifth", "describtion", true, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(5))),
                new SchoolTask(0,0,"Sixth", "describtion", false,TaskGroup.Actual, DateTime.Now),
                new SchoolTask(0,0,"Third", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask(0,0,"Second", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(3))),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
        
        [Test]
        public void SortListTestWithDatetime2()
        {
            ObservableCollection<SchoolTask> toSort = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"First", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(32)))),
                new SchoolTask(0,0,"Second", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(31)))),
                new SchoolTask(0,0,"Third", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(1))),
            }.Sort();
            ObservableCollection<SchoolTask> other = new ObservableCollection<SchoolTask>()
            {
                new SchoolTask(0,0,"Third", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(1))),
                new SchoolTask(0,0,"Second", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(31)))),
                new SchoolTask(0,0,"First", "describtion", false, TaskGroup.Actual,DateTime.Now.Add(TimeSpan.FromDays(2).Add(TimeSpan.FromDays(32)))),
            };
            Assert.True(toSort.SequenceEqual(other));
        }
    }
}