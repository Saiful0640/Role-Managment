
using FirstTimeWebAPI.Models.Settings;
using FirstTimeWebAPI.Services.SettingServices;

using Microsoft.AspNetCore.Mvc;

namespace FirstTimeWebAPI.Controllers.Setting
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly SettingService _settingService;
        public SettingController(SettingService settingService)
        {
            _settingService = settingService;
        }


        [HttpGet ("/category/{category}")]
        public async Task <ActionResult<IEnumerable<Settings>>> GetAllSettings(string category)
        {
            var setting = await _settingService.GetAllSettingAsync(category);

            return Ok(setting);

        }

        [HttpPost("/SettingSave")]
        public async Task<IActionResult> SaveOrUpdateSetting( [FromBody] Dictionary<string, string> updateSetting)
        {

            if(updateSetting == null || !updateSetting.Any())
            {
                return BadRequest("Setting data is requierd");
            }

            foreach(var setting in updateSetting)
            {
                await _settingService.SaveAndUpdateGetAllSettingAsync(setting.Key, setting.Value);
            }
            return NoContent();
        }



    }
}
