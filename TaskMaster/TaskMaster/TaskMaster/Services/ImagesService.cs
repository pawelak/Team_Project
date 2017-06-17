namespace TaskMaster.Services
{
    public class ImagesService
    {
        private static ImagesService _instance;
        private ImagesService() {}
        public static ImagesService Instance => _instance ?? (_instance = new ImagesService());

        public string SelectImage(string item)
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
    }
}
