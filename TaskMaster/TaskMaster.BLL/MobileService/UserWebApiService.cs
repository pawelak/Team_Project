using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper.Execution;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileServices
{
    public class UserWebApiService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();
        private readonly VeryficationService _veryficationService = new VeryficationService();

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


        public UserMobileDto GetUserByEmail(string email)
        {
            var tmpUserDto = _userRepositories.GetAll().First(u => u.Email.Equals(email));
            return new UserMobileDto()
            {
                Email = tmpUserDto.Email,
                Description = tmpUserDto.Description,
                Token = null,
                PlatformType = PlatformType.None
            };

        }

        public string AddNewUser(UserMobileDto userWebApi, string jwtToken)
        {
            if (_veryficationService.Verify(jwtToken))              
            {
                if (!IsEmailInDatabase(userWebApi.Email))
                {
                    var tmpUserDto = new UserDto()
                    {
                        Email = userWebApi.Email,
                        Description = userWebApi.Description,
                        Activities = new List<ActivityDto>(),
                        Favorites = new List<FavoritesDto>(),
                        UserGroup = new List<UserGroupDto>(),
                        Tokens = new List<TokensDto>()
                    };
                    var nrId = _userRepositories.Add(tmpUserDto);

                    var t = _veryficationService.GenereteToken();
                    var tmpTokenDto = new TokensDto()
                    {
                        Token = t,
                        PlatformType = userWebApi.PlatformType,
                        User = _userRepositories.Get(nrId),
                        BrowserType = BrowserType.None
                    };
                    _tokensRepositories.Add(tmpTokenDto);

                    return t;
                }
            }

            return null;
        }

        public string LogInUser(string email, string jwtToken)
        {
            if (_veryficationService.Verify(jwtToken) && (IsEmailInDatabase(email)))
            {
                var t = _veryficationService.GenereteToken();
                var tmpTokenDto = new TokensDto()
                {
                    Token = t,
                    PlatformType = PlatformType.Android,
                    User = _userRepositories.Get(email)
                };
                _tokensRepositories.Add(tmpTokenDto);
                return t;
            }
            return null;
        }


        public bool DeleteUserByEmail(UserMobileDto userMobileDto)
        {
            if (true)       //google
            {
                var listTokens =
                    _userRepositories.GetAll().Where(u => u.Email.Equals(userMobileDto.Email)).Select(t => t.Tokens);
                foreach (var tmpToken in listTokens) _tokensRepositories.Delete((TokensDto)tmpToken);

                if (IsEmailInDatabase(userMobileDto.Email))
                {
                    var userDto =_userRepositories.GetAll().First(user => user.Email.Equals(userMobileDto.Email));
                    _userRepositories.Delete(userDto);
                }
            }
            return true;
        }


        public bool EditUser(UserMobileDto userMobileDto)
        {
            if (true)       //google
            {
                if (IsEmailInDatabase(userMobileDto.Email))
                {
                    var tmpUserDto = _userRepositories.GetAll().First(user => user.Email.Equals(userMobileDto.Email));

                    tmpUserDto.Description = userMobileDto.Description;

                    _userRepositories.Edit(tmpUserDto);

                    var tmpTokenDto = _tokensRepositories.GetAll()
                        .First(t => (t.PlatformType == userMobileDto.PlatformType)
                                    && (t.PlatformType == PlatformType.None)
                                    && !(t.Token.Equals(userMobileDto.Token)));

                    tmpTokenDto.Token = userMobileDto.Token;

                    _tokensRepositories.Edit(tmpTokenDto);
                }
                
            }
            return true;
        }



        public bool UpdateToken(UserMobileDto userMobileDto)
        {
            UserDto user;
            if (IsEmailInDatabase(userMobileDto.Email))
            {
                user = _userRepositories
                    .GetAll(
                    ).First(u => u.Email.Equals(userMobileDto.Email));
            }
            else
            {
                return false;
            }


            if (user.Tokens.Any(t => t.PlatformType == userMobileDto.PlatformType))
            {
                var token = user.Tokens.First(t => t.PlatformType == userMobileDto.PlatformType);
                token.Token = userMobileDto.Token;
                _tokensRepositories.Edit(token);
            }
            else
            {
                var token = new TokensDto()
                {
                    BrowserType = BrowserType.None,
                    PlatformType = userMobileDto.PlatformType,
                    Token = userMobileDto.Token,
                };
                user.Tokens.Add(token);
            }
            return true;
        }
    }

}