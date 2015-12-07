using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class GeolocationRepository
    {
        BistroDriveEntities context;

        IEnumerable<city> bufferedCity;
        public GeolocationRepository(BistroDriveEntities _context)
        {
            this.context = _context;
            bufferedCity = context.cities.ToArray();
        }

        public IEnumerable<city> GetAllCities()
        {
            return bufferedCity;
        }

        public city GetCityById(int id)
        {
            return bufferedCity.FirstOrDefault(c => c.id_city == id);
        }
    }
}