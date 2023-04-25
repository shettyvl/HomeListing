using API.Core.Maps;
using Dapper.FluentMap;

namespace API.Core
{
    public class Registrations
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ListingMap());
            });
        }
    }
}
