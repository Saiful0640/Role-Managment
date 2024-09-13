namespace FirstTimeWebAPI.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        // Navigation Property for related users (virtual enables lazy loading)
        public virtual ICollection<User> Users { get; set; }
    }
}
