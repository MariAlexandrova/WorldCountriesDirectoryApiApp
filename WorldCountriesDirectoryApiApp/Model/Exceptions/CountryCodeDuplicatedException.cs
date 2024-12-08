namespace WorldCountriesDirectoryApiApp.Model.Exception
{
    public class CountryCodeDuplicatedException : ApplicationException
    {
        public CountryCodeDuplicatedException(string isoAlpha2) : base($"code '{isoAlpha2}' is duplicated") { }
    }
}
