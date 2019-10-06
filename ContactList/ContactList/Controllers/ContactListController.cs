using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactList;
namespace AspNetDemoApplication1.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactListController : ControllerBase
    {
        private static readonly List<Contact> contacts = new List<Contact> { };

        [HttpGet]
        public IActionResult GetAllItems() => Ok(contacts);
        
        [HttpGet]
        [Route("{personId}", Name = "GetSpecificContact")]
        public IActionResult GetItem(Int32 personId)
        {
            foreach(Contact c in contacts)
            {
                if(personId == c.id)
                {
                    return Ok(c);
                }
            }
            return NotFound("No Person found with that ID");
        }

        [HttpGet]
        [Route("findByName", Name = "GetContactByName")]
        public IActionResult GetContactByName([FromQuery] string nameFilter)
        {
            if (String.IsNullOrEmpty(nameFilter))
            {
                return BadRequest("Invalid or missing name");
            }
            List<Contact> filteredContacts = new List<Contact> { };
            foreach (Contact c in contacts)
            {
                if(c.firstname.Contains(nameFilter) || c.lastname.Contains(nameFilter))
                {
                    filteredContacts.Add(c);
                }
            }
            if (filteredContacts.Count > 0)
            {
                return Ok(filteredContacts);
            }
            return Ok(filteredContacts);
            
        }

        [HttpPost]
        public IActionResult addContact([FromBody] Contact newContact)
        {

            if(newContact.lastname == null || newContact.id == 0)
            {
                return BadRequest("Invalid Input (id can not be 0)");
            }
            foreach(Contact c in contacts)
            {
                if(c.id == newContact.id)
                {
                    return BadRequest("ID already exists");
                }
            }
            contacts.Add(newContact);
            return CreatedAtRoute("GetSpecificContact", new { personId = newContact.id }, newContact);
        }


        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeleteContact(Int32 personId)
        {

            foreach(Contact c in contacts)
            {
                if(c.id == personId)
                {
                    contacts.Remove(c);
                    return NoContent();
                }
            }

            return NotFound("Person not found");
        }
    }
}
