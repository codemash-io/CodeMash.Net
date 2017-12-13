namespace CodeMash.Data.MongoDB
{
    public interface IEntityWithResponsibleUser
    {
        string ResponsibleUserId { get; set; }
    }
}