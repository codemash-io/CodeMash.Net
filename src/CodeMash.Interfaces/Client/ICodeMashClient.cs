namespace CodeMash.Interfaces.Client
{
    public interface ICodeMashClient : ICodeMashRestAsync, ICodeMashRest
    {
        ICodeMashSettings Settings { get; }
        
        ICodeMashResponse Response { get; }
        
        ICodeMashRequest Request { get; }
    }
}