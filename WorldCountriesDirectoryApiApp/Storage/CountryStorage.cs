using WorldCountriesDirectoryApiApp.Model.Service;
using Microsoft.EntityFrameworkCore;
using WorldCountriesDirectoryApiApp.Model.Entity;
using WorldCountriesDirectoryApiApp.Model.Exception;

namespace WorldCountriesDirectoryApiApp.Storage
{
    public class CountryStorage : ICountryStorage
    {
        private readonly ApplicationDbContext _dbContext;
        public CountryStorage(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<DbCountry>> SelectAllAsync()
        {
            return await _dbContext.Countries.ToListAsync();
        }

        public async Task<DbCountry?> SelectByCodeAsync(string isoAlpha2)
        {
            return await _dbContext.Countries
                .FirstOrDefaultAsync(c => c.IsoAlpha2 == isoAlpha2);
        }

        public async Task InsertAsync(DbCountry dbCountry)
        {
            _dbContext.Countries.Add(dbCountry);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(DbCountry country)
        {
            _dbContext.Countries.Update(country);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(DbCountry country)
        {
            _dbContext.Countries.Remove(country);
            await _dbContext.SaveChangesAsync();
        }
    }
}
