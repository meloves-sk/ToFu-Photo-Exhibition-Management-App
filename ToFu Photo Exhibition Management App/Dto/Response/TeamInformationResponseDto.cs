using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Dto.Response
{
	public class TeamInformationResponseDto
	{
		public TeamInformationResponseDto(int id, int teamId, int manufacturerId, int categoryId, string team, string manufacturer, string category)
		{
			Id = id;
			TeamId = teamId;
			ManufacturerId = manufacturerId;
			CategoryId = categoryId;
			Team = team;
			Manufacturer = manufacturer;
			Category = category;
		}
		public int Id { get; }
		public int TeamId { get; }
		public int ManufacturerId { get; }
		public int CategoryId { get; }
		public string Team { get; }
		public string Manufacturer { get; }
		public string Category { get; }
	}
}
