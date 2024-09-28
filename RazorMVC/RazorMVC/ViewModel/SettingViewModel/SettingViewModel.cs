namespace RazorMVC.ViewModel.SettingViewModel
{
    public class SettingViewModel
    {
        public Dictionary<string, string> Settings { get; set; }

        public SettingViewModel() {

            Settings = new Dictionary<string, string>();
        }
    }
}
