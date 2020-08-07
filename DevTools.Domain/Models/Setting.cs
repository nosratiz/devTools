namespace DevTools.Domain.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string GoogleAnalytic { get; set; }
        public string GoogleMaster { get; set; }
        public string CopyRight { get; set; }
        public string Email { get; set; }
        public string SocialMedia { get; set; }
    }
}