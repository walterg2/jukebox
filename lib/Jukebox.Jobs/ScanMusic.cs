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
                Directory.EnumerateFiles(folderPath, "*.mp3", SearchOption.AllDirectories).ForEach(x => Add(x, session));

                _artists.ForEach(session.Store);
                session.SaveChanges();
            }
        }

        private void Add(string filePath, Session session)
        {
            var info = TrackInformation.For(filePath);

            _artists.AddOrUpdate(new Artist(info.Artist))
                .Albums.AddOrUpdate(new Album(info.Album))
                .Tracks.AddOrUpdate(new Track(info.Title));

            session.AddAttachment(info.Path, info.FileName, File.OpenRead(filePath));
        }
    }
}
