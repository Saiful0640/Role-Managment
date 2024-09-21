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
                
            }
            throw new NotImplementedException();
        }

        public void UpdateSettings(List<Settings> settings)
        {
            throw new NotImplementedException();
        }
    }
}
