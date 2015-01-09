using System.IO;
using ID3TagLibrary;
using Jukebox.Data;
using Jukebox.Data.Repositories;
using Jukebox.Jobs.Extensions;

namespace Jukebox.Jobs
{
    public class ScanMusic
    {
        private readonly SessionFactory _sessionFactory;
        private readonly ArtistRepository _artistRepository;

        public ScanMusic(SessionFactory sessionFactory, ArtistRepository artistRepository)
        {
            _sessionFactory = sessionFactory;
            _artistRepository = artistRepository;
        }

        public void ScanFolder(string folderPath)
        {
            Directory.EnumerateFiles(folderPath, "*.mp3", SearchOption.AllDirectories).ForEach(AddOrIgnore);
        }

        private void AddOrIgnore(string filePath)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var info = TrackInformation.For(filePath);
                var artist = _artistRepository.FindByName(info.Artist) ?? _artistRepository.Add(info.Artist);
                var album = artist.AlbumFor(info.Album);
                album.TrackFor(info.Title);
                session.SaveChanges();
            }
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
}
