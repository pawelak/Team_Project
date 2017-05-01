using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using TaskMaster.ModelsDto;

namespace TaskMaster.Droid
{
    [Activity(Label = "TaskMaster", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly UserService _userService = new UserService();

        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            var started = await _userService.GetActivitiesByStatus(StatusType.Start);
            foreach (var start in started)
            {
                var alert = new AlertDialog.Builder(this);
                if (start.TaskId == 0)
                {
                    var noTask = new TasksDto
                    {
                        Name = "Unnamed Activity " + start.ActivityId
                    };
                    alert.SetTitle("Masz niezapauzowaną aktywność " + noTask.Name);
                }
                else
                {
                    var task = await _userService.GetTaskById(start.TaskId);
                    alert.SetTitle("Masz niezapauzowaną aktywność " + task.Name);
                }
                alert.SetMessage("Czy była ona aktywna od zamknięcia aplikacji? \n" +
                                 "Jeżeli wybierzesz nie, czas aktywności może być niewłaściwy \n" +
                                 "Jeżeli wybierzesz tak, czas końca aktywności będzie czasem zatwierdzenia tego komunikatu");
                alert.SetNegativeButton("Nie", (s, a) =>
                {
                    var alertNo = new AlertDialog.Builder(this);
                    alertNo.SetTitle("Kontynuacja");
                    alertNo.SetMessage("Czy chcesz żeby aktywność była kontynuowana?");
                    alertNo.SetPositiveButton("Tak", async (s1, a1) =>
                    {
                        var part2 = new PartsOfActivityDto
                        {
                            Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                            ActivityId = start.ActivityId,
                            Duration = "0"
                        };
                        part2.PartId = await _userService.SavePartOfActivity(part2);
                        var sw = new Stopwatch();
                        var stopwatch = new Stopwatches(sw, part2.PartId);
                        App.Stopwatches.Add(stopwatch);
                        App.Stopwatches[App.Stopwatches.Count - 1].Start();
                        await _userService.SaveActivity(start);
                    });
                    alertNo.SetNegativeButton("Nie", async (s1, a1) =>
                    {
                        start.Status = StatusType.Pause;
                        await _userService.SaveActivity(start);
                    });
                    var dialogNo = alertNo.Create();
                    dialogNo.Show();
                });
                alert.SetPositiveButton("Tak", async (s, a) =>
                {
                    var part = await _userService.GetLastActivityPart(start.ActivityId);
                    part.Stop = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                    var time = (long)(DateTime.Now - DateTime.ParseExact(part.Start, "HH:mm:ss dd/MM/yyyy", null)).TotalMilliseconds;
                    part.Duration = time.ToString();
                    await _userService.SaveActivity(start);
                    await _userService.SavePartOfActivity(part);
                    var alertOk = new AlertDialog.Builder(this);
                    alertOk.SetTitle("Kontynuacja");
                    alertOk.SetMessage("Czy chcesz żeby aktywność była kontynuowana?");
                    alertOk.SetPositiveButton("Tak", async (s1, a1) =>
                    {
                        var part3 = new PartsOfActivityDto
                        {
                            Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                            ActivityId = start.ActivityId,
                            Duration = "0"
                        };
                        part3.PartId = await _userService.SavePartOfActivity(part3);
                        var sw2 = new Stopwatch();
                        var stopwatch2 = new Stopwatches(sw2, part3.PartId);
                        App.Stopwatches.Add(stopwatch2);
                        App.Stopwatches[App.Stopwatches.Count - 1].Start();
                    });
                    alertOk.SetNegativeButton("Nie", async (s1, a1) =>
                    {
                        start.Status = StatusType.Pause;
                        await _userService.SaveActivity(start);
                    });
                    var dialog = alertOk.Create();
                    dialog.Show();
                });
                var dialogOk = alert.Create();
                dialogOk.Show();
            }

            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App());
        }

        protected override void OnPause()
        {
            PauseActivities();
            StartService(new Intent(this, typeof(BackgroundStopwatches)));
            base.OnPause();
        }

        protected override void OnResume()
        {
            RestartActivities();
            StopService(new Intent(this, typeof(BackgroundStopwatches)));
            base.OnResume();
        }

        private async void PauseActivities()
        {
            if (App.Stopwatches.Count == 0)
            {
                return;
            }
            foreach (var item in App.Stopwatches)
            {
                var part = await _userService.GetPartsOfActivityById(item.GetPartId());
                part.Duration = item.GetTime().ToString();
                await _userService.SavePartOfActivity(part);
                item.Stop();
            }
        }

        private static void RestartActivities()
        {
            if (App.Stopwatches.Count == 0)
            {
                return;
            }
            foreach (var item in App.Stopwatches)
            {
                item.Restart();
            }
        }

    }
}

