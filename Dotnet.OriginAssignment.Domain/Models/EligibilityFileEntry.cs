using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models
{
    public class EligibilityFileRequest
    {
        public string FileUrl { get; set; }
        public string EmployerName { get; set; }
    }
}
