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

    public class GetUserByEmail : IReturn<GetUserResponse>
    {
        public string Email { get; set; }
    }
}