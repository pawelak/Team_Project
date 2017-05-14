using System.Collections.Generic;
using System.Linq;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;
using TaskMaster.WebApi.Models;

namespace TaskMaster.BLL.WebApiServices
{
    public class PrintAll
    {
        private readonly FavoritesRepositories favoritesRepositories = new FavoritesRepositories();
        private readonly ActivityRepositories activityRepositories =new ActivityRepositories();
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();


        public List<UserDto> PrintAllUserDtos()
        {
            return (List<UserDto>) _userRepositories.GetAll();
        }

        public List<UserWebApi> PrintAllUserWebApis()
        {
            var listOfUsers = _userRepositories.GetAll();
            var listOfTokens = _tokensRepositories.GetAll();
            var listOfWebApiModels = new List<UserWebApi>();
            foreach (var tmpLoopUser in listOfUsers)
            {
                TokensDto tmpTokensDto = null;
                foreach (var VARIABLE in listOfTokens)
                {
                    if (VARIABLE.User.Email.Equals(tmpLoopUser.Email))
                    {
                        tmpTokensDto = VARIABLE;
                    }
                }
                
                var tmpUserWebApi = new UserWebApi()
                {
                    Email = tmpLoopUser.Email,
                    Name = tmpLoopUser.Name,
                    Description = tmpLoopUser.Description,
                    //Token = tmpTokensDto.Token,
                    //BrowserType = tmpTokensDto.BrowserType,
                    //PlatformType = tmpTokensDto.PlatformType
                };
                listOfWebApiModels.Add(tmpUserWebApi);
            }
            return listOfWebApiModels;
        }



    }
}