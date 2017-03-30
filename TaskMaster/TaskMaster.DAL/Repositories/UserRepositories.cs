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
            //Dane testowe do wyrzucenia po zakonczeniu badan
            User alfa = new User();
            alfa.Email = "dlanorberta@gmail.com";
            alfa.Name = "Norbercik";
            db.User.Add(alfa);
            db.SaveChanges();
            User beta = new User();
            beta.Email = "dlapawela@gmail.com";
            beta.Name = "Pawełek";
            db.User.Add(beta);
            db.SaveChanges();
            User gamma = new User();
            gamma.Email = "dlabartosza@gmail.com";
            gamma.Name = "Bartoszek";
            db.User.Add(gamma);
            db.SaveChanges();
        }
    }
}