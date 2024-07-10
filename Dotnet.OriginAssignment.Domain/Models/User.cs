using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string AccessType { get; set; } // "dtc" or "employer"
        public string FullName { get; set; }
        public string EmployerId { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Salary { get; set; }
    }

}
