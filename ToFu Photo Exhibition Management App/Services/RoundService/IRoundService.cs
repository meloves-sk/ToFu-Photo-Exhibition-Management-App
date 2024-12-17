namespace ToFu_Photo_Exhibition_Management_App.Services.RoundService
{
	public interface IRoundService
	{
		List<RoundResponseDto> Rounds { get; }
		List<RoundResponseDto> RoundsWithAll { get; }
		bool IsSearch { get; set; }
		Task GetRounds(int categoryId);
		Task GetRoundsWithAll(int categoryId);
		Task<ServiceResponse<bool>> AddRound(RoundRequestDto request);
		Task<ServiceResponse<bool>> UpdateRound(RoundRequestDto request);
		Task<ServiceResponse<bool>> DeleteRound(int roundId);
	}
}
