using Cloud_DataBase_Testing.Entities;

namespace Cloud_DataBase_Testing.Interfaces
{
    public interface ITableStorageService
    {
        Task<StudentEntity> GetEntityAsync(string major, string id);
        Task<StudentEntity> UpsertEntityAsync(StudentEntity student);
        Task DeleteEntityAsync(string major, string id);
    }
}
