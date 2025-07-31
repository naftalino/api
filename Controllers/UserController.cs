using gacha.Dto;
using gacha.Models;
using gacha.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gacha.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _service.GetAll();
            return Ok(new { totalItems = users.Count, items = users });
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(long id)
        {
            var user = _service.Get(id);
            if (user == null)
            {
                return NotFound(new { error = "Usuário não encontrado." });
            }

            Console.WriteLine(user.FavoriteCard);
            return Ok(new User
            {
                Id = user.Id,
                FavoriteCard = user.FavoriteCard,
            });
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto user)
        {
            var created = _service.Create(user);
            return Created($"/user/{created.Id}", created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody] UpdateUserDto updatedUser, long id)
        {
            try
            {
                var result = _service.Update(updatedUser, id);
                if (result == null)
                {
                    return NotFound(new { error = "Usuário não encontrado para atualização." });
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    erro = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            var deleted = _service.Delete(id);
            if (!deleted)
                return NotFound(new { error = "Usuário não encontrado para exclusão." });

            return Ok(new { message = "Usuário removido com sucesso." });
        }
    }
}
