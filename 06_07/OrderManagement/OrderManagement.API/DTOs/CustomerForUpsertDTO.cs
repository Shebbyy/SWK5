using System.Text.Json.Serialization;
using OrderManagement.Domain;

namespace OrderManagement.API.DTOs;

public record CustomerForUpsertDTO {
    // For API: Required automatically validates if property is set, otherwise 400; Applies for entire Application like this
    // Formerly [JsonRequired], only for Validating, not for rest of application, would require additional [Required]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required int ZipCode { get; set; }
    
    // Advantage of these Attributes: ObjectMapper can still map to Domain Object, whilst Endpoint Output can be configured according to ones wishes
    public required string City { get; set; }

    public Rating Rating { get; set; }
}