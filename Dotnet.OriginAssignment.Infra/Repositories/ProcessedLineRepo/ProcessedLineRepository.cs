using Dotnet.OriginAssignment.Infra.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Dotnet.OriginAssignment.Infra.Repositories.ProcessedLineRepo
{
    public class ProcessedLineRepository : IProcessedLineRepository
    {
        private IUnitOfWork _unitOfWork;
        public ProcessedLineRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProcessedLine> CreateProcessedLine(ProcessedLine processedLine)
        {
            var context = _unitOfWork.getContext();

            context.ProcessedLines.Add(processedLine);
            await context.SaveChangesAsync();

            return processedLine;
        }

        public async Task<ProcessedLine> GetValidProcessedLineByEmailAsync(string email)
        {
            return await _unitOfWork.getContext().ProcessedLines.SingleOrDefaultAsync(e => e.Email == email);
        }
    }
}
