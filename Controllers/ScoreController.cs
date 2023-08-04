using Microsoft.AspNetCore.Mvc;

namespace tetris_api.Controllers;

[Route("[controller]")]
public class ScoreController : ControllerBase
{
    private readonly UserContext context;

    public ScoreController(UserContext context) { this.context = context; }

    [HttpGet]
    public List<Score> Get()
    {
        var scores = this.context.Scores.ToList();
        return scores;
    }

    [Route("User/{UserId}")]
    [HttpGet]
    public List<Score> Get(int UserId)
    {
        var scores = this.context.Scores.Where(s => s.UserId == UserId).ToList();
        return scores;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Score score)
    {
        this.context.Scores.Add(score);
        this.context.SaveChanges();
        return Ok();
    }
}
