using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly HttpClient _client = new HttpClient();
        public static SynchronizationService Instance => _instance ?? (_instance = new SynchronizationService());

        private SynchronizationService()
        {
            _client.MaxResponseContentBufferSize = 256000;
            _client.Timeout = TimeSpan.FromSeconds(2);
        }

        public async Task<bool> SendUser(UserDto user)
        {
            var userRest = new UserRest
            {
                Email = user.Name,
                Token = user.Token,
                Description = "",
                PlatformType = PlatformType.Android
            };
            var userJson = JsonConvert.SerializeObject(userRest);
            var contentUser = new StringContent(userJson, Encoding.UTF8, "application/json");
            var uri = new Uri($"{Ip}/User/?jwtToken={user.Token}");
            try
            {
                var response = await _client.PutAsync(uri, contentUser);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var content = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenRest>(content);
                user.ApiToken = token.Token;
                user.SyncStatus = SyncStatus.Uploaded;
                await UserService.Instance.SaveUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendTasks()
        {
            var tasks = await UserService.Instance.GetTasksToUpload();
            foreach (var task in tasks)
            {
                var send = await SendTask(task);
                if (!send)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> SendTask(TasksDto task)
        {
            var user = UserService.Instance.GetLoggedUser();
            var taskRest = new TasksRest
            {
                Name = task.Name,
                Type = task.Typ,
                Email = user.Name,
                Token = user.ApiToken
            };
            var taskJson = JsonConvert.SerializeObject(taskRest);
            var contentUser = new StringContent(taskJson, Encoding.UTF8, "application/json");
            var uri = new Uri($"{Ip}/Task");
            try
            {
                var response = await _client.PutAsync(uri, contentUser);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                task.SyncStatus = SyncStatus.Uploaded;
                await UserService.Instance.SaveTask(task);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendActivity(ActivitiesDto activity,TasksDto task)
        {
            var parts = await UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
            var list = parts.Select(partsOfActivityDto => new PartsOfActivityRest
                {
                    Start = partsOfActivityDto.Start,
                    Stop = partsOfActivityDto.Stop,
                    Duration = TimeSpan.FromMilliseconds(int.Parse(partsOfActivityDto.Duration)).ToString("G", CultureInfo.InvariantCulture)
            })
                .ToList();
            var user = UserService.Instance.GetLoggedUser();
            var activityRest = new ActivitiesRest
            {
                Guid = activity.Guid,
                Comment = activity.Comment,
                EditState = EditState.EditedOnMobile,
                TaskName = task.Name,
                UserEmail = user.Name,
                Token = user.ApiToken,
                State = activity.Status,
                GroupName = null,
                TaskPartsList = list
            };
            var activityJson = JsonConvert.SerializeObject(activityRest);
            var contentActivity = new StringContent(activityJson, Encoding.UTF8, "application/json");
            var uri = new Uri($"{Ip}/Activity");
            try
            {
                var response = await _client.PutAsync(uri, contentActivity);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                activity.SyncStatus = SyncStatus.Uploaded;
                await UserService.Instance.SaveActivity(activity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SendActivities()
        {
            var activities = await UserService.Instance.GetActivitiesToUpload(StatusType.Stop);
            foreach (var activity in activities)
            {
                var task = await UserService.Instance.GetTaskById(activity.TaskId);
                var send = await SendActivity(activity, task);
                if (!send)
                {
                    break;
                }
            }
        }

        public async Task SendFavorites()
        {
            var favorites = await UserService.Instance.GetFavoritesToUpload();
            foreach (var favorite in favorites)
            {
                var send = await SendFavorite(favorite);
                if (!send)
                {
                    break;
                }
            }
        }

        public async Task<bool> SendFavorite(FavoritesDto favorite)
        {
            var user = UserService.Instance.GetLoggedUser();
            var task = await UserService.Instance.GetTaskById(favorite.TaskId);
            var taskRest = new TasksRest
            {
                Name = task.Name,
                Type = task.Typ,
                Email = user.Name,
                Token = user.ApiToken
            };
            var favoriteRest = new FavoritesRest
            {
                UserEmail = user.Name,
                Token = user.ApiToken,
                Task = taskRest
            };
            var favoriteJson = JsonConvert.SerializeObject(favoriteRest);
            var contentFavorite = new StringContent(favoriteJson, Encoding.UTF8, "application/json");
            var uri = new Uri($"{Ip}/Favorites");
            try
            {
                var response = await _client.PutAsync(uri, contentFavorite);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                favorite.SyncStatus = SyncStatus.Uploaded;
                await UserService.Instance.SaveFavorite(favorite);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendPlanned(ActivitiesDto activity,TasksDto task)
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
                GroupName = null,
                Guid = activity.Guid,
                State = activity.Status,
                TaskName = task.Name,
                TaskPart = lastPartRest,
                UserEmail = user.Name,
                Token = user.ApiToken
            };
            var taskJson = JsonConvert.SerializeObject(plannedRest);
            var contentTask = new StringContent(taskJson, Encoding.UTF8, "application/json");
            var uri = new Uri($"{Ip}/Planned");
            try
            {
                var response = await _client.PutAsync(uri, contentTask);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                activity.SyncStatus = SyncStatus.Uploaded;
                await UserService.Instance.SaveActivity(activity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SendPlannedAsync()
        {
            var planned = await UserService.Instance.GetActivitiesToUpload(StatusType.Planned);
            foreach (var activity in planned)
            {
                var task = await UserService.Instance.GetTaskById(activity.TaskId);
                var send = await SendPlanned(activity,task);
                if (!send)
                {
                    break;
                }
            }
        }

        public async Task<bool> DeletePlanned(ActivitiesDto activity, TasksDto task)
        {
            var user = UserService.Instance.GetLoggedUser();
            var uri = new Uri($"{Ip}/Planned/?guid={activity.Guid}&email={user.Name}&token={user.ApiToken}");
            try
            {
                var response = await _client.DeleteAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                activity.SyncStatus = SyncStatus.Uploaded;
                await UserService.Instance.SaveActivity(activity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GetActivities()
        {
            var user = UserService.Instance.GetLoggedUser();
            var uri = new Uri($"{Ip}/Activity/?email={user.Name}&token={user.ApiToken}");
            try
            {
                var response = await _client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var content = await response.Content.ReadAsStringAsync();
                var activities = JsonConvert.DeserializeObject<List<ActivitiesRest>>(content);
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
                                    var t = TimeSpan.Parse(activity.TaskPartsList[i].Duration,
                                            CultureInfo.InvariantCulture)
                                        .TotalMilliseconds;
                                    var time = (int) t;
                                    part.Duration = time.ToString();
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
                                        var t = TimeSpan
                                            .Parse(activity.TaskPartsList[i].Duration, CultureInfo.InvariantCulture)
                                            .TotalMilliseconds;
                                        var time = (int) t;
                                        part.Duration = time.ToString();
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
                                        var t = TimeSpan
                                            .Parse(activity.TaskPartsList[i].Duration, CultureInfo.InvariantCulture)
                                            .TotalMilliseconds;
                                        var time = (int) t;
                                        var newPart = new PartsOfActivityDto
                                        {
                                            ActivityId = item.ActivityId,
                                            Start = activity.TaskPartsList[i].Start,
                                            Stop = activity.TaskPartsList[i].Stop,
                                            Duration = time.ToString()
                                        };
                                        await UserService.Instance.SavePartOfActivity(newPart);
                                    }
                                    else
                                    {
                                        part.Start = activity.TaskPartsList[i].Start;
                                        part.Stop = activity.TaskPartsList[i].Stop;
                                        var t = TimeSpan
                                            .Parse(activity.TaskPartsList[i].Duration, CultureInfo.InvariantCulture)
                                            .TotalMilliseconds;
                                        var time = (int) t;
                                        part.Duration = time.ToString();
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
                        GroupId = 0,
                        SyncStatus = SyncStatus.Received,
                        UserId = user.UserId,
                        TaskId = taskDto2.TaskId
                    };
                    activityDto.ActivityId = await UserService.Instance.SaveActivity(activityDto);
                    foreach (var parts in activity.TaskPartsList)
                    {
                        var t = TimeSpan.Parse(parts.Duration, CultureInfo.InvariantCulture)
                            .TotalMilliseconds;
                        var time = (int) t;
                        var part = new PartsOfActivityDto
                        {
                            ActivityId = activityDto.ActivityId,
                            Start = parts.Start,
                            Stop = parts.Stop,
                            Duration = time.ToString()
                        };
                        await UserService.Instance.SavePartOfActivity(part);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GetFavorites()
        {
            var user = UserService.Instance.GetLoggedUser();
            var uri = new Uri($"{Ip}/Favorites/?email={user.Name}&token={user.ApiToken}");
            try
            {
                var response = await _client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var content = await response.Content.ReadAsStringAsync();
                var favorites = JsonConvert.DeserializeObject<List<FavoritesRest>>(content);
                foreach (var favorite in favorites)
                {
                    var taskDto = new TasksDto
                    {
                        Name = favorite.Task.Name,
                        Typ = favorite.Task.Type
                    };
                    var task = await UserService.Instance.GetTask(taskDto);
                    if (favorite.EditState == EditState.Delete)
                    {
                        var fav = await UserService.Instance.GetFavoriteByTaskId(task.TaskId);
                        await UserService.Instance.DeleteFavorite(fav);
                        continue;
                    }
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GetPlanned()
        {
            var user = UserService.Instance.GetLoggedUser();
            var uri = new Uri($"{Ip}/Planned/?email={user.Name}&token={user.ApiToken}");
            try
            {
                var response = await _client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                var content = await response.Content.ReadAsStringAsync();
                var plannedList = JsonConvert.DeserializeObject<List<PlannedRest>>(content);
                foreach (var plan in plannedList)
                {
                    var guids = await UserService.Instance.GetActivitiesByStatus(StatusType.Planned);
                    if (guids.Any(item => item.Guid == plan.Guid))
                    {
                        continue;
                    }
                    var taskDto = new TasksDto
                    {
                        Name = plan.TaskName,
                        SyncStatus = SyncStatus.Received
                    };
                    var task = await UserService.Instance.GetTask(taskDto);
                    if (task == null)
                    {
                        taskDto.TaskId = await UserService.Instance.SaveTask(taskDto);
                    }
                    else
                    {
                        taskDto = task;
                    }
                    var plannedDto = new ActivitiesDto
                    {
                        Comment = plan.Comment,
                        Guid = plan.Guid,
                        SyncStatus = SyncStatus.Received,
                        TaskId = taskDto.TaskId,
                        UserId = user.UserId,
                        Status = StatusType.Planned
                    };
                    plannedDto.ActivityId = await UserService.Instance.SaveActivity(plannedDto);
                    var part = new PartsOfActivityDto
                    {
                        ActivityId = plannedDto.ActivityId,
                        Start = plan.TaskPart.Start
                    };
                    await UserService.Instance.SavePartOfActivity(part);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
