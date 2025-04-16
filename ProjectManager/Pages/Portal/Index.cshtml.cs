using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManager.Models;
using ProjectManager.Services;

namespace ProjectManager.Pages.Portal
{
    [Authorize]
    public class IndexModel : PageModel
    {
        /// Inject the ProjectService to access project data
        private readonly ProjectService _projectService;
        /// Constructor to initialize the ProjectService
        public IndexModel(ProjectService projectService)
        {
            // Initialize the ProjectService
            _projectService = projectService;
        }
        // Bind the NewProject property to the form in the Razor page
        [BindProperty]
        public Project NewProject { get; set; } = new();

        public List<Project> Projects { get; set; } = new();

        public string? CurrentFilter { get; set; }

        public int AllCount { get; set; }
        public int StartedCount { get; set; }
        public int CompletedCount { get; set; }
        /// GET: /Portal/Index
        public async Task OnGetAsync(string? status)
        {
            CurrentFilter = status;
            // Fetch all projects from the database
            var allProjects = await _projectService.GetAllAsync();
            // Count the number of projects in each status
            AllCount = allProjects.Count;
            StartedCount = allProjects.Count(p => p.Status == ProjectStatus.Started);
            CompletedCount = allProjects.Count(p => p.Status == ProjectStatus.Completed);
            // Filter projects based on the status parameter
            Projects = status switch
            {
                "started" => allProjects.Where(p => p.Status == ProjectStatus.Started).ToList(),
                "completed" => allProjects.Where(p => p.Status == ProjectStatus.Completed).ToList(),
                _ => allProjects
            };
        }
        // Create or update project
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Log the validation errors
                foreach (var entry in ModelState)
                {
                    // Check if the entry has errors
                    var key = entry.Key;
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Key: {key} – Error: {error.ErrorMessage}");
                    }
                }

                return Page();
            }
            // Check if the project ID is null or 0, indicating a new project
            if (!NewProject.Id.HasValue || NewProject.Id == 0)
            {
                await _projectService.CreateAsync(NewProject);
            }
            else
            {
                await _projectService.UpdateAsync(NewProject);
            }

            return RedirectToPage();
        }
        //Delete project
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _projectService.DeleteAsync(id);
            return RedirectToPage();
        }

    }
}
