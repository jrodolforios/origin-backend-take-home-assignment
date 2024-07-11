using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models
{
    public class ProcessedLineReport
    {
        public string Email { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
