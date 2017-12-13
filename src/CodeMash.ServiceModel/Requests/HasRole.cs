using ServiceStack;

namespace CodeMash.ServiceModel
{
    public class HasRole : IReturn<HasRoleResponse>
    {
        public string Role { get; set; }
    }
}