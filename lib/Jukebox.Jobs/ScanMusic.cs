using System.Collections.Generic;
using System.IO;
using Jukebox.Data;
using Jukebox.Data.Models;
using Jukebox.Jobs.Extensions;

namespace Jukebox.Jobs
{
    public class ScanMusic
    {
        private readonly SessionFactory _sessionFactory;

        public ScanMusic(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void PerformInitialScan(string folderPath)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                Directory.EnumerateFiles(folderPath, "*.mp3", SearchOption.AllDirectories).ForEach(x =>
                {
                    var track = TrackFrom(x);
                    session.Store(track);
                    using (var stream = File.OpenRead(x))
                    {
                        session.AddAttachment(track.Id, track.Title + ".mp3", stream);
                    }
                });

                session.SaveChanges();
            }
        }

        private static Track TrackFrom(string x)
        {
            var info = TrackInformation.For(x);
            return new Track(info.Artist, info.Album, info.Year, info.Title);
        }
    }
}
