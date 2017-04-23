using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace TaskMaster.DAL.Repositories
{
     public class RepoBase<T> where T : class
     {
            protected Context.DatabaseContext DatabaseContext = new Context.DatabaseContext();

            public T Get(int x)
            {
                return DatabaseContext.Set<T>().Find(x);
            }

            public void Add(T x)
            {
                DatabaseContext.Set<T>().Add(x);
                DatabaseContext.SaveChanges();
            }

            public void Delete(T x)
            {
                DatabaseContext.Set<T>().Remove(x);
                DatabaseContext.SaveChanges();
            }

            public IList<T> GetAll()
            {
                return DatabaseContext.Set<T>().ToList();
            }

            //public T Get(int ID)
            //{
            //    return db.Set<T>().FirstOrDefault<>();
            //}

            public void Edit(T x)
            {
                // Proszę o zwórcenie bacznej uwagi na kod ponizej i informacje o jego poprawnosci
                DatabaseContext.Entry(x).State = EntityState.Modified;
                DatabaseContext.SaveChanges();
            }

            public int Count()
            {
                return DatabaseContext.Set<T>().Count();
            }
        }
}