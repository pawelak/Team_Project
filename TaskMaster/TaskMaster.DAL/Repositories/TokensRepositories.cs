using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class TokensRepositories : RepoBase<Tokens>
    {
        Context db = new Context();
        public TokensRepositories()
        {

        }
    }
}