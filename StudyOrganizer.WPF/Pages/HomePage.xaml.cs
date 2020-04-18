using System.Windows.Controls;
using StudyOrganizer.WPF.ViewModels;

namespace StudyOrganizer.WPF.Pages
{
    public partial class HomePage : Page
    {
        private readonly MenuViewModel _model;
        public HomePage(MenuViewModel model)
        {
            _model = model;
            DataContext = _model;
            InitializeComponent();
        }
    }
}