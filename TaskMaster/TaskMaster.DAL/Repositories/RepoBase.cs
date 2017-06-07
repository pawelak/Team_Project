using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

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
            protected void Edit(T x, params Expression<Func<T, object>>[] key)
            {
                Db.Set<T>().Attach(x);
                foreach (var k in key)
                {
                    Db.Entry(x).Property(k).IsModified = true;
                }
                Db.SaveChanges();
            }
            protected int Count()
            {
                return Db.Set<T>().Count();
            }
        }
}