using Microsoft.AspNetCore.Mvc;
using ONF.Portfolio.Application.Interfaces;
using ONF.Portfolio.Domain.Entities;

namespace ONF.Portfolio.Web.Controllers;

public class ProjectController : Controller
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IProjectService _projectService;

    public ProjectController(ILogger<ProjectController> logger, IProjectService projectService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
    }

    public IActionResult Index()
    {
        var projects = _projectService.GetAllProjectsAsync().Result;
        if (projects == null || !projects.Any())
        {
            _logger.LogInformation("No projects found.");
            return View(new List<ProjectModel>());
        }

        return View(projects);
    }

    public async Task<IActionResult> Details(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectModel project)
    {
        if (ModelState.IsValid)
        {
            var createdProject = await _projectService.AddProjectAsync(project);
            return RedirectToAction(nameof(Details), new { id = createdProject.Id });
        }
        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProjectModel project)
    {
        if (ModelState.IsValid)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(project);
            return RedirectToAction(nameof(Details), new { id = updatedProject.Id });
        }
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        await _projectService.DeleteProjectAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
