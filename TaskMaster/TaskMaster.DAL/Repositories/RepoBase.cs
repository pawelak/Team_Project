using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TaskMaster.DAL
{
     public class RepoBase<T> where T : class
     {
            Context db = new Context();

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

            public T Get(T X)
            {
                return db.Set<T>().FirstOrDefault<T>();
            }

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