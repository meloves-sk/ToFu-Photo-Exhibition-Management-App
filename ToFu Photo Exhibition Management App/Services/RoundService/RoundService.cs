namespace ToFu_Photo_Exhibition_Management_App.Services.RoundService
{
	class RoundService : IRoundService
	{
		public List<RoundResponseDto> Rounds { get; } = new List<RoundResponseDto>();
		public async Task GetFilterRounds(int categoryId)
		{
			Rounds.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<RoundResponseDto>>>($"api/round/category/{categoryId}");
			if(result != null && result.Data != null)
			{
				Rounds.AddRange(result.Data);
			}
		}
	}
}
