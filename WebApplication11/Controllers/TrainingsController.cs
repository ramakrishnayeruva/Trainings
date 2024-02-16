using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.model;
using Microsoft.AspNetCore.Authorization;
using Business.Interface;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]    
    public class TrainingsController : ControllerBase
    {
        private TrainingsApiDBContext dbContext;
        private ITraining _ITraining;

        public TrainingsController(TrainingsApiDBContext dbContext, ITraining _ITraining)
        {
            this.dbContext = dbContext;
            this._ITraining = _ITraining;
        }
        [HttpGet]
        [Authorize(Roles ="Trainer,TrainingCoordinator")]
        public async Task<ActionResult<IEnumerable<Trainings>>> GetTrainings()
        {
            return await Task.FromResult(_ITraining.GetTrainings());

        }
        [HttpPost]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<ActionResult<Trainings>> AddTraining(Trainings addTrainingRequest)
        {
            _ITraining.AddTraining(addTrainingRequest);            
            return await Task.FromResult(addTrainingRequest);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainings>> GetTraining(int id)
        {
            var trainings = await Task.FromResult(_ITraining.GetTraining(id));
            if (trainings == null)
            {
                return NotFound();
            }
            return trainings;
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<ActionResult<Trainings>> UpdateUser(int id, Trainings training)
        {
            if (id != training.Id)
            {
                return BadRequest();
            }
            try
            {
                _ITraining.UpdateTraining(training);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(training);
        }

        //[HttpPut]
        //[Route("{id:int}")]
        //[Authorize(Roles = "TrainingCoordinator")]
        //public async Task<IActionResult> UpdateTraining([FromRoute] int id, AddTrainingRequest updateTrainingRequest)
        //{
        //    var training = await dbContext.Trainings.FindAsync(id);
        //    if (training != null)
        //    {
        //        training.TrainingName = updateTrainingRequest.TrainingName;
        //        training.Catagory = updateTrainingRequest.Catagory;
        //        training.TrainingType = updateTrainingRequest.TrainingType;

        //        await dbContext.SaveChangesAsync();
        //        return Ok(training);
        //    }
        //    return NotFound();
        //}
        private bool UserExists(int id)
        {
            return _ITraining.CheckTraining(id);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<ActionResult<Trainings>> DeleteTraining(int id)
        {
            var training = _ITraining.DeleteTraining(id);
            return await Task.FromResult(training);
        }

       
    }
}
