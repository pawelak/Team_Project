using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class UserRepositories : RepoBase<User>
    {
        Context db = new Context();
        public UserRepositories()
        {

        }
    }
}