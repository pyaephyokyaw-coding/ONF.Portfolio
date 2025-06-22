using ONF.Portfolio.Application.Interfaces;
using ONF.Portfolio.Domain.Entities;
using ONF.Portfolio.Infrastructure.Repositories;

namespace ONF.Portfolio.Application.Services;

public class ProjectService : IProjectService
{
    private readonly ProjectRepository _projectRepository;

    public ProjectService(ProjectRepository projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllProjectsAsync();
    }

    public async Task<ProjectModel?> GetProjectByIdAsync(int projectId)
    {
        return await _projectRepository.GetProjectByIdAsync(projectId);
    }

    public async Task<ProjectModel?> GetProjectByUrlAsync(string projectUrl)
    {
        return await _projectRepository.GetProjectByUrlAsync(projectUrl);
    }

    public async Task<ProjectModel> AddProjectAsync(ProjectModel project)
    {
        if (project == null) throw new ArgumentNullException(nameof(project));
        return await _projectRepository.AddProjectAsync(project);
    }

    public async Task<ProjectModel> UpdateProjectAsync(ProjectModel project)
    {
        if (project == null) throw new ArgumentNullException(nameof(project));
        return await _projectRepository.UpdateProjectAsync(project);
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        if (projectId <= 0) throw new ArgumentOutOfRangeException(nameof(projectId), "Project ID must be greater than zero.");
        await _projectRepository.DeleteProjectAsync(projectId);
    }
}
