using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskMaster.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActiveTasksPage : ContentPage
    { 
		public ActiveTasksPage ()
		{
			InitializeComponent ();
		    var taskList = ListInitiate().Result;
		    ActiveTasks.ItemsSource = taskList;
		}

	    private void ActiveTasks_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        throw new NotImplementedException();
	    }

        private async Task<List<Tasks>> ListInitiate()
        {
            var result = await App.Database.GetActivitiesByStatus(StatusType.Start);
            List<Tasks> activeTasks = new List<Tasks>();
            foreach (var activity in result)
            {
                var task = await App.Database.GetTaskById(activity.TaskId);
                activeTasks.Add(task);
            }
            return activeTasks;
        }
	}
}
