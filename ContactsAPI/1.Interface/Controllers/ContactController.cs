using ContactsAPI._1.Interface.ViewModels;
using ContactsAPI._2.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI._1.Interface.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] ContactViewModel productViewModel)
        {
            return Ok(await _contactService.Add(productViewModel));
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            return Ok(await _contactService.Index());
        }

        [HttpGet("Details/{Id}")]
        public async Task<ActionResult> Details([FromRoute] int id)
        {
            return Ok(await _contactService.Details(id));
        }

        [HttpPost("Disable/{Id}")]
        public async Task<ActionResult> Disable([FromRoute] int id)
        {
            return Ok(await _contactService.Disable(id));
        }

        [HttpPost("Delete/{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _contactService.Delete(id));
        }
    }
}
