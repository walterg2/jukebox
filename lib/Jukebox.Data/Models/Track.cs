namespace Jukebox.Data.Models
{
    public class Track
    {
        public Track(string artist, string album, string year, string title)
        {
            Artist = artist;
            Album = album;
            Year = year;
            Title = title;
        }

        public string Id { get; protected set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Year { get; set; }
        public string Title { get; set; }
    }
}