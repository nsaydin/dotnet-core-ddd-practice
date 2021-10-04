using Application.Dtos;
using Core.Domain.Product;
using Core.Domain.Product.Dtos;
using Mapster;

namespace Application.Mapping
{
    public class MapsterProfile : IMapperProfile
    {
        public void Configure()
        {
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);

            TypeAdapterConfig<Product, ProductOverviewDto>
                .NewConfig()
                .Map(dest => dest.DiscountedPrice, src => src.Price);

            TypeAdapterConfig<ProductOverviewDto, ProductDto>
                .NewConfig()
                .Map(dest => dest.Price, src => src.DiscountedPrice ?? src.Price);
        }
    }
}