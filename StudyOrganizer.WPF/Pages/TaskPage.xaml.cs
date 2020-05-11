using System;
using System.Windows;
using System.Windows.Controls;
using StudyOrganizer.DLL.DataBase;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.UserControls;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Pages
{
    public partial class TaskPage : Page
    {
        private readonly MenuViewModel Model;
        private UpdatesTransmitter _transmitter;
        public TaskPage(MenuViewModel model)
        {
            Model = model;
            DataContext = Model;
            InitializeComponent();

            PrepareTaskTemplate(ActualItemsControl,SchoolTaskListType.Actual);
            PrepareTaskTemplate(RealizedItemsControl,SchoolTaskListType.Realized);
            PrepareTaskTemplate(GoalsItemsControl,SchoolTaskListType.Goals);
            _transmitter = new UpdatesTransmitter(new ConnectionToDb());
        }

        private void TaskPage_OnUnloaded(object sender, RoutedEventArgs e)
        {    
            Model.IsNewTaskPanelVisible = false;
        }

        private void PrepareTaskTemplate(TaskTemplate taskTemplate, SchoolTaskListType type)
        {
            switch (type)
            {
                case SchoolTaskListType.Actual:
                    ActualItemsControl.Model = new SingleTaskTemplateModel(Model.User.TaskList.Actual, Model.ColorMode, SchoolTaskListType.Actual);
                    break;
                case SchoolTaskListType.Realized:
                    RealizedItemsControl.Model = new SingleTaskTemplateModel(Model.User.TaskList.Realized, Model.ColorMode, SchoolTaskListType.Realized);
                    break;
                case SchoolTaskListType.Goals:
                    GoalsItemsControl.Model = new SingleTaskTemplateModel(Model.User.TaskList.Planned, Model.ColorMode, SchoolTaskListType.Goals);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            taskTemplate.OnDelete += SchoolTaskDeletedHandler;
            taskTemplate.OnMove += SchoolTaskMoveHandler;
            taskTemplate.OnRealize += SchoolTaskDoneEventHandler;
            taskTemplate.OnAwardChanged += listType => Model.User.TaskList.ListChanged(listType);
        }

        public void AddSchoolTask(SchoolTask toAdd)
        {
            switch (toAdd.TaskGroup)
            {
                case TaskGroup.Planned:
                    Model.User.TaskList.Planned.Add(toAdd);
                    Model.User.TaskList.ListChanged(SchoolTaskListType.Goals);
                    break;
                case TaskGroup.Actual:
                    Model.User.TaskList.Actual.Add(toAdd);
                    Model.User.TaskList.ListChanged(SchoolTaskListType.Actual);
                    break;
                case TaskGroup.Realized:
                    Model.User.TaskList.Realized.Add(toAdd);
                    Model.User.TaskList.ListChanged(SchoolTaskListType.Realized);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SchoolTaskDeletedHandler(SchoolTask task, SchoolTaskListType type)
        {
            _transmitter.DeleteTask(task);
            switch (type)
            {
                case SchoolTaskListType.Realized:
                    Model.User.TaskList.Realized.Remove(task);
                    break;
                case SchoolTaskListType.Actual:
                    Model.User.TaskList.Actual.Remove(task);
                    break;
                case SchoolTaskListType.Goals:
                    Model.User.TaskList.Planned.Remove(task);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        private void SchoolTaskMoveHandler(SchoolTask task, SchoolTaskListType type)
        {
            switch (type)
            {
                case SchoolTaskListType.Realized:
                    Model.User.TaskList.Realized.Remove(task);
                    Model.User.TaskList.Actual.Add(task);
                    Model.User.TaskList.ListChanged(SchoolTaskListType.Actual);
                    _transmitter.UpdateTaskGroup(task, TaskGroup.Actual);
                    break;
                case SchoolTaskListType.Actual:
                    Model.User.TaskList.Actual.Remove(task);
                    Model.User.TaskList.Planned.Add(task);
                    Model.User.TaskList.ListChanged(SchoolTaskListType.Goals);
                    _transmitter.UpdateTaskGroup(task, TaskGroup.Planned);
                    break;
                case SchoolTaskListType.Goals:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        private void SchoolTaskDoneEventHandler(SchoolTask task, SchoolTaskListType type)
        {
            switch (type)
            {
                case SchoolTaskListType.Realized:
                    break;
                case SchoolTaskListType.Actual:
                    Model.User.TaskList.Actual.Remove(task);
                    Model.User.TaskList.Realized.Add(task);
                    Model.User.TaskList.ListChanged(SchoolTaskListType.Realized);
                    _transmitter.UpdateTaskGroup( task, TaskGroup.Realized);
                    break;
                case SchoolTaskListType.Goals:
                    Model.User.TaskList.Planned.Remove(task);
                    Model.User.TaskList.Realized.Add(task);
                    Model.User.TaskList.ListChanged(SchoolTaskListType.Realized);
                    _transmitter.UpdateTaskGroup( task, TaskGroup.Realized);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}