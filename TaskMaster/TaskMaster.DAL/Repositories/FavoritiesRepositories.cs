﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.DAL
{
    public class FavoritiesRepositories : RepoBase<Favorities>
    {
        Context db = new Context();
        FavoritiesRepositories()
        {

        }
    }
}