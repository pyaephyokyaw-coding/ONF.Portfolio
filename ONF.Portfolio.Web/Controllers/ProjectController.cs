using Microsoft.AspNetCore.Mvc;
using ONF.Portfolio.Application.Interfaces;
using ONF.Portfolio.Domain.Entities;
using System.Threading.Tasks;

namespace ONF.Portfolio.Web.Controllers;

public class ProjectController : BaseController
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
        try
        {
            var projects = _projectService.GetAllProjectsAsync().Result;
            if (projects == null || !projects.Any())
            {
                _logger.LogInformation("No projects found.");
                return View(new List<ProjectModel>());
            }

            return View(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving projects.");
            return HandleError("An error occurred while retrieving projects.");
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving project details.");
            return HandleError("An error occurred while retrieving project details.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? id)
    {
        if (id.HasValue)
        {
            var model = await _projectService.GetProjectByIdAsync(id.Value);
            if (model == null)
                return HttpNotFound();

            return View(model); // Edit mode
        }

        return View(new ProjectModel()); // Create mode
    }

    private ActionResult HttpNotFound()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Id == 0)
            {
                // Create
                var projects = await _projectService.AddProjectAsync(model);
                TempData["Success"] = "Project created!";
                return View(projects);
            }
            else
            {
                // Update
                var existing = await _projectService.GetProjectByIdAsync(model.Id);
                if (existing != null)
                {
                    existing.Title = model.Title;
                    existing.Description = model.Description;
                    existing.Url = model.Url;
                    existing.CreatedDate = model.CreatedDate;

                    var updatedProject = await _projectService.UpdateProjectAsync(existing);

                    TempData["Success"] = "Project updated!";

                    return View(updatedProject);
                }
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        try
        {
            return RedirectToAction(nameof(Create), new { id }); // Redirect to Create view for editing
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while preparing to edit the project.");
            return HandleError("An error occurred while preparing to edit the project.");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProjectModel project)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var updatedProject = await _projectService.UpdateProjectAsync(project);
                return View(updatedProject);
                //return RedirectToAction(updatedProject, new { id = updatedProject.Id });
            }
            return View(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the project.");
            return HandleError("An error occurred while updating the project.");
        }
    }

    [HttpGet]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            await _projectService.DeleteProjectAsync(id);
            TempData["Delete"] = $"Project: [{project.Title}] deleted successfully!";
            return View("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the project.");
            return HandleError("An error occurred while deleting the project.");
        }
    }
}
