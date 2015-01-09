using Jukebox.Data.Models;

namespace Jukebox.Data.Repositories
{
    public interface ArtistRepository
    {
        Artist FindByName(string artistName);
        Artist Add(string name);
    }
}
