namespace ToFu_Photo_Exhibition_Management_App.Services.RoundService
{
	class RoundService : IRoundService
	{
		public List<RoundResponseDto> Rounds { get; } = new List<RoundResponseDto>();
		public List<RoundResponseDto> RoundsWithAll { get; } = new List<RoundResponseDto>();
		public async Task GetRounds(int categoryId)
		{
			Rounds.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<RoundResponseDto>>>($"api/round/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				Rounds.AddRange(result.Data);
			}
		}
		public async Task GetRoundsWithAll(int categoryId)
		{
			RoundsWithAll.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<RoundResponseDto>>>($"api/round/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				RoundsWithAll.Add(new RoundResponseDto(0, "ALL"));
				RoundsWithAll.AddRange(result.Data);
			}
		}
		public async Task<ServiceResponse<bool>> AddRound(RoundRequestDto request)
		{
			return await Api.Post("api/round", request);
		}
		public async Task<ServiceResponse<bool>> UpdateRound(RoundRequestDto request)
		{
			return await Api.Put("api/round", request);
		}
		public async Task<ServiceResponse<bool>> DeleteRound(int roundId)
		{
			return await Api.Delete($"api/round/{roundId}");
		}
	}
}
