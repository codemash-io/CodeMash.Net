namespace CodeMash.Interfaces
{
    public interface IEntityWithTenant
    {
        string TenantId { get; set; }
    }
}