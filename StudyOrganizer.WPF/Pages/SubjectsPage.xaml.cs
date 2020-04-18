using System.Windows.Controls;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Pages
{
    public partial class SubjectsPage : Page
    {
        public readonly MenuViewModel Model;
        public SubjectsPage(MenuViewModel model)
        {
            Model = model;
            DataContext = Model;
            InitializeComponent();
        }
    }
}