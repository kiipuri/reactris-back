using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Services;

namespace tetris_api.Controllers;

[Authorize]
[Route("[controller]")]
public class ScoreController : ControllerBase
{
    private readonly UserContext context;

    public ScoreController(UserContext context) { this.context = context; }

    [AllowAnonymous]
    [HttpGet]
    public List<Score> Get()
    {
        var scores = this.context.Scores.Include(s => s.User).ToList();
        return scores;
    }

    [AllowAnonymous]
    [Route("User/{userIdStr}")]
    [HttpGet]
    public List<Score> Get(string userIdStr)
    {
        var userId = Guid.Parse(userIdStr);
        int userFK = this.context.Users.Single(u => u.UserId == userId).Id;
        var scores = this.context.Scores.Where(s => s.UserId == userFK).ToList();
        return scores;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Score score)
    {
        var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        score.UserId = this.context.Users.First(u => u.Id == userId).Id;
        score.ScoreId = Guid.NewGuid();

        this.context.Scores.Add(score);
        this.context.SaveChanges();
        return Ok();
    }

    [AllowAnonymous]
    [Route("Highscores")]
    [HttpGet]
    public List<Score> Highscores()
    {
        var scores =
            this.context.Scores.Include(s => s.User)
                .GroupBy(s => s.UserId)
                .Select(group =>
                            group.OrderByDescending(s => s.PlayerScore).First())
                .ToList()
                .OrderByDescending(s => s.PlayerScore)
                .ToList();
        return scores;
    }
}
