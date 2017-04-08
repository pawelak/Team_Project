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
    public partial class CallendarPage : ContentPage
    {
        XamForms.Controls.Calendar calendar = new XamForms.Controls.Calendar();
        List<PartsOfActivity> parts;
        public CallendarPage()
        {
           
            parts = App.Database.GetPartsList().Result;

         
            foreach (PartsOfActivity p in parts)
            {
                calendar.SpecialDates.Add(new SpecialDate(Convert.ToDateTime(p.start)) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.Blue, BorderWidth = 8 });
            }
            calendar.RaiseSpecialDatesChanged();//refresh
            InitializeComponent ();
            
          
           // Activities actitivity = App.Database.GetActivity(parts[1].activityID).Result;
           // Tasks task = App.Database.GetTaskById(actitivity.taskId).Result;
            //DisplayAlert("Task",parts[0].start,"Task","Task");
        }
    }
}