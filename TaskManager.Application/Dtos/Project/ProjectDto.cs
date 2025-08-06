﻿using TaskManager.Application.Dtos.Base;

namespace TaskManager.Application.Dtos.Project;

public class ProjectDto : BaseDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsArchived { get; set; }
}
