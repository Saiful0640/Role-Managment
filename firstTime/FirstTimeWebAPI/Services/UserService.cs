using FirstTimeWebAPI.ConfigModel;
using FirstTimeWebAPI.DTO;
using FirstTimeWebAPI.Models;
using FirstTimeWebAPI.Repositories;
using FirstTimeWebAPI.Views;
using Microsoft.EntityFrameworkCore;

namespace FirstTimeWebAPI.Services
{
    public class UserService : IuserRepo
    {

        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<UserView> getAllUser()
        {
            try
            {

                List<User> users = _context.Users.ToList();
                List<UserView> listUser = users.Select(user => 
                    new UserView
                    { 
                        Id = user.Id,
                        Name = user.Name,
                        UserName = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Password = user.Password,
                        RoleId = user.RoleId,
                        RoleName = _context.Roles.SingleOrDefault(r => r.Id == user.RoleId).RoleName,
                        UserTypeId = user.UserTypeId,
                        UserTypeName = _context.UserTypes.SingleOrDefault(ut => ut.Id == user.UserTypeId).TypeName,
                    }).ToList();



                return listUser;


            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            
        }

        public UserView getUserById(int id)
        {
            try
            {
                User user = _context.Users.SingleOrDefault(x => x.Id == id);

                UserView userView = new UserView
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    RoleId = user.RoleId,
                    RoleName = _context.Roles.SingleOrDefault(r => r.Id == user.RoleId).RoleName,
                    UserTypeId = user.UserTypeId,
                    UserTypeName = _context.UserTypes.SingleOrDefault(ut => ut.Id == user.UserTypeId).TypeName
                };

                return userView;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool updateUser(UserDTO user)
        {
            try
            {
                var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                if (existingUser == null)
                {
                    throw new ArgumentException("Invalid role.");
                }

                // Update the properties
                existingUser.Name = user.Name;
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.RoleId = user.RoleId;
                existingUser.UserTypeId = user.UserTypeId;

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


        public async Task saveUser( UserDTO userDto)
        {
            try
            {

                Role role = _context.Roles.SingleOrDefault(r => r.Id == userDto.RoleId);
                if (role == null) {
                    throw new Exception("Not found role");
                }

                UserType userType = _context.UserTypes.SingleOrDefault(ut => ut.Id == userDto.UserTypeId);
                if (userType == null) { 
                    throw new Exception("Not found UserType ");
                }

                var existingUser =await _context.Users.AnyAsync(u => u.UserName == userDto.UserName || u.Email == userDto.Email);

                if (existingUser)
                {
                    // Handle the case where the user already exists
                    throw new InvalidOperationException("A user with the same username or email already exists.");
                }


                User user = new User
                {
                    Id = userDto.Id,
                    Name = userDto.Name,
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    PhoneNumber = userDto.PhoneNumber,
                    Role = role,
                    RoleId = userDto.RoleId,
                    UserType = userType,
                    UserTypeId = userDto.UserTypeId,

                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public Task Login(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        public  List<Role> GetRoles()
        {
            try
            {
                List<Role> roles = _context.Roles.ToList();

                return roles;

            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<UserType>> GetAllUserTypes()
        {
            try
            {
                List<UserType> userTypesList = await _context.UserTypes.ToListAsync();

                if (userTypesList.Count > 0)
                {
                    return userTypesList;
                }
                else {

                    return [];
                }
                    
            }
            catch (Exception ex) { 
            
                throw new Exception(ex.Message);
            }
            
        }
    }


    
}
