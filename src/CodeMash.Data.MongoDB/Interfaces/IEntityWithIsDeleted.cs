namespace CodeMash.Data.MongoDB
{
    public interface IEntityWithIsDeleted
    {
        bool IsDeleted { get; set; }
    }
}