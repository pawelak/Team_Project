using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.MobileService
{
    public class PlannedService
    {

        //machlojona baza
        private DataSimulation _database = new DataSimulation();


        public List<PlannedMobileDto> GetPlanned(string email)
        {

            var activityRawList = new List<ActivityDto>();

            //------------machlojenia z bazą----------------
            var user = _database.userDtosList.First(a => a.Email.Equals(email));
            //var user = _userRepositories.Get(email);


            var tmpList = user.Activity.Where(act => act.State ==State.Planned).ToList();


            return (from raw in tmpList
                let tmpPart = new PartsOfActivityMobileDto()
                {
                    Start = raw.PartsOfActivity.First().Start.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Stop = raw.PartsOfActivity.First().Stop.ToString("HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Duration = raw.PartsOfActivity.First().Duration.ToString("G", CultureInfo.InvariantCulture)
                }
                select new PlannedMobileDto()
                {
                    UserEmail = raw.User.Email,
                    Comment = raw.Comment,
                    Guid = raw.Guid,
                    TaskName = raw.Task.Name,
                    Token = null,
                    EditState = raw.EditState,
                    State = raw.State,
                    TaskPart = tmpPart
                }).ToList();
        }
    }
}