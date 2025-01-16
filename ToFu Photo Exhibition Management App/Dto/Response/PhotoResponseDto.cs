using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Dto.Response
{
	public class PhotoResponseDto
	{
		public PhotoResponseDto(int id, string filePath, string description, int roundId, int carId, string round, string category, string car, int carNo, string team, string manufacturer)
		{
			Id = id;
			FilePath = filePath;
			Description = description;
			RoundId = roundId;
			CarId = carId;
			Round = round;
			Category = category;
			Car = car;
			CarNo = carNo;
			Team = team;
			Manufacturer = manufacturer;
		}
		public int Id { get; }
		public string FilePath { get; }
		public string Description { get; }
		public int RoundId { get; }
		public int CarId { get; }
		public string Round { get; }
		public string Category { get; }
		public string Car { get; }
		public int CarNo { get; }
		public string Team { get; }
		public string Manufacturer { get; }
	}
}
