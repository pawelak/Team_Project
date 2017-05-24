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
            _client.Timeout = TimeSpan.FromSeconds(3);
        }

        private readonly HttpClient _client = new HttpClient();
        public static SynchronizationService Instance => _instance ?? (_instance = new SynchronizationService());

        public async Task SendUser(UserDto user)
        {
            var userRest = new UserRest
            {
                Email = user.Name,
                Token = user.Token
            };
            var userJson = JsonConvert.SerializeObject(userRest);
            var contentUser = new StringContent(userJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/users");
            var response = await _client.PostAsync(uri,contentUser);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            user.SyncStatus = SyncStatus.Uploaded;
            await UserService.Instance.SaveUser(user);
        }

        public async Task SendActivity(ActivitiesDto activity,TasksDto task)
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
                EditState = EditState.EditedOnMobile,
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
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            activity.SyncStatus = SyncStatus.Uploaded;
            await UserService.Instance.SaveActivity(activity);
        }

        public async Task SendFavorite()
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
                    Type = task.Typ
                };
                favoriteTasks.Add(item);
            }
            var favoriteRest = new FavoritesRest
            {
                UserEmail = user.Name,
                Token = user.Token,
                TasksList = favoriteTasks
            };
            var favoriteJson = JsonConvert.SerializeObject(favoriteRest);
            var contentFavorite = new StringContent(favoriteJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/favorites");
            var response = await _client.PostAsync(uri, contentFavorite);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            foreach (var fav in userFavorites)
            {
                fav.SyncStatus = SyncStatus.Uploaded;
                await UserService.Instance.SaveFavorite(fav);
            }
        }

        public async Task<bool> SendPlanned(ActivitiesDto activity,TasksDto task)
        {
            var taskJson = JsonConvert.SerializeObject(activity);
            var contentTask = new StringContent(taskJson, Encoding.UTF8, "application/json");
            var uri = new Uri("http://localhost:8000/api/planned");
            var response = await _client.PostAsync(uri, contentTask);
            return response.IsSuccessStatusCode;
        }

        public async Task GetActivities()
        {
            var user = UserService.Instance.GetLoggedUser();
            var uri = new Uri($"http://localhost:8000/api/activities/{user.Name}");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            var activities = JsonConvert.DeserializeObject <List<ActivitiesRest>>(content);
            
            foreach (var activity in activities)
            {
                var find = false;
                var guids = await UserService.Instance.GetActivitiesByStatus(StatusType.Stop);
                foreach (var item in guids)
                {
                    if (item.Guid != activity.Guid)
                    {
                        continue;
                    }
                    find = true;
                    if (activity.EditState == EditState.EditedOnWeb)
                    {
                        item.Comment = activity.Comment;
                        item.GroupId = int.Parse(activity.GroupName);
                        item.SyncStatus = SyncStatus.Received;
                        var taskDto = new TasksDto
                        {
                            Name = activity.TaskName
                        };
                        var task = await UserService.Instance.GetTask(taskDto);
                        if (task == null)
                        {
                            taskDto.TaskId = await UserService.Instance.SaveTask(taskDto);
                        }
                        else
                        {
                            taskDto.TaskId = task.TaskId;
                        }
                        item.TaskId = taskDto.TaskId;
                        item.ActivityId = await UserService.Instance.SaveActivity(item);
                        var parts = await UserService.Instance.GetPartsOfActivityByActivityId(item.ActivityId);
                        if (activity.TaskPartsList.Count == parts.Count)
                        {
                            var i = 0;
                            foreach (var part in parts)
                            {
                                part.Start = activity.TaskPartsList[i].Start;
                                part.Stop = activity.TaskPartsList[i].Stop;
                                part.Duration = activity.TaskPartsList[i].Duration;
                                await UserService.Instance.SavePartOfActivity(part);
                                i++;
                            }
                        }
                        else if (activity.TaskPartsList.Count < parts.Count)
                        {
                            var i = 0;
                            foreach (var part in parts)
                            {
                                if (i >= activity.TaskPartsList.Count)
                                {
                                    await UserService.Instance.DeletePartOfActivity(part);
                                }
                                else
                                {
                                    part.Start = activity.TaskPartsList[i].Start;
                                    part.Stop = activity.TaskPartsList[i].Stop;
                                    part.Duration = activity.TaskPartsList[i].Duration;
                                    await UserService.Instance.SavePartOfActivity(part);
                                }
                                i++;
                            }
                        }
                        else if (activity.TaskPartsList.Count > parts.Count)
                        {
                            var i = 0;
                            foreach (var part in parts)
                            {
                                if (i >= parts.Count)
                                {
                                    var newPart = new PartsOfActivityDto
                                    {
                                        ActivityId = item.ActivityId,
                                        Start = activity.TaskPartsList[i].Start,
                                        Stop = activity.TaskPartsList[i].Stop,
                                        Duration = activity.TaskPartsList[i].Duration
                                    };
                                    await UserService.Instance.SavePartOfActivity(newPart);
                                }
                                else
                                {
                                    part.Start = activity.TaskPartsList[i].Start;
                                    part.Stop = activity.TaskPartsList[i].Stop;
                                    part.Duration = activity.TaskPartsList[i].Duration;
                                    await UserService.Instance.SavePartOfActivity(part);
                                }
                                i++;
                            }
                        }
                    }
                    break;
                }
                if (find)
                {
                    continue;
                }
                var taskDto2 = new TasksDto
                {
                    Name = activity.TaskName
                };
                var task2 = await UserService.Instance.GetTask(taskDto2);
                if (task2 == null)
                {
                    taskDto2.TaskId = await UserService.Instance.SaveTask(taskDto2);
                }
                else
                {
                    taskDto2.TaskId = task2.TaskId;
                }
                var activityDto = new ActivitiesDto
                {
                    Guid = activity.Guid,
                    Comment = activity.Comment,
                    Status = activity.State,
                    GroupId = int.Parse(activity.GroupName),
                    SyncStatus = SyncStatus.Received,
                    UserId =  user.UserId,
                    TaskId = taskDto2.TaskId
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
                if (favorite.EditState == EditState.Delete)
                {
                    
                }
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
                        var checkFav = await UserService.Instance.GetFavoriteByTaskId(taskDto.TaskId);
                        if (checkFav != null)
                        {
                            continue;
                        }
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
