using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DAL
{
    public class RepoBase<T> where T:class
    {
        Connection db = new Connection();

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
            // Proszę o sprawdzenie tego miejsca i odpoiedzenie na następujące pytania:
            // Czy ma prawo zadziałać?
            // Czy istnieje lepsze zaimlementowanie sposobu edycji danych?
            // Czy jest prawidłowe ze względu na konwencję kodowania?
            // Z poważaniem Norbert 
            db.Entry(X).State = EntityState.Modified;
            db.SaveChanges();
            db.Database.
        }

        public int Count()
        {
           return  db.Set<T>().Count<T>();
        }
    }
}