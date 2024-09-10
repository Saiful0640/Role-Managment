using Newtonsoft.Json;
using RazorMVC.Models;

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
            var response = await _httpClient.PostAsJsonAsync("api/user", user);

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
            var response = await _httpClient.PutAsJsonAsync($"api/User/{id}", user);
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

        public async Task<List<User>> GetAllUser()
        {
            var userList = await _httpClient.GetAsync("api/user");
            if (userList == null)
            {
                var errorMessage = await userList.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            else
            {
                var userList1 = await userList.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(userList1);
            }
        }

      public async Task<User> getuserById(int id)
       {
            var response = await _httpClient.GetAsync($"api/user/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error retrieving user: {response.StatusCode} - {errorMessage}");
            }

        }

    }
}
