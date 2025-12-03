using OrderManagement.API.DTOs;
using OrderManagement.Domain;

namespace OrderManagement.API.Mappers;

public static class OrderMapper {
    public static OrderDTO ToOrderDTO(this Order order) {
        return new() {
            Id = order.Id,
            Article = order.Article,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice
        };
    }
}