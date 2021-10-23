namespace NavManagerExample.Client.Models
{
    public class Navigation
    {
        public bool IsCanceled { get; set; }

        public string NewLocation { get; set; }

        public string CurrentLocation { get; set; }

        public bool IsNavigationIntercepted { get; set; }
    }
}
