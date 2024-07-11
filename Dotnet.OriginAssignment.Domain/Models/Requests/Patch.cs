using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models.Requests
{
    public class Patch
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }
}
