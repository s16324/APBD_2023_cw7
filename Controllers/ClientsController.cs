using cwiczenia_7_s16324.Models;
using cwiczenia_7_s16324.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia_7_s16324.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {


        private readonly IClientDbRepo _repo;


        public ClientsController(IClientDbRepo repo)
        {
            this._repo = repo;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            await _repo.DeleteClient(id);
            return Ok($"Client ID: {id} deleted");
        }

        /*[HttpGet]
        public async Task<IActionResult> GetClients()
        {

            return Ok(await _repo.GetClients());

        }*/
    }
}
