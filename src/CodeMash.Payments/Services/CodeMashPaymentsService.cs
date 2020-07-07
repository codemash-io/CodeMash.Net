using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Payments;

namespace CodeMash.Payments.Services
{
    public partial class CodeMashPaymentsService : IPaymentService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashPaymentsService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}