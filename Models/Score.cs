using System.ComponentModel.DataAnnotations.Schema;

namespace tetris_api;

public class Score
{
    public int Id { get; set; }

    public int ScoreId { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }

    public int PlayerScore { get; set; }
}
