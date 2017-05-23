using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskMaster.ModelsDto;
using TaskMaster.ModelsRest;

namespace TaskMaster.Services
{
    public class SynchronizationService
    {
        private static SynchronizationService _instance;

        private SynchronizationService()
        {
            _client.MaxResponseContentBufferSize = 256000;
        }

        private readonly HttpClient _client = new HttpClient();
        public static SynchronizationService Instance => _instance ?? (_instance = new SynchronizationService());

        public async Task<bool> SendUser(UserDto user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var contentUser = new StringContent(userJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/users");
            var response = await _client.PostAsync(uri,contentUser);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendActivity(ActivitiesDto activity)
        {
            var activityJson = JsonConvert.SerializeObject(activity);
            var contentActivity = new StringContent(activityJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/activities");
            var response = await _client.PostAsync(uri, contentActivity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendFavorite(FavoritesDto favorite)
        {
            var favoriteJson = JsonConvert.SerializeObject(favorite);
            var contentFavorite = new StringContent(favoriteJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/favorites");
            var response = await _client.PostAsync(uri, contentFavorite);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendPlanned(ActivitiesDto activity)
        {
            var taskJson = JsonConvert.SerializeObject(activity);
            var contentTask = new StringContent(taskJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/planned");
            var response = await _client.PostAsync(uri, contentTask);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ActivitiesDto>> GetActivities()
        {
            var uri = new Uri("http://localhost:8000/api/activities");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var activities = JsonConvert.DeserializeObject <List<ActivitiesRest>>(content);
            return null;
        }

        public async Task<List<FavoritesDto>> GetFavorites()
        {
            var uri = new Uri("http://localhost:8000/api/favorites");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var favorites = JsonConvert.DeserializeObject<List<FavoritesRest>>(content);
            return null;
        }

        public async Task<TasksDto> GetPlanned()
        {
            var uri = new Uri("http://localhost:8000/api/planned");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<TasksDto>(content);
            return null;
        }
        
    }
}
