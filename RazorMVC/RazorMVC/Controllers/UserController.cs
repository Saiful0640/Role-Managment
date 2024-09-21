using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RazorMVC.Models;
using RazorMVC.Services;
using RazorMVC.ViewModel;

namespace RazorMVC.Controllers
{
    public class UserController : Controller
    {


        private readonly UserService _userService;

        public UserController(UserService userService)
        {

            _userService = userService;

        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            // Return the Login view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await _userService.LoginService(userName, password);

            if (user != null)
            {
                // Handle successful login (e.g., redirect to a different page or show user details)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Handle login failure (e.g., show error message)
                ModelState.AddModelError("", "Login failed.");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userlist = await _userService.GetAllUser();
                return View(userlist);
            }
            catch (Exception ex)
            {
                // Log the error (ex)
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the user list.");
                return View(new List<UserView>());  // Return an empty list or handle it in another way
            }
        }
        

        [HttpGet]
        public async Task<IActionResult> SaveUser()
        {
            // Return an empty user model to the view
            var role = await _userService.GetAllRole();
            var userType = await _userService.GetAllUserType();

            UserViewModel userViewModel = new UserViewModel
            {
                User = new User(),
                Roles = role,
                UserTypes = userType,
            };
           
            return View(userViewModel); // Return an empty user if the list is empty
        }


        [HttpPost]
        public async Task<IActionResult> SaveUser([FromForm] User user)
        {
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User data is missing.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.AddUser(user);
                    return RedirectToAction("GetAll");
                }
                catch (Exception ex)
                {
                    // Add logging here if necessary
                    ModelState.AddModelError(string.Empty, $"Error saving user: {ex.Message}");
                }
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditUSer(int id)
        {
            var userById = await _userService.getuserById(id);
            var role = await _userService.GetAllRole();
            var userType = await _userService.GetAllUserType();
            EditUserView editUserView = new EditUserView
            {
                UserView = userById,
                Roles = role,
                UserTypes = userType,
            };

            if (userById == null) { 
                return NotFound();
            }
            else
            {
                return View("EditUSer", editUserView);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUSer(int id, User user)
        {
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User data is missing.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateUser(user.Id, user);
                    return RedirectToAction("GetAll");
                }
                catch (Exception ex)
                {
                    // Add logging here if necessary
                    ModelState.AddModelError(string.Empty, $"Error updating user: {ex.Message}");
                }

            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);

                if (result)
                {
                    TempData["SuccessMessage"] = "User deleted successfully!";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    //return NotFound();
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return View("GetAll");
            }

        }

        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.getuserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user); // Return the user object to the view
            }
            catch (Exception ex)
            {
                // Handle errors
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}