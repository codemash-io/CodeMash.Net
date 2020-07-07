using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Logs;
using CodeMash.Interfaces.Membership;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Logs.Services
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