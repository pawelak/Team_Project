using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class TaskRepositories : RepoBase<Task>
    {
        Context db = new Context();
        public TaskRepositories()
        {

        }
    }
}