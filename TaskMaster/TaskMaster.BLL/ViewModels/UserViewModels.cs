using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.DAL.DTOModels;

namespace TaskMaster.BLL.ViewModels
{
    public class UserViewModels
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
