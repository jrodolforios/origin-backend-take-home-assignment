using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models
{
    public class EligibilityFileEntry
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Salary { get; set; }
    }
}
