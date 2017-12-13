using ServiceStack;

namespace CodeMash.ServiceModel
{
    public class HasPermission : IReturn<HasPermissionResponse>
    {
        public string Permission { get; set; }
    }
}