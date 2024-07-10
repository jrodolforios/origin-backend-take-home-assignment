using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.OriginAssignment.Infra.Models;

public class Car
{
    [Key]
    public int CarId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Make { get; set; }

    [Required]
    [MaxLength(50)]
    public string Model { get; set; }

    [Range(1886, 2100)]
    public int Year { get; set; }

    [ForeignKey("EngineId")]
    public Engine Engine { get; set; }
    public int EngineId { get; set; }

    public List<Wheel> Wheels { get; set; }

    [ForeignKey("InteriorId")]
    public Interior Interior { get; set; }
    public int InteriorId { get; set; }
}