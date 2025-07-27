using GameTrackerAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameTrackerAPI.Services
{
    public class GameService : IGameService
    {
        private readonly List<Game> _games = new();
        private int _nextId = 1;

        public IEnumerable<Game> GetAll()
        {
            return _games;
        }

        public Game? GetById(int id)
        {
            return _games.FirstOrDefault(g => g.Id == id);
        }

        public Game Create(Game game)
        {
            game.Id = _nextId++;
            _games.Add(game);
            return game;
        }

        public bool Update(int id, Game updated)
        {
            var existing = GetById(id);
            if (existing == null)
                return false;

            // Mise à jour des champs
            existing.Title = updated.Title;
            existing.Platform = updated.Platform;
            existing.Genre = updated.Genre;
            existing.ReleaseDate = updated.ReleaseDate;
            existing.IsCompleted = updated.IsCompleted;
            return true;
        }

        public bool Delete(int id)
        {
            var game = GetById(id);
            if (game == null)
                return false;

            _games.Remove(game);
            return true;
        }
    }
}
