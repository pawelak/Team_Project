using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;
using TaskMaster.WebApi.Models;

namespace TaskMaster.BLL.WebApiServices
{
    public class UserWebApiService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();

        public bool IsEmailInDatabase(string email)
        {
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Email.Equals(email));
        }

        public bool IsNameInDatabase(string name)
        {
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Name.Equals(name));
        }


        public bool AddNewUser(UserWebApi userWebApi)
        {
            if (!IsEmailInDatabase(userWebApi.Email))
            {
                //var tmpTokenDto = new TokensDto()
                //{
                //    Token = userWebApi.Token,
                //    PlatformType = userWebApi.PlatformType,
                //    BrowserType = userWebApi.BrowserType,
                //};

                var tmpUserDto = new UserDto()
                {
                    Email = userWebApi.Email,
                    Name = userWebApi.Name,
                    Description = userWebApi.Description,
                    Activity = new List<ActivityDto>(),
                    Favorites = new List<FavoritesDto>(),
                    UserGroup = new List<UserGroupDto>(),
                    Tokens = new List<TokensDto>()
                };
                //tmpTokenDto.User = tmpUserDto;
                //tmpUserDto.Tokens.Add(tmpTokenDto);

                _userRepositories.Add(tmpUserDto);
                //_tokensRepositories.Add(tmpTokenDto);

                return true;
            }
            return false;
        }






    }
}