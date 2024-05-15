using QueryApp2.Models;
using QueryApp2.DTOs;
namespace QueryApp2.Services
{
    public interface ICustomQuestionRepository
    {
        Task<IEnumerable<CustomQuestion>> GetAllCustomQuestionsAsync();
        Task<CustomQuestion> GetCustomQuestionByIdAsync(string id);
        Task<CustomQuestion> CreateQuestionAsync(CustomQuestion question);
        Task<CustomQuestion> UpdateQuestionAsync(CustomQuestion question);
        Task DeleteQuestionAsync(string id);
    }
}
