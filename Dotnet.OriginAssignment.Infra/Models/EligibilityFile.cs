using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.OriginAssignment.Infra.Models;

public class EligibilityFile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string FileUrl { get; set; }

    [Required]
    [MaxLength(255)]
    public string EmployerId { get; set; }
}