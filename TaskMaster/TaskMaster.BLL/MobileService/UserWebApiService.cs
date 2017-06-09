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
        // TODO interfejsy
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();

        public bool IsEmailInDatabase(string email)
        {
            // TODO tą operację można zrobić bezposrednio na bazie danych, nie trzeba pobierac wszystkich userów
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Email.Equals(email));
        }

        public bool IsNameInDatabase(string name)
        {
            // TODO tą operację można zrobić bezposrednio na bazie danych, nie trzeba pobierac wszystkich userów
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Name.Equals(name));
        }


        public UserMobileDto GetUserByEmail(string email)
        {
            // TODO pobierz jednego usera bezposrednio z bazy danych, a nie wszystkich a potem filtrowanie
            var tmpUserDto = _userRepositories.GetAll().First(u => u.Email.Equals(email));
            return new UserMobileDto()
            {
                Email = tmpUserDto.Email,
                Description = tmpUserDto.Description,
                Token = null,
                PlatformType = PlatformType.None
            };

        }

        public bool AddNewUser(UserMobileDto userWebApi)
        {
            // TODO ????? if true ?
            if (true)                   //potwierdzenie od googla
            {
                if (!IsEmailInDatabase(userWebApi.Email))
                {
                    var tmpTokenDto = new TokensDto()
                    {
                        Token = userWebApi.Token,
                        PlatformType = userWebApi.PlatformType,
                    };

                    // TODO automapper
                    var tmpUserDto = new UserDto()
                    {
                        Email = userWebApi.Email,
                        Description = userWebApi.Description,
                        
                        // TODO inicjalizacja list powinna byc w konstruktorze
                        Activities = new List<ActivityDto>(),
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
                // TODO pobrac listę tokenów bezposrednio z tabeli tokenow 
                var listTokens =
                    _userRepositories.GetAll().Where(u => u.Email.Equals(userMobileDto.Email)).Select(t => t.Tokens);
                foreach (var tmpToken in listTokens) _tokensRepositories.Delete((TokensDto)tmpToken);

                // TODO po co sprawdzać czy użytkownik jest w bazie? usuncie go i tyle, jak nie bedzie to 0 rows affected
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
                // TODO po co to sprawdzenie?
                if (IsEmailInDatabase(userMobileDto.Email))
                {
                    // TODO filtrowanie powinno byc na bazie danych
                    var tmpUserDto = _userRepositories.GetAll().First(user => user.Email.Equals(userMobileDto.Email));

                    tmpUserDto.Description = userMobileDto.Description;

                    _userRepositories.Edit(tmpUserDto);

                    // TODO filtrowanie na bazie danych
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
            // TODO po co to sprawdzenie?
            if (IsEmailInDatabase(userMobileDto.Email))
            {
                // TODO filtrowanie na bazie danych
                user = _userRepositories
                    .GetAll(
                    ).First(u => u.Email.Equals(userMobileDto.Email));
            }
            else
            {
                return false;
            }

            // TODO to można na bazie danych zrobić
            if (user.Tokens.Any(t => t.PlatformType == userMobileDto.PlatformType))
            {
                var token = user.Tokens.First(t => t.PlatformType == userMobileDto.PlatformType);
                token.Token = userMobileDto.Token;
                _tokensRepositories.Edit(token);
            }
            else
            {
                // TODO update token robi add token ?
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