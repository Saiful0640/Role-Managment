using FirstTimeWebAPI.ConfigModel;
using FirstTimeWebAPI.Models;
using FirstTimeWebAPI.Repositories;

namespace FirstTimeWebAPI.Services
{
    public class UserService : IuserRepo
    {

        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<User> getAllUser()
        {
            try
            {
                List<User> users = _context.Users.ToList();
                return users;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            
        }

        public User getUserById(int id)
        {
            try
            {
                User user = _context.Users.SingleOrDefault(x => x.Id == id);
                return user;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }



        public bool updateUser(User user)
        {
            try
            {
                var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                if (existingUser == null)
                {
                    return false;
                }

                // Update the properties
                existingUser.Name = user.Name;
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.PhoneNumber = user.PhoneNumber;

                _context.Users.Update(existingUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Failed to update user", ex);
            }
        }


        public bool deleteUser(int id)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Failed to delete user", ex);
            }
        }


        public async Task saveUser( User user)
        {
            try
            {
                 await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
