using ServiceStack;

namespace CodeMash
{
    public class HasRole : IReturn<HasRoleResponse>
    {
        public string Role { get; set; }
    }
}