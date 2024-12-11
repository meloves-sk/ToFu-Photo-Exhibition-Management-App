namespace ToFu_Photo_Exhibition_Management_App.Services.PhotoService
{
	public interface IPhotoService
	{
		public List<PhotoResponseDto> Photos { get; }
		Task GetPhotos(int categoryId, int roundId, int manufacturerId, int teamId, int carId);
		Task<ServiceResponse<bool>> AddPhoto(PhotoRequestDto photoRequestDto);
		Task<ServiceResponse<bool>> UpdatePhoto(PhotoRequestDto photoRequestDto);
		Task<ServiceResponse<bool>> DeletePhoto(int photoId);

	}
}
