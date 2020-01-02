using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Membership;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService : IMembershipService
    {
        public ICodeMashClient Client { get; }

        public CodeMashMembershipService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}