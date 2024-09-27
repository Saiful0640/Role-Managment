using FirstTimeWebAPI.Views;

namespace FirstTimeWebAPI.Models
{
    public class AuthResponse
    {

        public string Token { get; set; }
        public UserView UserDetails { get; set; }
    }
}
