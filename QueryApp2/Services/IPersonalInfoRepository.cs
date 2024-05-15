using QueryApp2.Models;
using QueryApp2.DTOs;
namespace QueryApp2.Services
{
    public interface IPersonalInfoRepository
    {
        Task<IEnumerable<PersonalInfo>> GetAllPersonalInfoAsync();
        Task<PersonalInfo>GetPersonalInfoByIdAsync(string id);
        Task<PersonalInfo> CreatePersonalInfoAsync(PersonalInfo person);
        Task<PersonalInfo> UpdatePersonalInfoAsync(PersonalInfo person);
        Task DeletePersonalInfoAsync(string id);
    }
}
