using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TaskMaster.DAL.Context;

namespace TaskMaster.DAL.Repositories
{
     public class RepoBase<T> where T : class
     {
            protected Context.DatabaseContext db = new Context.DatabaseContext();

            public void Add(T X)
            {
                db.Set<T>().Add(X);
                db.SaveChanges();
            }

            public void Delete(T X)
            {
                db.Set<T>().Remove(X);
                db.SaveChanges();
            }

            public IList<T> GetAll()
            {
                return db.Set<T>().ToList<T>();
            }

            //public T Get(int ID)
            //{
            //    return db.Set<T>().FirstOrDefault<>();
            //}

            public void Edit(T X)
            {
                // Proszę o zwórcenie bacznej uwagi na kod ponizej i informacje o jego poprawnosci
                db.Entry(X).State = EntityState.Modified;
                db.SaveChanges();
            }

            public int Count()
            {
                return db.Set<T>().Count<T>();
            }
        }
}