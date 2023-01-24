using cwiczenia_7_s16324.DTO;
using cwiczenia_7_s16324.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia_7_s16324.Repository
{
    public interface ITripDbRepo
    {
        Task DeleteTrip(int id);

        Task<IEnumerable<GetTripsDTO>> GetTrips();

        Task AddClientToTrip(int id, AddClientToTripDTO reqBody);
    }

}
