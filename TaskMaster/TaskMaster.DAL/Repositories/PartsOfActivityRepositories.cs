using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class PartsOfActivityRepositories : RepoBase<PartsOfActivity>
    {
        Context db = new Context();
        public PartsOfActivityRepositories()
        {

        }
    }
}