using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.BLL;
using TaskMaster.DAL;

namespace TaskMaster.Web.App_Start
{
    public class MapperConfig
    {
        public static void MakeMaps()
        {
            Mapper.Initialize(m =>
            {
                m.AddProfile<MapperProfileBLL>();
                m.AddProfile<MapperProfileDal>();
            });
        }
    }

}