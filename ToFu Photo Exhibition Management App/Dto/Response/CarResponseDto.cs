using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFu_Photo_Exhibition_Management_App.Dto.Response
{
	public class CarResponseDto
	{
		public CarResponseDto(int id, string name, int carNo, int teamInformationId, string team, string manufacturer, string category)
		{
			Id = id;
			Name = name;
			CarNo = carNo;
			TeamInformationId = teamInformationId;
			Team = team;
			Manufacturer = manufacturer;
			Category = category;
		}
		public int Id { get; }
		public string Name { get; }
		public int CarNo { get; }
		public int TeamInformationId { get; }
		public string Team { get; }
		public string Manufacturer { get; }
		public string Category { get; }
	}
}
