using FirstTimeWebAPI.ConfigModel;
using FirstTimeWebAPI.Models.Config;
using FirstTimeWebAPI.Repositories.SeetingRepo;

namespace FirstTimeWebAPI.Services.SettingServices
{
    public class SettingService : ISettingRepo
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context) {

            _context = context;
        }

        public  List<Settings> GetSettings()
        {

            try
            {
                List<Settings> settings = _context.Settings.ToList();

                return settings;
                
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public void UpdateSettings(List<Settings> settings)
        {
            
           foreach(var setting in settings)
            {
               var existingSetting = _context.Settings.FirstOrDefault(s=> s.Setting_Key == setting.Setting_Key);
                if (existingSetting != null)
                {
                    existingSetting.Setting_Value = setting.Setting_Value;
                }
                else { 
                
                 _context.Settings.Add(setting);
                }
            }
        }
    }
}
