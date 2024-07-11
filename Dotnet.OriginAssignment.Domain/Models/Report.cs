using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models
{
    public class Report
    {
        public List<ProcessedLineReport> ProcessedLines { get; set; } = new List<ProcessedLineReport>();
        public List<ProcessedLineReport> UnprocessedLines { get; set; } = new List<ProcessedLineReport>();
    }


}
