using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientNetAPI.Repositories;

namespace ClientNetAPI.Controllers
{
    public class NetDTO
    { 
        public string IpAddress { get; set; }
        public string Info { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ClientNetController : ControllerBase
    {
        private IClientNetServise _clientNetServise;

        public ClientNetController(IClientNetServise clientNetServise)
        {
            _clientNetServise = clientNetServise;
        }

        [HttpPost("{id}")]
        public IActionResult CreateNet(int id, [FromBody] NetDTO netDTO)
        {
            try
            {
                if (netDTO == null)
                    return BadRequest();
                return Ok(_clientNetServise.CreateNet(netDTO, id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNet(int id)
        {
            try
            {
                _clientNetServise.DeleteNetByClient(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        [Produces(typeof(ClientEntity))]
        public IActionResult GetNet(int id)
        {
            try
            {
                return Ok(_clientNetServise.GetClientAndNet(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
