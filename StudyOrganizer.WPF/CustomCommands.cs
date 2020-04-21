using System.Windows.Input;

namespace StudyOrganizer.WPF
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand BackHomePage = new RoutedUICommand(
            "Back to Home Page",
            "BackHome",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Alt)
            }
        );
        
        public static readonly RoutedUICommand GoTaskPage = new RoutedUICommand(
            "Go to Task Page",
            "GoTask",
            typeof(CustomCommands)
            );
        
        public static readonly RoutedUICommand GoSubjectsPage = new RoutedUICommand(
            "Go to Subjects Page",
            "GoSubjects",
            typeof(CustomCommands)
        );
        
        public static readonly RoutedUICommand GoAccountPage = new RoutedUICommand(
            "Account settings",
            "GoAccount",
            typeof(CustomCommands)
        );
        
        public static readonly RoutedUICommand AddNewTask = new RoutedUICommand(
            "Add new Task to your list",
            "AddNewTask",
            typeof(CustomCommands)
        );
        
        public static readonly RoutedUICommand AddNewSubject = new RoutedUICommand(
            "Add new Subject to your list",
            "AddNewSubject",
            typeof(CustomCommands)
        );
}
}