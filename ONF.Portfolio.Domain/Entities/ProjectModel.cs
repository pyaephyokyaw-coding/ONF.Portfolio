namespace ONF.Portfolio.Domain.Entities;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
}
