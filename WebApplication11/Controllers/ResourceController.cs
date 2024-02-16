using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccess.Data;
using DataAccess.model;
using Business.Interface;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResourceController : ControllerBase
    {
        private TrainingsApiDBContext dbContext;
        private IResource _IResource;
        public ResourceController(TrainingsApiDBContext dbContext)
        {
            this.dbContext = dbContext;
            this._IResource = _IResource;

        }
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        [Authorize(Roles = "Trainer,TrainingCoordinator,Trainee")]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResources()
        {
            return await Task.FromResult(_IResource.GetResources());

        }

        [HttpPost]
        [Authorize (Roles = "TrainingCoordinator")]
        public async Task<ActionResult<Resource>> AddResources(Resource addResourceRequest)
        {
            _IResource.AddResources(addResourceRequest);           
            return await Task.FromResult(addResourceRequest);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<ActionResult<Resource>> GetResource([FromRoute] int id)
        {
            var resource = await Task.FromResult(_IResource.GetResource(id));
            if (resource == null)
            {
                return NotFound();
            }
            return resource;
        }
                
        [HttpPut]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<ActionResult<Resource>> UpdateUser(int id, Resource resource)
        {
            if (id != resource.Id)
            {
                return BadRequest();
            }
            try
            {
                _IResource.UpdateResource(resource);
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
            return await Task.FromResult(resource);
        }
        
        private bool UserExists(int id)
        {
            return _IResource.CheckResource(id);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "TrainingCoordinator")]
        public async Task<ActionResult<Resource>> DeleteResource(int id)
        {
            var resource = _IResource.DeleteResource(id);
            return await Task.FromResult(resource);
        }

    }
}
