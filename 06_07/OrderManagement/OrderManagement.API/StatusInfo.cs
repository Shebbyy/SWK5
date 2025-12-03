using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.API;

public static class StatusInfo {
    public static ProblemDetails InvalidCustomerId(Guid customerId) {
        return new ProblemDetails() {
            Title = "Invalid Customer ID",
            Detail = $"Customer with ID {customerId} does not exist!"
        };
    }
}