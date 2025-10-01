using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Application.Extensions
{
    public static class EnumParserExtension
    {
        public static T ParseEnum<T>(string value)
        {

            if (Enum.TryParse(typeof(T), value, true, out var result))
            {
                return (T)result;
            }                

            throw new DomainException($"Invalid value '{value}' for {typeof(T).Name}");
        }
    }
}
