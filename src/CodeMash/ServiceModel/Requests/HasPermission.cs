using ServiceStack;

namespace CodeMash
{
    public class HasPermission : IReturn<HasPermissionResponse>
    {
        public string Permission { get; set; }
    }
}