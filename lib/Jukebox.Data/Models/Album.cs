using System.Collections.Generic;
using System.Linq;

namespace Jukebox.Data.Models
{
    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
        }

        public string Title { get; set; }
        public List<Track> Tracks { get; protected set; }

        public Track TrackFor(string title)
        {
            return Tracks.FirstOrDefault(x => x.Name == title) ?? AddTrack(title);
        }

        private Track AddTrack(string title)
        {
            Tracks.Add(new Track { Name = title });
            return Tracks.Last();
        }
    }
}