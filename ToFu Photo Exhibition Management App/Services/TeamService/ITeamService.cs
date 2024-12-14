namespace ToFu_Photo_Exhibition_Management_App.Services.TeamService
{
	public interface ITeamService
	{
		public List<TeamResponseDto> Teams { get; }
		public List<TeamResponseDto> TeamsWithAll { get; }
		Task GetTeams(int categoryId, int manufacturerId);
		Task GetTeamsWithAll(int categoryId, int manufacturerId);
		Task<ServiceResponse<bool>> AddTeam(TeamRequestDto request);
		Task<ServiceResponse<bool>> UpdateTeam(TeamRequestDto request);
		Task<ServiceResponse<bool>> DeleteTeam(int teamId);
	}
}
