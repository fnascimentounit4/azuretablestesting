using Cloud_DataBase_Testing.Entities;
using Cloud_DataBase_Testing.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_DataBase_Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ITableStorageService _storageService;

        public StudentsController(ITableStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        [HttpGet]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromQuery] string major, string id)
        {
            return Ok(await _storageService.GetEntityAsync(major, id)); 
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] StudentEntity entity)
        {
            entity.PartitionKey = entity.Course;

            string Id = Guid.NewGuid().ToString();
            entity.Id = Id;
            entity.RowKey = Id;

            var createEntity = await _storageService.UpsertEntityAsync(entity);
            return CreatedAtAction(nameof(GetAsync), createEntity);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] StudentEntity entity)
        {
            entity.PartitionKey = entity.Course;
            entity.RowKey = entity.Id;

            await _storageService.UpsertEntityAsync(entity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string major, string id)
        {
            await _storageService.DeleteEntityAsync(major, id);
            return NoContent();
        }
    }
}
