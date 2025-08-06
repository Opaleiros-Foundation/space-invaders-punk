using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceInvaders.Models;

public class Score
{
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int PlayerScore { get; set; }
    
    [Required]
    public DateTime DateAchieved { get; set; }
    
    // Optional: Link to a Player if you have a Player model
    public int? PlayerId { get; set; }
    public Player? Player { get; set; }
}
