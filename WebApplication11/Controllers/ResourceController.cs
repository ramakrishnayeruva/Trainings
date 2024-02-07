using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Training.Data;
using Training.model;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResourceController : ControllerBase
    {
        private TrainingsApiDBContext dbContext;
        public ResourceController(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [Authorize(Roles = "Trainer,TrainingCoordinator,Trainee")]
        public async Task<IActionResult> GetResources()
        {
            return Ok(await dbContext.Resources1.ToListAsync());

        }

        [HttpPost]
        [Authorize (Roles = "TrainingCoordinator")]
        public async Task<IActionResult> AddResources(Resource addResourceRequest)
        {
            var resource = new Resource()
            {
                // Id = Guid.NewGuid(),
                AssociateName = addResourceRequest.AssociateName,
                TrainingName = addResourceRequest.TrainingName,
                //TrainingType = addTrainingRequest.TrainingType,


            };
            await dbContext.Resources1.AddAsync(resource);
            await dbContext.SaveChangesAsync();
            return Ok(resource);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<IActionResult> GetResource([FromRoute] int id)
        {
            var resource = await dbContext.Resources1.FindAsync(id);
            var Trainingall = dbContext.Resources1.Where(x => x.Id == id).Select(x => x.TrainingName);
            // var TraiingNames = dbContext.Resources1.FirstOrDefault(c => c.Id == id).TrainingName;
            if (resource == null)
            {
                return NotFound();
            }
            return Ok(resource);
        }


        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<IActionResult> UpdateResource([FromRoute] int id, Resource updateResourceRequest)
        {
            var resource = await dbContext.Resources1.FindAsync(id);
            if (resource != null)
            {
                
                resource.AssociateName = updateResourceRequest.AssociateName;
                resource.TrainingName = updateResourceRequest.TrainingName;
                

                await dbContext.SaveChangesAsync();
                return Ok(resource);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var resource = await dbContext.Resources1.FindAsync(id);
            if (resource != null)
            {
                dbContext.Remove(resource);
                dbContext.SaveChanges();
                return Ok(resource);
            }
            return NotFound();
        }


    }
}
