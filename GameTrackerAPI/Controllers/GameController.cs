using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameTrackerAPI.Models;
using GameTrackerAPI.DTOs;
using GameTrackerAPI.Services;

namespace GameTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _service;

        public GamesController(IGameService service)
        {
            _service = service;
        }

        /// <summary>
        /// Récupère la liste de tous les jeux (accessible sans authentification).
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Game>), 200)]
        public ActionResult<IEnumerable<Game>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        /// <summary>
        /// Récupère le détail d’un jeu par son identifiant (accessible sans authentification).
        /// </summary>
        /// <param name="id">Identifiant du jeu.</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Game), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Game> GetById(int id)
        {
            var game = _service.GetById(id);
            if (game == null)
                return NotFound();
            return Ok(game);
        }

        /// <summary>
        /// Crée un nouveau jeu dans la collection (JWT requis).
        /// </summary>
        /// <param name="dto">Données du jeu à créer.</param>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Game), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult<Game> Create([FromBody] GameDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var game = new Game
            {
                Title = dto.Title,
                Platform = dto.Platform,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate,
                IsCompleted = dto.IsCompleted
            };

            var created = _service.Create(game);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Met à jour un jeu existant et renvoie l’objet modifié (JWT requis).
        /// </summary>
        /// <param name="id">Identifiant du jeu à modifier.</param>
        /// <param name="dto">Nouvelles données du jeu.</param>
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Game), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult<Game> Update(int id, [FromBody] GameDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var toUpdate = new Game
            {
                Title = dto.Title,
                Platform = dto.Platform,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate,
                IsCompleted = dto.IsCompleted
            };

            if (!_service.Update(id, toUpdate))
                return NotFound();

            var updated = _service.GetById(id)!;
            return Ok(updated);
        }

        /// <summary>
        /// Supprime un jeu de la collection et retourne un message de confirmation (JWT requis).
        /// </summary>
        /// <param name="id">Identifiant du jeu à supprimer.</param>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Delete(int id)
        {
            var game = _service.GetById(id);
            if (game == null)
                return NotFound();

            _service.Delete(id);
            return Ok(new { message = $"Le jeu '{game.Title}' (id={id}) a bien été supprimé." });
        }
    }
}
