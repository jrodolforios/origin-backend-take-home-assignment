using System.ComponentModel.DataAnnotations;

namespace Dotnet.OriginAssignment.Infra.Models;


public class Engine
{
    [Key]
    public int EngineId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; }

    [Range(1, 2000)]
    public int Horsepower { get; set; }
}