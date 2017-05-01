using System.IO;
using Xamarin.Forms;
using TaskMaster.Droid;

using Environment = System.Environment;

[assembly: Dependency(typeof(FileHelper))]
namespace TaskMaster.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}