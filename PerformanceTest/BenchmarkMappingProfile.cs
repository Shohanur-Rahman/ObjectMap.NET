using PerformanceTest.Models;
using SimpleMapper.Mapper.Profiles;

namespace PerformanceTest;

internal sealed class BenchmarkMappingProfile : Profile
{
	protected override void ConfigureMaps()
	{
		CreateMap<PersonSource, PersonDto>();
		CreateMap<AddressSource, AddressDto>();
		CreateMap<CustomerSource, CustomerDto>();
		CreateMap<OrderLineSource, OrderLineDto>();
		CreateMap<OrderSource, OrderDto>();
	}
}
