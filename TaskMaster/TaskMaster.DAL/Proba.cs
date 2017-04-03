using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class Proba
    {  
        UserRepositories alfa = new UserRepositories();
        

        public void robta()
        {
            alfa.GetAll();
        }
    }
}