using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models;
/// <summary>
public enum ProjectStatus
{
    NotStarted,
    Started,
    Completed
}
// For the project model, we will use the following properties:
public class Project
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Project name is required.")]
    public string ProjectName { get; set; }

    [Required(ErrorMessage = "Client name is required.")]
    public string ClientName { get; set; }

    public string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Budget must be non-negative.")]
    public decimal Budget { get; set; }

    public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
}
