using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class RepoAction : RepoBase<Action>
    {
        Connection db = new Connection();

        public void Add(Action obj)
        {
            db.Action.Add(obj);
            db.SaveChanges();
        }



    }
}


