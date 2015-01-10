using System.Collections.Generic;
using System.IO;
using Hangfire;
using Jukebox.Data;
using Jukebox.Data.Models;
using Jukebox.Jobs.Extensions;

namespace Jukebox.Jobs
{
    public class MusicScanner
    {
        private readonly SessionFactory _sessionFactory;

        public MusicScanner(SessionFactory sessionFactory)
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

                    BackgroundJob.Enqueue(() => Upload(track.Id, track.Title, x));
                });

                session.SaveChanges();
            }
        }

        public void Upload(string track, string title, string path)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var stream = File.OpenRead(path))
                {
                    session.AddAttachment(track, title + Path.GetExtension(path), stream);
                }
            }
        }

        private static Track TrackFrom(string x)
        {
            var info = TrackInformation.For(x);
            return new Track(info.Artist, info.Album, info.Year, info.Title);
        }
    }
}
