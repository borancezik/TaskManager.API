using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<TaskManager_Presentation>("task-manager-api");

builder.Build().Run();
