using System.Collections.Generic;

namespace TaskMaster.DAL.DTOModels
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }      

        public virtual ICollection<ActivityDto> Activities { get; set; }
        public virtual ICollection<TokensDto> Tokens { get; set; }
        public virtual ICollection<UserGroupDto> UserGroup { get; set; }
        public virtual ICollection<FavoritesDto> Favorites { get; set; }
    }
}