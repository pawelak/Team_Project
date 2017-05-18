using Newtonsoft.Json;
using TaskMaster.ModelsDto;
namespace TaskMaster.Services
{
    public class SynchronizationService
    {
        private static SynchronizationService _instance;
        private SynchronizationService(){}

        public static SynchronizationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SynchronizationService();
                }
                return _instance;
            }
        }

        public void SendUser(UserDto user)
        {
            var json = JsonConvert.SerializeObject(user);
        }

        public void SendActivity(ActivitiesDto activity)
        {
            var activityJson = JsonConvert.SerializeObject(activity);
            var parts = UserService.Instance.GetPartsOfActivityByActivityId(activity.ActivityId);
            var partsJson = JsonConvert.SerializeObject(parts, Formatting.Indented);
        }

        public void GetActivities()
        {
            
        }

        public void GetTasks()
        {
            
        }

        public void GetFavorites()
        {
            
        }

        public void GetPartsOfActivities()
        {
            
        }
    }
}
