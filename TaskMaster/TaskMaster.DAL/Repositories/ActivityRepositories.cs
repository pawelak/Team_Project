using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class ActivityRepositories : RepoBase<Activity>
    {
        Context db = new Context();
        public ActivityRepositories()
        {

        }
    }
}