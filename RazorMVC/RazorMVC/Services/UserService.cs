using Newtonsoft.Json;
using RazorMVC.Models;
using RazorMVC.ViewModel;

namespace RazorMVC.Services
{
    public class UserService
    {

        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<User> AddUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/adduser", user);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode} - {errorMessage}");
            }
        }

        public async Task<bool> UpdateUser( int id,User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/User/updateuser/{id}", user);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode} - {errorMessage}");
            }
        }

        public async Task<List<UserView>> GetAllUser()
        {
            var userList = await _httpClient.GetAsync("api/User/alluser");
            if (userList == null)
            {
                var errorMessage = await userList.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            else
            {
                var userList1 = await userList.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserView>>(userList1);
            }
        }

        

      public async Task<UserView> getuserById(int id)
       {
            var response = await _httpClient.GetAsync($"api/User/getuserbyid/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserView>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error retrieving user: {response.StatusCode} - {errorMessage}");
            }

       }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/User/deleteuser/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Role>> GetAllRole()
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/user/getallrole");
                if (response != null)
                {
                    return await response.Content.ReadFromJsonAsync<List<Role>>();
              
                }
                else
                {
                    return new List<Role>();
                }
            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message);   
            }
        }

        public async Task<List<UserType>> GetAllUserType()
        {
            try
            {
                var reponse = await _httpClient.GetAsync($"api/user/getallUserType");
                if(reponse != null)
                {

                    return await reponse.Content.ReadFromJsonAsync<List<UserType>>();
                }
                else
                {
                    return new List<UserType>();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

    }
}
