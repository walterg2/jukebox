using System.IO;
using System.Linq;
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
                if (0 != session.Query<Track>().Count())
                {
                    return;
                }

                Directory.EnumerateFiles(folderPath, "*.mp3", SearchOption.AllDirectories).ForEach(x =>
                {
                    var track = TrackFromFile(x);
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
                    var fileName = string.IsNullOrEmpty(title) ? Path.GetFileName(path) : title + Path.GetExtension(path);
                    session.AddAttachment(track, fileName, stream);
                }
            }
        }

        private static Track TrackFromFile(string path)
        {
            using (var file = TagLib.File.Create(path))
            {
                return new Track
                {
                    Artist = file.Tag.FirstAlbumArtist,
                    Artists = file.Tag.AlbumArtists,
                    Album = file.Tag.Album,
                    TrackNumber = file.Tag.Track,
                    Title = file.Tag.Title,
                    Genres = file.Tag.Genres,
                    Year = file.Tag.Year,
                };
            }
        }
    }
}
