using cwiczenia_7_s16324.DTO;
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
    [Route("api/trips")]
    public class TripsController : ControllerBase
    {


        private readonly ITripDbRepo _repo;


        public TripsController(ITripDbRepo repo)
        {
            this._repo = repo;
        }

        [HttpPost("{id}/clients")]
        public async Task<IActionResult> AddClientToTrip([FromRoute] int id, [FromBody] AddClientToTripDTO reqBody)
        {
            await _repo.AddClientToTrip(id, reqBody);
            return Ok("Zapisano klienta na wycieczkę");
        }

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip([FromRoute] int id)
        {
            await _repo.DeleteTrip(id);
            return Ok($"Trip {id} deleted");
        }*/

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            return Ok(await _repo.GetTrips());
        }
    }
}
