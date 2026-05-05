using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Diagnosers;
using PerformanceTest.Models;
using SimpleMapper.Mapper.Configuration;
using SimpleMapper.Mapper.Engine;

namespace PerformanceTest;

[MemoryDiagnoser]
[HideColumns(Column.Job, Column.Error, Column.StdDev, Column.Median, Column.Ratio, Column.RatioSD, Column.Gen0, Column.Gen1, Column.Gen2)]
public class SimpleMapperBenchmarks
{
	private PersonSource _person = null!;
	private OrderSource _order = null!;
	private List<PersonSource> _people = null!;
	private SRSimpleMapper _mapper = null!;
	private PersonDto _personDest = null!;

	[GlobalSetup]
	public void GlobalSetup()
	{
		var configuration = new MapperConfiguration();
		configuration.RegisterProfile<BenchmarkMappingProfile>();
		_mapper = new SRSimpleMapper(configuration);

		_person = new PersonSource
		{
			Id = 42,
			Name = "Ada Lovelace",
			Email = "ada@example.com",
			CreatedAt = new DateTime(1815, 12, 10),
			Score = 99.5m,
		};

		_order = new OrderSource
		{
			OrderId = 9001,
			PlacedAt = DateTime.UtcNow,
			Customer = new CustomerSource
			{
				Id = 7,
				Name = "Contoso",
				BillingAddress = new AddressSource
				{
					Line1 = "1 Microsoft Way",
					City = "Redmond",
					PostalCode = "98052",
				},
			},
			Lines =
			[
				new OrderLineSource { Sku = 100, Quantity = 2, UnitPrice = 19.99m },
				new OrderLineSource { Sku = 200, Quantity = 1, UnitPrice = 49.50m },
				new OrderLineSource { Sku = 300, Quantity = 5, UnitPrice = 3.25m },
			],
		};

		_people = new List<PersonSource>(256);
		for (var i = 0; i < 256; i++)
		{
			_people.Add(new PersonSource
			{
				Id = i,
				Name = $"User{i}",
				Email = $"user{i}@bench.test",
				CreatedAt = DateTime.UtcNow.AddMinutes(-i),
				Score = i * 0.25m,
			});
		}

		_personDest = new PersonDto();
	}

	[Benchmark(Description = "Map flat POCO → DTO")]
	public PersonDto Map_FlatPoco()
	{
		return _mapper.Map<PersonSource, PersonDto>(_person);
	}

	[Benchmark(Description = "Map nested graph (order + customer + lines)")]
	public OrderDto Map_NestedGraph()
	{
		return _mapper.Map<OrderSource, OrderDto>(_order);
	}

	[Benchmark(Description = "Map List<PersonSource> → List<PersonDto> (256 items)")]
	public List<PersonDto> Map_Collection()
	{
		return _mapper.Map<List<PersonSource>, List<PersonDto>>(_people);
	}

	[Benchmark(Description = "Map into existing destination instance")]
	public PersonDto Map_IntoExisting()
	{
		return _mapper.Map(_person, _personDest);
	}
}
