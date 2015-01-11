using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Jukebox.Data;
using Jukebox.Data.Models;
using Jukebox.Models;

namespace Jukebox.Controllers
{
    public class AlbumsController : ApiController
    {
        private readonly SessionFactory _sessionFactory;

        public AlbumsController(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        [HttpGet]
        public IEnumerable<Album> All()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<Track>()
                    .Select(x => new Album { Artist = x.Artist, Title = x.Album })
                    .ToList();
            }
        }
    }
}
