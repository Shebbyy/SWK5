using OrderManagement.API.DTOs;
using OrderManagement.Domain;

namespace OrderManagement.API.Mappers;

public static class CustomerMapper {
    public static CustomerDTO ToDTO(this Customer cust) {
        return new() {
            Id = cust.Id,
            Name = cust.Name,
            ZipCode = cust.ZipCode,
            City = cust.City,
            Rating = cust.Rating,
            TotalRevenue = cust.TotalRevenue
        };
    }
}