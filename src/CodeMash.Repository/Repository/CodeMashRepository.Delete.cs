using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Delete Async */
        public Task<DeleteResult> DeleteOneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }
        
        
        
        /* Delete */
        public DeleteResult DeleteOne(string id) 
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), "id cannot be empty");
            }
            
            var request = new DeleteOne
            {
                ProjectId = Settings.ProjectId,
                Filter = new ExpressionFilterDefinition<T>(x => x.Id == id).ToJson(),
                CollectionName = GetCollectionName(),
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Client.Delete(request);

            return response.Result;
        }

        public DeleteResult DeleteOne(ObjectId id) 
        {
            return DeleteOne(id.ToString());
        }

        public DeleteResult DeleteMany(FilterDefinition<T> filter) 
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
            }

            var request = new DeleteMany
            {
                ProjectId = Settings.ProjectId,
                Filter = filter.ToJson(),
                CollectionName = GetCollectionName(),
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Client.Delete(request);

            return response.Result;
        }

        public DeleteResult DeleteMany(Expression<Func<T, bool>> filter) 
        {
            return DeleteMany(new ExpressionFilterDefinition<T>(filter));
        }


        
        /* Find One and Delete Async */
        public Task<T> FindOneAndDeleteAsync(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter,
            FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter,
            FindOneAndDeleteOptions<T> findOneAndDeleteOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        
        
        /* Find One and Delete */
        public T FindOneAndDelete(string id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(ObjectId id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null)
            
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(FilterDefinition<T> filter,
            FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null) 
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(Expression<Func<T, bool>> filter,
            FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions) 
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(Expression<Func<T, bool>> filter) 
        {
            throw new NotImplementedException();
        }
    }
}