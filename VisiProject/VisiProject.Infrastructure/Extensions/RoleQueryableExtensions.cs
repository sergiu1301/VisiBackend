using Validation;
using VisiProject.Contracts.Filters;
using VisiProject.Infrastructure.Entities;

namespace VisiProject.Infrastructure.Extensions;

public static class RoleQueryableExtensions
{
    public static IQueryable<RoleEntity> ApplyFiltering(
        this IQueryable<RoleEntity> query,
        IRoleFilter filter)
    {
        Requires.NotNull(query, nameof(query));
        Requires.NotNull(filter, nameof(filter));

        if (filter.RoleId is not null)
        {
            query = query.Where(x => x.RoleId == filter.RoleId);
        }

        if (filter.Name is not null)
        {
            query = query.Where(x => x.Name == filter.Name);
        }

        return query;
    }
}