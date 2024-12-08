using System.Text.RegularExpressions;
using WorldCountriesDirectoryApiApp.Model.Entity;
using WorldCountriesDirectoryApiApp.Model.Exception;
using WorldCountriesDirectoryApiApp.Model.Service;
using WorldCountriesDirectoryApiApp.Storage;

namespace WorldCountriesDirectoryApiApp.Model.Scenarious
{
    // CountryScenarios - реализация сценариев работы с объектами типа Country
    public class CountryScenarios
    {
        private readonly ICountryStorage _storage;

        public CountryScenarios(ICountryStorage storage)
        {
            _storage = storage;
        }

        // ListAllAsync - получение списка всех стран кодом
        // вход: -
        // выход: список объектов Country, в т.ч. пустой
        // исключения: -
        public async Task<List<Country>> ListAllAsync()
        {
            var dbCountries = await _storage.SelectAllAsync();
            return dbCountries.Select(c => c.ToModel()).ToList();
        }

        // GetByCodeAsync - получить запись о стране по коду
        // вход: IsoAlpha2 - двухбуквенный код страны
        // выход: найденная страна с данным кодом
        // исключения: CountryNotFoundException, CountryCodeFormatException
        public async Task<Country> GetByCodeAsync(string isoAlpha2)
        {
            if (!IsValidCode(isoAlpha2))
                throw new CountryCodeFormatException();

            var dbCountry = await _storage.SelectByCodeAsync(isoAlpha2);

            if (dbCountry == null)
                throw new CountryNotFoundException(isoAlpha2);

            return dbCountry.ToModel();
        }

        // AddAsync - добавить новую запись о стране
        // вход: собранный объект аэропорта
        // выход: -
        // исключения: CountryCodeFormatException, CountryCodeDuplicatedException
        public async Task AddAsync(Country country)
        {
            var existingCountryByCode = await _storage.SelectByCodeAsync(country.IsoAlpha2);
            if (existingCountryByCode != null)
            {
                throw new CountryCodeDuplicatedException(country.IsoAlpha2);
            }

            var dbCountry = DbCountry.FromModel(country);
            await _storage.InsertAsync(dbCountry);
        }
        // EditAsync - редактирование страны по коду
        // вход:  code - интернациональный код аэропорта, country - обновление страны
        // выход: -
        // исключения: CountryNotFoundException, CountryCodeFormatException
        public async Task<Country> EditAsync(string isoAlpha2, Country updatedCountry)
        {
            if (!IsValidCode(isoAlpha2))
                throw new CountryCodeFormatException();

            var dbCountry = await _storage.SelectByCodeAsync(isoAlpha2);
            if (dbCountry == null)
                throw new CountryNotFoundException(isoAlpha2);

            dbCountry.ShortName = updatedCountry.ShortName;
            dbCountry.FullName = updatedCountry.FullName;

            await _storage.UpdateAsync(dbCountry);

            return dbCountry.ToModel();
        }
        // DeleteAsync - удалить запись о стране по коду
        // вход: IsoAlpha2 - двухбуквенный код страны
        // выход: -
        // исключения: CountryNotFoundException, CountruCodeFormatException
        public async Task DeleteAsync(string isoAlpha2)
        {
            if (!IsValidCode(isoAlpha2))
                throw new CountryCodeFormatException();

            var dbCountry = await _storage.SelectByCodeAsync(isoAlpha2);
            if (dbCountry == null)
                throw new CountryNotFoundException(isoAlpha2);

            await _storage.DeleteAsync(dbCountry);
        }
        private bool IsValidCode(string isoAlpha2)
        {
            return !string.IsNullOrWhiteSpace(isoAlpha2) &&
                   isoAlpha2.Length == 2 &&
                   isoAlpha2.All(char.IsUpper);
        }

    }
}
