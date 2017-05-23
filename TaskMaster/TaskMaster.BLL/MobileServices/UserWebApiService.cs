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
            var tmpTokensDto = tmpUserDto.Tokens.First(t => false);
            return new UserMobileDto()
            {
                Email = tmpUserDto.Email,
                Description = tmpUserDto.Description,
                Token = tmpTokensDto.Token,
                PlatformType = tmpTokensDto.PlatformType
            };

        }

        public bool AddNewUser(UserMobileDto userWebApi)
        {
            if (true)                   //potwierdzenie od googla
            {
                if (!IsEmailInDatabase(userWebApi.Email))
                {
                    var tmpTokenDto = new TokensDto()
                    {
                        Token = userWebApi.Token,
                        PlatformType = userWebApi.PlatformType,
                    };
                    var tmpUserDto = new UserDto()
                    {
                        Email = userWebApi.Email,
                        Description = userWebApi.Description,
                        Activity = new List<ActivityDto>(),
                        Favorites = new List<FavoritesDto>(),
                        UserGroup = new List<UserGroupDto>(),
                        Tokens = new List<TokensDto>()
                    };
                    tmpUserDto.Tokens.Add(tmpTokenDto);

                    _userRepositories.Add(tmpUserDto);

                    return true;
                }
            }
            
            return false;
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
                                    && (t.PlatformType == PlatformType.none)
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
                    BrowserType = BrowserType.none,
                    PlatformType = userMobileDto.PlatformType,
                    Token = userMobileDto.Token,
                };
                user.Tokens.Add(token);
            }
            return true;
        }
    }

}