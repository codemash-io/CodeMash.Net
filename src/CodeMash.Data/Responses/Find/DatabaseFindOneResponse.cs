using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Repository
{
    public class DatabaseFindOneResponse<T>
    {
        public T Result { get; set; }
        
        public Schema Schema { get; set; }
    }
}