namespace ToFu_Photo_Exhibition_Management_App.Services.RoundService
{
	public interface IRoundService
	{
		public List<RoundResponseDto> Rounds { get; }
		public List<RoundResponseDto> RoundsWithAll { get; }
		Task GetRounds(int categoryId);
		Task GetRoundsWithAll(int categoryId);
		Task<ServiceResponse<bool>> AddRound(RoundRequestDto request);
		Task<ServiceResponse<bool>> UpdateRound(RoundRequestDto request);
		Task<ServiceResponse<bool>> DeleteRound(int roundId);
	}
}
