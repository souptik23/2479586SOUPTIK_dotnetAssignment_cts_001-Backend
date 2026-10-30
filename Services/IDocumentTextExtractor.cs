namespace EmployeeManagemantDotnetV1.Services
{
    public interface IDocumentTextExtractor
    {
        Task<string> ExtractTextAsync(string filePath);
    }
}
