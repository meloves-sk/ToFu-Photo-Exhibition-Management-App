namespace ToFu_Photo_Exhibition_Management_App.Services.TeamService
{
	class TeamService : ITeamService
	{
		public List<TeamResponseDto> Teams { get; } = new List<TeamResponseDto>();
		public async Task GetFilterTeams(int categoryId, int manufacturerId)
		{
			Teams.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<TeamResponseDto>>>($"api/team/category/{categoryId}/manufacturer/{manufacturerId}");
			if (result != null && result.Data != null)
			{
				Teams.AddRange(result.Data);
			}
		}
	}
}
