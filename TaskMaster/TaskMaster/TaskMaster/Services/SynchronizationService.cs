using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskMaster.Enums;
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
            var userRest = new UserRest
            {
                Email = user.Name,
                Token = user.Token,
                Description = user.Description,
            };
            var userJson = JsonConvert.SerializeObject(userRest);
            var contentUser = new StringContent(userJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/users");
            var response = await _client.PostAsync(uri,contentUser);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendActivity(ActivitiesDto activity,TasksDto task)
        {
            var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
            var list = parts.Select(partsOfActivityDto => new PartsOfActivityRest
                {
                    Start = partsOfActivityDto.Start,
                    Stop = partsOfActivityDto.Duration,
                    Duration = partsOfActivityDto.Duration
                })
                .ToList();
            var user = UserService.Instance.GetLoggedUser();
            var activityRest = new ActivitiesRest
            {
                Comment = activity.Comment,
                EditState = activity.SyncStatus,
                TaskName = task.Name,
                UserEmail = user.Name,
                Token = user.Token,
                State = activity.Status,
                GroupName = activity.GroupId.ToString(),
                TaskPartsList = list
            };
            var activityJson = JsonConvert.SerializeObject(activityRest);
            var contentActivity = new StringContent(activityJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/activities");
            var response = await _client.PostAsync(uri, contentActivity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendFavorite()
        {
            var user = UserService.Instance.GetLoggedUser();
            var userFavorites = await UserService.Instance.GetUserFavorites(user.UserId);
            var favoriteTasks = new List<TasksRest>();
            foreach (var favorite in userFavorites)
            {
                var task = await UserService.Instance.GetTaskById(favorite.TaskId);
                var item = new TasksRest
                {
                    Name = task.Name,
                    Type = task.Typ,
                };
                favoriteTasks.Add(item);
            }
            var favoriteRest = new FavoritesRest
            {
                UserEmail = user.Name,
                Token = user.Token,
                TasksList = favoriteTasks,
            };
            var favoriteJson = JsonConvert.SerializeObject(favoriteRest);
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

        public async Task GetActivities()
        {
            var uri = new Uri("http://localhost:8000/api/activities");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            var activities = JsonConvert.DeserializeObject <List<ActivitiesRest>>(content);
            foreach (var activity in activities)
            {
                var user = await UserService.Instance.GetUserByEmail(activity.UserEmail);
                var taskDto = new TasksDto
                {
                    Name = activity.TaskName
                };
                var task = await UserService.Instance.GetTask(taskDto);
                var activityDto = new ActivitiesDto
                {
                    Comment = activity.Comment,
                    Status = activity.State,
                    GroupId = int.Parse(activity.GroupName),
                    SyncStatus = EditState.EditedOnWeb,
                    UserId =  user.UserId,
                    TaskId = task.TaskId
                };
                activityDto.ActivityId = await UserService.Instance.SaveActivity(activityDto);
                foreach (var parts in activity.TaskPartsList)
                {
                    var part = new PartsOfActivityDto
                    {
                        ActivityId = activityDto.ActivityId,
                        Start = parts.Start,
                        Stop = parts.Stop,
                        Duration = parts.Duration,
                        SyncStatus = EditState.EditedOnWeb
                    };
                    await UserService.Instance.SavePartOfActivity(part);
                }
            }
        }

        public async Task GetFavorites()
        {
            var uri = new Uri("http://localhost:8000/api/favorites");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            var favorites = JsonConvert.DeserializeObject<List<FavoritesRest>>(content);
            foreach (var favorite in favorites)
            {
                var user = await UserService.Instance.GetUserByEmail(favorite.UserEmail);
                foreach (var task in favorite.TasksList)
                {
                    var taskDto = new TasksDto
                    {
                        Name = task.Name,
                        Typ = task.Type
                    };
                    var newTask = await UserService.Instance.GetTask(taskDto);
                    if (newTask == null)
                    {
                        taskDto.TaskId = await UserService.Instance.SaveTask(taskDto);
                    }
                    else
                    {
                        taskDto.TaskId = newTask.TaskId;
                    }
                    var favoriteDto = new FavoritesDto
                    {
                        TaskId = taskDto.TaskId,
                        UserId = user.UserId
                    };
                    await UserService.Instance.SaveFavorite(favoriteDto);
                }
            }
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
