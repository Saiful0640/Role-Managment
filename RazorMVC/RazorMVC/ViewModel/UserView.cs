using RazorMVC.Models;

namespace RazorMVC.ViewModel
{
    public class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        // Foreign Key to Role
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
       
    }
}
