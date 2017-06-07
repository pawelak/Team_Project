using System.Collections.Generic;
using TaskMaster.BLL.MobileServices;
using TaskMaster.DAL.DTOModels;
using TaskMaster.DAL.Enum;

namespace TaskMaster.BLL.WebApiModels
{
    public class FavoritesMobileDto
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }

        public EditState EditState { get; set; }

        public TasksMobileDto Task { get; set; }
    }
}