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
            alfa.email = "dlanorberta@gmail.com";
            alfa.name = "Norbercik";
            db.user.Add(alfa);
            db.SaveChanges();
            User beta = new User();
            beta.email = "dlapawela@gmail.com";
            beta.name = "Pawełek";
            db.user.Add(beta);
            db.SaveChanges();
            User gamma = new User();
            gamma.email = "dlabartosza@gmail.com";
            gamma.name = "Bartoszek";
            db.user.Add(gamma);
            db.SaveChanges();
            //Koniec testu
        }
    }
}