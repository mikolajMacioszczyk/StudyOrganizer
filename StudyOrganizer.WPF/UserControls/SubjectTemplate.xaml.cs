using System.Windows;
using System.Windows.Controls;
using StudyOrganizer.DLL.Models;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.UserControls
{
    public partial class SubjectTemplate : UserControl
    {
        public SubjectTemplateModel Model { get; set; }
        public SubjectTemplate(SubjectTemplateModel model)
        {
            Model = model;
            DataContext = model;
            InitializeComponent();
        }

        public delegate void SubjectDeleted(Subject subject);
        public event SubjectDeleted OnDelete;
        
        public delegate void SubjectEditing(Subject subject);
        public event SubjectEditing OnEdite;

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            OnDelete?.Invoke(Model.ThisSubject);
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            OnEdite?.Invoke(Model.ThisSubject);
        }
    }
}