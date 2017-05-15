using ServiceStack;

namespace CodeMash
{
    public class DeleteUser : IReturn<DeleteUserResponse>
    {
        public virtual int Id { get; set; }
    }
}