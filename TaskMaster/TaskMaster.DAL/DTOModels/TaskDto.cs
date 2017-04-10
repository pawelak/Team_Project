using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class TaskDto
    {       
        public string Name { get; set; }
        public string Descryption { get; set; }

        public ICollection<ActivityDto> Activity { get; set; }
        public ICollection<FavoritesDto> Favorites { get; set; }

    }
}