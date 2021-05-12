using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Repository
{
    public class DatabaseFindOneResponse<T>
    {
        public T Result { get; set; }
        
        public Schema Schema { get; set; }
    }
}