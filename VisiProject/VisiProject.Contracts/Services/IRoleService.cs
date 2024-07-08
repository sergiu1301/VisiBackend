using VisiProject.Contracts.Models;

namespace VisiProject.Contracts.Services;

public interface IRoleService
{
    Task DeleteRoleAsync(string roleId);
    Task<IReadOnlyList<IRole>> GetRolesAsync();
    Task UpdateRoleAsync(string roleId, string roleName, string roleDescription);
    Task<IRole> CreateRoleAsync(string roleName, string roleDescription);
}