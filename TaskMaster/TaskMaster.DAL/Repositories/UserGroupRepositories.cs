using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class UserGroupRepositories : RepoBase<UserGroup>
    {
        Context db = new Context();

        public UserGroupRepositories()
        {

        }
    }
}