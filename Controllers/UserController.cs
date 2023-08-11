using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace tetris_api.Controllers;

[Authorize]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private readonly UserContext context;

    public UserController(UserContext context, IUserService userService)
    {
        this.context = context;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    // public ActionResult<User> GetAll()
    {
        var users = await _userService.GetAll();
        // var users = this.context.Users.ToList();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        this.context.Users.Add(user);
        this.context.SaveChanges();
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult>
    Authenticate([FromBody] UsernameAndPassword model)
    {
        var user =
            await _userService.Authenticate(model.Username, model.HashedPassword);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(user);
    }
}
