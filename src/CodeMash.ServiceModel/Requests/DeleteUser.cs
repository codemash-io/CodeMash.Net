using ServiceStack;

namespace CodeMash.ServiceModel
{
    public class DeleteUser : IReturn<DeleteUserResponse>
    {
        public virtual int Id { get; set; }
    }
}