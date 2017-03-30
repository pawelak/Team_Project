using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class User_GroupRepositories : RepoBase<User_Group>
    {
        Context db = new Context();
        public User_GroupRepositories()
        {

        }
    }
}