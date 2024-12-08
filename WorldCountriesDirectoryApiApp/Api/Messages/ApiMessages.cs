namespace WorldCountriesDirectoryApiApp.Api.Messages
{
    public class ApiMessages
    {
        // общие сообщения API

        //StringMessage - record строкового сообщения
        public record StringMessage(string Message);

        //ErrorMessage - record сообщения об ошибке
        public record ErrorMessage(string Type,  string Message);
    }
}
