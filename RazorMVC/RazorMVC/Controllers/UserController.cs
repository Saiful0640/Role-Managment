using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RazorMVC.Models;
using RazorMVC.Services;

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
                return View(new List<User>());  // Return an empty list or handle it in another way
            }
        }

        [HttpGet]
        public async Task<IActionResult> SaveUser()
        {
            // Return an empty user model to the view
            var users = await _userService.GetAllUser();

            if (users != null && users.Any())
            {
                var firstUser = users.First();
                return View(firstUser); // Pass the first user to the view
            }

            return View(new User()); // Return an empty user if the list is empty
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
            if (userById == null) { 
                return NotFound();
            }
            else
            {
                return View("EditUSer", userById);
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

    }
}