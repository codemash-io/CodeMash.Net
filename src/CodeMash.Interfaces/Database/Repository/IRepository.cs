using CodeMash.Interfaces.Client;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> : IClientService where T : IEntity 
    {
        IRepository<T> WithCollection(string collectionName);
    }
}