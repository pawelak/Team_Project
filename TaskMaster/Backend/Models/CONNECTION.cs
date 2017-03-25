using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Backend.Models
{
    class CONNECTION : DbContext
    {
        public CONNECTION(): base()
        {
            //Database.Connection.ConnectionString = "Data Source=DESKTOP-I62ETTQ;Initial Catalog=NOWA;User ID=Konserwator; Password=poziomkowa13";
           // Database.SetInitializer<CONNECTION>(new CreateDatabaseIfNotExists<CONNECTION>());
        }

        public DbSet<USER> USER { get; set; }
        public DbSet<ACTION> ACTION { get; set; }
        public DbSet<EVENT> EVENT { get; set; }
        public DbSet<ACTIVITY> ACTIVITY { get; set; }
        public DbSet<TOKEN> TOKEN { get; set; }


    }
}
