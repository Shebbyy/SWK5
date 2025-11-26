using System.Text.Json.Serialization;
using OrderManagement.Domain;

namespace OrderManagement.API.DTOs;

// Used for 1-Way-Flows
// good idea to use immutable records
// DTO used to never allow Domain-Objects outside of Backend
public record CustomerDTO {
    // For API: Required automatically validates if property is set, otherwise 400; Applies for entire Application like this
    // Formerly [JsonRequired], only for Validating, not for rest of application, would require additional [Required]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required int ZipCode { get; set; }
    
    // Advantage of these Attributes: ObjectMapper can still map to Domain Object, whilst Endpoint Output can be configured according to ones wishes
    [JsonPropertyName("location")] // Returns City as Location in the Endpoint JSON
    public required string City { get; set; }

    public Rating Rating { get; set; }
    [JsonIgnore] // do not output in serializer
    public decimal TotalRevenue { get; set; }
}