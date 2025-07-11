using backendNetCore.Profiles.Domain.Model.Commands;
using backendNetCore.Profiles.Domain.Model.Entities;
using backendNetCore.Profiles.Domain.Repositories;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Shared.Domain.Repositories;

namespace backendNetCore.Profiles.Application.Internal.CommandServices;

public class ObjectiveCommandService(IObjectiveRepository objectiveRepository, IUnitOfWork unitOfWork) : IObjectiveCommandService
{
    public async Task<Objective?> Handle(CreateObjectiveCommand command)
    {
        var objective = new Objective(command.Name, command.Score);
        await objectiveRepository.AddAsync(objective);
        await unitOfWork.CompleteAsync();
        return objective;
    }
}