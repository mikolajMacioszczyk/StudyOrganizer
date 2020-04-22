using System.Windows;
using System.Windows.Controls;
using StudyOrganizer.WPF.UserControls;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Pages
{
    public partial class TaskPage : Page
    {
        public readonly MenuViewModel Model;
        public TaskPage(MenuViewModel model)
        {
            Model = model;
            DataContext = Model;
            InitializeComponent();
            
            ActualItemsControl.Model = new SingleTaskTemplateModel(Model.User.TaskList.Actual, Model.ColorMode, SchoolTaskListType.Actual);
            ActualItemsControl.DataContext = ActualItemsControl.Model;
            
            RealizedItemsControl.Model = new SingleTaskTemplateModel(Model.User.TaskList.Realized, Model.ColorMode, SchoolTaskListType.Realized);
            RealizedItemsControl.DataContext = RealizedItemsControl.Model;

            GoalsItemsControl.Model = new SingleTaskTemplateModel(Model.User.TaskList.Planned, Model.ColorMode, SchoolTaskListType.Goals);
            GoalsItemsControl.DataContext = GoalsItemsControl.Model;
        }

        private void TaskPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            Model.IsNewTaskPanelVisible = false;
        }
    }
}