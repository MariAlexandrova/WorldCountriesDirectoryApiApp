using WorldCountriesDirectoryApiApp.Model.Entity;
using WorldCountriesDirectoryApiApp.Storage;

namespace WorldCountriesDirectoryApiApp.Model.Service
{
    public interface ICountryStorage
    {
        Task<List<DbCountry>> SelectAllAsync();
        Task<DbCountry?> SelectByCodeAsync(string isoAlpha2);
        Task InsertAsync(DbCountry dbCountry);
        Task UpdateAsync(DbCountry country);
        Task DeleteAsync(DbCountry country);
    }
}
