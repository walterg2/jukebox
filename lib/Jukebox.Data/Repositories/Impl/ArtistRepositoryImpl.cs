using System.Linq;
using Jukebox.Data.Models;

namespace Jukebox.Data.Repositories.Impl
{
    public class ArtistRepositoryImpl : ArtistRepository
    {
        private readonly SessionContext _context;

        public ArtistRepositoryImpl(SessionContext context)
        {
            _context = context;
        }

        public Artist FindByName(string artistName)
        {
            return Session.Query<Artist>()
                .FirstOrDefault(x => x.Name == artistName);
        }

        public Artist Add(string artist)
        {
            return Session.Create(new Artist { Name = artist });
        }

        private Session Session
        {
            get { return _context.CurrentSession; }
        }
    }
}