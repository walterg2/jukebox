namespace Jukebox.Data.Models
{
    public class Track
    {
        public string Id { get; protected set; }
        public string Artist { get; set; }
        public string[] Artists { get; set; }
        public string Album { get; set; }
        public uint Year { get; set; }
        public string Title { get; set; }
        public uint TrackNumber { get; set; }
        public string[] Genres { get; set; }
        public string FileName { get; set; }
    }
}