using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class AddressMapper
    {
        public static Address MapBarDTOToAddress(this BarDTO bar)
        {
            var newAddress = new Address();
            newAddress.Country = bar.Country;
            newAddress.City = bar.City;
            newAddress.Details = bar.Details;
            return newAddress;
        }

    }
}
