using StudyOrganizer.DLL.Models;

namespace StudyOrganizer.WPF.ViewModels
{
    public class MenuViewModel
    {
        public MenuViewModel(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}