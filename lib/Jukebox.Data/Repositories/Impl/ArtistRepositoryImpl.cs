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

        private Session Session
        {
            get { return _context.CurrentSession; }
        }
    }
}