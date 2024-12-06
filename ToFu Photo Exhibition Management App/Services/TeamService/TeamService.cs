namespace ToFu_Photo_Exhibition_Management_App.Services.TeamService
{
	class TeamService : ITeamService
	{
		public List<TeamResponseDto> Teams { get; } = new List<TeamResponseDto>();
		public List<TeamResponseDto> TeamsWithAll { get; } = new List<TeamResponseDto>();
		public async Task GetTeams(int categoryId, int manufacturerId)
		{
			Teams.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<TeamResponseDto>>>($"api/team/category/{categoryId}/manufacturer/{manufacturerId}");
			if (result != null && result.Data != null)
			{
				Teams.AddRange(result.Data);
			}
		}
		public async Task GetTeamsWithAll(int categoryId, int manufacturerId)
		{
			TeamsWithAll.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<TeamResponseDto>>>($"api/team/category/{categoryId}/manufacturer/{manufacturerId}");
			if (result != null && result.Data != null)
			{
				TeamsWithAll.Add(new TeamResponseDto(0, "ALL"));
				TeamsWithAll.AddRange(result.Data);
			}
		}
	}
}
