namespace ToFu_Photo_Exhibition_Management_App.Services.TeamInformationService
{
	public interface ITeamInformationService
	{
		List<TeamInformationResponseDto> TeamInformations { get; }
		bool IsSearch { get; set; }
		Task GetTeamInformations(int categoryId, int manufacturerId, int teamId);
		Task<ServiceResponse<bool>> AddTeamInformation(TeamInformationRequestDto request);
		Task<ServiceResponse<bool>> UpdateTeamInformation(TeamInformationRequestDto request);
		Task<ServiceResponse<bool>> DeleteTeamInformation(int teamInformationId);
	}
}
