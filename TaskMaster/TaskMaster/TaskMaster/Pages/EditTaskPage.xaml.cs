using System;
using System.Threading.Tasks;
using System.Timers;
using TaskMaster.Enums;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Services;

namespace TaskMaster.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage
    {
        private PartsOfActivityDto _part;
        private ActivitiesDto _activity;
        private TasksDto _task;
        private long _duration;
        private long _startTime;
        private readonly Timer _timer = new Timer();
        private readonly MainPageListItem _initItem;
        public EditTaskPage(MainPageListItem item)
        {
            InitializeComponent();
            _initItem = item;
            ActivityName.Text = item.Name;
            ActivityDescription.Text = item.Description;
            TaskName.Text = item.Name;
            TaskDescription.Text = item.Description;
            TypePickerImage.Source = "OK.png";
            AddItemsToPicker();
        }

        private void AddItemsToPicker()
        {
            TypePicker.Items.Add("Sztuka");
            TypePicker.Items.Add("Inne");
            TypePicker.Items.Add("Programowanie");
            TypePicker.Items.Add("Sport");
            TypePicker.Items.Add("Muzyka");
            TypePicker.Items.Add("Języki");
            TypePicker.Items.Add("Jedzenie");
            TypePicker.Items.Add("Rozrywka");
            TypePicker.Items.Add("Podróż");
            TypePicker.Items.Add("Przerwa");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Initial();
            await AddToFavoritesList();
        }

        private async Task AddToFavoritesList()
        {
            var user = UserService.Instance.GetLoggedUser();
            var favorites = await UserService.Instance.GetUserFavorites(user.UserId);
            if (favorites == null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    FavoritePicker.IsVisible = false;
                    FavText.IsVisible = false;
                });
            }
            else
            {
                foreach (var item in favorites)
                {
                    var task = await UserService.Instance.GetTaskById(item.TaskId);
                    FavoritePicker.Items.Add(task.Name);
                }
            }

        }
        private async Task Initial()
        {
            _activity = await UserService.Instance.GetActivity(_initItem.ActivityId);
            _part = await UserService.Instance.GetLastActivityPart(_activity.ActivityId);
            if (_initItem.TaskId != 0)
            {
                _task = await UserService.Instance.GetTaskById(_activity.TaskId);
            }
            else
            {
                _task = new TasksDto
                {
                    Name = _initItem.Name
                };
            }
            TaskDate.Text = _part.Start;
            _startTime = _initItem.Time;
            var t = TimeSpan.FromMilliseconds(_startTime + StopwatchesService.Instance.GetStopwatchTime(_part.PartId));
            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
            TaskDuration.Text = answer;
            UpdateButtons();
            _timer.Elapsed += UpdateTime;
            _timer.Interval = 1000;
            _timer.Start();
        }

        private void UpdateTime(object source, ElapsedEventArgs e)
        {
            if (_activity.Status != StatusType.Start)
            {
                return;
            }
            _duration = _startTime + StopwatchesService.Instance.GetStopwatchTime(_part.PartId);
            var t = TimeSpan.FromMilliseconds(_duration);
            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
            Device.BeginInvokeOnMainThread(() =>
            {
                TaskDuration.Text = answer;
            });            
        }

        private void UpdateButtons()
        {
            PauseButton.IsVisible = _activity.Status == StatusType.Start;
            ResumeButton.IsVisible = _activity.Status == StatusType.Pause;
            StopButton.IsVisible = _activity.Status != StatusType.Planned;
        }
        private async void StopButton_OnClicked(object sender, EventArgs e)
        {
            _timer.Stop();
            StopwatchesService.Instance.StopStopwatch(_part.PartId);
            _part.Stop = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            _part.Duration = StopwatchesService.Instance.GetStopwatchTime(_part.PartId).ToString();
            _activity.Status = StatusType.Stop;
            await UserService.Instance.SaveActivity(_activity);
            await UserService.Instance.SavePartOfActivity(_part);
            if (_task.TaskId == 0)
            {
                await Navigation.PushModalAsync(new FillInformationPage(_activity));
            }
            else
            {
                _task.TaskId = await UserService.Instance.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await UserService.Instance.SaveActivity(_activity);
                await SynchronizationService.Instance.SendActivity(_activity,_task);
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
        }

        private async void PauseButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Pause;
            _part.Stop = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            StopwatchesService.Instance.StopStopwatch(_part.PartId);
            _part.Duration = StopwatchesService.Instance.GetStopwatchTime(_part.PartId).ToString();
            _startTime += StopwatchesService.Instance.GetStopwatchTime(_part.PartId);
            await UserService.Instance.SaveActivity(_activity);
            await UserService.Instance.SavePartOfActivity(_part);
            UpdateButtons();
            _timer.Stop();
        }

        private async void ResumeButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Start;
            var part = new PartsOfActivityDto
            {
                ActivityId = _activity.ActivityId,
                Start = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                Stop = "",
                Duration = "0"
            };
            part.PartId = await UserService.Instance.SavePartOfActivity(part);
            StopwatchesService.Instance.AddStopwatch(part.PartId);
            _part = part;
            await UserService.Instance.SaveActivity(_activity);
            _timer.Start();
            UpdateButtons();
        }

        private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskDescription.Text = ActivityDescription.Text;
            _activity.Comment = ActivityDescription.Text;
        }

        private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskName.Text = ActivityName.Text;
            _task.Name = ActivityName.Text;
        }

        private async void AcceptButton_OnClicked(object sender, EventArgs e)
        {
            if (_task.TaskId == 0)
            {
                if (TaskName.Text == $"Nowa Aktywność {_activity.ActivityId}")
                {
                    return;
                }
                _task.TaskId = await UserService.Instance.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await UserService.Instance.SaveActivity(_activity);
                await Navigation.PopModalAsync();
            }
            else
            {
                _task.TaskId = await UserService.Instance.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await UserService.Instance.SaveActivity(_activity);
                await Navigation.PopModalAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {            
            if (_task.Name == ActivityName.Text)
            {
                _timer.Stop();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                });
                return true;
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Error", "Niezapisane dane zostaną utracone. Czy kontynuować",
                    "Tak", "Nie");
                if (!result)
                {
                    return;
                }
                _timer.Stop();
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            });
            return true;
        }

        private async void AddFavorite_OnClicked(object sender, EventArgs e)
        {
            if (_task.TaskId == 0)
            {
                await DisplayAlert("Error", "Nie możesz dodać do ulubionych nienazwanego tasku", "Ok");
            }
            else
            {
                var favorite = new FavoritesDto
                {
                    TaskId = _task.TaskId,
                    UserId = _activity.UserId
                };
                await UserService.Instance.SaveFavorite(favorite);
                if (_task.SyncStatus == SyncStatus.ToUpload)
                {
                    await SynchronizationService.Instance.SendTask(_task);
                }
                await SynchronizationService.Instance.SendFavorite(favorite);
                AddFavorite.IsEnabled = false;
            }
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var typ = TypePicker.Items[TypePicker.SelectedIndex];
            TypePickerImage.Source = SelectImage(typ);
            _task.Typ = typ;
        }

        private static string SelectImage(string item)
        {
            string type;
            switch (item)
            {
                case "Sztuka":
                    type = "art.png";
                    break;
                case "Inne":
                    type = "OK.png";
                    break;
                case "Programowanie":
                    type = "programming.png";
                    break;
                case "Sport":
                    type = "sport.png";
                    break;
                case "Muzyka":
                    type = "music.png";
                    break;
                case "Języki":
                    type = "language.png";
                    break;
                case "Jedzenie":
                    type = "eat.png";
                    break;
                case "Rozrywka":
                    type = "instrument.png";
                    break;
                case "Podróż":
                    type = "car.png";
                    break;
                case "Przerwa":
                    type = "Cafe.png";
                    break;
                default:
                    type = "OK.png";
                    break;
            }
            return type;
        }

        private async void FavoritePicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var select = FavoritePicker.Items[FavoritePicker.SelectedIndex];
            var taskDto = new TasksDto
            {
                Name = select
            };
            var task = await UserService.Instance.GetTask(taskDto);
            TaskName.Text = task.Name;
            ActivityName.Text = task.Name;
            TypePickerImage.Source = SelectImage(task.Typ);
        }
    }
}