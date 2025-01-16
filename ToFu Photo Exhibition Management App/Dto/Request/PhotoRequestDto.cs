using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Shared.Dto.Request
{
	public class PhotoRequestDto
	{
		public PhotoRequestDto(int id, string description, int roundId, int carId, byte[] photoData)
		{
			Id = id;
			Description = description;
			RoundId = roundId;
			CarId = carId;
			PhotoData = photoData;
		}
		public int Id { get; }
		public string Description { get; }
		public int RoundId { get; set; }
		public int CarId { get; set; }
		public byte[] PhotoData { get; }
	}
}
