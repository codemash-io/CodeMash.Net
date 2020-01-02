using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Database.Terms
{
    public partial interface ITermService
    {
        FindTermsResponse Find(FindTermsRequest request);
        
        FindTermsResponse FindAsync(FindTermsRequest request);
    }
}