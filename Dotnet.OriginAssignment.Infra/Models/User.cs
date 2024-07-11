using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.OriginAssignment.Infra.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }

    [Required]
    [MaxLength(2)]
    public string Country { get; set; }

    [Required]
    [MaxLength(50)]
    public string AccessType { get; set; } // "dtc" or "employer"

    [MaxLength(255)]
    public string FullName { get; set; }

    [MaxLength(255)]
    public string EmployerId { get; set; }

    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salary { get; set; }
}