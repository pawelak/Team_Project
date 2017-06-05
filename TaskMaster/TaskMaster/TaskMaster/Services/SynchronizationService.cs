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
        private const string Ip = "http://192.168.1.14:65116/api"; 
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
            var uri = new Uri($"{Ip}/users");
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
            var uri = new Uri($"{Ip}/activities");
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
            var favoriteTasks = new List<FavoritesRest>();
            foreach (var favorite in userFavorites)
            {
                var task = await UserService.Instance.GetTaskById(favorite.TaskId);
                var taskRest = new TasksRest
                {
                    Name = task.Name,
                    Type = task.Typ
                };
                var favoriteRest = new FavoritesRest
                {
                    UserEmail = user.Name,
                    Token = user.Token,
                    TasksList = taskRest
                };
                favoriteTasks.Add(favoriteRest);
            }
            
            var favoriteJson = JsonConvert.SerializeObject(favoriteTasks);
            var contentFavorite = new StringContent(favoriteJson, Encoding.UTF8, "application/json");
            var uri = new Uri($"{Ip}/favorites");
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

        public async Task SendPlanned(ActivitiesDto activity,TasksDto task)
        {
            var user = UserService.Instance.GetLoggedUser();
            var lastPart = await UserService.Instance.GetLastActivityPart(activity.ActivityId);
            var lastPartRest = new PartsOfActivityRest
            {
                Start = lastPart.Start,
                Stop = lastPart.Stop,
                Duration = lastPart.Duration
            };
            var plannedRest = new PlannedRest
            {
                Comment = activity.Comment,
                EditState = EditState.EditedOnMobile,
                GroupName = activity.GroupId.ToString(),
                Guid = activity.Guid,
                State = activity.Status,
                TaskName = task.Name,
                TaskParts = lastPartRest,
                UserEmail = user.Name,
                Token = user.Token
            };
            var taskJson = JsonConvert.SerializeObject(plannedRest);
            var contentTask = new StringContent(taskJson, Encoding.UTF8, "application/json");
            var uri = new Uri($"{Ip}/planned");
            var response = await _client.PostAsync(uri, contentTask);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            activity.SyncStatus = SyncStatus.Uploaded;
            await UserService.Instance.SaveActivity(activity);
        }

        public async Task GetActivities()
        {
            //var user = UserService.Instance.GetLoggedUser();
            var uri = new Uri($"{Ip}/Activity/?email=b@b.pl");
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
                    //UserId =  user.UserId,
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
            var uri = new Uri($"{Ip}/favorites");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            var favorites = JsonConvert.DeserializeObject<List<FavoritesRest>>(content);
            foreach (var favorite in favorites)
            {
                var taskDto = new TasksDto
                {
                    Name = favorite.TasksList.Name,
                    Typ = favorite.TasksList.Type
                };
                var task = await UserService.Instance.GetTask(taskDto);
                if (favorite.EditState == EditState.Delete)
                {
                    
                    var fav = await UserService.Instance.GetFavoriteByTaskId(task.TaskId);
                    await UserService.Instance.DeleteFavorite(fav);
                    continue;
                }
                var user = await UserService.Instance.GetUserByEmail(favorite.UserEmail);
                if (task == null)
                {
                    taskDto.TaskId = await UserService.Instance.SaveTask(taskDto);

                }
                else
                {
                    taskDto.TaskId = task.TaskId;
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

        public async Task GetPlanned()
        {
            var uri = new Uri($"{Ip}/planned");
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            var plannedList = JsonConvert.DeserializeObject<List<PlannedRest>>(content);
            foreach (var plan in plannedList)
            {
                var user = await UserService.Instance.GetUserByEmail(plan.UserEmail);
                var taskDto = new TasksDto
                {
                    Name = plan.TaskName
                };
                var task = await UserService.Instance.GetTask(taskDto);
                var plannedDto = new ActivitiesDto
                {
                    Comment = plan.Comment,
                    Guid = plan.Guid,
                    SyncStatus = SyncStatus.Received,
                    TaskId = task.TaskId,
                    UserId = user.UserId,
                    Status = StatusType.Planned,
                };
                await UserService.Instance.SaveActivity(plannedDto);
            }
        }
        
    }
}
