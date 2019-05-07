using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CodeMash.Interfaces;
using CodeMash.ServiceModel;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Checks if deletion of a record is successful
        /// </summary>
        /// <param name="result">
        /// returns boolean as result
        /// </param>
        /// <returns></returns>
        public static bool IsDeleted(this DeleteResult result)
        {
            if(result.IsAcknowledged)
            {
                return result.DeletedCount == 1;
            }
            //throw new BusinessException("Cannot perform delete action");
            throw new Exception("Cannot perform delete action");
        }
        
        private static SortDefinition<T> ToSortDefinition<T>(this IRequestWithSorting sorting)
        {
            SortDefinition<T> sortDefinition = null;

            if (sorting != null)
            {
                sortDefinition = sorting.SortDirection == SortDirection.Ascending
                    ? Builders<T>.Sort.Ascending(sorting.SortPropertyName)
                    : Builders<T>.Sort.Descending(sorting.SortPropertyName);
            }

            return sortDefinition;
        }
        
        public static FilterDefinition<T> ToFilterDefinition<T>(this List<FilterDefinition<T>> filters)
        {
            var filterDefinition = Builders<T>.Filter.Empty;
            
            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    filterDefinition = filterDefinition & filter;
                }
            }

            return filterDefinition;
        }
        

        private static int? Skip(this IRequestWithPaging paging)
        {
            return paging?.PageNumber;
        }

        private static int? Take(this IRequestWithPaging paging)
        {
            return paging?.PageSize;
        }
        
        
        public static T InsertOne<T>(this IRepository<T> repo, T instance, IRequestContext<T> requestContext) where T : new()
        {
            if (requestContext == null)
            {
                return repo.InsertOne(instance);
            }

            if (requestContext.IdentityProvider.IsAuthenticated)
            {
                if (typeof(IEntityWithTenant).IsAssignableFrom(typeof(T)))
                {
                    ((IEntityWithTenant) instance).TenantId = requestContext.IdentityProvider.TenantId;
                }
                
                if (typeof(IEntityWithResponsibleUser).IsAssignableFrom(typeof(T)))
                {
                    ((IEntityWithResponsibleUser) instance).ResponsibleUserId = requestContext.IdentityProvider.UserId;
                }
            }
            
            return repo.InsertOne(instance);
        }

        public static UpdateResult UpdateOne<T>(this IRepository<T> repo, string id, UpdateDefinition<T> update, IRequestContext<T> requestContext, UpdateOptions updateOptions)
            where T : new()
        {
            if (requestContext == null)
            {
                return repo.UpdateOne(id, update, updateOptions);
            }
            
            
            var result = repo.UpdateOne(id, update, updateOptions);
            
            if (requestContext.IdentityProvider.IsAuthenticated)
            {
                if (typeof(IEntityWithResponsibleUser).IsAssignableFrom(typeof(T)))
                {
                    var modifiedUser = Builders<T>.Update.Set("ResponsibleUserId", requestContext.IdentityProvider.UserId);
                    repo.UpdateOne(id, modifiedUser, updateOptions);
                }
            }

            return result;
        }
        
        public static UpdateResult UpdateOne<T>(this IRepository<T> repo, ObjectId id, UpdateDefinition<T> update, IRequestContext<T> requestContext, UpdateOptions updateOptions)
            where T : new()
        {
            if (requestContext == null)
            {
                return repo.UpdateOne(id, update, updateOptions);
            }
            
            var result = repo.UpdateOne(id, update, updateOptions);
            
            if (requestContext.IdentityProvider.IsAuthenticated)
            {
                if (typeof(IEntityWithResponsibleUser).IsAssignableFrom(typeof(T)))
                {
                    var modifiedUser = Builders<T>.Update.Set("ResponsibleUserId", requestContext.IdentityProvider.UserId);
                    repo.UpdateOne(id, modifiedUser, updateOptions);
                }
            }

            return result;
        }
        
        public static UpdateResult UpdateOne<T>(this IRepository<T> repo, Expression<Func<T, bool>> expression, UpdateDefinition<T> update, IRequestContext<T> requestContext, UpdateOptions updateOptions)
            where T : new()
        {
            if (requestContext == null)
            {
                return repo.UpdateOne(expression, update, updateOptions);
            }
            
            var result = repo.UpdateOne(expression, update, updateOptions);
            
            if (requestContext.IdentityProvider.IsAuthenticated)
            {
                if (typeof(IEntityWithResponsibleUser).IsAssignableFrom(typeof(T)))
                {
                    var modifiedUser = Builders<T>.Update.Set("ResponsibleUserId", requestContext.IdentityProvider.UserId);
                    repo.UpdateOne(expression, modifiedUser, updateOptions);
                }
            }

            return result;
        }
        
        
        public static UpdateResult UpdateOne<T>(this IRepository<T> repo, FilterDefinition<T> filter, UpdateDefinition<T> update, IRequestContext<T> requestContext, UpdateOptions updateOptions)
            where T : new()
        {
            if (requestContext == null)
            {
                return repo.UpdateOne(filter, update, updateOptions);
            }
            
            var result = repo.UpdateOne(filter, update, updateOptions);
            
            if (requestContext.IdentityProvider.IsAuthenticated)
            {
                if (typeof(IEntityWithResponsibleUser).IsAssignableFrom(typeof(T)))
                {
                    var modifiedUser = Builders<T>.Update.Set("ResponsibleUserId", requestContext.IdentityProvider.UserId);
                    repo.UpdateOne(filter, modifiedUser, updateOptions);
                }
            }

            return result;
        }


        public static DeleteResult DeleteOne<T>(this IRepository<T> repo, string id, IRequestContext<T> requestContext) where T : new()
        {
            if (typeof(IEntityWithIsDeleted).IsAssignableFrom(typeof(T)))
            {
                var updateResult = repo.UpdateOne(id, Builders<T>.Update.Set("IsDeleted", true), null);  
                return new DeleteResult.Acknowledged(updateResult.ModifiedCount);
            }
            
            return repo.DeleteOne(id);

            
        }

        public static T FindOne<T>(this IRepository<T> repo, Expression<Func<T, bool>> expression, IRequestContext<T> requestContext, ProjectionDefinition<T> projection = null,
            FindOptions findOptions = null) where T : new()
        {
            if (requestContext == null)
            {
                return repo.FindOne(expression, projection, findOptions);
            }
            var filter = expression & requestContext.Filters.ToFilterDefinition();
            return repo.FindOne(filter, projection, findOptions);
        }

        public static T FindOneById<T>(this IRepository<T> repo, string id, IRequestContext<T> requestContext) where T : EntityBase, new()
        {
            if (requestContext == null)
            {
                return repo.FindOneById(id);
            }

            return repo.FindOne(x => x.Id == id, requestContext);
        }

        public static T FindOneById<T>(this IRepository<T> repo, ObjectId id, IRequestContext<T> requestContext) where T : EntityBase, new()
        {
            if (requestContext == null)
            {
                return repo.FindOneById(id);
            }
            return repo.FindOne(x => x.Id == id.ToString(), requestContext);
        }
        
        
        public static List<T> Find<T>(this IRepository<T> repo, Expression<Func<T, bool>> expression, IRequestContext<T> requestContext) where T : new()
        {
            if (requestContext == null)
            {
                return repo.Find(expression);
            }
            
            var filter = expression & requestContext.Filters.ToFilterDefinition();
            
            return repo.Find(filter, requestContext.Sorting.ToSortDefinition<T>(), requestContext.Pagining.Skip(), requestContext.Pagining.Take());
            
        }

        public static List<T> Find<T>(this IRepository<T> repo, FilterDefinition<T> filter, IRequestContext<T> requestContext) where T : new()
        {
            if (requestContext == null)
            {
                return repo.Find(filter);
            }

            filter = filter & requestContext.Filters.ToFilterDefinition();
            
            // TODO : check if requestSorting.Sorting.PropertyName is not NULL> sometimes you can miss add Sorting, Paging capability on top of your 
            // request, so requestContext can miss that informatiom
            
            return repo.Find(filter, requestContext.Sorting.ToSortDefinition<T>(), requestContext.Pagining.Skip(), requestContext.Pagining.Take());
        }

        public static List<T> Find<T>(this IRepository<T> repo, FilterDefinition<T> filter, ProjectionDefinition<T,T> projection, IRequestContext<T> requestContext) where T : new()
        {
            if (requestContext == null)
            {
                return repo.Find(filter, projection);
            }
            
            filter = filter & requestContext.Filters.ToFilterDefinition();

            return repo.Find(filter, projection, requestContext.Sorting.ToSortDefinition<T>(), requestContext.Pagining.Skip(), requestContext.Pagining.Take());
        }

        public static T FindOneAndReplace<T>(this IRepository<T> repo, Expression<Func<T, bool>> expression, T entity, IRequestContext<T> requestContext) where T : new()
        {
            if (requestContext == null)
            {
                return repo.FindOneAndReplace(expression, entity);
            }
            
            var filter = expression & requestContext.Filters.ToFilterDefinition();
            
            return repo.FindOneAndReplace(filter, entity);
        }

        public static T FindOneAndUpdate<T>(this IRepository<T> repo, FilterDefinition<T> filter,
            UpdateDefinition<T> entity, IRequestContext<T> requestContext, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null) where T : new()
        {
            if (requestContext == null)
            {
                return repo.FindOneAndUpdate(filter, entity);
            }
            
            filter = filter & requestContext.Filters.ToFilterDefinition();
            
            if (typeof(IEntityWithTenant).IsAssignableFrom(typeof(T)))
            {
                entity.Set("TenantId", requestContext.IdentityProvider.TenantId);
            }
            
            return repo.FindOneAndUpdate(filter, entity);
        }

        public static T FindOneAndUpdate<T>(this IRepository<T> repo, Expression<Func<T, bool>> expression,
            UpdateDefinition<T> entity, IRequestContext<T> requestContext,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null) where T : new()
        {
            if (requestContext == null)
            {
                return repo.FindOneAndUpdate(expression, entity);
            }
            
            var filter = expression & requestContext.Filters.ToFilterDefinition();
            
            
            if (typeof(IEntityWithTenant).IsAssignableFrom(typeof(T)))
            {
                entity.Set("TenantId", requestContext.IdentityProvider.TenantId);
            }
            
            return repo.FindOneAndUpdate(filter, entity);
        }
    }
}