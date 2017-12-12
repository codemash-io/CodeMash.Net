namespace CodeMash.Interfaces.IAM
{
    public class IdentityProvider : IIdentityProvider
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
    }
}