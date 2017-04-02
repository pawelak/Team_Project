using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallendarPage : ContentPage
    {
        public CallendarPage()
        {
            
            InitializeComponent ();
            List<PartsOfActivity> parts;
            parts = App.Database.GetPartsList().Result;
            Activities actitivity = App.Database.GetActivity(parts[1].activityID).Result;
            Tasks task = App.Database.GetTaskById(actitivity.taskId).Result;
            DisplayAlert("Task",task.name,"Task","Task");
        }
    }
}