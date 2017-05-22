using ServiceStack;

namespace CodeMash
{
    public class GetUser : IReturn<GetUserResponse>
    {
        public string Id { get; set; }
    }

    public class GetUserByName : IReturn<GetUserResponse>
    {
        public string Name { get; set; }
    }
}