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
    }
}