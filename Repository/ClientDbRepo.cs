using cwiczenia_7_s16324.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia_7_s16324.Repository
{
    

    public class ClientDbRepo : IClientDbRepo
    {
        private readonly s16324Context context;

        public ClientDbRepo(s16324Context context)
        {
            this.context = context;
        }

        public async Task DeleteClient(int id)
        {
            bool hasTrips = await context.ClientTrips.AnyAsync(row => row.IdClient == id);
            if (hasTrips) throw new Exception("Usunięcie rekordu niemożliwe - klient ma zarezerwowaną wycieczkę");
            Client client = await context.Clients.Where(row => row.IdClient == id).FirstOrDefaultAsync();
            context.Remove(client);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            var clients = await context.Clients.Select(c => c).ToListAsync();
            return clients;
        }

    }
}
