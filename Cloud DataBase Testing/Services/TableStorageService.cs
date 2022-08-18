using Azure.Data.Tables;
using Cloud_DataBase_Testing.Entities;
using Cloud_DataBase_Testing.Interfaces;

namespace Cloud_DataBase_Testing.Services
{
    public class TableStorageService : ITableStorageService
    {

        private const string TableName = "Students";
        private readonly IConfiguration _configuration;

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Connects to Azure Storage and returns reference to TableClient object
        public async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);

            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        } 
        
        // --- CRUD Methods ---
        public async Task<StudentEntity> GetEntityAsync(string major, string id)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<StudentEntity>(major, id);
        }

        public async Task<StudentEntity> UpsertEntityAsync(StudentEntity student)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(student);
            return student;
        }

        public async Task DeleteEntityAsync(string major, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(major, id);
        }



        // ---     ---    ---
    }
}
