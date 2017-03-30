using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Parts_of_activityRepositories : RepoBase<Parts_of_activity>
    {
        Context db = new Context();
        public Parts_of_activityRepositories()
        {

        }
    }
}