using Mapster;
using MapsterMapper;

namespace ManagerVM.Services.Helper
{
    public static class MapsterHelper
    {
        private static readonly Lazy<IMapper> MapperFactory = new Lazy<IMapper>(() =>
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
            typeAdapterConfig.Scan(AppDomain.CurrentDomain.GetAssemblies());
            // register the mapper as Singleton service for my application
            var mapperConfig = new Mapper(typeAdapterConfig);

            return mapperConfig;
        });

        public static IMapper Mapper => MapperFactory.Value;

        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public static void Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            Mapper.Map(source, destination);
        }
    }
}
