using Azure;
using Azure.Data.Tables;

namespace Cloud_DataBase_Testing.Entities
{
    public class StudentEntity : ITableEntity
    {

        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Course { get; set; } = default!;
        // Represents Major
        public string PartitionKey { get; set; } = default!;
        // Represents Student ID
        public string RowKey { get; set; } = default!;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
