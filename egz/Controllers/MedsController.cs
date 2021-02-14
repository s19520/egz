using egz.DTOs;
using egz.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace egz.Controllers
{
        [Route("api/medicaments")]
        [ApiController]
    public class MedsController : ControllerBase
    {
        
        private DbService _service;
        public IConfiguration Configuration { get; set; }
        public  MedsController(DbService service, IConfiguration configuration)
        {
            _service = service;
            Configuration = configuration;
        }



        [HttpGet("{id}")]
        public IActionResult GetPrescriptions(int id)
        {
            
            try
            {
                List<GetPrescriptionsResponse> resp = _service.GetPrescriptions(id);
                return CreatedAtRoute(new RouteValues(), resp);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult DelPatient(int id)
        {
            try
            {
                _service.DelPatient(id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
