namespace FirstTimeWebAPI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        // Foreign Key to Role
        public int RoleId { get; set; }
        public int UserTypeId { get; set; }
    }
}
