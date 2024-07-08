using VisiProject.Contracts.Models;

namespace VisiProject.Infrastructure.Models;

public class UserRole : IUserRole
{
    public string UserId { get; set; }

    public string RoleId { get; set; }
}