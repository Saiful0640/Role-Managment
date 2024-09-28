using FirstTimeWebAPI.ConfigModel;
using FirstTimeWebAPI.DTO;
using FirstTimeWebAPI.Models;
using FirstTimeWebAPI.Repositories;
using FirstTimeWebAPI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstTimeWebAPI.Services
{
    public class UserService : IuserRepo
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
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


        public async Task<AuthResponse> Login(string UserName, string Password)
        {


            try
            {
                User user1 = await _context.Users.SingleOrDefaultAsync(u => u.UserName == UserName && u.Password == Password);
                Role role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == user1.RoleId);
                UserType userType = await _context.UserTypes.SingleOrDefaultAsync(t => t.Id == user1.UserTypeId);

                UserView userView = new UserView
                {
                    Id = user1.Id,
                    UserName = user1.UserName,
                    Password = user1.Password,
                    Name = user1.UserName,
                    PhoneNumber = user1.PhoneNumber,
                    Email = user1.Email,
                    RoleId = user1.RoleId,
                    RoleName = role.RoleName,
                    UserTypeId = user1.UserTypeId,
                    UserTypeName = userType.TypeName
                };

                // Generate JWT token
                string token = GenerateJwtToken(userView);

                return new AuthResponse
                {
                    Token = token,
                    UserDetails = userView
                };

            }
            catch (Exception ex) { 

                throw new Exception(ex.Message, ex);
            }
           
        }
        private string GenerateJwtToken(UserView user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.RoleName)
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }



    
}
