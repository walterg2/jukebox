using System;
using System.Collections.Generic;

namespace Jukebox.Data.Models
{
    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
        }

        public Album(string title) : this()
        {
            Title = title;
        }

        public string Title { get; set; }
        public List<Track> Tracks { get; protected set; }

        public override bool Equals(object o)
        {
            return this.CeremoniallyEquals(o, theirs => 0 == string.Compare(Title, theirs.Title, StringComparison.CurrentCultureIgnoreCase));
        }

        public override int GetHashCode()
        {
            return this.CombinedHashCodes(Title.ToLower());
        }
    }
}