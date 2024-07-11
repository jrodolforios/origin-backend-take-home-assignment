using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.OriginAssignment.Infra.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Report
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [ForeignKey("EligibilityFile")]
    public string EligibilityFileId { get; set; }

    public EligibilityFile EligibilityFile { get; set; }

    public ICollection<ProcessedLine> ProcessedLines { get; set; }
    public ICollection<ProcessedLine> UnprocessedLines { get; set; }
}