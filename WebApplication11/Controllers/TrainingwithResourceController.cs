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
    [ApiController, Authorize]
    public class TrainingwithResourceController:ControllerBase
    {
        private TrainingsApiDBContext dbContext;
        public TrainingwithResourceController(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "TrainingCoordinator,Trainee")]
        public async Task<IActionResult>GetTrainingswithresource([FromRoute] int id)
        {
            var trainingAssociates = dbContext.TrainingAssociates.Where(u => u.AssociateId == 1).ToList();
            var trainingIds = trainingAssociates.Select(s => s.TrainingId).ToList();
            var trainings = dbContext.Trainings.Where(r => trainingIds.Contains(r.Id)).ToList();            
            if (trainings == null)
            {
                return NotFound();
            }
            return Ok(trainings);
        }

    }
}
