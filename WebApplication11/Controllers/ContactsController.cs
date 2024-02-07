using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplication11.Data;
using Training.model;
using WebApplication11.model;

namespace WebApplication11.Controllers
{
    //[ApiController]
    [Route("api/[Controller]")]
    [ApiController,Authorize]
    public class ContactsController : Controller
    {
        private ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok( await dbContext.Contacts.ToListAsync());

        }

        [HttpPost]

        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Address = addContactRequest.Address,
                email = addContactRequest.email,
                phone = addContactRequest.phone,



            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Name = updateContactRequest.Name;
                contact.phone = updateContactRequest.phone;
                contact.Address = updateContactRequest.Address;
                contact.email = updateContactRequest.email;
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                dbContext.SaveChanges();
                return Ok(contact);
            }
            return NotFound();
        }



        





    }
}
