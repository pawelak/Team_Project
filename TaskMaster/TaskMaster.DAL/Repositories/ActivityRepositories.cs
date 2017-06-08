using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.Models;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;


namespace TaskMaster.DAL.Repositories
{
    public class ActivityRepositories : RepoBase<Activity>, IActivityRepositories
    {
        public void Add(ActivityDto dto)
        {
            var result = Mapper.Map<Activity>(dto);
            result.UserId = result.User.UserId;
            result.User = null;
            result.TaskId = result.Task.TaskId;
            result.Task = null;
            result.GroupId = result.Group.GroupId;
            result.Group = null;
            result.PartsOfActivity = null;
            base.Add(result);
        }
        public void Delete(ActivityDto dto)
        {
            var obj = Mapper.Map<Activity>(dto);
            var result = Db.Activity.Find(obj.ActivityId);
            base.Delete(result);
        }
        public new IList<ActivityDto> GetAll()
        {
            var result = base.GetAll().Select(Mapper.Map<ActivityDto>);
            return result.ToList();
        }
        public new ActivityDto Get(int id)
        {
            var result = Mapper.Map<ActivityDto>(base.Get(id));
            return result;
        }
        public IList<ActivityDto> Get(string email)
        {
            var result = GetAll().Where(v => v.User.Email.Equals(email));
            return result.ToList();
        }
        public void Edit(ActivityDto dto)
        {
            var obj = Mapper.Map<Activity>(dto);
            var result = Db.Activity.Find(obj.ActivityId);
            Db.Activity.Attach(result);
            result.State = obj.State;
            result.EditState = obj.EditState;
            result.Comment = obj.Comment;
            result.Guid = obj.Guid;
            result.UserId = obj.UserId;
            result.GroupId = obj.GroupId;
            result.TaskId = obj.TaskId;
            result.Comment = obj.Comment;
            base.Edit(result);
        }
    }
}