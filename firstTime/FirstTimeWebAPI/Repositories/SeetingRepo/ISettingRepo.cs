using FirstTimeWebAPI.Models.Config;
using Org.BouncyCastle.Asn1.Mozilla;

namespace FirstTimeWebAPI.Repositories.SeetingRepo
{
    public interface ISettingRepo
    {
        List<Settings> GetSettings();
        void UpdateSettings(List<Settings> settings);
    }
}
