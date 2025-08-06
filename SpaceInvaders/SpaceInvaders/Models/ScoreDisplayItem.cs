namespace SpaceInvaders.Models;

public class ScoreDisplayItem
{
    public int Rank { get; set; }
    public string PlayerName { get; set; }
    public int PlayerScore { get; set; }
    public string DateAchievedFormatted { get; set; }

    public string RankDisplay
    {
        get
        {
            return Rank switch
            {
                1 => "🥇 1st",
                2 => "🥈 2nd",
                3 => "🥉 3rd",
                _ => Rank.ToString()
            };
        }
    }
}
