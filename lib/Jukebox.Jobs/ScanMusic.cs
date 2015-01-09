using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using ID3TagLibrary;
using Jukebox.Data;
using Jukebox.Data.Models;
using Jukebox.Jobs.Extensions;

namespace Jukebox.Jobs
{
    public class ScanMusic
    {
        private readonly SessionFactory _sessionFactory;
        private readonly List<Artist> _artists;

        public ScanMusic(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _artists = new List<Artist>();
        }

        public void PerformInitialScan(string folderPath)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                Directory.EnumerateFiles(folderPath, "*.mp3", SearchOption.AllDirectories).ForEach(Add);

                _artists.ForEach(session.Store);
                session.SaveChanges();
            }
        }

        private void Add(string filePath)
        {
            var info = TrackInformation.For(filePath);

            _artists.AddOrUpdate(new Artist(info.Artist))
                .Albums.AddOrUpdate(new Album(info.Album))
                .Tracks.AddOrUpdate(new Track(info.Title));
        }
    }

    public class TrackInformation
    {
        private readonly MP3File _id3;

        private TrackInformation(string filePath)
        {
            _id3 = new MP3File(filePath);
        }

        public static TrackInformation For(string filePath)
        {
            return new TrackInformation(filePath);
        }

        public string Artist { get { return _id3.Artist; } }
        public string Album { get { return _id3.Album; } }
        public string Year { get { return _id3.Year; } }
        public string Title { get { return _id3.Title; } }
    }

    static class EnumerableExtensions
    {
        public static T AddOrUpdate<T>(this IList<T> list, T item) where T : class
        {
            var found = list.FirstOrDefault(x => x.Equals(item));
            return found ?? Add(list, item);
        }

        private static T Add<T>(ICollection<T> collection, T item)
        {
            collection.Add(item);
            return item;
        }
    }
}
