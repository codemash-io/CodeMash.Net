namespace CodeMash.Interfaces.IAM
{
    public interface IIdentityProvider
    {
        bool IsAuthenticated { get; set; }
        string UserId { get; set; }
        string SessionId { get; set; }
        string TenantId { get; set; }
        string ApplicationId { get; set; }
    }
}