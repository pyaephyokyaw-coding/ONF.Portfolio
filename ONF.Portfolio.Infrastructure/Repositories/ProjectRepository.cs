using Dapper;
using Microsoft.Data.SqlClient;
using ONF.Portfolio.Domain.Entities;
using ONF.Portfolio.Infrastructure.Data;

namespace ONF.Portfolio.Infrastructure.Repositories;

public class ProjectRepository 
{
    private readonly DapperContext _context;
    private readonly SqlConnection _connection;
    public ProjectRepository(DapperContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _connection = _context.CreateConnection() as SqlConnection
            ?? throw new InvalidOperationException("Failed to create SQL connection.");
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        const string sql = "SELECT * FROM Projects (NOLOCK)";
        return await _connection.QueryAsync<ProjectModel>(sql);
    }

    public async Task<ProjectModel?> GetProjectByIdAsync(int projectId)
    {
        const string sql = "SELECT * FROM Projects (NOLOCK) WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<ProjectModel>(sql, new { Id = projectId });
    }

    public async Task<ProjectModel?> GetProjectByUrlAsync(string projectUrl)
    {
        const string sql = "SELECT * FROM Projects (NOLOCK) WHERE Url = @Url";
        return await _connection.QueryFirstOrDefaultAsync<ProjectModel>(sql, new { Url = projectUrl });
    }

    public async Task<ProjectModel> AddProjectAsync(ProjectModel project)
    {
        const string sql = @"
INSERT INTO Projects (Title, Description, Url, CreateDate)
VALUES (@Title, @Description, @Url, @CreateDate);

SELECT * FROM Projects (NOLOCK) WHERE Id = SCOPE_IDENTITY()
";
        var newProject = await _connection.QuerySingleAsync<ProjectModel>(sql, project);
        return newProject;
    }

    public async Task<ProjectModel> UpdateProjectAsync(ProjectModel project)
    {
        const string sql = @"
UPDATE Projects
SET Title = @Title, 
    Description = @Description, 
    Url = @Url, 
    CreateDate = @CreateDate
WHERE Id = @Id;
";
        await _connection.ExecuteAsync(sql, project);
        return project;
    }

    public async Task<bool> DeleteProjectAsync(int projectId)
    {
        const string sql = @"
DELETE FROM Projects WHERE Id = @Id
";
        var rowsAffected = await _connection.ExecuteAsync(sql, new { Id = projectId });
        return rowsAffected > 0;
    }
}
