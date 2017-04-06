using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace TaskMaster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {

        public CalendarPage()
        {
            InitializeComponent();
            // Activities actitivity = App.Database.GetActivity(parts[1].activityID).Result;
            // Tasks task = App.Database.GetTaskById(actitivity.taskId).Result;
            //DisplayAlert("Task",parts[0].start,"Task","Task");
        }

    }
}

