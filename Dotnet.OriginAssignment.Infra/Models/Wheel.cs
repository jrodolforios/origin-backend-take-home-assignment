using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.OriginAssignment.Infra.Models;


public class Wheel
{
    [Key]
    public int WheelId { get; set; }

    [Range(1, 50)]
    public int Size { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; }

    [ForeignKey("CarId")]
    public int CarId { get; set; }
    public Car Car { get; set; }
}