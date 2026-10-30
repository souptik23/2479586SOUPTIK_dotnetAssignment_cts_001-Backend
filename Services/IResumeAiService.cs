using EmployeeManagemantDotnetV1.DTO;

namespace EmployeeManagemantDotnetV1.Services
{
    public interface IResumeAiService
    {
        Task<ResumeEmployeeDTO> ExtractEmployeeDetailsAsync(string resumeText);
    }
}