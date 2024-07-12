using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Infra.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Dotnet.OriginAssignment.Infra.Repositories.ProcessedLineRepo
{
    public class EligibilityFileRepository : IEligibilityFileRepository
    {
        private IUnitOfWork _unitOfWork;
        public EligibilityFileRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EligibilityFile> CreateEligibilityFile(EligibilityFile eligibilityFile)
        {
            var context = _unitOfWork.getContext();

            context.EligibilityFiles.Add(eligibilityFile);
            await context.SaveChangesAsync();

            return eligibilityFile;
        }
    }
}
