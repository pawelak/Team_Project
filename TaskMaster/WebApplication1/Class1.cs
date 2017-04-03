using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.DAL;

namespace WebApplication1
{
    public class Class1
    {
        public void NOW()
        {
            UserRepositories DAL = new UserRepositories();
                DAL.GetAll();
            //using ()
            //{
            //} 
            
        }
        
    }
}