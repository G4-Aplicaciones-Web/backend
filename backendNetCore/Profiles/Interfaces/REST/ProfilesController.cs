using System.Net.Mime;
using backendNetCore.Profiles.Domain.Model.Queries;
using backendNetCore.Profiles.Domain.Services;
using backendNetCore.Profiles.Interfaces.REST.Resources;
using backendNetCore.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace backendNetCore.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile Endpoints.")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService)
: ControllerBase
{
    [HttpGet("{profileId:int}")]
    [SwaggerOperation("Get Profile by Id", "Get a profile by its unique identifier.", OperationId = "GetProfileById")]
    [SwaggerResponse(200, "The profile was found and returned.", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found.")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Profile", "Create a new profile.", OperationId = "CreateProfile")]
    [SwaggerResponse(201, "The profile was created.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile was not created.")]
    public async Task<IActionResult> CreateProfile(CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await profileCommandService.Handle(createProfileCommand);
        if (profile is null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return CreatedAtAction(nameof(GetProfileById), new { profileId = profile.Id }, profileResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Profiles", "Get all profiles.", OperationId = "GetAllProfiles")]
    [SwaggerResponse(200, "The profiles were found and returned.", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(404, "The profiles were not found.")]
    public async Task<IActionResult> GetAllProfiles()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(getAllProfilesQuery);
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }
    
    [HttpPut("{profileId:int}")]
    [SwaggerOperation("Update Profile", "Update profile details (height, weight, activity level, objective).", OperationId = "UpdateProfile")]
    [SwaggerResponse(200, "The profile was updated successfully.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile could not be updated.")]
    public async Task<IActionResult> UpdateProfile(int profileId, [FromBody] UpdateProfileResource resource)
    {
        var command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(profileId, resource);
        var profile = await profileCommandService.Handle(command);
        if (profile is null) return NotFound();

        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    
    [HttpPost("{profileId:int}/allergies")]
    [SwaggerOperation("Add Allergy to Profile", "Adds a new allergy to the user's profile.", OperationId = "AddAllergyToProfile")]
    [SwaggerResponse(200, "The allergy was added successfully.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The request was invalid.")]
    public async Task<IActionResult> AddAllergyToProfile(int profileId, [FromBody] AddAllergyToProfileResource resource)
    {
        var command = AddAllergyToProfileCommandFromResourceAssembler.ToCommandFromResource(profileId, resource);
        var profile = await profileCommandService.Handle(command);
        if (profile is null) return NotFound();
    
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    
    [HttpDelete("{profileId:int}/allergies")]
    [SwaggerOperation("Remove Allergy from Profile", "Removes an allergy from the user's profile.", OperationId = "RemoveAllergyFromProfile")]
    [SwaggerResponse(200, "The allergy was removed successfully.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The request was invalid.")]
    public async Task<IActionResult> RemoveAllergyFromProfile(int profileId, [FromBody] RemoveAllergyFromProfileResource resource)
    {
        var command = RemoveAllergyFromProfileCommandFromResourceAssembler.ToCommandFromResource(profileId, resource);
        var profile = await profileCommandService.Handle(command);
        if (profile is null) return NotFound();
    
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

}