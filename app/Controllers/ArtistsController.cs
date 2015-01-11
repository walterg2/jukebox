using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Jukebox.Data;
using Jukebox.Data.Models;
using Jukebox.Models;

namespace Jukebox.Controllers
{
    public class ArtistsController : ApiController
    {
        private readonly SessionFactory _sessionFactory;

        public ArtistsController(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        [HttpGet]
        public IEnumerable<Artist> All()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryIndex<Track>("Artists")
                    .Select(x => x.Artist).Distinct().ToList()
                    .Select(x => new Artist { Name = x }).OrderBy(x => x.Name)
                    .ToList();
            }
        }
    }
}
