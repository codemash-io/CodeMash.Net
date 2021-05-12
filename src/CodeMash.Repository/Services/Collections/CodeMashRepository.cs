using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        public ICodeMashClient Client { get; }
        
        public string Cluster { get; set; }
        
        public CodeMashRepository(ICodeMashClient client)
        {
            Client = client;
        }
        
        public CodeMashRepository(ICodeMashClient client, string cluster)
        {
            Client = client;
            Cluster = cluster;
        }
    }
}