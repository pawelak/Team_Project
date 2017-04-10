using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserRepositories : RepoBase<User>, IUserRepositories
    {
        public UserRepositories()
        {
            
        }
        public void Add(UserDto dto)
        {
            User entity = dto.ToUser();
            base.Add(entity);
        }

        public void Delete(UserDto dto)
        {
            User entity = dto.ToUser();
            base.Delete(entity);
        }

        public IList<UserDto> GetAll()
        {
            IList<UserDto> list = new List<UserDto>();
            foreach (var VARIABLE in base.GetAll())
            {
                list.Add(new UserDto(VARIABLE));
            }
            return list;
        }

        public UserDto Get(int ID)
        {
            return new UserDto(base.Get(ID));
        }

        public void Edit(UserDto dto)
        {
            User entity = dto.ToUser();
            base.Edit(entity);
        }
    }
}