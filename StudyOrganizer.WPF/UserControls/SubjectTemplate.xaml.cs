using System.Windows.Controls;
using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.WPF.UserControls
{
    public partial class SubjectTemplate : UserControl
    {
        public Subject ThisSubject { get; set; }
        public SubjectTemplate(Subject thisSubject)
        {
            ThisSubject = thisSubject;
            DataContext = ThisSubject;
            InitializeComponent();
        }
    }
}