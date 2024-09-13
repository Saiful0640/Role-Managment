namespace RazorMVC.Models
{
    public class UserType
    {
       
        public int Id { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
