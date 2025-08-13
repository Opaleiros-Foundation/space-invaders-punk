using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceInvaders.Models;

/// <summary>
/// Represents a player's score record in the game.
/// </summary>
public class Score
{
    /// <summary>
    /// Gets or sets the unique identifier for the score record.
    /// </summary>
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the player's score.
    /// </summary>
    [Required]
    public int PlayerScore { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the score was achieved.
    /// </summary>
    [Required]
    public DateTime DateAchieved { get; set; }
    
    /// <summary>
    /// Gets or sets the foreign key for the player associated with this score.
    /// </summary>
    public int? PlayerId { get; set; }
    /// <summary>
    /// Gets or sets the navigation property to the player associated with this score.
    /// </summary>
    public Player? Player { get; set; }
}
