using System;
using System.Collections.Generic;
using System.Linq;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileServices
{
    public class PrintAllTestService
    {
        private readonly FavoritesRepositories favoritesRepositories = new FavoritesRepositories();
        private readonly ActivityRepositories activityRepositories = new ActivityRepositories();
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();


        public List<UserDto> PrintAllUserDtos()
        {
            return (List<UserDto>)_userRepositories.GetAll();
        }

        public List<TokensDto> PrintAllTokensDtos()
        {
            return (List<TokensDto>) _userRepositories.GetAll();
        }

        public List<UserMobileDto> PrintAllUserWebApi()
        {
            var listOfUsers = _userRepositories.GetAll();
            var listOfTokens = _tokensRepositories.GetAll();
            var listOfWebApiModels = new List<UserMobileDto>();
            foreach (var tmpLoopUser in listOfUsers)
            {
                TokensDto tmpToken = _tokensRepositories.GetAll().First(t => t.User.Email.Equals(tmpLoopUser.Email));
                var tmpMobileDto = new UserMobileDto()
                {
                    Email = tmpLoopUser.Email,
                    Description = tmpLoopUser.Description,
                    Token = tmpToken.Token,
                    PlatformType = tmpToken.PlatformType
                };
                listOfWebApiModels.Add(tmpMobileDto);
            }
            return listOfWebApiModels;
        }


    }
}