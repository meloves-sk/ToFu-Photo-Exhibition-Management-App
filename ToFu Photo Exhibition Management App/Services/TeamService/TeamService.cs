namespace ToFu_Photo_Exhibition_Management_App.Services.TeamService
{
	class TeamService : ITeamService
	{
		public List<TeamResponseDto> Teams { get; } = new List<TeamResponseDto>();
		public List<TeamResponseDto> TeamsWithAll { get; } = new List<TeamResponseDto>();
		public bool IsSearch { get; set; } = false;
		public async Task GetTeams(int categoryId, int manufacturerId)
		{
			Teams.Clear();
			IsSearch = true;
			var result = await Api.Get<ServiceResponse<IEnumerable<TeamResponseDto>>>($"api/team/category/{categoryId}/manufacturer/{manufacturerId}");
			if (result != null && result.Data != null)
			{
				Teams.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task GetTeamsWithAll(int categoryId, int manufacturerId)
		{
			TeamsWithAll.Clear();
			IsSearch = true;
			var result = await Api.Get<ServiceResponse<IEnumerable<TeamResponseDto>>>($"api/team/category/{categoryId}/manufacturer/{manufacturerId}");
			if (result != null && result.Data != null)
			{
				TeamsWithAll.Add(new TeamResponseDto(0, "ALL"));
				TeamsWithAll.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task<ServiceResponse<bool>> AddTeam(TeamRequestDto request)
		{
			return await Api.Post($"api/team", request);
		}
		public async Task<ServiceResponse<bool>> UpdateTeam(TeamRequestDto request)
		{
			return await Api.Put($"api/team", request);
		}
		public async Task<ServiceResponse<bool>> DeleteTeam(int teamId)
		{
			return await Api.Delete($"api/team/{teamId}");
		}
	}
}
