using System;
using System.Linq;
using TaskMaster.ModelsDto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskMaster.Services;

namespace TaskMaster.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTaskPage
    {
        private bool _isPageNotChanged = true;
        private PartsOfActivityDto _part;
        private ActivitiesDto _activity;
        private TasksDto _task;
        private DateTime _now;
        private long _duration;
        private long _startTime;
        public EditTaskPage(MainPageList item)
        {
            InitializeComponent();
            Initial(item);
            ActivityName.Text = item.Name;
            ActivityDescription.Text = item.Description;
            TaskName.Text = item.Name;
            TaskDescription.Text = item.Description;
            TypePickerImage.Source = "OK.png";
            AddItemsToPicker();
            AddToFavoritesList();
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

        private async void AddToFavoritesList()
        {
            var favorites = await UserService.Instance.GetUserFavorites(1);
            if (favorites == null)
            {
                FavoritePicker.IsEnabled = false;
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
        private async void Initial(MainPageList item)
        {
            _activity = await UserService.Instance.GetActivity(item.ActivityId);
            _part = await UserService.Instance.GetLastActivityPart(_activity.ActivityId);
            _task = new TasksDto
            {
                Name = item.Name,
                Description = item.Description,
                TaskId = item.TaskId,
            };
            TaskDates.Text = _part.Start;
            TaskDate.Text = _part.Start;
            var parts = await UserService.Instance.GetPartsOfActivityByActivityId(_activity.ActivityId);
            _startTime = parts.Sum(part => long.Parse(part.Duration));
            var t = TimeSpan.FromMilliseconds(_duration);
            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
            TaskDuration.Text = answer;
            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
            UpdateButtons();
        }

        private bool UpdateTime()
        {
            if (_activity.Status != StatusType.Start)
            {
                return false;
            }
            _duration = _startTime + StopwatchesService.Instance.GetStopwatchTime(_part.PartId);
            var t = TimeSpan.FromMilliseconds(_duration);
            var answer = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s";
            TaskDuration.Text = answer;
            return _isPageNotChanged;
        }

        private void UpdateButtons()
        {
            PauseButton.IsEnabled = _activity.Status == StatusType.Start;
            ResumeButton.IsEnabled = _activity.Status == StatusType.Pause;
            StopButton.IsEnabled = _activity.Status != StatusType.Planned;
        }
        private async void StopButton_OnClicked(object sender, EventArgs e)
        {
            _isPageNotChanged = false;
            StopwatchesService.Instance.StopStopwatch(_part.PartId);
            _now = DateTime.Now;
            _part.Stop = _now.ToString("HH:mm:ss dd/MM/yyyy");
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
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
        }

        private async void PauseButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Pause;
            _now = DateTime.Now;
            var date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            _part.Stop = date;
            StopwatchesService.Instance.StopStopwatch(_part.PartId);
            _part.Duration = StopwatchesService.Instance.GetStopwatchTime(_part.PartId).ToString();
            _startTime += StopwatchesService.Instance.GetStopwatchTime(_part.PartId);
            await UserService.Instance.SaveActivity(_activity);
            await UserService.Instance.SavePartOfActivity(_part);
            UpdateButtons();
        }

        private async void ResumeButton_OnClicked(object sender, EventArgs e)
        {
            _activity.Status = StatusType.Start;
            _now = DateTime.Now;
            var date = _now.ToString("HH:mm:ss dd/MM/yyyy");
            var part = new PartsOfActivityDto
            {
                ActivityId = _activity.ActivityId,
                Start = date,
                Duration = "0",
            };
            part.PartId = await UserService.Instance.SavePartOfActivity(part);
            StopwatchesService.Instance.AddStopwatch(part.PartId);
            _part = part;
            await UserService.Instance.SaveActivity(_activity);
            Device.StartTimer(TimeSpan.FromSeconds(1), UpdateTime);
            UpdateButtons();
        }

        private void ActivityDescription_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskDescription.Text = ActivityDescription.Text;
            _task.Description = ActivityDescription.Text;
        }

        private void ActivityName_OnUnfocused(object sender, FocusEventArgs e)
        {
            TaskName.Text = ActivityName.Text;
            _task.Name = ActivityName.Text;
        }

        private async void AcceptButton_OnClicked(object sender, EventArgs e)
        {
            _isPageNotChanged = false;
            if (_task.TaskId == 0)
            {
                await Navigation.PushModalAsync(new FillInformationPage(_activity));
            }
            else
            {
                _task.TaskId = await UserService.Instance.SaveTask(_task);
                _activity.TaskId = _task.TaskId;
                await UserService.Instance.SaveActivity(_activity);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (_task.Name == ActivityName.Text)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _isPageNotChanged = false;
                    await Navigation.PopModalAsync();
                });
                return true;
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Error", "Niezapisane dane zostaną utracone. Czy kontynuować",
                    "Tak", "Nie");
                if (!result) return;
                _isPageNotChanged = false;
                await Navigation.PopModalAsync();
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
            _task = await UserService.Instance.GetTask(taskDto);
            TaskName.Text = _task.Name;
            TaskDescription.Text = _task.Description;
            TypePickerImage.Source = SelectImage(_task.Typ);
        }
    }
}
