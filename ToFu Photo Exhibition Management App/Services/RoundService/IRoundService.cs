namespace ToFu_Photo_Exhibition_Management_App.Services.RoundService
{
	public interface IRoundService
	{
		public List<RoundResponseDto> Rounds { get; }
		Task GetFilterRounds(int categoryId);
	}
}
