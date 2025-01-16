namespace ToFu_Photo_Exhibition_Management_App.Services.RoundService
{
	class RoundService : IRoundService
	{
		private readonly IApiService _apiService;
		public List<RoundResponseDto> Rounds { get; } = new List<RoundResponseDto>();
		public List<RoundResponseDto> RoundsWithAll { get; } = new List<RoundResponseDto>();
		public bool IsSearch { get; set; } = false;
		public RoundService(IApiService apiService)
		{
			_apiService = apiService;
		}
		public async Task GetRounds(int categoryId)
		{
			Rounds.Clear();
			IsSearch = true;
			var result = await _apiService.Get<ServiceResponse<IEnumerable<RoundResponseDto>>>($"api/round/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				Rounds.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task GetRoundsWithAll(int categoryId)
		{
			RoundsWithAll.Clear();
			IsSearch = true;
			var result = await _apiService.Get<ServiceResponse<IEnumerable<RoundResponseDto>>>($"api/round/category/{categoryId}");
			if (result != null && result.Data != null)
			{
				RoundsWithAll.Add(new RoundResponseDto(0, "ALL"));
				RoundsWithAll.AddRange(result.Data);
				IsSearch = false;
			}
		}
		public async Task<ServiceResponse<bool>> AddRound(RoundRequestDto request)
		{
			return await _apiService.Post("api/round", request);
		}
		public async Task<ServiceResponse<bool>> UpdateRound(RoundRequestDto request)
		{
			return await _apiService.Put("api/round", request);
		}
		public async Task<ServiceResponse<bool>> DeleteRound(int roundId)
		{
			return await _apiService.Delete($"api/round/{roundId}");
		}
	}
}
