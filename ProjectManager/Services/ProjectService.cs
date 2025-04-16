using ProjectManager.Data;
using ProjectManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Services;
// / <summary>
public class ProjectService
{
    private readonly ApplicationDbContext _context;
    // Fetch the database context from the constructor
    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }
    /// Fetch all projects from the database
    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }
    // Fetch a project by ID
    public async Task CreateAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }
    // Update a project
    public async Task UpdateAsync(Project updatedProject)
    {
        // Fetch the existing project from the database
        var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == updatedProject.Id);
        // Check if the project exists
        if (existingProject is null) return;
        // Update the existing project with the new values
        existingProject.ProjectName = updatedProject.ProjectName;
        existingProject.ClientName = updatedProject.ClientName;
        existingProject.Description = updatedProject.Description;
        existingProject.StartDate = updatedProject.StartDate;
        existingProject.EndDate = updatedProject.EndDate;
        existingProject.Budget = updatedProject.Budget;
        existingProject.Status = updatedProject.Status;
        // Save the changes to the database
        await _context.SaveChangesAsync();
    }
    // Delete a project by ID
    public async Task DeleteAsync(int id)
    {
        // Fetch the project from the database
        var project = await _context.Projects.FindAsync(id);
        // Check if the project exists
        if (project != null)
        {
            // Remove the project from the database
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }

}
