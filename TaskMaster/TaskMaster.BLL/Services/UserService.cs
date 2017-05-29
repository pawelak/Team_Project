using System.Collections.Generic;
using System.Linq;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class UserService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();
        private readonly TokensRepositories _tokensRepositories = new TokensRepositories();
        private readonly GroupRepositories _groupRepositories = new GroupRepositories();

        public bool IsEmailInBase(string email) //TODO - zmiana repo
        {
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Email.Equals(email));
        }

        public bool IsNameInBase(string name) //TODO - zmiana repo
        {
            var userList = _userRepositories.GetAll();
            return userList.Any(u => u.Name.Equals(name));
        }

        public List<UserDto> UsersInGroup(string groupName) //TODO - zmiana repo
        {
            var groupList = _groupRepositories.GetAll().FirstOrDefault(g => g.NameGroup.Equals(groupName));
            var userList = groupList.UserGroup.Select(u => u.User).ToList();
            return userList;
        }

        public void SaveUser(string email, string login, string password)
        {
            var user = new UserDto
            {
                Email = email,
                Name = login
            };
            var token = new TokensDto
            {
                Token = password,
                User = user
            };
            _tokensRepositories.Add(token);
        }

        public UserDto GetUser(int id)
        {
            return _userRepositories.Get(id);
        }

        public List<UserDto> GetUserList()
        {
            return _userRepositories.GetAll().ToList();
        }

        public bool Authorization(string login, string password)
        {
            var user = _userRepositories.Get(login);
            if (user == null) return false;
            var tokensList = user.Tokens.ToList();
            return tokensList.Any(t => t.Token.Equals(password));
        }
    }
}
