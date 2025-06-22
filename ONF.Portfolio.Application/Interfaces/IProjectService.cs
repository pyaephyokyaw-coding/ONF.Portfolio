using ONF.Portfolio.Domain.Entities;

namespace ONF.Portfolio.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
    Task<ProjectModel?> GetProjectByIdAsync(int projectId);
    Task<ProjectModel?> GetProjectByUrlAsync(string projectUrl);
    Task<ProjectModel> AddProjectAsync(ProjectModel project);
    Task<ProjectModel> UpdateProjectAsync(ProjectModel project);
    Task DeleteProjectAsync(int projectId);
}
