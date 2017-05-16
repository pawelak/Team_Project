using System.Collections.Generic;
using System.Linq;
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
                Name = tmpUserDto.Name,
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
                        Name = userWebApi.Name,
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
                    userMobileDto.Name = userMobileDto.Name;

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

        public bool AddPlatform(UserMobileDto userMobileDto)
        {
            if (IsEmailInDatabase(userMobileDto.Email))
            {
                var tmpToken =
                    _tokensRepositories.GetAll()
                        .Where(
                            t =>
                                t.PlatformType != userMobileDto.PlatformType &&
                                (t.User.Email.Equals(userMobileDto.Email)));
                foreach (var var in tmpToken)
                {
                    var tmpTokenDto = new TokensDto()
                    {
                        BrowserType = BrowserType.none,
                        PlatformType = userMobileDto.PlatformType,
                        Token = userMobileDto.Token,
                        User = var.User
                    };
                    _tokensRepositories.Add(tmpTokenDto);
                    return true;
                }

            }
            return false;
        }


    }

}