using ServiceStack;

namespace CodeMash
{
    public class GetUser : IReturn<GetUserResponse>
    {
        public string Id { get; set; }
    }
}