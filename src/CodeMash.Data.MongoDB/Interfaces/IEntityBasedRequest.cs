using CodeMash.Interfaces;

namespace CodeMash.Data.MongoDB
{
    public interface IEntityBasedRequest<T> : IRequestBase where T : EntityBase
    {
    }
}