using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToFu_Photo_Exhibition_Management_App.Shared.Dto.Response;

namespace ToFu_Photo_Exhibition_Management_App.Services.PhotoService
{
	public interface IPhotoService
	{
		Task<ServiceResponse<IEnumerable<PhotoResponseDto>>> GetFilterPhotos(int categoryId, int roundId, int manufacturerId, int teamId, int carId);

	}
}
