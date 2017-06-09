using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.Models;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Interface;


namespace TaskMaster.DAL.Repositories
{
    // TODO zła nazwa klasy to jest jedno czy wiele repozytoriów ?
    public class ActivityRepositories : RepoBase<Activity>, IActivityRepositories
    {
        public void Add(ActivityDto dto)
        {
            var result = Mapper.Map<Activity>(dto);
            // TODO pola UserId, TaksId, GroupId powinny być w DTO 
            result.UserId = result.User.UserId;
            // TODO ustawiania nulla są niepotrzebne, trzeba to zigonorować na poziomie EF
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
            // TODO można usuwac z EF bez wczesniejszego pobierania z bazy
            //  var toDelete = new StudentReportDetail { Id = 2 };
            //  context.StudentReportDetail.Attach(toDelete);
            //  context.StudentReportDetail.Remove(toDelete);
            //  context.SaveChanges();

            var obj = Mapper.Map<Activity>(dto);
            var result = Db.Activity.Find(obj.ActivityId);
            base.Delete(result);


        }
        public new IList<ActivityDto> GetAll()
        {
            // TODO mapowowanie poza select'em 
            var result = base.GetAll().Select(Mapper.Map<ActivityDto>);
            return result.ToList();
        }
        public new ActivityDto Get(int id)
        {
            // TODO nie wywolywac metody w funkcji
            var result = Mapper.Map<ActivityDto>(base.Get(id));
            return result;
        }

        // TODO zwracaj IEnumerable, pozbedziesz sie rzutowania na lite
        public IList<ActivityDto> Get(string email)
        {
            var result = GetAll().Where(v => v.User.Email.Equals(email));
            return result.ToList();
        }
        public void Edit(ActivityDto dto)
        {
            // TODO mapowanie dto na encje i mapowanie encji na encje
            // db.Activities.Attach(obj);
            // db.Entry(obj).State = EntityState.Modified;
            // db.SaveChanges();

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