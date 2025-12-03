using OrderManagement.API.DTOs;
using OrderManagement.Domain;
using Riok.Mapperly.Abstractions;

namespace OrderManagement.API.Mappers;

[Mapper]
public static partial class CustomerMapper {
    public static partial CustomerDTO ToDTO(this Customer customer);
    public static partial IEnumerable<CustomerDTO> ToDTOEnumerable(this IEnumerable<Customer> customers);
    public static partial Customer ToDomain(this CustomerDTO cust);

    [MapperIgnoreTarget(nameof(Domain.Customer.TotalRevenue))]
    [MapperIgnoreTarget(nameof(Domain.Customer.Id))]
    public static partial CustomerForUpsertDTO ToUpsertDTO(this Customer cust);

    public static partial Customer ToDomain(this CustomerForUpsertDTO cust);
    
    // so they dont get overridden by accident in any way, in case the UpsertDTO might change at some point
    [MapperIgnoreTarget(nameof(Domain.Customer.TotalRevenue))]
    [MapperIgnoreTarget(nameof(Domain.Customer.Id))]
    public static partial void UpdateCustomer(this CustomerForUpsertDTO cust, Customer domainCustomer);


    /*public static CustomerDTO ToDTO(this Customer cust) {
        return new() {
            Id = cust.Id,
            Name = cust.Name,
            ZipCode = cust.ZipCode,
            City = cust.City,
            Rating = cust.Rating,
            TotalRevenue = cust.TotalRevenue
        };
    }

    public static Customer ToCustomer(this CustomerDTO cust) {
        return new Customer(
            cust.Id,
            cust.Name,
            cust.ZipCode,
            cust.City,
            cust.Rating);
    }*/
}