using FirstTimeWebAPI.DTO;
using FirstTimeWebAPI.Models;
using FirstTimeWebAPI.Views;

namespace FirstTimeWebAPI.Repositories
{
    public interface IuserRepo 
    {

        public List<UserView> getAllUser();
        public UserView getUserById(int id);
        Task saveUser(UserDTO userDto);
        public bool deleteUser(int id);
        public bool updateUser(UserDTO user);

        public List<Role> GetRoles();
        public Task<List<UserType>> GetAllUserTypes();

        public Task<User> Login(string UserName, string Password);

    }
}
