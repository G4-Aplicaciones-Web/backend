using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Domain.Model.Entities;

namespace backendNetCore.Profiles.Domain.Services;

public interface IActivityLevelCommandService
{
    Task<ActivityLevel?> Handle(CreateActivityLevelCommand command);
}