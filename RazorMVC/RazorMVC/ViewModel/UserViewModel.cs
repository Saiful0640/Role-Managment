using RazorMVC.Models;

namespace RazorMVC.ViewModel
{
    public class UserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<UserType> UserTypes { get; set; }
    }
}
