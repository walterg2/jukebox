using System;

namespace Jukebox.Data.Models
{
    public class Track
    {
        public Track(string artist, string album, string year, string title)
        {
            Artist = artist;
            Album = album;
            Year = YearFrom(year);
            Title = title;
        }

        public string Id { get; protected set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }


        private static int YearFrom(string year)
        {
            try
            {
                return (int) Convert.ChangeType(year, typeof(int));
            }
            catch (FormatException)
            {
                return 0;
            }
        }

    }
}