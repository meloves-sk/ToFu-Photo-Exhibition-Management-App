namespace ToFu_Photo_Exhibition_Management_App.Services.PhotoService
{
	class PhotoService : IPhotoService
	{
		public List<PhotoResponseDto> Photos { get; } = new List<PhotoResponseDto>();
		public async Task GetFilterPhotos(int categoryId, int roundId, int manufacturerId, int teamId, int carId)
		{
			Photos.Clear();
			var result = await Api.Get<ServiceResponse<IEnumerable<PhotoResponseDto>>>($"api/photo/category/{categoryId}/round/{roundId}/manufacturer/{manufacturerId}/team/{teamId}/car/{carId}");
			if (result != null && result.Data != null)
			{
				Photos.AddRange(result.Data);
			}
		}
	}
}
