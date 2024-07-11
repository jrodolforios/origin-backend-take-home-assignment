using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.OriginAssignment.Infra.Models;

public class ProcessedLine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    public bool Success { get; set; }

    [MaxLength(255)]
    public string ErrorMessage { get; set; }

    [ForeignKey("EligibilityFile")]
    public string EligibilityFileId { get; set; }

    public EligibilityFile EligibilityFile { get; set; }
}