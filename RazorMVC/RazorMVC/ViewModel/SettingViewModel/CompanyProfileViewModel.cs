namespace RazorMVC.ViewModel.SettingViewModel
{
    public class CompanyProfileViewModel:SettingViewModel
    {
        public CompanyProfileViewModel()
        {
            Settings = new Dictionary<string, string>
            {
                {"CompanyProfile_Name", string.Empty },
                {"CompanyProfile_Adress", string.Empty },
                {"CompanyProfile_PhoneNumber", string.Empty }

            };
        }
        
    }
}
