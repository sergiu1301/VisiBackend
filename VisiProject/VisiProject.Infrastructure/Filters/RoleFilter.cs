using VisiProject.Contracts.Filters;

namespace VisiProject.Infrastructure.Filters;

public class RoleFilter : IRoleFilter
{
    public string? RoleId { get; set; }

    public string? Name { get; set; }
}