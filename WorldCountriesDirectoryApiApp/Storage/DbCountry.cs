using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorldCountriesDirectoryApiApp.Model.Entity;

namespace WorldCountriesDirectoryApiApp.Storage
{
    public class DbCountry
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string IsoAlpha2 { get; set; } = string.Empty;

        // Маппинг DbCountry -> Country
        public Country ToModel()
        {
            return new Country(ShortName, FullName, IsoAlpha2);
        }

        // Маппинг Country -> DbCountry
        public static DbCountry FromModel(Country country)
        {
            return new DbCountry
            {
                ShortName = country.ShortName,
                FullName = country.FullName,
                IsoAlpha2 = country.IsoAlpha2
            };
        }
    }
}
