using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models.Models
{
    public class Signup
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
    }
}
