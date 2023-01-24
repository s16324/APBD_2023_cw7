using cwiczenia_7_s16324.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia_7_s16324.Repository
{
    public interface IClientDbRepo
    {
        Task DeleteClient(int id);

        Task<IEnumerable<Client>> GetClients();
    }

}
