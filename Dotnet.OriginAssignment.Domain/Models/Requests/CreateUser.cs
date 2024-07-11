using Dotnet.OriginAssignment.Domain.Models.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models.Requests
{
    public class CreateUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public AccessType AccessType { get; set; }
        public string FullName { get; set; }
        public string EmployerId { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Salary { get; set; }
    }
}
