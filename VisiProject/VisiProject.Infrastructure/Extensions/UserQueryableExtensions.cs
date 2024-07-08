﻿using Validation;
using VisiProject.Contracts.Filters;
using VisiProject.Infrastructure.Entities;

namespace VisiProject.Infrastructure.Extensions;

public static class UserQueryableExtensions
{
    public static IQueryable<UserEntity> ApplyFiltering(
        this IQueryable<UserEntity> query,
        IUserFilter filter)
    {
        Requires.NotNull(query, nameof(query));
        Requires.NotNull(filter, nameof(filter));

        if (filter.UserId is not null)
        {
            query = query.Where(x => x.UserId == filter.UserId);
        }

        if (filter.Email is not null)
        {
            query = query.Where(x => x.Email == filter.Email);
        }

        return query;
    }
}