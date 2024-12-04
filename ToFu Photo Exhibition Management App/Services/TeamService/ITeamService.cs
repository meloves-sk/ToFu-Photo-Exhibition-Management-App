namespace ToFu_Photo_Exhibition_Management_App.Services.TeamService
{
	public interface ITeamService
	{
		public List<TeamResponseDto> Teams { get; }
		Task GetFilterTeams(int categoryId, int manufacturerId);
	}
}
