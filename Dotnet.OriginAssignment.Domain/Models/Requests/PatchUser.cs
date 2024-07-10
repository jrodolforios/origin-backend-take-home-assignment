using Swashbuckle.AspNetCore.Annotations;

namespace Dotnet.OriginAssignment.Domain.Models.Models
{
    public class PatchUser
    {
        public List<Patch> Changes { get; set; } = new List<Patch>();
    }
}
