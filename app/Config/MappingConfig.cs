using AutoMapper;
using Jukebox.Data.Models;
using Jukebox.Models;

namespace Jukebox.Config
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Track, ArtistAlbum>();
        }
    }
}