using EmployeeManagemantDotnetV1.DTO;
using EmployeeManagemantDotnetV1.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagemantDotnetV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IDocumentTextExtractor _documentTextExtractor;
        private readonly IResumeAiService _resumeAiService;

        public PdfController(
            IDocumentTextExtractor documentTextExtractor,
            IResumeAiService resumeAiService)
        {
            _documentTextExtractor = documentTextExtractor;
            _resumeAiService = resumeAiService;
        }

        [HttpPost("extract-employee")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ResumeEmployeeDTO>> ExtractEmployee(
            IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a PDF resume.");
            }

            if (!string.Equals(
                Path.GetExtension(file.FileName),
                ".pdf",
                StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only PDF files are supported.");
            }

            // Limit uploads to 10 MB.
            if (file.Length > 10 * 1024 * 1024)
            {
                return BadRequest("The PDF must be 10 MB or smaller.");
            }

            var tempFilePath = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid()}.pdf");

            try
            {
                await using (var stream = System.IO.File.Create(tempFilePath))
                {
                    await file.CopyToAsync(stream);
                }

                // Step 1: Extract text from the PDF.
                var resumeText =
                    await _documentTextExtractor.ExtractTextAsync(
                        tempFilePath);

                if (string.IsNullOrWhiteSpace(resumeText))
                {
                    return BadRequest(
                        "No readable text was found in the PDF.");
                }

                // Step 2: Send the text to the LLM.
                var employee =
                    await _resumeAiService.ExtractEmployeeDetailsAsync(
                        resumeText);

                // Step 3: Return the extracted fields to Angular.
                // No employee is saved to the database here.
                return Ok(employee);
            }
            finally
            {
                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Delete(tempFilePath);
                }
            }
        }
    }
}