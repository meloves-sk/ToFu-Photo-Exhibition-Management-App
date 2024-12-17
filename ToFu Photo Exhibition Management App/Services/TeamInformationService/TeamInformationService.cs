
namespace ToFu_Photo_Exhibition_Management_App.Services.TeamInformationService
{
	public class TeamInformationService : ITeamInformationService
	{
		public List<TeamInformationResponseDto> TeamInformations { get; } = new List<TeamInformationResponseDto>();
		public bool IsSearch { get; set; } = false;
		public async Task GetTeamInformations(int categoryId, int manufacturerId, int teamId)
		{
			TeamInformations.Clear();
			IsSearch = true;
			var result = await Api.Get<ServiceResponse<IEnumerable<TeamInformationResponseDto>>>($"api/teaminformation/category/{categoryId}/manufacturer/{manufacturerId}/team/{teamId}");
			if (result != null && result.Data != null)
			{
				TeamInformations.AddRange(result.Data);
				IsSearch = false;
			}
		}

		public async Task<ServiceResponse<bool>> AddTeamInformation(TeamInformationRequestDto request)
		{
			return await Api.Post("api/teaminformation", request);
		}

		public async Task<ServiceResponse<bool>> UpdateTeamInformation(TeamInformationRequestDto request)
		{
			return await Api.Put("api/teaminformation", request);
		}

		public async Task<ServiceResponse<bool>> DeleteTeamInformation(int teamInformationId)
		{
			return await Api.Delete($"api/teaminformation/{teamInformationId}");
		}
	}
}
