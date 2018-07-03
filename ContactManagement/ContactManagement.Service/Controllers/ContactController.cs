using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactManagement.Lib.AbstractRepository;
using ContactManagement.Service.Models;
using ContactManagement.Lib.Model;

namespace ContactManagement.Service.Controllers
{
    [Route("api/[controller]")]
    
    public class ContactsController : Controller
    {
        private IContactRepository _contactRepository;
        int page = 1;
        int pageSize = 50;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // GET api/contacts
        [HttpGet]
        //public IEnumerable<Contact> Get()
        public IActionResult Get()
        {
            try
            {
                var totalContacts = _contactRepository.Count();
                if (totalContacts > 0)
                {
                    if (totalContacts > pageSize)
                        return (new OkObjectResult(GetContactsAsPerPaging(totalContacts)));

                    IEnumerable<Contact> contacts = _contactRepository.GetAll()
                        .OrderBy(s => s.Id)
                        .ToList();
                    return new OkObjectResult(contacts);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error occcured while processing your request.");
            }
            return new NotFoundResult();
        }

        // GET api/contacts/1
        [HttpGet("{id}", Name = "GetContact")]
        public IActionResult Get(int id)
        {
            try
            {
                var totalContacts = _contactRepository.Count();
                if (totalContacts > 0)
                {
                    Contact contact = _contactRepository.GetSingle(id);
                    if (contact != null)
                        return new OkObjectResult(contact);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occcured while processing your request.");
            }
            return new NotFoundResult();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]ContactApiModel contactApiModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Contact contact = ContactApiMapper.MapToEntiry(contactApiModel);
                _contactRepository.Add(contact);
                _contactRepository.Commit();

                CreatedAtRouteResult result = CreatedAtRoute("GetContact", new { controller = "Contacts", id = contact.Id }, contact);
                
                return result;
            }
            catch
            {
                throw new Exception("Error occcured while processing your request.");
            }
        }

        // PUT api/contact/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ContactApiModel contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Contact contactDb = _contactRepository.GetSingle(id);

                if (contactDb == null)
                {
                    return NotFound("Contact having Id - " + id + " not found");
                }
                else
                {
                    contactDb.FirstName = contact.FirstName;
                    contactDb.LastName = contact.LastName;
                    contactDb.Email = contact.Email;
                    contactDb.PhoneNumber = contact.PhoneNumber;
                    contactDb.Status = contact.Status;
                    _contactRepository.Update(contactDb);
                    _contactRepository.Commit();
                    return new OkObjectResult("Contact having Id - " + id + " updated successfully.");
                }
            }
            catch
            {
                throw new Exception("Error occcured while processing your request.");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var totalContacts = _contactRepository.Count();
                if (totalContacts > 0)
                {
                    Contact contact = _contactRepository.GetSingle(id);
                    if (contact == null)
                        return NotFound("Contact having Id - " + id + " not found");
                    else
                    {
                        _contactRepository.Delete(contact);
                        _contactRepository.Commit();
                        return new NoContentResult();
                    }
                }
            }
            catch
            {
                throw new Exception("Error occcured while processing your request.");
            }
            return NotFound("Contact having Id - " + id + " not found");

        }

        

        private IEnumerable<Contact> GetContactsAsPerPaging(int totalContacts)
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;

            var totalPages = (int)Math.Ceiling((double)totalContacts / pageSize);

            IEnumerable<Contact> contacts = _contactRepository.GetAll()
                .OrderBy(s => s.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            Response.AddPagination(page, pageSize, totalContacts, totalPages);

            return contacts;
        }
    }
}
