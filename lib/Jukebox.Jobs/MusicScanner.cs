using System.IO;
using System.Linq;
using System.Reflection;
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

                    BackgroundJob.Enqueue(() => Upload(track.Id, track.Title, track.FileName, x));
                });

                session.SaveChanges();
            }
        }

        public void Upload(string track, string title, string fileName,  string path)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var stream = File.OpenRead(path))
                {
                    session.AddAttachment(track, fileName, stream);
                }
            }
        }

        private static Track TrackFromFile(string path)
        {
            using (var file = TagLib.File.Create(path))
            {
                var tag = file.Tag;
                return new Track
                {
                    Artist = tag.FirstAlbumArtist,
                    Artists = tag.AlbumArtists,
                    Album = tag.Album,
                    TrackNumber = tag.Track,
                    Title = tag.Title,
                    Genres = tag.Genres,
                    Year = tag.Year,
                    FileName = string.IsNullOrEmpty(tag.Title) 
                                    ? Path.GetFileName(path) 
                                    : tag.Title + Path.GetExtension(path)
                };
            }
        }
    }
}
