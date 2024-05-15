using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using QueryApp2.DTOs;
using QueryApp2.Models;

namespace QueryApp2.Services
{
    public class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration configuration;
        private readonly Container _personalInfoContainer;
        public PersonalInfoRepository(CosmosClient cosmosClient, IConfiguration configuration) 
        {
            _cosmosClient = cosmosClient;
            this.configuration = configuration;

            var databaseName = configuration["CosmosDBSettings:DatabaseName"];
            var personalinfoContainerName = "PersonalInfo";
            _personalInfoContainer = _cosmosClient.GetContainer(databaseName, personalinfoContainerName);
        }

        public async Task<PersonalInfo> CreatePersonalInfoAsync(PersonalInfo person)
        {
            person.Id = Guid.NewGuid().ToString();
            var response = await _personalInfoContainer.CreateItemAsync(person);
            return response.Resource;
        }

        public async Task DeletePersonalInfoAsync(string id)
        {
            await _personalInfoContainer.DeleteItemAsync<PersonalInfo>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<PersonalInfo>> GetAllPersonalInfoAsync()
        {
            var query = _personalInfoContainer.GetItemLinqQueryable<PersonalInfo>()
                .ToFeedIterator();
            var personal = new List<PersonalInfo>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                personal.AddRange(response);
            }
            return personal;
            
        }

        public async Task<PersonalInfo> GetPersonalInfoByIdAsync(string id)
        {
            var query = _personalInfoContainer.GetItemLinqQueryable<PersonalInfo>()
                .Where(p => p.Id == id)
                .Take(1)
                .ToQueryDefinition();
            var sqlQuery = query.QueryText;
            var response = await _personalInfoContainer.GetItemQueryIterator<PersonalInfo>(sqlQuery).ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<PersonalInfo> UpdatePersonalInfoAsync(PersonalInfo person)
        {
            var response = await _personalInfoContainer.ReplaceItemAsync<PersonalInfo>(person, person.Id);
            return response.Resource;
        }
    }
}
