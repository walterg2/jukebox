using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IEnumerable<ArtistAlbum> Get(string id)
        {
            var artist = Encoding.UTF8.GetString(Convert.FromBase64String(id));

            using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryIndex<Track>("Albums")
                    .Where(x => x.Artist == artist)
                    .Select(Mapper.Map<ArtistAlbum>)
                    .OrderBy(x => x.Album)
                    .ToList();
            }
        }
    }
}
