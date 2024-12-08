namespace WorldCountriesDirectoryApiApp.Model.Entity
{
    public class Country
    {
        public string ShortName { get; set; } = string.Empty; // короткое наименование страны                         
        public string FullName { get; set; } = string.Empty;// полное наименование страны
        public string IsoAlpha2 { get; set; } = string.Empty; // двухбуквенный код страны

        public Country() { }

        public Country(string shortName, string fullName, string isoAlpha2)
        {
            ShortName = shortName;
            FullName = fullName;
            IsoAlpha2 = isoAlpha2;
        }
    }
}
