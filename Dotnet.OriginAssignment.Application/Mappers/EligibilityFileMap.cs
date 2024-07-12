using CsvHelper.Configuration;
using Dotnet.OriginAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.OriginAssignment.Application.Mappers
{
    public class EligibilityFileMap : ClassMap<EligibilityFileEntry>
    {
        public EligibilityFileMap()
        {
            Map(m => m.Email).Name("email");
            Map(m => m.FullName).Name("full name");
            Map(m => m.Country).Name("country");
            Map(m => m.BirthDate).Name("birth_date");
            Map(m => m.Salary).Name("salary");
        }
    }
}
