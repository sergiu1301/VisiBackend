namespace VisiProject.Contracts.Models;

public interface IRole
{
    string RoleId { get; }

    string Name { get; }

    string Description { get; }
}