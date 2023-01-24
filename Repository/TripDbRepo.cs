using cwiczenia_7_s16324.DTO;
using cwiczenia_7_s16324.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia_7_s16324.Repository
{
    

    public class TripDbRepo : ITripDbRepo
    {
        private readonly s16324Context context;

        public TripDbRepo(s16324Context context)
        {
            this.context = context;
        }

        public async Task DeleteTrip(int id)
        {
            bool hasClients = await context.ClientTrips.AnyAsync(row => row.IdTrip == id);
            if (hasClients) throw new Exception("Trip has Clients");
            Trip trip = await context.Trips.Where(row => row.IdTrip == id).FirstOrDefaultAsync();
            context.Remove(trip);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<GetTripsDTO>> GetTrips()
        {
            var trips = await context.Trips.Select(t => new GetTripsDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.CountryTrips.Select(t => new CountryDTO
                {
                    Name = t.IdCountryNavigation.Name
                }),
                Clients = t.ClientTrips.Select(t => new ClientDTO
                {
                    FirstName = t.IdClientNavigation.FirstName,
                    LastName = t.IdClientNavigation.LastName
                })

            }).OrderByDescending(t => t.DateFrom).ToListAsync();
            return trips;
        }

        public async Task AddClientToTrip(int id, AddClientToTripDTO reqBody)
        {
            bool existingClient = await context.Clients.Select(c => c).Where(c => /*c.FirstName == reqBody.FirstName && c.LastName == reqBody.LastName &&*/ c.Pesel == reqBody.Pesel).AnyAsync();

            Client client;
            if (existingClient) {
                client = await context.Clients.Select(c => c).Where(c => /*c.FirstName == reqBody.FirstName && c.LastName == reqBody.LastName &&*/ c.Pesel == reqBody.Pesel).FirstOrDefaultAsync();
            }
            else
            {
                client = new Client
                {
                    FirstName = reqBody.FirstName,
                    LastName = reqBody.LastName,
                    Email = reqBody.Email,
                    Telephone = reqBody.Telephone,
                    Pesel = reqBody.Pesel

                };
                await context.Clients.AddAsync(client);
                await context.SaveChangesAsync();
            }
            
            bool existingTrip = await context.Trips.Select(t => t).Where(t => t.IdTrip == reqBody.IdTrip).AnyAsync();
            if (!existingTrip)
            {
                throw new Exception("Wycieczka nie istnieje");
            }


            bool alreadyAddedClient = await context.ClientTrips.Select(r => r).Where(r => r.IdTrip == reqBody.IdTrip && r.IdClient == client.IdClient).AnyAsync();
            if (alreadyAddedClient)
            {
                throw new Exception("Klient jest już zapisany na tę wycieczkę");
            }
            else
            {
                await context.ClientTrips.AddAsync(new ClientTrip
                {
                    IdClient = client.IdClient,
                    IdTrip = reqBody.IdTrip,
                    RegisteredAt = DateTime.Now,
                    PaymentDate = DateTime.Parse(reqBody.PaymentDate)
                });
                await context.SaveChangesAsync();
            }
        }

    }
}
