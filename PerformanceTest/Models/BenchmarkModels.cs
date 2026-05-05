namespace PerformanceTest.Models;

public sealed class PersonSource
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public decimal Score { get; set; }
}

public sealed class PersonDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public decimal Score { get; set; }
}

public sealed class AddressSource
{
	public string Line1 { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public string PostalCode { get; set; } = string.Empty;
}

public sealed class AddressDto
{
	public string Line1 { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public string PostalCode { get; set; } = string.Empty;
}

public sealed class CustomerSource
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public AddressSource? BillingAddress { get; set; }
}

public sealed class CustomerDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public AddressDto? BillingAddress { get; set; }
}

public sealed class OrderLineSource
{
	public int Sku { get; set; }
	public int Quantity { get; set; }
	public decimal UnitPrice { get; set; }
}

public sealed class OrderLineDto
{
	public int Sku { get; set; }
	public int Quantity { get; set; }
	public decimal UnitPrice { get; set; }
}

public sealed class OrderSource
{
	public long OrderId { get; set; }
	public DateTime PlacedAt { get; set; }
	public CustomerSource? Customer { get; set; }
	public List<OrderLineSource> Lines { get; set; } = [];
}

public sealed class OrderDto
{
	public long OrderId { get; set; }
	public DateTime PlacedAt { get; set; }
	public CustomerDto? Customer { get; set; }
	public List<OrderLineDto> Lines { get; set; } = [];
}
