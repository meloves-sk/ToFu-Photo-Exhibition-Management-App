
namespace ToFu_Photo_Exhibition_Management_App.Services.PhotoService
{
	class PhotoService : IPhotoService
	{
		private readonly IApiService _apiService;
		public List<PhotoResponseDto> Photos { get; } = new List<PhotoResponseDto>();
		public bool IsSearch { get; set; } = false;
		public PhotoService(IApiService apiService)
		{
			_apiService = apiService;
		}

		public async Task GetPhotos(int categoryId, int roundId, int manufacturerId, int teamId, int carId)
		{
			Photos.Clear();
			IsSearch = true;
			var result = await _apiService.Get<ServiceResponse<IEnumerable<PhotoResponseDto>>>($"api/photo/category/{categoryId}/round/{roundId}/manufacturer/{manufacturerId}/team/{teamId}/car/{carId}");
			if (result != null && result.Data != null)
			{
				Photos.AddRange(result.Data.Select(a =>
				new PhotoResponseDto(
					a.Id,
					$"https://www.meloves.net/tofu-photo-exhibition/{a.FilePath}",
					a.Description,
					a.RoundId,
					a.CarId,
					a.Round,
					a.Category,
					a.Car,
					a.CarNo,
					a.Team,
					a.Manufacturer)));
				IsSearch = false;
			}
		}
		public async Task<ServiceResponse<bool>> AddPhoto(PhotoRequestDto request)
		{
			return await _apiService.Post("api/photo", request);
		}
		public async Task<ServiceResponse<bool>> UpdatePhoto(PhotoRequestDto request)
		{
			return await _apiService.Put("api/photo", request);
		}
		public async Task<ServiceResponse<bool>> DeletePhoto(int photoId)
		{
			return await _apiService.Delete($"api/photo/{photoId}");
		}
	}
}
