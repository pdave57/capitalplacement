using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using QueryApp2.DTOs;
using QueryApp2.Models;

namespace QueryApp2.Services
{
    public class CustomQuestionRepository : ICustomQuestionRepository
    {
        private readonly Container _questionContainer;
        private readonly IConfiguration _configuration;
        public CustomQuestionRepository(CosmosClient cosmosClient, IConfiguration configuration) 
        { 
            _configuration = configuration;
            var databaseName = configuration["CosmosDBSettings:DatabaseName"];
            var questionContainerName = "CustomQuestion";
            _questionContainer = cosmosClient.GetContainer(databaseName, questionContainerName);
        }

        public async Task<CustomQuestion> CreateQuestionAsync(CustomQuestion question)
        {
            question.Id = Guid.NewGuid().ToString();

            var response = await _questionContainer.CreateItemAsync(question);
            return response.Resource;
        }

        public async Task DeleteQuestionAsync(string id)
        {
            await _questionContainer.DeleteItemAsync<CustomQuestion>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<CustomQuestion>> GetAllCustomQuestionsAsync()
        {
            var query = _questionContainer.GetItemLinqQueryable<CustomQuestion>()
                .ToFeedIterator();
            var custquestion = new List<CustomQuestion>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                custquestion.AddRange(response);
            }
            return custquestion;
               
        }

        public async Task<CustomQuestion> GetCustomQuestionByIdAsync(string id)
        {
            var query = _questionContainer.GetItemLinqQueryable<CustomQuestion>()
                .Where(x => x.Id == id)
                .Take(1)
                .ToFeedIterator();
            var response = await query.ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<CustomQuestion> UpdateQuestionAsync(CustomQuestion question)
        {
            var response = await _questionContainer.ReplaceItemAsync<CustomQuestion>(question, question.Id);
            return response.Resource;
        }
    }
}
