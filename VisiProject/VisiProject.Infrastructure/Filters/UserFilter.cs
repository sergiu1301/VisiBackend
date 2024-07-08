using VisiProject.Contracts.Filters;

namespace VisiProject.Infrastructure.Filters;

public class UserFilter : IUserFilter
{
    public string? UserId { get; set; }

    public string? Email { get; set; }
}