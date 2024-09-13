using RazorMVC.Models;

namespace RazorMVC.ViewModel
{
    public class EditUserView
    {
        public UserView UserView { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<UserType> UserTypes { get; set; }
    }
}
