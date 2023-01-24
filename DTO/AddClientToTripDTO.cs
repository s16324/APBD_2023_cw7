using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia_7_s16324.DTO
{
    public class AddClientToTripDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        [NotNull]
        public string Pesel { get; set; }
        [NotNull]
        public int IdTrip { get; set; }
        public string TripName { get; set; }
        public string PaymentDate { get; set; }
    }
}
