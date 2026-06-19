namespace ChessAcademy.Models;

public class Game
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public string OpponentName { get; set; } = string.Empty;
    public string OpponentNationality { get; set; } = string.Empty;
    public string Result { get; set; } = "Loss"; // Win, Draw, Loss
    public string Opening { get; set; } = string.Empty;
}