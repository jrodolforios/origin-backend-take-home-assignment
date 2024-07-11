using Dotnet.OriginAssignment.Application.Services.Interfaces;
using Dotnet.OriginAssignment.Domain.Models.Models;
using Dotnet.OriginAssignment.Infra.Repositories;
using AutoMapper;
using Dotnet.OriginAssignment.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.OriginAssignment.Application.Services
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(2)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccessType { get; set; } // "dtc" or "employer"

        [MaxLength(255)]
        public string FullName { get; set; }

        [MaxLength(255)]
        public string EmployerId { get; set; }

        public DateTime? BirthDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
    }
}
