namespace CodeMash.Data.MongoDB
{
    public interface IEntityWithTenant
    {
        string TenantId { get; set; }
    }
}