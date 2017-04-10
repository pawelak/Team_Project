using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TaskMaster.DAL.Models;

namespace TaskMaster.DAL.DTOModels
{
    public class FavoritesDto
    {
        public UserDto User { get; set; }
        public TaskDto Task { get; set; }

    }
}