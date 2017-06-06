using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace TaskMaster.DAL.Repositories
{
     public class RepoBase<T> where T : class
     {
            protected Context.DatabaseContext Db = new Context.DatabaseContext();

            protected void Add(T x)
            {
                Db.Set<T>().Add(x);
                Db.SaveChanges();
            }


            protected void Delete(T x)
            {
                Db.Set<T>().Remove(x);
                Db.SaveChanges();
            }

            protected IList<T> GetAll()
            {
                return Db.Set<T>().ToList();
            }

            protected T Get(int id)
            {
                return Db.Set<T>().Find(id);
            }

            protected void Edit(T x)
            {
                Db.Entry(x).State = EntityState.Modified;
                Db.SaveChanges();
            }

            protected int Count()
            {
                return Db.Set<T>().Count();

            }
        }
}