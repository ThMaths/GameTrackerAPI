using GameTrackerAPI.Models;
using System.Collections.Generic;

namespace GameTrackerAPI.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Game Create(Game game);
        bool Update(int id, Game updated);
        bool Delete(int id);
    }
}
