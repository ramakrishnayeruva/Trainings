using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Data;
using WebApplication11.Data;
using WebApplication11.model;
using Training.model;
using Microsoft.AspNetCore.Authorization;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class TrainingsController : ControllerBase
    {
        private TrainingsApiDBContext dbContext;

        public TrainingsController(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        [Authorize(Roles ="Trainer,TrainingCoordinator")]
        public async Task<IActionResult> GetTrainings()
        {
            return Ok(await dbContext.Trainings.ToListAsync());

        }

        [HttpPost]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<IActionResult> AddTraining(AddTrainingRequest addTrainingRequest)
        {
            var training = new Trainings()
            {
               // Id = Guid.NewGuid(),
                TrainingName = addTrainingRequest.TrainingName,
                Catagory = addTrainingRequest.Catagory,
                TrainingType = addTrainingRequest.TrainingType,          


            };
            await dbContext.Trainings.AddAsync(training);
            await dbContext.SaveChangesAsync();
            return Ok(training);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<IActionResult> GetTraining([FromRoute] int id)
        {
            var training = await dbContext.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            return Ok(training);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<IActionResult> UpdateTraining([FromRoute] int id, AddTrainingRequest updateTrainingRequest)
        {
            var training = await dbContext.Trainings.FindAsync(id);
            if (training != null)
            {
                training.TrainingName = updateTrainingRequest.TrainingName;
                training.Catagory = updateTrainingRequest.Catagory;
                training.TrainingType = updateTrainingRequest.TrainingType;
                
                await dbContext.SaveChangesAsync();
                return Ok(training);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]       
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            var training = await dbContext.Trainings.FindAsync(id);
            if (training != null)
            {
                dbContext.Remove(training);
                dbContext.SaveChanges();
                return Ok(training);
            }
            return NotFound();
        }
    }
}
