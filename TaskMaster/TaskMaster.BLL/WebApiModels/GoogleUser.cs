using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.BLL.WebApiModels
{
    public class GoogleUser
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string ProviderUserId { get; set; }
    }
}
