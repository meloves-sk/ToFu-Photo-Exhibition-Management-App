namespace ToFu_Photo_Exhibition_Management_App.Services.PhotoService
{
	public interface IPhotoService
	{
		List<PhotoResponseDto> Photos { get; }
		bool IsSearch { get; set; }
		Task GetPhotos(int categoryId, int roundId, int manufacturerId, int teamId, int carId);
		Task<ServiceResponse<bool>> AddPhoto(PhotoRequestDto request);
		Task<ServiceResponse<bool>> UpdatePhoto(PhotoRequestDto request);
		Task<ServiceResponse<bool>> DeletePhoto(int photoId);

	}
}
