using System.ComponentModel.DataAnnotations;

namespace Dotnet.OriginAssignment.Infra.Models;


public class Interior
{
    [Key]
    public int InteriorId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Material { get; set; }

    [Required]
    [MaxLength(50)]
    public string Color { get; set; }
}