namespace WorldCountriesDirectoryApiApp.Model.Exception
{
    public class CountryNotFoundException : ApplicationException
    {
        public CountryNotFoundException() : base("country is not found") { }
        public CountryNotFoundException(string isoAlpha2) : base($"country '{isoAlpha2}' is not found") { }
    }
}
