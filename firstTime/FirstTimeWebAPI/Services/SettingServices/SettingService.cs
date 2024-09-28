using FirstTimeWebAPI.ConfigModel;
using FirstTimeWebAPI.Models.Config;
using FirstTimeWebAPI.Repositories.SeetingRepo;
using Microsoft.EntityFrameworkCore;

namespace FirstTimeWebAPI.Services.SettingServices
{
    public class SettingService : ISettingRepo
    {
        private readonly AppDbContext _appDbContext;

        public SettingService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Settings>> GetAllSettingAsync(string category)
        {
            try
            {
                

                return await _appDbContext.Settings
                .Where(s => s.Setting_Key.StartsWith(category))
                .ToListAsync();
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task SaveAndUpdateGetAllSettingAsync(string key, string value)
        {
            try
            {
                
                var setting = await _appDbContext.Settings.FirstOrDefaultAsync(s => s.Setting_Key == key);


                if (setting == null)
                {
                    setting = new Settings
                    {
                        Setting_Key = key,
                        Setting_Value = value

                    };

                    await _appDbContext.AddAsync(setting);
                }
                else
                {
                    setting.Setting_Value = value;
                    
                }
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }
    }
}
