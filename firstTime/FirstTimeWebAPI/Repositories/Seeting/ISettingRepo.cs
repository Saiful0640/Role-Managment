

using FirstTimeWebAPI.Models.Settings;

namespace FirstTimeWebAPI.Repositories.SeetingRepo
{
    public interface ISettingRepo
    {
        Task<List<Settings>> GetAllSettingAsync(string category);

        Task SaveAndUpdateGetAllSettingAsync(string key, string value);

    }
}
