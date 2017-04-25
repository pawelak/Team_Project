using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.Services
{
    public class Norbert
    {
        public UserRepositories UserRepositories = new UserRepositories();


        public bool IsEmailInBase(string email)
        {
            var list = UserRepositories.GetAll();
            return list.Any(var => var.Email.Equals(email));
        }

        //public List<ActivityDto> FromTime(DateTime start, DateTime stop)
        //{
        //    return 
        //}

        //grupy:
        //wyszukiwanie urzytkowników po emailu i nazwie // wszystkich z grupy?
        //pobieranie czonków grupy


        //    activity i partsof:
        //pobierz z określonych dat - o ile się dat
    }

}