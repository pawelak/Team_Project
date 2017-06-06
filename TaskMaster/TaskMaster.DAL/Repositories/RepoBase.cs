using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            protected void Attach(T x)
            {
                Db.Set<T>().Attach(x);
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
            protected void Edit(T x,string key)
            {
                Db.Set<T>().Attach(x);
                Db.Entry(x).Property(i => GetAll()).IsModified = true;
                Db.Entry(x).Property(i => i).IsModified = true;
                Db.SaveChanges();
            }
            protected int Count()
            {
                return Db.Set<T>().Count();
            }
        }
}