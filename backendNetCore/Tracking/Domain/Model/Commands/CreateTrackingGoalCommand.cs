using AlimentateplusPlatform.API.Tracking.Domain.Model.Entities;
using AlimentateplusPlatform.API.Tracking.Domain.Model.ValueObjects;

namespace AlimentateplusPlatform.API.Tracking.Domain.Model.Commands;

public record CreateTrackingGoalCommand(UserId UserId, TrackingMacronutrient Macronutrient);