using System.Collections.Generic;
using System.Linq;

namespace Jukebox.Data.Models
{
    public class Artist
    {
        public Artist()
        {
            Albums = new List<Album>();
        }

        public string Id { get; protected set; }
        public string Name { get; set; }
        public List<Album> Albums { get; protected set; }

        public Album AlbumFor(string name)
        {
            return Albums.FirstOrDefault(x => x.Title == name) ?? AddAlbum(name);
        }

        private Album AddAlbum(string name)
        {
            Albums.Add(new Album { Title = name });
            return Albums.Last();
        }
    }
}