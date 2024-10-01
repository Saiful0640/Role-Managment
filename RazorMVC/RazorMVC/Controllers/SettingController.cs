using Microsoft.AspNetCore.Mvc;
using RazorMVC.Models.Settings;
using RazorMVC.Services;
using RazorMVC.ViewModel.SettingViewModel;

namespace RazorMVC.Controllers
{
    public class SettingController : Controller
    {

        private readonly SettingService _settingService;
        public SettingController(SettingService settingService)
        {
            _settingService = settingService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSetting(string category)
        {
            List<Settings> settings;

            try
            {
                settings = await _settingService.GetSettingsByCategoryAsync(category) ?? new List<Settings>();

                if (!settings.Any())
                {
                    return View(GetViewName(category), CreateEmptyViewModel(category)); // Return an empty model if no settings
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return View(GetViewName(category), CreateEmptyViewModel(category)); // Return an empty model
            }

            var model = CreateViewModel(category);

            foreach (var setting in settings)
            {
                model.Settings[setting.Setting_Key] = setting.Setting_Value;
            }

            return View(GetViewName(category), model);
        }

        // POST: /Settings/SaveSettings
        [HttpPost]
        public async Task<IActionResult> SaveSettings(SettingViewModel model, string category)
        {
            foreach (var setting in model.Settings)
            {
                await _settingService.SaveOrUpdateSettingAsync(setting.Key, setting.Value);
            }

          //  return RedirectToAction("Index", new { category = category });
            return RedirectToAction("GetAllSetting", new { category = category });
        }


        //// Helper method to dynamically select view model based on category
        private SettingViewModel CreateViewModel(string category)
        {
            var model = category switch
            {
                "CompanyProfile" => new CompanyProfileViewModel(),
                // "Email" => new EmailSettingModelView(),

                _ => new SettingViewModel() // Default case if category doesn't match
            };

            return model;
        }

        // Helper method to create an empty view model
        private SettingViewModel CreateEmptyViewModel(string category)
        {
            return CreateViewModel(category); // Can be customized if needed
        }

        // Helper method to dynamically select view name based on category
        private string GetViewName(string category)
        {
            return category switch
            {
                "CompanyProfile" => "CompanyProfileSetting",// this part is the view name
                "Email" => "EmailSetting",

                _ => "Settings" // Default view name if category doesn't match
            };

        }
    }
}