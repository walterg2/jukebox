using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
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
        public IEnumerable<ArtistAlbum> All()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryIndex<Track>("Albums")
                    .Select(Mapper.Map<ArtistAlbum>)
                    .OrderBy(x => x.Artist).ThenBy(x => x.Album)
                    .ToList();
            }
        }
    }
}
