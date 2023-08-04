using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace tetris_api.Controllers;

[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserContext context;

    public UserController(UserContext context) { this.context = context; }

    [HttpGet]
    public List<User> Get()
    {
        var users = this.context.Users.ToList();
        return users;
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        this.context.Users.Add(user);
        this.context.SaveChanges();
        return Ok();
    }
}
