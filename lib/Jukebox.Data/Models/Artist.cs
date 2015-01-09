using System;
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

        public Artist(string name) : this()
        {
            Name = name;
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

        public override bool Equals(object o)
        {
            return this.CeremoniallyEquals(o, theirs => 0 == string.Compare(Name, theirs.Name, StringComparison.CurrentCultureIgnoreCase));
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(Name.ToLower());
        }
    }
}