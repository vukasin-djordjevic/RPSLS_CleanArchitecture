using System.ComponentModel.DataAnnotations;

namespace Domain.GameResults;

public class GameResult
{
    [Key]
    public Guid Id { get; set; }
    public required string Results { get; set; }
    public int Player { get; set; }
    public int Computer { get; set; }
    public DateTime Created { get; set; }
}
