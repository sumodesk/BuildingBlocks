using Sumodesk.Domain.Abstractions;

namespace Sumodesk.Domain.Shared;

public class AddressInfo : ValueObject
{
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? PostCode { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? CountryRegionCode { get; set; }
}
