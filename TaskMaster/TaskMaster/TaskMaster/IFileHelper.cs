using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMaster
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
