namespace CodeMash.Interfaces
{
    public interface IRequestWithFilterByName : IRequestBase
    {
        string Name { get; set; }
    }
}