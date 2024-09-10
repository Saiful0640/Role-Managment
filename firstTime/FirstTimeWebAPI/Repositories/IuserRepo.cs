using FirstTimeWebAPI.Models;

namespace FirstTimeWebAPI.Repositories
{
    public interface IuserRepo 
    {

        public List<User> getAllUser();
        public User getUserById(int id);
        Task saveUser(User user);
        public bool deleteUser(int id);
        public bool updateUser(User user);
         
    }
}
