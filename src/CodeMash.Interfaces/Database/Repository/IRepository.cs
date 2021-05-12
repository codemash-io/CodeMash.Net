using CodeMash.Interfaces.Client;
using CodeMash.Models;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> : IClientService where T : IEntity 
    {
        string Cluster { get; set; }
    }
}