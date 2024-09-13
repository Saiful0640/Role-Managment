namespace FirstTimeWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        // Foreign Key to Role
        public int RoleId { get; set; }  // Foreign key to Role table
        public int UserTypeId { get; set; }

        // Navigation Property
        public Role Role { get; set; }
        public UserType UserType { get; set; }

    }
}
