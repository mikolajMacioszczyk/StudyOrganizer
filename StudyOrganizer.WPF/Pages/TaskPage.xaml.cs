using System.Windows;
using System.Windows.Controls;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Pages
{
    public partial class TaskPage : Page
    {
        public readonly MenuViewModel Model;
        public TaskPage(MenuViewModel model)
        {
            Model = model;
            InitializeComponent();
            ActualItemsControl.DataContext = Model.User.TaskList.Actual;
            ActualItemsControl.ThisSchoolTaskList = Model.User.TaskList;
            ActualItemsControl.AddColor(Model.ColorMode.Selected);
            
            RealizedItemsControl.DataContext = Model.User.TaskList.Realized;
            RealizedItemsControl.ThisSchoolTaskList = Model.User.TaskList;
            RealizedItemsControl.AddColor(Model.ColorMode.Selected);
            
            GoalsItemsControl.DataContext = Model.User.TaskList.Planned;
            GoalsItemsControl.ThisSchoolTaskList = Model.User.TaskList;
            GoalsItemsControl.AddColor(Model.ColorMode.Selected);
        }

        private void TaskPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            Model.IsNewTaskPanelVisible = false;
        }
    }
}