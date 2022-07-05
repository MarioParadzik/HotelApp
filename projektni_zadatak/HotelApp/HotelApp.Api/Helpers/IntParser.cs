using HotelApp.Api.Exceptions;

namespace HotelApp.Api.Helpers
{
    public static class IntParser
    {
        public static int parse(string value)
        {
            int result;
            try
            {
                result = int.Parse(value);
            }
            catch (FormatException)
            {
                throw new BadRequestException($"Unable to parse '{value}'");
            }

            return result;
        }
    }
}
