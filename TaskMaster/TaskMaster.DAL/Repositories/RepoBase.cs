using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace TaskMaster.DAL.Repositories
{
     public class RepoBase<T> where T : class
     {
            protected Context.DatabaseContext db = new Context.DatabaseContext();

            protected void Add(T X)
            {
                db.Set<T>().Add(X);
                db.SaveChanges();
            }

            protected void Delete(T X)
            {
                db.Set<T>().Remove(X);
                db.SaveChanges();
            }

            protected IList<T> GetAll()
            {
                return db.Set<T>().ToList<T>();
            }

             protected T Get(int ID)
            {
                return db.Set<T>().Find(ID);
            }

            protected void Edit(T X)
            {
                db.Entry(X).State = EntityState.Modified;
                db.SaveChanges();
            }

            protected int Count()
            {
                return db.Set<T>().Count<T>();
            }
        }
}