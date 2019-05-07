namespace CodeMash.ServiceModel
{
    public interface IRequestWithFilterByName : IRequestBase
    {
        string Name { get; set; }
    }
}