using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class GroupRepositories : RepoBase<Group>
    {
        Context db = new Context();
        GroupRepositories()
        {

        }
    }
}