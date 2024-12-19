namespace ToFu_Photo_Exhibition_Management_App.Services.TeamInformationService
{
	public class TeamInformationService : ITeamInformationService
	{
		private readonly IApiService _apiService;
		public List<TeamInformationResponseDto> TeamInformations { get; } = new List<TeamInformationResponseDto>();
		public bool IsSearch { get; set; } = false;
		public TeamInformationService(IApiService apiService)
		{
			_apiService = apiService;
		}
		public async Task GetTeamInformations(int categoryId, int manufacturerId, int teamId)
		{
			TeamInformations.Clear();
			IsSearch = true;
			var result = await _apiService.Get<ServiceResponse<IEnumerable<TeamInformationResponseDto>>>($"api/teaminformation/category/{categoryId}/manufacturer/{manufacturerId}/team/{teamId}");
			if (result != null && result.Data != null)
			{
				TeamInformations.AddRange(result.Data);
				IsSearch = false;
			}
		}

		public async Task<ServiceResponse<bool>> AddTeamInformation(TeamInformationRequestDto request)
		{
			return await _apiService.Post("api/teaminformation", request);
		}

		public async Task<ServiceResponse<bool>> UpdateTeamInformation(TeamInformationRequestDto request)
		{
			return await _apiService.Put("api/teaminformation", request);
		}

		public async Task<ServiceResponse<bool>> DeleteTeamInformation(int teamInformationId)
		{
			return await _apiService.Delete($"api/teaminformation/{teamInformationId}");
		}
	}
}
