using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.Repositories
{
    public class UserRepositories : RepoBase<User>
    {
        public UserRepositories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDto>());
        }

        public UserDto GetUserById(int id)
        {
            var user = this.Get(id);
            var obj = Mapper.Map<User, UserDto>(user);
            return obj;
        }

        public void AddToDatabase(UserDto userDto)
        {
            var obj = Mapper.Map<UserDto, User>(userDto);
            this.Add(obj);
        }

        public List<UserDto> GetAllToList()
        {
            var listOut = GetAll().Select(Mapper.Map<User, UserDto>).ToList();

            //--------------test dodania-------------------
           // var test = new User {Name = "Dziala"};
           // listOut.Add(Mapper.Map<User, UserDto>(test));
            //---------------------------------------------

            return listOut;
        }
    }
}