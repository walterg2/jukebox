using System;

namespace Jukebox.Data.Models
{
    public class Track
    {
        public Track(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

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