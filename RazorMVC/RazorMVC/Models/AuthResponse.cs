using RazorMVC.ViewModel;

namespace RazorMVC.Models
{
    public class AuthResponse
    {

        public string Token { get; set; }
        public UserView UserDetails { get; set; }
    }
}
