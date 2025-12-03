namespace OrderManagement.API.DTOs;

public record OrderDTO {
    public Guid Id { get; set; }

    public string Article { get; set; }

    public DateTimeOffset OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

}