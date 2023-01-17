﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AVIV.Core.Common.Models;

namespace AVIV.Core.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration, CancellationToken cancellationToken = default)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync(cancellationToken);
        
        public static Task<TDestination> ProjectToFirstOrDefaultAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration, CancellationToken cancellationToken = default)
            => queryable.ProjectTo<TDestination>(configuration).FirstOrDefaultAsync(cancellationToken);
        
    }
}
