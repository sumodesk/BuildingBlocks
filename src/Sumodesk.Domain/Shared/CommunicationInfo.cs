using Sumodesk.Domain.Abstractions;

namespace Sumodesk.Domain.Shared;

public class CommunicationInfo : ValueObject
{
    public string? FaxNo_ { get; set; }
    public string? TelExNo_ { get; set; }
    public string? TelexAnswerBack { get; set; }
    public string? PhoneNo_1 { get; set; }
    public string? PhoneNo_2 { get; set; }
    public string? Email { get; set; }
}
