namespace SpaceInvaders.Models;

/// <summary>
/// Represents an item for displaying scores in a leaderboard or score list.
/// </summary>
public class ScoreDisplayItem
{
    /// <summary>
    /// Gets or sets the rank of the score in the leaderboard.
    /// </summary>
    public int Rank { get; set; }
    /// <summary>
    /// Gets or sets the name of the player who achieved the score.
    /// </summary>
    public string PlayerName { get; set; }
    /// <summary>
    /// Gets or sets the player's score.
    /// </summary>
    public int PlayerScore { get; set; }
    /// <summary>
    /// Gets or sets the formatted date when the score was achieved.
    /// </summary>
    public string DateAchievedFormatted { get; set; }

    /// <summary>
    /// Gets a formatted string representing the rank, including special emojis for top ranks.
    /// </summary>
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
