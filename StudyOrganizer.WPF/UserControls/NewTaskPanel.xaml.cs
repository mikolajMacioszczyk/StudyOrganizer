using System.Windows.Controls;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.UserControls
{
    public partial class NewTaskPanel : UserControl
    {
        private MenuViewModel Model;
        public NewTaskPanel(MenuViewModel model)
        {
            Model = model;
            DataContext = Model;
            InitializeComponent();
        }
    }
}