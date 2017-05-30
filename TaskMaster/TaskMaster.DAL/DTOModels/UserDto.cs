using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public IList<ActivityDto> Activity { get; set; }
        public IList<TokensDto> Tokens { get; set; }
        public IList<UserGroupDto> UserGroup { get; set; }
        public IList<FavoritesDto> Favorites { get; set; }
    }
}